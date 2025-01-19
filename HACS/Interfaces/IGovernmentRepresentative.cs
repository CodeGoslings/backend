using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IGovernmentRepresentative:IUser
    {
        string userRegion { get; set; }
        public string userAuthorityLevel { get; set; }
    }
}
