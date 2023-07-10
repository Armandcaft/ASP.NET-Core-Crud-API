using AutoMapper;
using CrudAPI.Models;
using CrudAPI.Repositories;
using Microsoft.EntityFrameworkCore;

public class MyUserRepository : IMyUserRepository
{
    private readonly ProductDbContext _dbContext;
    private readonly IMapper _mapper;

    public MyUserRepository(ProductDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    public bool AnyUsers()
    {
        return _dbContext.Users.Any();
    }

    public MyUser GetUserById(int id)
    {
        return _dbContext.Set<MyUser>().Find(id);
    }

    public MyUser GetUserByEmail(string email)
    {
        return _dbContext.Set<MyUser>().FirstOrDefault(u => u.Email == email);
    }

    public void AddUser(MyUser user)
    {
        _dbContext.Set<MyUser>().Add(user);
        _dbContext.SaveChanges();
    }

    public void UpdateUser(MyUser user)
    {
        _dbContext.Set<MyUser>().Update(user);
        _dbContext.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            _dbContext.Set<MyUser>().Remove(user);
            _dbContext.SaveChanges();
        }
    }

    public object GetAllUsers()
    {
        throw new NotImplementedException();
    }
}
