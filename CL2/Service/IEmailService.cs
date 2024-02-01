using CL2.Models;
using System.Threading.Tasks;

namespace CL2.Service
{
    public interface IEmailService
    {
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
        //Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}