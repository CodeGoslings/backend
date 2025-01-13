// See https://aka.ms/new-console-template for more information

using MRCService;
using MRCModel;

Console.WriteLine("Hello, World!");

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
Console.WriteLine(jsonObject.ToJsonString());