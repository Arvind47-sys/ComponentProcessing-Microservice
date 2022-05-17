using System.Collections.Generic;

namespace Api.DTOs
{
    /// <summary>
    /// Dto for user's return requests and responses
    /// </summary>
    public class UserRequestDetailsDto
    {

        public IEnumerable<ProcessRequestDto> ProcessRequests { get; set; }
        public IEnumerable<ProcessResponseDto> ProcessResponses { get; set; }
    }
}