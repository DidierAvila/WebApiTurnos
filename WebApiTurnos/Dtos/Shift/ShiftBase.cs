using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTurnos.Dtos.Shift
{
    public class ShiftBase
    {
        public int? UserId { get; set; }
        public int BranchId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int Status { get; set; }
    }
}
