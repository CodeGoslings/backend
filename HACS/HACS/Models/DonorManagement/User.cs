using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HACS.Models.DonorManagement;

// This class was created for the urgent need of the donor and donation management component.
// Feel free to also utilize it (recommended).

public class User 
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    protected User() {}
    
    protected User(string firstName, string lastName, string email, string password, string? middleName = null, Guid id = default)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Password = password;
        Id = id;
    }
    public string GetFullName()
    {
        var fullName = $"{FirstName}";
        if (MiddleName is not null) fullName += $" {MiddleName}";
        fullName += $" {LastName}";
        return fullName;
    }
}