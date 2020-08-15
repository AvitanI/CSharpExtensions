using CSharpExtensions.Extensions;
using System.ComponentModel;

namespace CSharpExtensions.Models
{
    public class Company
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public ECompanyType CompanyType { get; set; }

        [DisplayName("Number Of Employees")]
        public int NumberOfEmployees { get; set; }

        [IgnoreDifferenceCheck]
        public CompanyAddress CompanyAddress { get; set; }
    }
}
