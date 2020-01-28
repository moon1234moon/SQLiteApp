namespace DataAccess.Authentication.DataAccess
{
    public interface IAuthenticationEndpoint
    {
        void Login(string username, string password);
    }
}