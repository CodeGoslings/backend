using System.Text.Json.Nodes;
using Interfaces;
namespace MRCModel.Models;
public class Request: IRequest
{
    // Additional fields
    public int requestId { get; set; }
    public string type { get; set; }
    public string description { get; set; }
    public string priority { get; set; }
    public string location { get; set; }
    public DateTime submissionDate { get; set; }
    public RequestStatus status { get; set; }
    public string submittedBy { get; set; }

    // Constructor
    public Request(int requestId, string type, string description, string priority, string location, DateTime submissionDate, RequestStatus status, string submittedBy)
    {
        this.requestId = requestId;
        this.type = type;
        this.description = description;
        this.priority = priority;
        this.location = location;
        this.submissionDate = submissionDate;
        this.status = status;
        this.submittedBy = submittedBy;
    }
    public Request() { }

    public JsonObject ToJsonObject()
    {
        return new JsonObject
            {
                { "requestId", requestId },
                { "type", type },
                { "description", description },
                { "priority", priority },
                { "location", location },
                { "submissionDate", submissionDate.ToString("yyyy-MM-dd") },  // Format DateTime to string
                { "status", status.ToString() },
                { "submittedBy", submittedBy }
            };
    }

}