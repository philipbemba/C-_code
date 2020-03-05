using System;
using System.Collections.Generic;
using System.Text;

namespace Experian.Api
{
    public class AddressInformation
    {
        public string city { get; set; }
        public string dwellingType { get; set; }
        public string firstReportedDate { get; set; }
        public string lastReportingSubscriberCode { get; set; }
        public string lastUpdatedDate { get; set; }
        public string source { get; set; }
        public string state { get; set; }
        public string streetName { get; set; }
        public string streetPrefix { get; set; }
        public string streetSuffix { get; set; }
        public string timesReported { get; set; }
        public string zipCode { get; set; }
    }

    public class Dob
    {
        public string day { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }

   public class NameExperianResponse
    {
        public string firstName { get; set; }
        public string surname { get; set; }
    }

    public class ConsumerIdentity
    {
        public Dob dob { get; set; }
        public List<NameExperianResponse> name { get; set; }
    }

    public class DirectCheck
    {
        public string subscriberAddress { get; set; }
        public string subscriberCity { get; set; }
        public string subscriberCode { get; set; }
        public string subscriberName { get; set; }
        public string subscriberPhone { get; set; }
        public string subscriberState { get; set; }
        public string subscriberZipCode { get; set; }
    }

    public class EmploymentInformation
    {
        public string firstReportedDate { get; set; }
        public string lastUpdatedDate { get; set; }
        public string name { get; set; }
        public string source { get; set; }
    }

    public class FraudShield
    {
        public string addressCount { get; set; }
        public string addressDate { get; set; }
        public string addressErrorCode { get; set; }
        public string socialCount { get; set; }
        public string socialDate { get; set; }
        public string socialErrorCode { get; set; }
        public string ssnFirstPossibleIssuanceYear { get; set; }
        public string ssnLastPossibleIssuanceYear { get; set; }
        public string type { get; set; }
    }

    public class Inquiry
    {
        public string amount { get; set; }
        public string date { get; set; }
        public string subscriberCode { get; set; }
        public string subscriberName { get; set; }
        public string terms { get; set; }
        public string type { get; set; }
    }

    public class InformationalMessage
    {
        public string messageNumber { get; set; }
        public string messageNumberDetailed { get; set; }
        public string messageText { get; set; }
    }

    public class Mla
    {
        public string messageNumber { get; set; }
        public string messageText { get; set; }
    }

    public class Attribute
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class Summary
    {
        public string summaryType { get; set; }
        public List<Attribute> attributes { get; set; }
    }

    public class ScoreFactor
    {
        public string importance { get; set; }
        public string code { get; set; }
    }

    public class RiskModel
    {
        public string evaluation { get; set; }
        public string modelIndicator { get; set; }
        public string score { get; set; }
        public List<ScoreFactor> scoreFactors { get; set; }
    }

    public class Ssn
    {
        public string number { get; set; }
        public string ssnIndicators { get; set; }
    }

    public class EnhancedPaymentData
    {
        public string enhancedAccountCondition { get; set; }
        public string enhancedAccountType { get; set; }
        public string enhancedPaymentHistory84 { get; set; }
        public string enhancedPaymentStatus { get; set; }
        public string enhancedTerms { get; set; }
        public string originalLoanAmount { get; set; }
        public string paymentLevelDate { get; set; }
        public string enhancedTermsFrequency { get; set; }
        public string secondaryAgencyCode { get; set; }
        public string secondaryAgencyId { get; set; }
    }

    public class Tradeline
    {
        public string accountNumber { get; set; }
        public string accountType { get; set; }
        public string amount1 { get; set; }
        public string amount1Qualifier { get; set; }
        public string balanceDate { get; set; }
        public string delinquencies30Days { get; set; }
        public string delinquencies60Days { get; set; }
        public string delinquencies90to180Days { get; set; }
        public string derogCounter { get; set; }
        public string ecoa { get; set; }
        public EnhancedPaymentData enhancedPaymentData { get; set; }
        public string evaluation { get; set; }
        public string kob { get; set; }
        public string monthsHistory { get; set; }
        public string openDate { get; set; }
        public string openOrClosed { get; set; }
        public string paymentHistory { get; set; }
        public string revolvingOrInstallment { get; set; }
        public string status { get; set; }
        public string statusDate { get; set; }
        public string subscriberCode { get; set; }
        public string subscriberName { get; set; }
        public string terms { get; set; }
    }

    public class Statement
    {
        public string dateReported { get; set; }
        public string statementText { get; set; }
        public string type { get; set; }
    }

    public class Error
    {
        public string errorCode { get; set; }
        public string message { get; set; }
    }

    public class CreditProfile
    {
        public List<AddressInformation> addressInformation { get; set; }
        public ConsumerIdentity consumerIdentity { get; set; }
        public List<DirectCheck> directCheck { get; set; }
        public List<EmploymentInformation> employmentInformation { get; set; }
        public List<FraudShield> fraudShield { get; set; }
        public List<Inquiry> inquiry { get; set; }
        public List<InformationalMessage> informationalMessage { get; set; }
        public Mla mla { get; set; }
        public List<Summary> summaries { get; set; }
        public List<RiskModel> riskModel { get; set; }
        public List<Ssn> ssn { get; set; }
        public List<Tradeline> tradeline { get; set; }
        public List<Statement> statement { get; set; }
    }

    public class CreditReport
	{
		public List<CreditProfile> creditProfile { get; set; }
        public List<Error> errors { get; set; }
    }
}