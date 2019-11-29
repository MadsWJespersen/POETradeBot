using POETestBot.Entities;
using POETestBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POETradeBot
{
    public class MessageBuilder
    {
        private static string startPattern  = $"^From {userPattern}:";
        private static string userPattern = "(?< user>[a-zA-Z]*)";
        private static string PoeDotTradeFlag = " Hi,";
        private static string itemCreateFlag = " \\[IC\\] ";
        private static string userCreateFlag = " \\[UC\\] ";
        private static string fill = ".*?";
        private static string itemPattern = "(?<item>[a-zA-Z' ]*?)";
        private static string pricePattern = "(?<price>(?<amount>[0-9]*)( |,)(?<currency>[a-zA-Z]*))";
        private static string tabPattern = "(?<tab>[a-zA-Z 0-9]*)";
        private static string tabPositionPattern = "(?<position>left [1-9][1-9]?, top [1-9][1-9]?)";
        

        private static string buyMessagePatternEnglish = $"{startPattern}{PoeDotTradeFlag}{fill}buy your {itemPattern} listed for {pricePattern}{fill} tab {tabPattern}{fill}{tabPositionPattern}";
        private static string itemCreatePatternEnglish = $"{startPattern}{itemCreateFlag}\\[{itemPattern}\\] price \\[{pricePattern}\\]";

        //add patterns for other message types
        public static DTO ReadMessage(string message)
        {
            if (message.Contains(PoeDotTradeFlag))
            {
                return interpretPoeDotTrade(message);
            }

            if (message.Contains(itemCreateFlag))
            {
                return interpretIC(message);
            }else
            if (message.Contains(userCreateFlag))
            {
                return interpretUC(message);
            }
            
            return null;
        }

        private static ItemBuyDTO interpretPoeDotTrade(string message)
        {
            throw new NotImplementedException();
        }

        private static UserCreateDTO interpretUC(string message)
        {
            return new UserCreateDTO
            {

            }
        }

        private static ItemCreateDTO interpretIC(string message)
        {
            throw new NotImplementedException();
        }
    }
}
