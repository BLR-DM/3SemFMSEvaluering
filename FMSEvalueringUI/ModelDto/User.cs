namespace FMSEvalueringUI.ModelDto;

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }

    public User(string firstName, string lastName, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }
}