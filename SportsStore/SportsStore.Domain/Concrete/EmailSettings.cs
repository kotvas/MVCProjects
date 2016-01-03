using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public String MailToAddress = "orders@example.com";
        public String MailFromAddress = "sportsstore@example.com";
        public Boolean UseSsl = true;
        public String Username = "MySmtpUsername";
        public String Password = "MySmtpPassword";
        public String ServerName = "smtp.example.com";
        public Int32 ServerPort = 587;
        public Boolean WriteAsFile = true;
        public String FileLocation = @"c:\sports_store_emails";
    }
}
