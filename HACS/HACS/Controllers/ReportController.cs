using HACS.Data;
using HACS.Helpers;
using HACS.Interfaces;
using HACS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers;

[Route("api/report")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IRepository<Donation> _donationRepository;
    private readonly IConfiguration _config;

    public ReportController(IConfiguration config, IAssignmentRepository assignmentRepository,
        IRepository<Donation> donationRepository)
    {
        _config = config;
        _assignmentRepository = assignmentRepository;
        _donationRepository = donationRepository;
    }

    [HttpGet("assignments/{year:int}")]
    public async Task<IActionResult> GetAssignmentsByYear(int year)
    {
        var ass = await _assignmentRepository.GetAllAsync();
        var report = PdfHelper.GenerateAssignmentsReport(ass, year);
        return File(report, "application/pdf", $"assignment_report_{year}.pdf");
    }
    
    [HttpGet("resources/{year:int}")]
    public async Task<IActionResult> GetResourcesByYear(int year)
    {
        var res = await _donationRepository.GetAllAsync();
        var report = PdfHelper.GenerateResourcesStatusReport(res, year);
        return File(report, "application/pdf", $"resources_report_{year}.pdf");
    }
    
    [HttpGet("individuals")]
    public async Task<IActionResult> GetIndividuals(int year)
    {
        var ind = new List<Individual>();
        var report = PdfHelper.GenerateAffectedIndividualsReport(ind);
        return File(report, "application/pdf", $"individuals_report_{year}.pdf");
    }
}