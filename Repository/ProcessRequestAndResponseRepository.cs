using api.Repository.IRepository;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repository
{
    /// <summary>
    /// Repository related to CRUD operation for Process Request and Response
    /// </summary>
    public class ProcessRequestAndResponseRepository : IProcessRequestAndResponseRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProcessRequestAndResponseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateNewProcessRequest(ProcessRequest processRequest)
        {
            _context.ProcessRequest.Add(processRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CreateNewProcessResponse(ProcessResponse processResponse)
        {
            _context.ProcessResponses.Add(processResponse);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateProcessRequest(ProcessRequest processRequest)
        {
            _context.Entry(processRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProcessRequest(ProcessRequest processRequest)
        {
            _context.ProcessRequest.Remove(processRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public UserRequestDetailsDto GetAllUserReturnRequests(int appUserId)
        {

            List<ProcessRequest> processRequestsFromDb = new List<ProcessRequest>();
            List<ProcessResponse> processResponsesFromDb = new List<ProcessResponse>();

            List<Task> lstTask = new List<Task>
            {
                Task.Factory.StartNew(async() => processRequestsFromDb =
                await _context.ProcessRequest.Where(req => req.AppUserId == appUserId).ToListAsync()),
                Task.Factory.StartNew(async() => processResponsesFromDb =
                await _context.ProcessResponses.Where(resp => resp.AppUserId == appUserId).ToListAsync())
            };
            Task.WaitAll(lstTask.ToArray());

            UserRequestDetailsDto userRequestDetails = new UserRequestDetailsDto()
            {
                ProcessRequests = _mapper.Map<IEnumerable<ProcessRequestDto>>(processRequestsFromDb),
                ProcessResponses = _mapper.Map<IEnumerable<ProcessResponseDto>>(processResponsesFromDb)
            };

            return userRequestDetails;
        }

        public async Task<bool> ReturnRequestExists(int requestId)
        {
            return await _context.ProcessRequest.AnyAsync(x => x.Id == requestId);
        }

        public async Task<ProcessRequest> GetProcessRequest(int requestId)
        {
            return await _context.ProcessRequest.SingleOrDefaultAsync(x => x.Id == requestId);
        }
    }
}
