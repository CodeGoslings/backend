using System.Text.Json.Nodes;
namespace MRCService;

public interface IMRCService
{
    public static IMRCService createService()
    {
        return new MRCService();
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
