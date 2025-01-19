using System.Globalization;
using HACS.Interfaces;
using HACS.Models;
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
        var total = 0.0;
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
}