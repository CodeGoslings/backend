
using MRCModel;
using MRCModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRequest
    {
         int requestId { get; set; }
         string type { get; set; }
         string description { get; set; }
         string priority { get; set; }
         string location { get; set; }
         DateTime submissionDate { get; set; }
         RequestStatus status { get; set; }
         string submittedBy { get; set; }
    }
}
