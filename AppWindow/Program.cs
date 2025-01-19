using MRCService;
using System.ComponentModel;
using System.Text.Json.Nodes;
using MRCModel.Models;

Console.WriteLine("For demonstration purposes");

Request req = new Request(
    requestId: 1,
    type: "urgent",
    description: "Fix server issue",
    priority: "high",
    location: "Gibraltar",
    submissionDate: DateTime.Now,
    status: RequestStatus.Pending,
    submittedBy: "Johan"
);

var jsonObject = req.ToJsonObject();
// Console.WriteLine(jsonObject.ToJsonString());

Console.WriteLine("Creating two requests and fetching them");

IMRCService service = IMRCService.createService();

service.createRequest(1, "type", "description", "priority", "location", DateTime.Now, "Pending", "Anna");
service.createRequest(2, "type", "description", "priority", "location", DateTime.Now, "Pending", "Jeremi");

JsonArray requests = service.viewRequests();

foreach (var request in requests)
{
    Console.WriteLine(request.ToJsonString(new System.Text.Json.JsonSerializerOptions
    {
        WriteIndented = true
    }));
}

Console.WriteLine("Changing request status of request 1 to Denied");

service.updateRequestStatus(1, "Denied");

requests = service.viewRequests();

foreach (var request in requests)
{
    Console.WriteLine(request.ToJsonString(new System.Text.Json.JsonSerializerOptions
    {
        WriteIndented = true
    }));
}

Console.WriteLine("Fetching request information of request 2");

requests = service.viewRequestDetails(2);
foreach (var request in requests)
{
    Console.WriteLine(request.ToJsonString(new System.Text.Json.JsonSerializerOptions
    {
        WriteIndented = true
    }));
}
