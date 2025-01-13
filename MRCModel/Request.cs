using System.Text.Json.Nodes;

namespace MRCModel;
public class Request
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

    public JsonObject ToJsonObject()
    {
        return new JsonObject
            {
                { "requestId", this.requestId },
                { "type", this.type },
                { "description", this.description },
                { "priority", this.priority },
                { "location", this.location },
                { "submissionDate", this.submissionDate.ToString("yyyy-MM-dd") },  // Format DateTime to string
                { "status", this.status.ToString() },
                { "submittedBy", this.submittedBy }
            };
    }

}