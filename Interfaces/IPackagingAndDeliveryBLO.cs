using Api.Entities;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IPackagingAndDeliveryBLO
    {
        /// <summary>
        /// Interface for calling the PackagingAndDelivery Microservice
        /// </summary>
        Task<double> ComputePackagingAndDeliveryCost(ProcessRequest processRequest);
    }
}
