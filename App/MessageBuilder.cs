using POETestBot.Entities;
using POETestBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POETradeBot
{
    public class MessageBuilder
    {
        //flags
        private static string PoeDotTradeFlag = " Hi,";
        private static string itemCreateFlag = "\\[IC\\]";
        private static string userUpdateFlag = "\\[UU\\]";

        //Components
        private static string startPattern = $"^From (?<guild><....?.?.?>)? ?(?<user>[a-zA-Z_ÉéÍíÓóÁáÚú]*):";
        private static string itemPattern = "(?<item>[a-zA-Z'’ ]*?)";
        private static string pricePattern = "(?<amount>[0-9]*)( |,)(?<currency>[a-zA-Z]*)";
        private static string tabPositionPattern = "(?<tab>[a-zA-Z 0-9]*).*?left (?<x>[1-9][1-9]?), top (?<y>[1-9][1-9]?)";
        private static string passwordPattern = "(?<password>[0-9a-zA-Z_æøåÆØÅ\\.*^`´'_z\\/!#@¤\\>\\<~$%&\\(\\)=\\?!\\|{},\\\\ \":;|+\\/\\-]*)";

        //Compositions
        private static string buyMessagePatternEnglish = $"{startPattern}{PoeDotTradeFlag}.*? your {itemPattern} listed for {pricePattern}.*? tab {tabPositionPattern}";
        private static string itemCreatePatternEnglish = $"{startPattern} {itemCreateFlag} \\[{itemPattern}\\] price \\[{pricePattern}\\]";
        private static string userUpdatePatternEnglish = $"{startPattern} {userUpdateFlag} \\[{passwordPattern}\\]";

        //add patterns for other message types
        public static DTO ReadMessage(string message, bool _debug)
        {
            if (Regex.IsMatch(message,PoeDotTradeFlag))
            {
                if (_debug)
                    Console.WriteLine("Interpret PoeDotTrade");
                
                return interpretPoeDotTrade(message, _debug);
            }
            else if (Regex.IsMatch(message, itemCreateFlag))
            {
                if (_debug)
                    Console.WriteLine("Interpret IC");
                
                return interpretIC(message, _debug);
            }
            else if (Regex.IsMatch(message, userUpdateFlag))
            {
                if (_debug)
                    Console.WriteLine("Interpret UU");
                
                return interpretUU(message, _debug);
            }
            
            return null;
        }

        private static ItemBuyDTO interpretPoeDotTrade(string message, bool _debug)
        {
            var match = Regex.Match(message, buyMessagePatternEnglish).Groups;
            if (_debug)
            {
                Console.WriteLine("user " + match["user"].Value);
                Console.WriteLine("item " + match["item"].Value);
                Console.WriteLine("currency " + match["currency"].Value + " amount " + match["amount"].Value);
                Console.WriteLine("tab " + match["tab"].Value + " x " + match["x"].Value + " y " + match["y"].Value);
            }

            var price = (match["currency"].Value, int.Parse(match["amount"].Value));
            var position = (match["tab"].Value, int.Parse(match["x"].Value), int.Parse(match["y"].Value));

            return new ItemBuyDTO
            {
                username = match["user"].Value,
                itemname = match["item"].Value,
                price = price,
                position = position
            };
        }

        private static UserUpdateDTO interpretUU(string message, bool _debug)
        {
            var match = Regex.Match(message, userUpdatePatternEnglish).Groups;
            if (_debug)
            {
                Console.WriteLine(ComputeSha256Hash(match["password"].Value));
                Console.WriteLine(match["user"].Value);

            }

            return new UserUpdateDTO
            {
                Name = match["user"].Value,
                passwordHash = ComputeSha256Hash(match["password"].Value)
            };
        }

        private static ItemCreateDTO interpretIC(string message, bool _debug)
        {
            var match = Regex.Match(message, itemCreatePatternEnglish).Groups;
            if (_debug)
            {
                Console.WriteLine("user " + match["user"].Value);
                Console.WriteLine("item " + match["item"].Value);
                Console.WriteLine("currency " + match["currency"].Value + " amount " + match["amount"].Value);

            }

            return new ItemCreateDTO
            {
                itemname = match["item"].Value,
                username = match["user"].Value,
                price = (match["currency"].Value, int.Parse(match["amount"].Value))
            };
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
