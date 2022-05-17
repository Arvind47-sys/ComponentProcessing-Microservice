using Api.DTOs;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    /// <summary>
    /// Interface for complete processing workflow of saving the process response in DB or deleting the process request
    /// as a whole
    /// </summary>
    public interface ICompleteProcessingBLO
    {
        Task<bool> SaveReturnRequest(PaymentDetailsDto paymentDetails, string username);
        Task<bool> CancelReturnRequest(int requestId);
    }
}