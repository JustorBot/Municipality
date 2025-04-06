using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvironmentalMunicipality.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        [Required(ErrorMessage = "Citizen is required.")]
        public int CitizenID { get; set; }

        [ForeignKey("CitizenID")]
        public Citizen Citizen { get; set; }

        [Required(ErrorMessage = "Report Type is required.")]
        public string ReportType { get; set; }

        [Required(ErrorMessage = "Details are required.")]
        public string Details { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Under Review";
    }
}