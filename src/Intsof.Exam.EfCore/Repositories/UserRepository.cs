using Intsof.Exam.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Intsof.Exam.EfCore.Repositories;

public class UserRepository:IUserRepository
{
    public DbSet<User> Users { get; }

    public UserRepository(DbSet<User> users)
    {
        Users = users;
    }
    public async void Create(User user)
    {
        await Users.AddAsync(user);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await Users.FindAsync(id) ?? throw new Exception("User Not Found");
    }

    public void Update(User user)
    {
        Users.Update(user);
    }
}