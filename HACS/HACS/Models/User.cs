using Microsoft.AspNetCore.Identity;

namespace HACS.Models;

public class User : IdentityUser
{
    protected User()
    {
    }

    protected User(string firstName, string lastName, string username, string email, string? middleName = null,
        Guid id = default)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        UserName = username;
        Email = email;
        Id = id.ToString();
    }

    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }

    public string GetFullName()
    {
        var fullName = $"{FirstName}";
        if (MiddleName is not null) fullName += $" {MiddleName}";
        fullName += $" {LastName}";
        return fullName;
    }
}