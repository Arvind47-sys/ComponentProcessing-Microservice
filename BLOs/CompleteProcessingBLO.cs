using api.Repository.IRepository;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace Api.BLOs
{
    /// <summary>
    /// Contains business logic for complete processing workflow of saving the process response in DB or deleting the process request
    /// as a whole
    /// </summary>
    public class CompleteProcessingBLO : ICompleteProcessingBLO
    {
        private readonly IMapper _mapper;
        private readonly IProcessRequestAndResponseRepository _processRequestAndResponseRepository;
        private readonly IUserRepository _userRepository;

        public CompleteProcessingBLO(IProcessRequestAndResponseRepository processRequestAndResponseRepository,
            IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _processRequestAndResponseRepository = processRequestAndResponseRepository;
            _mapper = mapper;

        }

        /// <summary>
        /// Saves the process response from paymentDetails to the DB
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> SaveReturnRequest(PaymentDetailsDto paymentDetails, string username)
        {
            var userId = await _userRepository.GetUserId(username);
            var processResponseDto = paymentDetails.ProcessResponse;
            var processResponse = _mapper.Map<ProcessResponse>(processResponseDto);
            processResponse.AppUserId = userId;

            return await _processRequestAndResponseRepository.CreateNewProcessResponse(processResponse);
        }

        /// <summary>
        /// Deletes the generated process request from DB
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<bool> CancelReturnRequest(int requestId)
         {
            if (await _processRequestAndResponseRepository.ReturnRequestExists(requestId))
            {
                var processRequestObj = await _processRequestAndResponseRepository.GetProcessRequest(requestId);

                return await _processRequestAndResponseRepository.DeleteProcessRequest(processRequestObj);
            }

            return false;
        }

    }
}
