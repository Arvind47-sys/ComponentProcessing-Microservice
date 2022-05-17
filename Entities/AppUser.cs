using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    /// <summary>
    /// Entity for App User. Used to store user details
    /// </summary>
    [Table("AppUser")]
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// One to many relationship with ProcessRequest entity
        /// </summary>
        public ICollection<ProcessRequest> ProcessRequests { get; set; }

        /// <summary>
        /// One to many relationship with ProcessResponse entity
        /// </summary>
        public ICollection<ProcessResponse> ProcessResponses { get; set; }

    }
}