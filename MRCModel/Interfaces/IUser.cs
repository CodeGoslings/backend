using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUser
    {
        string userId { get; set; }
        string userName { get; set; }
        string userEmail { get; set; }
        string userPassword { get; set; }
    }
}
