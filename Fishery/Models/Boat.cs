using System;
using System.Collections.Generic;

namespace Fishery.Models
{
    public partial class Boat
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string BoatName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LicenceExpired { get; set; }
        public string Belong { get; set; }
    }
}
