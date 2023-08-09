namespace Intsoft.Exam.API.Controllers;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; } = null;
    public string LastName { get; set; } = "";
    public string NationalCode { get; set; }= "";
    public string PhoneNumber { get; set; }= "";
}