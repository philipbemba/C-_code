using System.Collections.Generic;

namespace Experian.Api
{    
    public class ConsumerPii
    {
        public MemberDetail primaryApplicant { get; set; }
        public MemberDetail secondaryApplicant { get; set; }
    }

    public class Requestor
    {
        public string subscriberCode { get; set; } = "2919348";
    }

    public class RiskModels
    {
        public string[] modelIndicator { get; set; } = { "V3" };
        public string scorePercentile { get; set; } = "Y";
    }

    public class ConsumerIdentCheck
    {
        public string getUniqueConsumerIdentifier { get; set; } = "";
    }

    public class Summaries
    {
        public List<string> summaryType { get; set; }
    }

   /* public class AddOns
    {
        public string directCheck { get; set; }
        public string demographics { get; set; }
        public RiskModels riskModels { get; set; }
        public Summaries summaries { get; set; }
        public string mla { get; set; } = "";
        public string ofacmsg { get; set; } = "Y";
        public string fraudShield { get; set; } = "N";
        public string paymentHistory84 { get; set; } = "N";
        public ConsumerIdentCheck consumerIdentCheck { get; set; }
        public string joint { get; set; } = "";
    } */

    public class PermissiblePurpose
    {
        public string type { get; set; } = "97";
        public string terms { get; set; } = "010";
        public string abbreviatedAmount { get; set; } = "010";
    }

    public class CustomOptions
    {
        public string[] optionId { get; set; } = { "" };
    }

    public class ResellerInfo
    {
        public string endUserName { get; set; }
    }

    public class ExperianRequest
    {
        public ConsumerPii consumerPii { get; set; }
       // public AddOns addOns { get; set; }
        public Requestor requestor { get; set; }
        public CustomOptions customOptions { get; set; }
        public PermissiblePurpose permissiblePurpose { get; set; }
        //public ResellerInfo resellerInfo { get; set; }

        // Constructor
        public ExperianRequest(List<Applicant> applicants)
        {
            //addOns = new AddOns();
            requestor = new Requestor();
            customOptions = new CustomOptions();
            permissiblePurpose = new PermissiblePurpose();
            consumerPii = new ConsumerPii();
            //resellerInfo = new ResellerInfo();

            foreach (var applicant in applicants)
            {
                if (applicant.isPrimary == true)
                {
                    // Populate primaryApplicant.
                    consumerPii.primaryApplicant = new MemberDetail(applicant);
                   
                } else
                {
                    consumerPii.secondaryApplicant = new MemberDetail(applicant);
                }
            }

            /*if (applicants.Count > 1)
            {
                addOns.joint = "Y";
            }*/
        }
    }    
}
