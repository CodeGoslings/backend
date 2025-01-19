using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using MRCModel;
namespace MRCService;

public class MRCService : IMRCService
{
    private DatabaseManager databaseManager;

    public MRCService()
    {
        databaseManager = new DatabaseManager(); // Initialize databaseManager here
    }
    public bool createRequest(int requestId, string type, string description, string priority, string location, DateTime submissionDate, string status, string submittedBy)
    {
        try
        {
            if (!Enum.TryParse<RequestStatus>(status, out var parsedStatus) ||
                !Enum.IsDefined(typeof(RequestStatus), parsedStatus))
            {
                throw new ArgumentException($"{status} is not a valid RequestStatus.");
            }

            Request request = new Request(requestId, type, description, priority, location, submissionDate, parsedStatus, submittedBy);
            if (!databaseManager.AddNewRequest(request))
            {
                throw new InvalidOperationException("Failed to add the new request to the database.");
            }

            return true;
        }
        catch (Exception ex)
        {
            // Log the exception (e.g., to a file or monitoring system) and rethrow it
            Console.WriteLine($"Error in createRequest: {ex.Message}");
            throw;
        }
    }

    public bool updateRequestStatus(int requestId, string requestStatus)
    {
        try
        {
            if (!Enum.TryParse<RequestStatus>(requestStatus, out var parsedStatus) ||
                !Enum.IsDefined(typeof(RequestStatus), parsedStatus))
            {
                throw new ArgumentException($"{requestStatus} is not a valid RequestStatus.");
            }

            if (!databaseManager.EditRequestStatus(requestId, parsedStatus))
            {
                throw new InvalidOperationException($"Failed to update the status for request ID {requestId}.");
            }

            return true;
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow it
            Console.WriteLine($"Error in updateRequestStatus: {ex.Message}");
            throw;
        }
    }
    public bool sendNotification(int requestId)
    {
        try
        {
            // Call the notification component
            if (1 == 1) // Assuming that notification component exists
            {
                throw new InvalidOperationException($"Failed to send notification for request ID {requestId}.");
            }

            return true;
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow it
            Console.WriteLine($"Error in sendNotification: {ex.Message}");
            throw;
        }
    }
    public bool editRequest(int requestId, string type, string description, string priority, string location, DateTime submissionDate, string status, string submittedBy)
    {
        try
        {
            if (!Enum.TryParse<RequestStatus>(status, out var parsedStatus) ||
                !Enum.IsDefined(typeof(RequestStatus), parsedStatus))
            {
                throw new ArgumentException($"{status} is not a valid RequestStatus.");
            }

            Request request = new Request(requestId, type, description, priority, location, submissionDate, parsedStatus, submittedBy);
            if (!databaseManager.EditRequestById(request))
            {
                throw new InvalidOperationException($"Failed to edit the request with ID {requestId}.");
            }

            return true;
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow it
            Console.WriteLine($"Error in editRequest: {ex.Message}");
            throw;
        }
    }
    public JsonArray viewRequests()
    {
        List<Request> requests = databaseManager.FetchAllRequests();
        JsonArray arr = new JsonArray();
        foreach (var request in requests)
        {
            arr.Add(request.ToJsonObject());
        }
        return arr;
    }
    public string viewRequestStatus(int requestId)
    {
        var request = databaseManager.FindRequestById(requestId);
        if (request == null)
        {
            throw new ArgumentException($"Request with ID {requestId} does not exist.");
        }
        return request.status.ToString();
    }

    public JsonArray viewRequestDetails(int requestId)
    {
        try
        {
            JsonArray arr = new JsonArray();
            var request = databaseManager.FindRequestById(requestId);
            if (request == null)
            {
                throw new ArgumentException($"Request with ID {requestId} does not exist.");
            }
            arr.Add(request.ToJsonObject());
            return arr;
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow it
            Console.WriteLine($"Error in viewRequestDetails: {ex.Message}");
            throw;
        }
    }

    public JsonArray getRequestHistory(string userId)
    {
        try
        {
            JsonArray arr = new JsonArray();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.");
            }

            List<Request> requests = databaseManager.GetRequestHistory(userId);
            if (requests == null || requests.Count == 0)
            {
                throw new ArgumentException($"No request history found for User ID {userId}.");
            }

            foreach (var request in requests)
            {
                arr.Add(request.ToJsonObject());
            }
            return arr;
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow it
            Console.WriteLine($"Error in getRequestHistory: {ex.Message}");
            throw;
        }
    }

    public JsonArray getAllUsers()
    {
        List<User> users = databaseManager.FetchAllUsers();
        JsonArray arr = new JsonArray();
        foreach (var user in users)
        {
            arr.Add(user.ToJsonObject());
        }
        return arr;
    }
}