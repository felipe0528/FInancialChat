using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Models.Bot
{
    public interface IBot
    {
        string GetQuote(string message);
    }
}
