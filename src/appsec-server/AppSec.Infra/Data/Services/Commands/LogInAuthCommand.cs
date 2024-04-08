using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSec.Infra.Data.Services.Commands
{
    public class LogInAuthCommand
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
    }
}
