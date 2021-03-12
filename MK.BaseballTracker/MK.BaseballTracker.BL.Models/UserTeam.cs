using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;

namespace MK.BaseballTracker.BL.Models
{
    public class UserTeam
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }
    }
}
