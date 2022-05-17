using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    /// <summary>
    /// Entity for Process response. Stores all Process Responses
    /// </summary>
    [Table("ProcessResponse")]
    public class ProcessResponse
    {
        public int Id { get; set; }
        public double ProcessingCharge { get; set; }
        public double PackagingAndDeliveryCharge { get; set; }
        public DateTime DateOfDelivery { get; set; }

        /// <summary>
        /// Bound to column Id in AppUser entity (FK refers AppUser)
        /// </summary>
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }

        /// <summary>
        /// Bound to column Id in ProcessRequest (FK refers ProcessRequest)
        /// </summary>
        public ProcessRequest ProcessRequest { get; set; }
        public int RequestId { get; set; }
    }
}