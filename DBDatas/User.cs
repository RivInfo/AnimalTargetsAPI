using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBDatas;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(FirstName)), Index(nameof(LastName))]
public class User
{
    [Column("id")]
    public long Id { get; set; }
    [Column("firstName")]
    public string FirstName { get; set; }
    [Column("lastName")]
    public string LastName { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; }
}