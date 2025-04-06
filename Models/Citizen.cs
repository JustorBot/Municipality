using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace EvironmentalMunicipality.Models
{
    public class Citizen
    {
        [Key]
        public int CitizenID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
