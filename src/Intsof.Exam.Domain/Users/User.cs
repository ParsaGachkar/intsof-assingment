using DNTPersianUtils.Core;

namespace Intsof.Exam.Domain.Users;

public class User
{
    public User(Guid id,string firstName, string lastName, string nationalCode, string phoneNumber)
    {
        if (id == default)
        {
            throw new ArgumentException("Id Needs to be Assigned First",paramName: nameof(id));
        }
        Id = id;
        FirstName = firstName;
        if (string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException("LastName Cannot be Empty or Null",paramName: nameof(lastName));
        }
        LastName = lastName;
        NationalCode = nationalCode;
        PhoneNumber = phoneNumber;
        if (!NationalCode.IsValidIranianNationalCode())
        {
            NationalCode = nationalCode;    
        }
        if (!PhoneNumber.IsValidIranianPhoneNumber())
        {
            PhoneNumber = phoneNumber;   
        }
        
    }
    // todo fix this warning
    protected User()
    {
        
    }
    public Guid Id { get; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
}