using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HACS.Models.DonorManagement;

public class User 
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    protected User() {}
    
    protected User(string firstName, string lastName, string email, string passwordHash, string? middleName = null, Guid id = default)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
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