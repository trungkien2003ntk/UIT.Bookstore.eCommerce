namespace KKBookstore.Application.Users.Queries.GetUserList;

public record GetUserListRequest 
{
    // some filter value:
    public string Name { get; set; }

}
