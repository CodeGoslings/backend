using System.Text.Json.Nodes;
namespace MRCService;

public interface IMRC_Controller
{
    public static IMRC_Controller createService()
    {
        return new MRCController();
    }
    bool createRequest(int requestId, string type, string description, string priority, string location, DateTime submissionDate, string status, string submittedBy);
    bool updateRequestStatus(int requestId, string requestStatus);
    bool sendNotification(int requestId);
    bool editRequest(int requestId, string type, string description, string priority, string location, DateTime submissionDate, string status, string submittedBy);
    JsonArray viewRequests();
    string viewRequestStatus(int requestId);
    JsonArray viewRequestDetails(int requestId);
    JsonArray getRequestHistory(string userId);
}
