using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Estimate
{
    public interface IEstimateDetail
    {
        Customer Customer { get; set; }
        int CustomerID { get; set; }
        double EstimatedCharge { get; set; }
        double EstimatedCostOfMaterials { get; set; }
        double EstimatedHours { get; set; }
        int EstimateID { get; set; }
    }
}
