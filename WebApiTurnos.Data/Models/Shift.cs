using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiTurnos.Data.Models
{
    [Table(name: "Shift")]
    public class Shift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int BranchId { get; set; }

        public DateTime ShiftDate { get; set; }

        public int Status { get; set; }

        [NotMapped]
        public string? Error { get; set; }
    }
}
