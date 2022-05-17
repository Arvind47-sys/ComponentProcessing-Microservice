using Api.DTOs;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    /// <summary>
    /// Interface for Integral Part processing as per the pre defined processing charge and duration as
    /// given in the REQUIREMENT
    /// </summary>
    public interface IIntegralPartProcessingBLO
    {
        Task<ProcessResponseDto> ProcessDefectiveComponent(ProcessRequestDto processRequest, string username);
    }
}