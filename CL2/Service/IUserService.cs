namespace CL2.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}