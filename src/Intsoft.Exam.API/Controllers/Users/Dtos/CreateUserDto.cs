using System.ComponentModel.DataAnnotations;
using DNTPersianUtils.Core;

namespace Intsoft.Exam.API.Controllers;

public class CreateUserDto
{
    public string? FirstName { get; set; } = null;
    [Required]
    public string LastName { get; set; } = "";
    [Required]
    [ValidIranianNationalCode]
    public string NationalCode { get; set; }= "";
    [Required]
    [ValidIranianMobileNumber]
    public string PhoneNumber { get; set; }= "";
}