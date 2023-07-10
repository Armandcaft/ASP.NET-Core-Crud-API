namespace CrudAPI.Models
{
    /// <summary>
    /// A Product of the system.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; } // Foreign key for User
        public MyUser MyUser { get; set; } // Product's user relationship
    }
}