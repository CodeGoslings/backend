namespace HACS.Models;

// This class was created for the urgent need of the donor and donation management component.
// Feel free to also utilize it (recommended).

public class User(int id, string firstName, string? secondName, string lastName, string email, string password)
{
    private int _id = id;
    private readonly string _firstName = firstName;
    private readonly string _lastName = lastName;
    private readonly string _email = email;
    private string _password = password;

    public string GetName(){return _firstName + " " + secondName + " " + _lastName;}
    public string GetEmail(){return _email;}
}