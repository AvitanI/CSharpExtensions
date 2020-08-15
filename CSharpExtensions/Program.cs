using CSharpExtensions.Extensions;
using CSharpExtensions.Models;
using System;
using System.Collections.Generic;

namespace CSharpExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            var before = new Company
            {
                Name = "Company",
                Active = true,
                CompanyType = ECompanyType.Private,
                NumberOfEmployees = 10,
                CompanyAddress = new CompanyAddress
                {
                    StreetId = 1
                }
            };

            var after = new Company
            {
                Name = "New Company Name",
                Active = true,
                CompanyType = ECompanyType.Public,
                NumberOfEmployees = 20,
                CompanyAddress = new CompanyAddress
                {
                    StreetId = 2
                }
            };

            IEnumerable<Difference> differences = before.FindDifferences(after);

            foreach (Difference difference in differences)
            {
                Console.WriteLine(string.Format("{0} is changed from '{1}' to '{2}'", difference.Name, difference.Before, difference.After));
            }

            Console.ReadKey();
        }
    }
}
