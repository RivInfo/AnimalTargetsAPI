using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBDatas;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(FirstName)), Index(nameof(LastName))]
public class User
{
    [Column("id"), Key]
    public long Id { get; set; }
    [Column("firstName"), Required]
    public string FirstName { get; set; }
    [Column("lastName"), Required]
    public string LastName { get; set; }
    [Column("email"), Required]
    public string Email { get; set; }
    [Column("password"), Required]
    public string Password { get; set; }
}