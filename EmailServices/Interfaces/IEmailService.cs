using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailServices.EmailDomain;

namespace EmailServices.Interfaces
{
    public enum EmailType { Text, Html}
    public interface IEmailService
    {
        void SendEmail(TicketMasterEmailMessage message);
        EmailType EmailType { get; set; }
    }
}
