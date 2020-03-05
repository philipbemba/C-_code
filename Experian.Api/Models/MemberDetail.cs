using System.Collections.Generic;

namespace Experian.Api
{
    public class Name
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
    }

    public class SSN
    {
        public string ssn { get; set; }
    }

    public class DateOfBirth
    {
        public string dob { get; set; }
    }

    public class AddressExperian
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
    }

    public class DriversLicense
    {
        public string number { get; set; }
        public string state { get; set; }
    }

    public class Employment
    {
        public string employerName { get; set; }
    }

    public class MemberDetail
    {
        public Name name { get; set; }
        public SSN ssn { get; set; }
        public DateOfBirth dob { get; set; }
        public AddressExperian currentAddress { get; set; }
        public AddressExperian previousAddress { get; set; }
        public List<Phone> phone { get; set; }
        //public DriversLicense? driversLicense { get; set; }
        //public Employment? employment { get; set; }

        // Constructor
        public MemberDetail(Applicant applicant)
        {
            name = new Name
            {
                firstName = applicant.firstName,
                lastName = applicant.lastName,
                middleName = applicant.middleName
            };

            ssn = new SSN
            {
                ssn = applicant.socialSecurityNumber
            };

            dob = new DateOfBirth
            {
                dob = applicant.dateOfBirth
            };
            
            /*if (applicant.employment != null)
            {
                employment = new Employment
                {
                    employerName = applicant.employment.employerName
                };
            }
            
            if (applicant.driversLicense != null)
            {
                driversLicense = new DriversLicense
                {
                    number = applicant.driversLicense.number,
                    state = applicant.driversLicense.state
                };
            }*/    

            // Loop through the address information.
            if (applicant.addresses != null)
            {
                foreach (var address in applicant.addresses)
                {
                    AddressExperian residence = new AddressExperian
                    {
                        line1 = address.lineOne,
                        state = address.state,
                        city = address.city,
                        zipCode = address.zip,
                        line2 = address.lineTwo
                    };

                    if (address.isCurrent == true)
                    {
                        currentAddress = residence;
                    }
                    else
                    {
                        previousAddress = residence;
                    }
                }
            }
            
            if (applicant.phoneNumbers != null)
            {
                phone = new List<Phone>();
                foreach (string phoneNumber in applicant.phoneNumbers)
                {
                    Phone temp = new Phone
                    {
                        number = phoneNumber
                    };
                    phone.Add(temp);
                }
            }               
        }
    }
}
