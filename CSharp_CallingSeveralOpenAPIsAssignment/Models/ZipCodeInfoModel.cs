using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_CallingSeveralOpenAPIsAssignment.Models
{
    public class ZipCodeInfoModel
    {
        public string postcode { get; set; }
        public string country { get; set; }
        public string countryabbreviation { get; set; }
        public Place[] places { get; set; }
    }

    public class Place
    {
        public string placename { get; set; }
        public string longitude { get; set; }
        public string state { get; set; }
        public string stateabbreviation { get; set; }
        public string latitude { get; set; }
    }

}
