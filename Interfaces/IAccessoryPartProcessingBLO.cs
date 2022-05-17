using Api.DTOs;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    /// <summary>
    /// Interface for Accessory Part processing as per the pre defined processing charge and duration as
    /// given in the REQUIREMENT
    /// </summary>
    public interface IAccessoryPartProcessingBLO
    {
        Task<ProcessResponseDto> ProcessDefectiveComponent(ProcessRequestDto processRequest, string username);

    }
}