using System.ComponentModel.DataAnnotations;

namespace AnimalTargetUsers.UserData;

public class UserRequest
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress] //[RegularExpression(".+\\@.+\\..+")]//.+\@.+\..+   .*@.*\\..*
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}