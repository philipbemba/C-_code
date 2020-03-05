using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
//using Newtonsoft.Json;

namespace Experian.Api
{
    public class ExperianService : IExperianService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExperianService> _log;
        private readonly IHttpClientFactory _clientFactory;

        public ExperianService(IConfiguration configuration, ILogger<ExperianService> log, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _log = log;
            _clientFactory = clientFactory;
        }

        public ExperianResponse CheckCredit(List<Applicant> Applicants, Settings Settings)
        {
            if (Applicants == null || Applicants.Count == 0)
            {
                throw new ArgumentNullException(paramName: nameof(Applicants), message: "No Applicant information passed");
            }

            // Get the bearer-authorization token.
            string Token = getToken(Settings);

            string RequestParams = JsonSerializer.Serialize(new ExperianRequest(Applicants));

            // Create HTTP client and set the headers: Accept and Authorization.
            using var Client = _clientFactory.CreateClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // Create the request message object with the content.
            using HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, Settings.CreditCheckHost)
            {
                Content = new StringContent(RequestParams, Encoding.UTF8, "application/json")
            };
            Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var Response = Client.SendAsync(Request).Result;

            string Result = Response.Content.ReadAsStringAsync().Result;

            CreditReport CreditReport = JsonSerializer.Deserialize<CreditReport>(Result);

            // Create Experian Response.
            ExperianResponse ExpResponse = new ExperianResponse();
            ExpResponse.Path = "v1/experian/creditcheck";
            ExpResponse.TimeStamp = DateTime.Now;

            if (CreditReport.errors != null && CreditReport.errors.Count > 0)
            {
                _log.LogInformation("error");
                // For now we will assume that we get a singular error message.
                _log.LogInformation(CreditReport.errors[0].message);
                ExpResponse.Status = HttpStatusCode.BadRequest;
                ExpResponse.ErrorMessage = CreditReport.errors[0].message;                
            }
            else
            {
                _log.LogInformation(" no error");

                ExpResponse.Status = HttpStatusCode.OK;

                foreach (Applicant applicant in Applicants)
                {
                    // No errors so maybe we got a credit report?
                    foreach (CreditProfile profile in CreditReport.creditProfile)
                    {
                        if (profile.consumerIdentity != null && profile.consumerIdentity.name != null && profile.consumerIdentity.name.Count > 0)
                        {
                            foreach (NameExperianResponse name in profile.consumerIdentity.name)
                            {
                                if (name.firstName.ToLower() == applicant.firstName.ToLower() && name.surname.ToLower() == applicant.lastName.ToLower())
                                {
                                    applicant.creditProfile = profile;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            applicant.creditProfile = profile;
                            break;
                        }
                    }                    
                }
            }

            foreach (Applicant applicant in Applicants)
            {
                ExpResponse.Data.Add(applicant);
            }

            return ExpResponse;
        }

        private string getToken(Settings settings)
        {
             // The parameters to be passed into the token URL.
            var Credentials = new Credential
            {
                username = settings.Username,
                password = settings.Password,
                client_id = settings.ClientID,
                client_secret = settings.ClientSecret
            };

            string RequestParams = JsonSerializer.Serialize(Credentials);

            using HttpClient Client = _clientFactory.CreateClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Create the request message object with the content.
            using HttpRequestMessage Request = new HttpRequestMessage(HttpMethod.Post, settings.TokenHost)
            {
                Content = new StringContent(RequestParams, Encoding.UTF8, "application/json")
            };
            Request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var Response = Client.SendAsync(Request).Result;

            string Result = Response.Content.ReadAsStringAsync().Result;

            // Extracting the token from the response.
            return JObject.Parse(Result)["access_token"].ToString();
        }
    }
}
