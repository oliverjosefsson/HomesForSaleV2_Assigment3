using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager
{
    ///<summary> Address class, with getters and setters
    ///for country, zipcode, street and city.
    ///</summary>
    [Serializable]
    public class Address
    {
        private string country;
        private string zipCode;
        private string street;
        private string city;

       
        public string Country { get => country; set => country = value; }
        public string ZipCode { get => zipCode; set => zipCode = value; }
        public string Street { get => street; set => street = value; }
        public string City { get => city; set => city = value; }
    }
}
