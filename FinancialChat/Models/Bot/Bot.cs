using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace FinancialChat.Models.Bot
{
    public class Bot : IBot
    {
        public const string STOCK_URL = "https://stooq.com/q/l/?s=*REPLACE_HERE_CODE*&f=sd2t2ohlcv&h&e=csv";
        public const string STOCK_CHAT_COMMAND = "/stock=";

        private string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }

        private List<string> SplitCSV(string code)
        {
            List<string> splitted = new List<string>();
            string url = STOCK_URL.Replace("*REPLACE_HERE_CODE*", code);
            string fileList = GetCSV(url);
            string[] tempStr;

            tempStr = fileList.Split(',');

            foreach (string item in tempStr)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    splitted.Add(item);
                }
            }
            return splitted;
        }

        public string GetQuote(string message)
        {

            if (message.Contains(STOCK_CHAT_COMMAND))
            {
                string stringAfterChar = message.Substring(message.IndexOf(STOCK_CHAT_COMMAND) + STOCK_CHAT_COMMAND.Length);
                string[] split = stringAfterChar.Split(new Char[] { ',', ' ', '\n',';' },
                                 StringSplitOptions.RemoveEmptyEntries);

                if (split.Count()>0)
                {
                    string code = split[0];
                    var result = SplitCSV(code);
                    if (result.Count() == 0)
                    {
                        return "Error: Problem gathering the data from the Stock repository";
                    }
                    if (result[9] == "N/D")
                    {
                        return "Error: Code '" + code + "' it's not valid";
                    }
                    return code.ToUpper() + " quote is: $" + result[10] + " per share";
                }
            }

            return null;
        }
    }
}