using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArquitectura.Infrastructure.Email
{
    public class GmailSettings
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int Port { get; set; }

    }
}
