

namespace CRM.Data

{
    public interface IEstimate
    {
       
        Customer Customer { get; set; }
        int CustomerID { get; set; }
        double EstimatedCharge { get; set; }
        double EstimatedCostOfMaterials { get; set; }
        double EstimatedHours { get; set; }
        int EstimateID { get; set; }
    }
}