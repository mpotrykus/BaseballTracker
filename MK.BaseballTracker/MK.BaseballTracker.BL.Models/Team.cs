using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;

namespace MK.BaseballTracker.BL.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public String Logo { get; set; }
    }
}
