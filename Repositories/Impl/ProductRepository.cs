using AutoMapper;
using CrudAPI.Models;
using CrudAPI.Repositories;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(ProductDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    public bool AnyProducts()
    {
        return _dbContext.Products.Any();
    }

    public Product GetProductById(int id)
    {
        return _dbContext.Set<Product>().Find(id);
    }

    public List<Product> GetProductsByUserId(int userId)
    {
        return _dbContext.Set<Product>().Where(p => p.UserId == userId).ToList();
    }

    public void AddProduct(Product product)
    {
        _dbContext.Set<Product>().Add(product);
        _dbContext.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _dbContext.Set<Product>().Update(product);
        _dbContext.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var product = GetProductById(id);
        if (product != null)
        {
            _dbContext.Set<Product>().Remove(product);
            _dbContext.SaveChanges();
        }
    }

    public object GetAllProducts()
    {
        throw new NotImplementedException();
    }
}