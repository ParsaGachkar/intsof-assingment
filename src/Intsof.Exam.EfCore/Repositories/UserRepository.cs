using Intsof.Exam.Domain.Users;
using Intsof.Exam.EfCore.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Intsof.Exam.EfCore.Repositories;

public class UserRepository:IUserRepository
{
    private DbSet<User> Users { get; }
    private AppDbContext AppDbContext { get; }

    public UserRepository(AppDbContext appDbContext)
    {
        Users = appDbContext.Users;
        AppDbContext = appDbContext;
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
    public async Task<int> SaveChangesAsync()
    {
        return await AppDbContext.SaveChangesAsync();
    }
}