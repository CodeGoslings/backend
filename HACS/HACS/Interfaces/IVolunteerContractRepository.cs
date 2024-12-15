using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Models;

namespace HACS.Interfaces
{
    public interface IVolunteerContractRepository
    {
        Task<IEnumerable<VolunteerContract>> GetByVolunteer(int volunteerId);
    }
}