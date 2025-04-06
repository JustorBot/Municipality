using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvironmentalMunicipality.Models
{
    public class ServiceRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        [ForeignKey("Citizen")]
        public int CitizenID { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceType { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        // Navigation property
        public virtual Citizen Citizen { get; set; }
    }
}