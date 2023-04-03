using System.ComponentModel.DataAnnotations.Schema;

namespace DBDatas;

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