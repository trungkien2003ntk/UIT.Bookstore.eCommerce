using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Staffs;

public class Staff : User
{
    public string Description { get; set; } = null!;
}