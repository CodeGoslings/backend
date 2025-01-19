
using System.Collections.Generic;

namespace Interfaces
{
    public interface IRepository
    {
        // Fetch all Requests
        List<IRequest> FetchAllRequests();

        // Add a new Request
        bool AddNewRequest(IRequest request);

        // Find a Request by its ID
        IRequest FindRequestById(int requestId);

        // Edit a Request by its ID
        bool EditRequestById(IRequest request);

        // Delete a Request by its ID
        bool DeleteRequestById(int requestId);

        // Edit the status of a Request by its ID
        bool EditRequestStatus(int requestId, string status);

        // Get Request History by user ID
        List<IRequest> GetRequestHistory(string userId);
    }
}
