namespace KKBookstore.Common.Interfaces; 

public interface IBookstoreClientService
{ 
    string ConstructPasswordResetLink(string token);
}