using CRM.Data;
using CRM.Models.Estimate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class HouseCleaningEstimateService : IEstimateService
    {
        public HouseCleaningEstimateService() { }

        public bool CreateEstimate(IEstimateCreate model)
        {
            var estModel = (HouseCleaningEstimateCreate)model;
            var entity = new HouseCleaningEstimate
            {
                CustomerID = model.CustomerID,
                EstimatedCharge = model.EstimatedCharge,
                EstimatedCostOfMaterials = model.EstimatedCostOfMaterials,
                EstimatedHours = model.EstimatedHours,
                Basement = estModel.Basement,
                NumberOfBedrooms = estModel.NumberOfBedrooms,
                Notes = estModel.Notes,
                NumberOfFullBath = estModel.NumberOfFullBath,
                NumberOfHalfBath = estModel.NumberOfHalfBath
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.HouseCleaningEstimates.Add(entity);
                if (ctx.SaveChanges() == 1)
                    return true;
                return false;
            }
        }

       
        public IEstimateDetail GetEstimateById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.HouseCleaningEstimates.Find(id);
                var est = new HouseCleaningEstimateDetail
                {
                    Basement = entity.Basement,
                    CustomerID = entity.CustomerID,
                    EstimateID = entity.EstimateID,
                    EstimatedCharge = entity.EstimatedCharge,
                    EstimatedCostOfMaterials = entity.EstimatedCostOfMaterials,
                    EstimatedHours = entity.EstimatedHours,
                    Notes = entity.Notes,
                    NumberOfBedrooms = entity.NumberOfBedrooms,
                    NumberOfFullBath = entity.NumberOfFullBath,
                    NumberOfHalfBath = entity.NumberOfHalfBath
                };
                return (IEstimateDetail)est;
            }
        }
       
        public IEnumerable<IEstimateDetail> GetEstimates()
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

                return (IEnumerable<IEstimateDetail>)query.ToArray(); ;
            }
        }
    }
}
