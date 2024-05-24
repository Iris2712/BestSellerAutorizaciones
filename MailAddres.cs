using System.Net.Mail;

namespace BestSellerAutorizaciones
{
    internal class MailAddres : MailAddress
    {
        public MailAddres(string address) : base(address)
        {
        }
    }
}