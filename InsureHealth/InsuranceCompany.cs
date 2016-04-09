using System.Collections.Generic;

namespace InsureHealth
{
    public class InsuranceCompany
    {
        public string CompanyName;
        public string Address;
        public string City;
        public string State;
        public string ZipCode;
        public List<Policy> Policies;

        public InsuranceCompany()
        {
            
        }
    }
}