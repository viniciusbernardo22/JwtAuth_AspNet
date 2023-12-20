namespace JwtAspNet.Models
{
    public record User(int Id,
        string Email, 
        string Name, 
        string Password, 
        string[] Roles, 
        string Image);
  
}
