namespace Intsof.Exam.Domain.Users;

public interface IUserRepository
{
    void Create(User user);
    Task<User> GetByIdAsync(Guid id);
    void Update(User user);
}