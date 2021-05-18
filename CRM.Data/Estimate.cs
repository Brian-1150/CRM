using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    
    public abstract class Estimate
    {
       

        [Key]
        public int EstimateID { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public double EstimatedCharge { get; set; }
        public double EstimatedCostOfMaterials { get; set; }
        public double EstimatedHours { get; set; }



    }
}
