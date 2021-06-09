
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Estimate
{
   public class HouseCleaningEstimateCreate: IEstimateCreate
    {
        public int CustomerID { get; set; }
        public double EstimatedCharge { get; set; }
        public double EstimatedHours { get; set; }
        public double EstimatedCostOfMaterials { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfFullBath { get; set; }
        public int NumberOfHalfBath { get; set; }
        public bool Basement { get; set; }
        public string Notes { get; set; }

    }
}
