using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campuscloset.Models
{
    public class UserSettings
    {
        public string Theme { get; set; } // "Light" or "Dark"
        public bool Notifications { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}