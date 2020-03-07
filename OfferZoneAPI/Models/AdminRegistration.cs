using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferZoneAPI.Models
{
    public class AdminRegistration
    {
        public Int32 AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminDob { get; set; }
        public Int32 AdminMobile { get; set; }
    }
}
