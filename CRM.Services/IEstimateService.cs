using CRM.Models.Estimate;
using System.Collections.Generic;

namespace CRM.Services
{
    public interface IEstimateService
    {
        bool CreateEstimate(IEstimateCreate model);
        IEstimateDetail GetEstimateById(int id);
        IEnumerable<IEstimateDetail> GetEstimates();
    }
}