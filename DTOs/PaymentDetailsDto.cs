using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    /// <summary>
    /// Dto for Payment details and CompleteProcessing action
    /// </summary>
    public class PaymentDetailsDto
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public long CreditCardNumber { get; set; }

        [Required]
        public double CreditLimit { get; set; }

        [Required]
        public double TotalProcessingCharge { get; set; }

        [Required]
        public ProcessResponseDto ProcessResponse { get; set; }
    }
}