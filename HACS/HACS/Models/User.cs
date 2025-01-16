using System.ComponentModel.DataAnnotations;

namespace HACS.Models;

public class User
{
    protected User()
    {
    }

    protected User(string firstName, string lastName, string email, string passwordHash, string? middleName = null,
        Guid id = default)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Id = id;
    }

    [Key] public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public string GetFullName()
    {
        var fullName = $"{FirstName}";
        if (MiddleName is not null) fullName += $" {MiddleName}";
        fullName += $" {LastName}";
        return fullName;
    }
}