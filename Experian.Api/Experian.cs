using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;

namespace Experian.Api
{
    public class Experian
    {
        private readonly IExperianService _eExperianService;

        public Experian(IExperianService eExperianService)
        {
            _eExperianService = eExperianService;
        }

        [FunctionName("CheckCreditPost")]
        public async Task<IActionResult> Post(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/experian/creditcheck")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            log.LogInformation("POST: Experian Credit Check");

            var Content = await new StreamReader(req.Body).ReadToEndAsync();

            List<Applicant> Applicants = JsonConvert.DeserializeObject<List<Applicant>>(Content);

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("secret.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var Settings = new Settings();
            config.Bind("Settings", Settings);

            ExperianResponse Response = _eExperianService.CheckCredit(Applicants, Settings);

            return new OkObjectResult(Response);
        }
    }    
}
