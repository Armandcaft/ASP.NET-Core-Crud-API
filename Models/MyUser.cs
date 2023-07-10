namespace CrudAPI.Models
{
    /// <summary>
    /// A user of the system.
    /// </summary>
    public class MyUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Store hashed password
        public DateTime CreatedAt { get; set; }
        public List<Product> Products { get; set; } // User's products relationship
    }
}