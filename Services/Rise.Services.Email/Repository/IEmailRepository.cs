using Rise.Services.Email.Messages;

namespace Rise.Services.Email.Repository
{
    public interface IEmailRepository
    {
        //Task SendAndLogEmail(UpdatePaymentResultMessage message);
        void SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
