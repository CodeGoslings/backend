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
    public string? SecondName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    protected User() {}
    
    protected User(string firstName, string lastName, string email, string password, string? secondName = null, Guid id = new())
    {
        FirstName = firstName;
        SecondName = secondName;
        LastName = lastName;
        Email = email;
        Password = password;
        Id = id;
    }
    public string GetFullName()
    {
        var fullName = $"{FirstName}";
        if (SecondName is not null) fullName += $" {SecondName}";
        fullName += $" {LastName}";
        return fullName;
    }
}