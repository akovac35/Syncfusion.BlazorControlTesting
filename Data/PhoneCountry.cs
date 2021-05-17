using System.Collections.Generic;

namespace Syncfusion.BlazorControlTesting.Data
{
    public class PhoneCountry
    {
        /// <summary>
        /// Example: +386
        /// </summary>
        public string? Code { get; set; }
        public string? CountryName { get; set; }
        public string CountryNameAndCode { get => $"{CountryName} - {Code}"; }
        public string CountryCodeAndName { get => $"{Code} - {CountryName}"; }

        public static List<PhoneCountry> Countries { get; set; } = new List<PhoneCountry>() { 
            new PhoneCountry { Code = "+386", CountryName = "Slovenia"},
            new PhoneCountry { Code = "+385", CountryName = "Croatia"},
            new PhoneCountry { Code = "+381", CountryName = "Srbia"},
            new PhoneCountry { Code = "+382", CountryName = "Monte Negro"}
        };
    }
}
