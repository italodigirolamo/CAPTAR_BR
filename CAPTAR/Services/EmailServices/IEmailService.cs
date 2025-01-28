using CAPTAR.DTO;

namespace CAPTAR.Services.EmailServices
{
    public interface IEmailService
    {
       Task SendEmail(EmailDto email);
         //void SendEmail(EmailDto email);
    }
}