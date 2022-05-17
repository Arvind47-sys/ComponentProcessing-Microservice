using System;

namespace Api.DTOs
{
    /// <summary>
    /// Dto for process response and ProcessDetail action
    /// </summary>
    public class ProcessResponseDto
    {
        public int RequestId { get; set; }
        public double ProcessingCharge { get; set; }
        public double PackagingAndDeliveryCharge { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
}