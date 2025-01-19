using System.Globalization;
using HACS.Interfaces;
using HACS.Models;
using MRCModel.Models;
using Newtonsoft.Json.Serialization;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace HACS.Helpers;

public static class PdfHelper
{
    public static async Task<MemoryStream> GenerateDonationConfirmation(Donation donation, IRepository<Donor> donorRepository)
    {
        using var pdfDocumentBuilder = new PdfDocumentBuilder();
        var page = pdfDocumentBuilder.AddPage(PageSize.A4);

        var font = pdfDocumentBuilder.AddStandard14Font(Standard14Font.Courier);
        
        //Add user email, name and surname here
        var donor = (await donorRepository.GetAllAsync()).FirstOrDefault(x => x.DonationHistory.Contains(donation));
        if (donor == null) throw new Exception("Something went wrong");
        page.AddText($"{donor.GetFullName()}, {donor.Email}", 10, new PdfPoint(250, 750), font);

        page.AddText("Your donation confirmation", 16, new PdfPoint(50, 730), font);
        page.DrawLine(new PdfPoint(40, 60), new PdfPoint(40, 710), 1D);
        
        page.AddText($"Donation date: {donation.Date.ToString(CultureInfo.InvariantCulture)}", 12, new PdfPoint(50, 690), font);
        page.AddText($"Donation type: {donation.Type}", 12, new PdfPoint(50, 670), font);
        page.AddText(
            donation.Type == DonationType.Financial
                ? $"Donation amount: {donation.Amount} Bitcoins"
                : $"Drop off/pick up location: {donation.Location}", 12, new PdfPoint(50, 650), font);
        
        var stream = new MemoryStream(pdfDocumentBuilder.Build());

        return stream;
    }
    
    public static MemoryStream GenerateDonationsReport(List<Donation> donations, int year)
    {
        using var pdfDocumentBuilder = new PdfDocumentBuilder();
        var page = pdfDocumentBuilder.AddPage(PageSize.A4);

        var font = pdfDocumentBuilder.AddStandard14Font(Standard14Font.Courier);
        
        page.AddText("Donations report", 16, new PdfPoint(50, 750), font);
        page.DrawLine(new PdfPoint(40, 740), new PdfPoint(40, 740), 1D);

        var yPosition = 730; // Start just below the header
        var number = 0;
        double? total = 0.0;
        foreach (var donation in donations.Where(donation => donation.Date.Year == year))
        {
            if (yPosition < 50) // If we reach the bottom of the page, add a new one
            {
                page = pdfDocumentBuilder.AddPage(595, 842);
                yPosition = 800;
            }

            page.AddText($"{number}.", 12, new PdfPoint(50, yPosition), font);
            page.AddText(donation.Type == DonationType.Financial
                ? donation.Amount + " Bitcoins"
                : "Material donation", 12, new PdfPoint(250, yPosition), font);
            page.AddText(donation.Date.ToShortDateString(), 12, new PdfPoint(400, yPosition), font);

            yPosition -= 20; // Move down for the next row
            number++;
            if (donation.Type == DonationType.Financial) total += donation.Amount;
        }

        
        page.AddText($"Total amount of financial donations: {total}", 12, new PdfPoint(50, 80), font);
        
        var stream = new MemoryStream(pdfDocumentBuilder.Build());

        return stream;
    }

    public static MemoryStream GenerateAssignmentsReport(List<Assignment> assignments, int yearDue)
    {
        using var pdfDocumentBuilder = new PdfDocumentBuilder();
        var page = pdfDocumentBuilder.AddPage(PageSize.A4);

        var font = pdfDocumentBuilder.AddStandard14Font(Standard14Font.Courier);
        
        page.AddText("Assignments currently in progress (aid activities):", 16, new PdfPoint(50, 750), font);
        page.DrawLine(new PdfPoint(40, 740), new PdfPoint(40, 740), 1D);
        
        var yPosition = 730;
        var number = 0;
        foreach (var assignment in assignments.Where(assignment => assignment.Status == AssignmentStatus.InProgress))
        {
            if (yPosition < 50)
            {
                page = pdfDocumentBuilder.AddPage(595, 842);
                yPosition = 800;
            }
            
            page.AddText($"{number}.", 12, new PdfPoint(50, yPosition), font);
            
            page.AddText($"{assignment.Volunteer.LastName}, {assignment.Volunteer.FirstName}", 10, new PdfPoint(100, yPosition), font);
            page.AddText($"{assignment.DueDate.ToString(CultureInfo.InvariantCulture)}", 10, new PdfPoint(300, yPosition), font);
            page.AddText($"{assignment.Description[..150]}", 10, new PdfPoint(350, yPosition), font);
            
            yPosition -= 20;
            number++;
        }
        
        page = pdfDocumentBuilder.AddPage(595, 842);
        
        page.AddText($"Total number of assignments in progress: {number}", 16, new PdfPoint(50, 750), font);
        
        page = pdfDocumentBuilder.AddPage(595, 842);
        
        page.AddText("Inactive assignments:", 16, new PdfPoint(50, 750), font);
        page.DrawLine(new PdfPoint(40, 740), new PdfPoint(40, 740), 1D);
        
        var yPosition2 = 730;
        var number2 = 0;
        foreach (var assignment in assignments.Where(assignment => assignment.Status != AssignmentStatus.InProgress && assignment.DueDate.Year == yearDue))
        {
            if (yPosition2 < 50)
            {
                page = pdfDocumentBuilder.AddPage(595, 842);
                yPosition2 = 800;
            }
            
            page.AddText($"{number2}.", 12, new PdfPoint(50, yPosition), font);
            
            page.AddText($"{assignment.Volunteer.LastName}, {assignment.Volunteer.FirstName}", 10, new PdfPoint(100, yPosition), font);
            page.AddText($"{assignment.Status.ToString()}", 10, new PdfPoint(300, yPosition), font);
            page.AddText($"{assignment.Description.Substring(0, 150)}", 10, new PdfPoint(350, yPosition), font);
            
            yPosition2 -= 20;
            number2++;
        }
        
        page = pdfDocumentBuilder.AddPage(595, 842);
        
        page.AddText($"Total number of inactive assignments: {number2}", 16, new PdfPoint(50, 750), font);

        var stream = new MemoryStream(pdfDocumentBuilder.Build());

        return stream;
    }

