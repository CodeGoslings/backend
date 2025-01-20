using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Interfaces
{
    public interface IAffectedIndividual
    {
        string userLocation { get; set; }
        string userContactInfo { get; set; }
    }
}
