using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager
{
    /// <summary>
    /// class property that holds information about the property.
    /// </summary>
    [Serializable]
    public class Property
    {
        private int id;
        private Address address;
        private string legalForm;
        private string type;
        private string useForm;

        public int Id { get => id; set => id = value; }
        public Address Address { get => address; set => address = value; }
        public string LegalForm { get => legalForm; set => legalForm = value; }
        public string Type { get => type; set => type = value; }
        public string UseForm { get => useForm; set => useForm = value; }

        /// <summary>
        /// returns a override tostring with the values of the variables
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return 
                "Id: \t\t" + id + 
                "\nUseform:\t\t" + useForm + 
                "\nType:\t\t" + type + 
                "\nLegalform:\t" + legalForm + 
                "\nAddress:\t\t" + address.Country + 
                "\n\t\t" + address.City + 
                "\n\t\t" + address.Street + 
                "\n\t\t" + address.ZipCode;
        }
    }
}
