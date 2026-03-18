using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("users")] // Explicitly specify the table name in lowercase
public class Users : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("password")]
    public string Password { get; set; }
}