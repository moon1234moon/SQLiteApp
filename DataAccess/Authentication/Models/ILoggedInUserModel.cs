namespace DataAccess.Authentication.Models
{
    public interface ILoggedInUserModel
    {
        int Id { get; set; }
        string Token { get; set; }
    }
}