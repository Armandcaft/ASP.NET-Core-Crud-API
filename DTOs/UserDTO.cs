namespace CrudAPI.DTOs
{
    public class MyUserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}