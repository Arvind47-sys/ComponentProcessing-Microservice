using Api.DTOs;
using Api.Entities;
using System.Threading.Tasks;

namespace api.Repository.IRepository
{
    /// <summary>
    /// Repository related to CRUD operation for Process Request and Response
    /// </summary>
    public interface IProcessRequestAndResponseRepository
    {
        Task CreateNewProcessRequest(ProcessRequest processRequest);
        Task UpdateProcessRequest(ProcessRequest processRequest);
        Task<bool> DeleteProcessRequest(ProcessRequest processRequest);
        Task<bool> CreateNewProcessResponse(ProcessResponse processResponse);
        UserRequestDetailsDto GetAllUserReturnRequests(int appUserId);
        Task<bool> ReturnRequestExists(int requestId);
        Task<ProcessRequest> GetProcessRequest(int requestId);

    }
}
