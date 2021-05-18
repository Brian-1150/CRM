using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
   public class HouseCleaningEstimate : Estimate
    {
        public int NumberOfBedrooms { get; set; }
        public int NumberOfFullBath { get; set; }
        public int NumberOfHalfBath { get; set; }
        public bool Basement { get; set; }
        //public bool AdditionalRooms { get; set; }
        //public string BedroomNotes { get; set; }
        //public string BathNotes { get; set; }
        //public string AdditionalRoomsNotes { get; set; }
        //public string BasementNotes { get; set; }
        //public string KitchenNotes { get; set; }
        public string Notes { get; set; }


    }
}
