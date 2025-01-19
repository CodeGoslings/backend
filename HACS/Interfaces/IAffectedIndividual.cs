using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IAffectedIndividual : IUser
    {
        string userLocation { get; set; }
        string userContactInfo { get; set; }
    }
}
