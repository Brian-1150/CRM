using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Estimate
{
    public class HouseCleaningEstimateDetail
    {
        public int EstimateID { get; set; }
        public int CustomerID { get; set; }
        public double EstimatedCharge { get; set; }
        public double EstimatedHours { get; set; }
        public double EstimatedCostOfMaterials { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfFullBath { get; set; }
        public int NumberOfHalfBath { get; set; }

        [UIHint("OtherBool")]
        public bool Basement { get; set; }
        public string Notes { get; set; }
    }
}
