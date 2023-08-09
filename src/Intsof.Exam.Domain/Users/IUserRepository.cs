namespace Intsof.Exam.Domain.Users;

public interface IUserRepository
{
    void Create(User user);
    Task<User> GetByIdAsync(Guid id);
    Task<int> SaveChangesAsync();
    void Update(User user);
}