    public static MemoryStream GenerateResourcesStatusReport(List<Donation> allDonations, int year)
    {
        List<Donation> donations = new List<Donation>();
        
        foreach (var donation in allDonations.Where(donation => donation.Date.Year == year))
        {
            donations.Add(donation);
        }
        
        using var pdfDocumentBuilder = new PdfDocumentBuilder();
        var page = pdfDocumentBuilder.AddPage(PageSize.A4);

        var font = pdfDocumentBuilder.AddStandard14Font(Standard14Font.Courier);
        
        page.AddText("Finalized material donations:", 16, new PdfPoint(50, 750), font);
        page.DrawLine(new PdfPoint(40, 740), new PdfPoint(40, 740), 1D);

        var yPosition = 730;
        var number = 0;
        foreach (var donation in donations.Where(donation => donation.Type == DonationType.Material && donation.Status != DonationStatus.Pending))
        {
            if (yPosition < 50)
            {
                page = pdfDocumentBuilder.AddPage(595, 842);
                yPosition = 800;
            }
            
            page.AddText($"{number}. {donation.Description[..50]}", 10, new PdfPoint(50, yPosition), font);
            page.AddText($"{donation.Location}, {donation.Status}", 10, new PdfPoint(200, yPosition), font);
            
            yPosition -= 20;
            number++;
        }
        
        page = pdfDocumentBuilder.AddPage(595, 842);
        
        page.AddText($"Total number of finalized material donations: {number}", 16, new PdfPoint(50, 750), font);
        
        page = pdfDocumentBuilder.AddPage(595, 842);
        page.AddText("Material donations waiting for collection:", 16, new PdfPoint(50, 750), font);
        page.DrawLine(new PdfPoint(40, 740), new PdfPoint(40, 740), 1D);
        
        var yPosition2 = 730;
        var number2 = 0;
        foreach (var donation in donations.Where(donation => donation.Type == DonationType.Material && donation.Status != DonationStatus.Pending))
        {
            if (yPosition2 < 50)
            {
                page = pdfDocumentBuilder.AddPage(595, 842);
                yPosition2 = 800;
            }
            
            page.AddText($"{number2}. {donation.Description[..50]}", 10, new PdfPoint(50, yPosition2), font);
            page.AddText($"{donation.Location}, submitted: {donation.Date.ToString((CultureInfo.InvariantCulture))}", 10, new PdfPoint(200, yPosition2), font);
            
            yPosition2 -= 20;
            number2++;
        }
        
        page = pdfDocumentBuilder.AddPage(595, 842);
        
        page.AddText($"Total number of pending material donations: {number2}", 16, new PdfPoint(50, 750), font);

        double? received = 0.0;
        double? waiting = 0.0;
        foreach (var donation in donations.Where(donation => donation.Type == DonationType.Financial))
        {
            switch (donation.Status)
            {
                case DonationStatus.Accepted:
                    received += donation.Amount;
                    break;
                case DonationStatus.Pending:
                    waiting += donation.Amount;
                    break;
                case DonationStatus.Declined:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        page = pdfDocumentBuilder.AddPage(595, 842);
        
        page.AddText($"Total amount of financial donations received: {received}", 16, new PdfPoint(50, 750), font);
        page.AddText($"Total amount of financial donations waiting: {waiting}", 16, new PdfPoint(50, 750), font);
        
        var stream = new MemoryStream(pdfDocumentBuilder.Build());

        return stream;
    }

    public static MemoryStream GenerateAffectedIndividualsReport(List<AffectedIndividual> affectedIndividuals)
    {
        using var pdfDocumentBuilder = new PdfDocumentBuilder();
        var page = pdfDocumentBuilder.AddPage(PageSize.A4);

        var font = pdfDocumentBuilder.AddStandard14Font(Standard14Font.Courier);
        
        page.AddText("Affected individuals:", 16, new PdfPoint(50, 750), font);

        var yPosition = 730;
        var number = 0;
        foreach (var individual in affectedIndividuals)
        {
            if (yPosition < 50)
            {
                page = pdfDocumentBuilder.AddPage(595, 842);
                yPosition = 800;
            }
            
            page.AddText($"{number}. {individual.Name}", 10, new PdfPoint(50, yPosition), font);
            page.AddText($"{individual.userLocation}", 10, new PdfPoint(200, yPosition), font);
            
            yPosition -= 20;
            number++;
        }
        
        page.DrawLine(new PdfPoint(40, 740), new PdfPoint(40, 740), 1D);
        var stream = new MemoryStream(pdfDocumentBuilder.Build());

        return stream;
    }
}