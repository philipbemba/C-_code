using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Experian.Api
{
    public class Applicant
    {
        public bool isPrimary { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string socialSecurityNumber { get; set; }
        public string dateOfBirth { get; set; }
        public List<string> phoneNumbers { get; set; }
        public List<Address> addresses { get; set; }
        public CreditProfile creditProfile { get; set; }
        public DriversLicense driversLicense { get; set;  }
        public Employment employment { get; set; }
    }
}
