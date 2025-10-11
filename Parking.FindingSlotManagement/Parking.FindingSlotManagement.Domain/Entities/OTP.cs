using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Domain.Entities
{
    public class OTP
    {
        public int OTPId { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationTime { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsUsed { get; set; }

        public virtual User? User { get; set; }
    }
}
