using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BaseballTracker.PL;

namespace MK.BaseballTracker.BL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string DisplayName
        {
            get { return (FirstName + " " + LastName); }
        }
        public String Email { get; set; }
        public DateTime DateAdded { get; set; }
        public String Password { get; set; }
    }
}
