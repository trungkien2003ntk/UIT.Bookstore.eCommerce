namespace KKBookstore.Application.Common.Interfaces; 

public interface IBookstoreClientService
{ 
    string ConstructPasswordResetLink(string token);
}