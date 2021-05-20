using CRM.Data;
using CRM.Models.Estimate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
  public  class HouseCleaningEstimateService
    {
        public HouseCleaningEstimateService()  {}

        public bool CreateEstimate(HouseCleaningEstimateCreate model)
        {
            var entity = new HouseCleaningEstimate
            {
                CustomerID = model.CustomerID,
                EstimatedCharge = model.EstimatedCharge,
                EstimatedCostOfMaterials = model.EstimatedCostOfMaterials,
                EstimatedHours = model.EstimatedHours,
                Basement = model.Basement,
                NumberOfBedrooms = model.NumberOfBedrooms,
                Notes = model.Notes,
                NumberOfFullBath = model.NumberOfFullBath,
                NumberOfHalfBath = model.NumberOfHalfBath
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.HouseCleaningEstimates.Add(entity);
                if (ctx.SaveChanges() == 1)
                    return true;
                return false;
            }
        }



        public IEnumerable<HouseCleaningEstimateDetail> GetEstimates()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.HouseCleaningEstimates.Select(
                    e => new HouseCleaningEstimateDetail
                    {
                        Basement = e.Basement,
                        NumberOfBedrooms = e.NumberOfBedrooms,
                        Notes = e.Notes,
                        EstimatedCharge = e.EstimatedCharge,
                        EstimatedCostOfMaterials = e.EstimatedCostOfMaterials,
                        EstimatedHours = e.EstimatedHours,
                        EstimateID = e.EstimateID,
                        CustomerID = e.CustomerID,
                        NumberOfFullBath = e.NumberOfFullBath,
                        NumberOfHalfBath = e.NumberOfHalfBath
                    });

                return query.ToArray(); ;
            }
        }
    }
}
