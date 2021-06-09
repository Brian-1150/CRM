using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
   public class HouseCleaningEstimate : IEstimate
    {
       
        [Key]
        public int EstimateID { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public double EstimatedCharge { get; set; }
        public double EstimatedCostOfMaterials { get; set; }
        public double EstimatedHours { get; set; }

        public int NumberOfBedrooms { get; set; }
        public int NumberOfFullBath { get; set; }
        public int NumberOfHalfBath { get; set; }
        public bool Basement { get; set; }
        public string Notes { get; set; }
        //public bool AdditionalRooms { get; set; }
        //public string BedroomNotes { get; set; }
        //public string BathNotes { get; set; }
        //public string AdditionalRoomsNotes { get; set; }
        //public string BasementNotes { get; set; }
        //public string KitchenNotes { get; set; }
    }
}
