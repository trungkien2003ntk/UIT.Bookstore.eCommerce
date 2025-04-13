using KKBookstore.Domain.Users;

namespace KKBookstore.Staffs;

public class Staff : User
{
    public string Description { get; set; } = null!;
}