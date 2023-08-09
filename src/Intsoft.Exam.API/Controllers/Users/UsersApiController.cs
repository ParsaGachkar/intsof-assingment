using Intsof.Exam.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Intsoft.Exam.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private IUserRepository UserRepository { get; }

    public UsersController(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        UserRepository.Create(new User(Guid.NewGuid(), createUserDto.FirstName,createUserDto.LastName,createUserDto.NationalCode,createUserDto.PhoneNumber));
        await UserRepository.SaveChangesAsync();
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await UserRepository.GetByIdAsync(id);
        return Ok(new UserDetailsDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NationalCode = user.NationalCode,
            PhoneNumber = user.PhoneNumber
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto updateUserDto)
    {
        var user = await UserRepository.GetByIdAsync(id);
        if(user == null) { 
            return NotFound(); 
        }
        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.NationalCode = updateUserDto.NationalCode;
        user.PhoneNumber = updateUserDto.PhoneNumber;
        UserRepository.Update(user);
        await UserRepository.SaveChangesAsync();
        return Ok();
    }
}