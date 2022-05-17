using api.Repository.IRepository;
using Api.Constants;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Api.BLOs
{
    /// <summary>
    /// Contains Business logic for Integral Part processing as per the pre defined processing charge and duration as
    /// given in the REQUIREMENT
    /// </summary>
    public class IntegralPartProcessingBLO : IIntegralPartProcessingBLO
    {
        private readonly IMapper _mapper;
        private readonly IPackagingAndDeliveryBLO _packagingAndDeliveryBLO;
        private readonly IProcessRequestAndResponseRepository _processRequestAndResponseRepository;
        private readonly IUserRepository _userRepository;

        public IntegralPartProcessingBLO(IProcessRequestAndResponseRepository processRequestAndResponseRepository,
        IUserRepository userRepository, IMapper mapper,
        IPackagingAndDeliveryBLO packagingAndDeliveryBLO)
        {
            _userRepository = userRepository;
            _processRequestAndResponseRepository = processRequestAndResponseRepository;
            _mapper = mapper;
            _packagingAndDeliveryBLO = packagingAndDeliveryBLO;
        }

        /// <summary>
        /// Processes the defective component by storing the request information in DB and returns the request ID
        /// generated by the DB along with the processing charge, packaging charge and duration details in the ProcessResponseDto
        /// </summary>
        /// <param name="processRequestDto"></param>
        /// <param name="username"></param>
        /// <returns>Process Response details</returns>
        public async Task<ProcessResponseDto> ProcessDefectiveComponent(ProcessRequestDto processRequestDto, string username)
        {
            var userId = await _userRepository.GetUserId(username);
            var processRequest = _mapper.Map<ProcessRequest>(processRequestDto);
            ProcessResponseDto processResponseResult = new ProcessResponseDto();
            processRequest.AppUserId = userId;
            if (userId != 0)
            {
                if (processRequestDto.Id == 0)
                {
                    await _processRequestAndResponseRepository.CreateNewProcessRequest(processRequest);
                    processResponseResult = await ComputeProcessResponse(processRequest);
                }
                else
                {
                    await _processRequestAndResponseRepository.UpdateProcessRequest(processRequest);
                    processResponseResult = await ComputeProcessResponse(processRequest);
                }
            }
            return processResponseResult;

        }

        private async Task<ProcessResponseDto> ComputeProcessResponse(ProcessRequest processRequest)
        {
            return new ProcessResponseDto
            {
                RequestId = processRequest.Id,
                ProcessingCharge = ReturnOrderManagementConstants.ChargeForIntegralPart * processRequest.Quantity,
                DateOfDelivery = DateTime.Now.AddDays(ReturnOrderManagementConstants.DurationForIntegralPart * processRequest.Quantity),
                PackagingAndDeliveryCharge = await _packagingAndDeliveryBLO.ComputePackagingAndDeliveryCost(processRequest)
            };
        }


    }
}
