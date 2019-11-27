using POETestBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POETestBot
{
    class MessageBuilder
    {
        private static string buyMessagePatternEnglish = $"^@From (?<user>[a-zA-Z]*): Hi, .*?buy your (?<item>[a-zA-Z' \n]*)? listed for (?<price>(?<amount>[1-9]*) (?<currency>[a-zA-Z]*))";
        //add patterns for other message types
        public static Message ReadMessage(string message)
        {
            Console.WriteLine(buyMessagePatternEnglish);
            var output = Regex.Match(message, buyMessagePatternEnglish);
            if(output.Success)
            {
                Console.WriteLine("aww yeah");
                Console.WriteLine(output.Groups["user"].Value);
                Console.WriteLine(output.Groups["item"].Value);
                Console.WriteLine(output.Groups["price"].Value);

            }
            else
            {
                Console.WriteLine("fuuuck");
            }
            
            return null;
        }
    }
}
