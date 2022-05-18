using Api.Constants;
using Api.DTOs;
using Api.Interfaces;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// Contains end point methods for component processing services
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComponentProcessingController : Controller
    {
        private readonly IIntegralPartProcessingBLO _integralPartProcessingBLO;
        private readonly IAccessoryPartProcessingBLO _accessoryPartProcessingBLO;
        private readonly ICompleteProcessingBLO _completeProcessingBLO;

        public ComponentProcessingController(IIntegralPartProcessingBLO integralPartProcessingBLO,
         IAccessoryPartProcessingBLO accessoryPartProcessingBLO,
         ICompleteProcessingBLO completeProcessingBLO)
        {
            _completeProcessingBLO = completeProcessingBLO;
            _accessoryPartProcessingBLO = accessoryPartProcessingBLO;
            _integralPartProcessingBLO = integralPartProcessingBLO;
        }

        /// <summary>
        /// Processes the input request and generates corresponding process response based on some predefined parameters
        /// </summary>
        /// <param name="processRequest"></param>
        /// <returns></returns>
        [HttpPost("processDetail")]
        public async Task<ActionResult<ProcessResponseDto>> ProcessDetail([FromBody] ProcessRequestDto processRequest)
        {
            // Getting the logged in Username from the claim in JWT
            var username = User.GetUserName();

            var result = new ProcessResponseDto();

            if (processRequest.DefectiveComponentType == ReturnOrderManagementConstants.Integral)
            {
                result = await _integralPartProcessingBLO.ProcessDefectiveComponent(processRequest, username);

            }
            else if (processRequest.DefectiveComponentType == ReturnOrderManagementConstants.Accessory)
            {
                result = await _accessoryPartProcessingBLO.ProcessDefectiveComponent(processRequest, username);
            }

            return Ok(result);

        }

        /// <summary>
        /// Saves the process response
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        [HttpPost("completeProcessing")]
        public async Task<ActionResult<bool>> CompleteProcessing([FromBody] PaymentDetailsDto paymentDetails)
        {
            if (paymentDetails.CreditLimit < paymentDetails.TotalProcessingCharge)
            {
                return BadRequest("Credit limit is less than the total processing charge");
            }
            else
            {
                // Getting the logged in Username from the claim in JWT
                var username = User.GetUserName();

                var result = await _completeProcessingBLO.SaveReturnRequest(paymentDetails, username);
                return Ok(result);
            }
        }

        /// <summary>
        /// Deletes the process request based on the Id of process request in DB
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteReturnRequest/{requestId}")]
        public async Task<ActionResult> DeleteReturnRequest(int requestId)
        {
            if (await _completeProcessingBLO.CancelReturnRequest(requestId))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}