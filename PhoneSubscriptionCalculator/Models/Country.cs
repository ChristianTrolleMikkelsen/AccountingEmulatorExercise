using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneSubscriptionCalculator.Models
{
    public interface ICountry
    {
        string ISOCode { get; }
    }

    public class Country : ICountry
    {
        public string ISOCode { get; private set; }

        public Country(string isoCode)
        {
            ISOCode = isoCode;
        }
    }
}
