using System.Text.Json.Nodes;
using MRCModel;
namespace MRCService;

public class AIService : IAIService
{
    private DatabaseManager databaseManager;
    public bool createRequest(int requestId, string type, string description, string priority, string location, DateTime submissionDate, string status, string submittedBy)
    {
        if (!Enum.TryParse<RequestStatus>(status, out var parsedStatus) ||
                !Enum.IsDefined(typeof(RequestStatus), parsedStatus))
        {
            Console.WriteLine($"{status} is not a valid RequestStatus.");
            return false;
        }

        Request request = new Request(requestId, type, description, priority, location, submissionDate, parsedStatus, submittedBy);
        if (databaseManager.AddNewRequest(request))
        {
            return true;
        }
        return false;
    }
    public bool updateRequestStatus(int requestId, string requestStatus)
    {
        if (!Enum.TryParse<RequestStatus>(requestStatus, out var parsedStatus) ||
                !Enum.IsDefined(typeof(RequestStatus), parsedStatus))
        {
            Console.WriteLine($"{requestStatus} is not a valid RequestStatus.");
            return false;
        }
        if (databaseManager.EditRequestStatus(requestId, parsedStatus))
        {
            return true;
        }
        return false;
    }
    public bool sendNotification(int requestId)
    {
        // call the notification component
        return true;
    }
    public bool editRequest(int requestId, string type, string description, string priority, string location, DateTime submissionDate, string status, string submittedBy)
    {
        if (!Enum.TryParse<RequestStatus>(status, out var parsedStatus) ||
                !Enum.IsDefined(typeof(RequestStatus), parsedStatus))
        {
            Console.WriteLine($"{status} is not a valid RequestStatus.");
            return false;
        }

        Request request = new Request(requestId, type, description, priority, location, submissionDate, parsedStatus, submittedBy);
        if (databaseManager.EditRequestById(request))
        {
            return true;
        }
        return false;
    }
    public JsonArray viewRequests()
    {
        List<Request> requests = databaseManager.FetchAllRequests();
        JsonArray arr = [];
        foreach (var request in requests)
        {
            arr.Add(request.ToJsonObject);
        }
        return arr;
    }
    public string viewRequestStatus(int requestId)
    {
        return databaseManager.FindRequestById(requestId).status.ToString();
    }
    public JsonArray viewRequestDetails(int requestId)
    {
        JsonArray arr = [];
        arr.Add(databaseManager.FindRequestById(requestId).ToJsonObject());
        return arr;
    }
    public JsonArray getRequestHistory(string userId)
    {
        List<Request> requests = databaseManager.GetRequestHistory(userId);
        JsonArray arr = [];
        foreach (var request in requests)
        {
            arr.Add(request.ToJsonObject);
        }
        return arr;
    }
}