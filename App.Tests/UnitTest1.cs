using POETestBot.Models;
using POETradeBot;
using System;
using Xunit;

namespace App.Tests
{
    public class PoeDotTrade
    {
        [Fact]
        public void PoeDotTradeTest1()
        {
            
            //Arrange
            var input = "From <Gif+1> Baroox: Hi, | would like to buy your Lion's Roar Granite Flask listed for 1 gcp in Standard (stash tab 8; position: left 5, top 4)";
            
            //Act
            ItemBuyDTO output = (ItemBuyDTO) MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("Lion's Roar Granite Flask", output.itemname);
            Assert.Equal("Baroox", output.username);
            Assert.Equal(("gcp", 1), output.price);
            Assert.Equal(("8", 5, 4), output.position);
        }

        [Fact]
        public void PoeDotTradeTest_given_two_digit_position()
        {

            //Arrange
            var input = "From Ostaddd: Hi, I would like to buy your Quecholli Jagged Maul listed for 1 jewellers in Standard (stash tab hello my gamers42069; position: left 15, top 22";

            //Act
            ItemBuyDTO output = (ItemBuyDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("Quecholli Jagged Maul", output.itemname);
            Assert.Equal("Ostaddd", output.username);
            Assert.Equal(("jewellers", 1), output.price);
            Assert.Equal(("hello my gamers42069", 15, 22), output.position);
        }

        [Fact]
        public void PoeDotTradeTest_with_six_digit_guild()
        {

            //Arrange
            var input = "From <123456> Poozdrawiam: Hi, I would like to buy your Quecholli Jagged Maul listed for 1 jewellers in Standard (stash tab hello my gamers42069; position: left 15, top 22";

            //Act
            ItemBuyDTO output = (ItemBuyDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("Quecholli Jagged Maul", output.itemname);
            Assert.Equal("Poozdrawiam", output.username);
            Assert.Equal(("jewellers", 1), output.price);
            Assert.Equal(("hello my gamers42069", 15, 22), output.position);
        }

        [Fact]
        public void PoeDotTradeTest_underscore_in_username()
        {

            //Arrange
            var input = "From Bonobo_Bukkake: Hi, I would like to buy your Quecholli Jagged Maul listed for 1 jewellers in Standard (stash tab hello my gamers42069; position: left 15, top 22";

            //Act
            ItemBuyDTO output = (ItemBuyDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("Quecholli Jagged Maul", output.itemname);
            Assert.Equal("Bonobo_Bukkake", output.username);
            Assert.Equal(("jewellers", 1), output.price);
            Assert.Equal(("hello my gamers42069", 15, 22), output.position);
        }

        [Fact]
        public void PoeDotTradeTest_with_accents_in_username()
        {

            //Arrange
            var input = "From …ÈlloÕÌ”Û¡·⁄˙: Hi, I would like to buy your Quecholli Jagged Maul listed for 1 jewellers in Standard (stash tab hello my gamers42069; position: left 15, top 22";

            //Act
            ItemBuyDTO output = (ItemBuyDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("Quecholli Jagged Maul", output.itemname);
            Assert.Equal("…ÈlloÕÌ”Û¡·⁄˙", output.username);
            Assert.Equal(("jewellers", 1), output.price);
            Assert.Equal(("hello my gamers42069", 15, 22), output.position);
        }

        [Fact]
        public void PoeDotTradeTest_fails_given_guildname_with_2_digits()
        {
            //Arrange
            var input = "From <12> Poozdrawiam: Hi, I would like to buy your Quecholli Jagged Maul listed for 1 jewellers in Standard (stash tab hello my gamers42069; position: left 15, top 22";
          
            //Act
            Exception ex = Assert.Throws<FormatException>(() => MessageBuilder.ReadMessage(input,false));

            //Assert
            Assert.Equal("Input string was not in a correct format.", ex.Message);

        }

    }

    public class ItemCreate
    {
        [Fact]
        public void ItemCreateTest1()
        {

            //Arrange
            var input = "From <69420> TimeDownTheDrain: [IC] [Tabula Rasa Simple Robe] price [1 Exalted]";

            //Act
            ItemCreateDTO output = (ItemCreateDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("Tabula Rasa Simple Robe", output.itemname);
            Assert.Equal("TimeDownTheDrain", output.username);
            Assert.Equal(("Exalted", 1), output.price);
            
        }

        [Fact]
        public void ItemCreateTest1_given_large_amount()
        {

            //Arrange
            var input = "From <69420> TimeDownTheDrain: [IC] [Tabula Rasa Simple Robe] price [100 Exalted]";

            //Act
            ItemCreateDTO output = (ItemCreateDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("Tabula Rasa Simple Robe", output.itemname);
            Assert.Equal("TimeDownTheDrain", output.username);
            Assert.Equal(("Exalted", 100), output.price);

        }
    }

    public class UserUpdate
    {
        [Fact]
        public void UserUpdateTest1()
        {

            //Arrange
            var input = "From <69420> TimeDownTheDrain: [UU] [root]";

            //Act
            UserUpdateDTO output = (UserUpdateDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("TimeDownTheDrain", output.Name);
            Assert.Equal(MessageBuilder.ComputeSha256Hash("root"), output.passwordHash);

        }

        [Fact]
        public void UserUpdateTest1_given_weird_password()
        {

            //Arrange
            var input = "From <69420> TimeDownTheDrain: [UU] [jo8sfR5oocELzMk8$3NrETe#WKjQzFJItqIcm37P6d#J5gB7$l^@KdXhvbz2eEiS4#^5fQ&CH%hw6#f&a]";

            //Act
            UserUpdateDTO output = (UserUpdateDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("TimeDownTheDrain", output.Name);
            Assert.Equal(MessageBuilder.ComputeSha256Hash("jo8sfR5oocELzMk8$3NrETe#WKjQzFJItqIcm37P6d#J5gB7$l^@KdXhvbz2eEiS4#^5fQ&CH%hw6#f&a"), output.passwordHash);


        }

        [Fact]
        public void UserUpdateTest1_given_even_weirder_password()
        {

            //Arrange
            var input = "From <69420> TimeDownTheDrain: [UU] [8MSos78MSS\"-- > a\";w-lp3link levD ati19 -o47;cms&#AW&#AW&#AW& ganten er e7fsgs/M;cms&#A0 07:01:40 CET 2019 --> wGfY\" g ET20reshos78MSS\"ps:2ljulgeneric-309x530#47mpuas-babyluenul cC30#4-twoi Dec 01 02:18:43 CET 2019 --> wGfY\" g ET 20reshos78MSS\"p3lig>h¯rhingo@Nov 30 07:01: ter]";

            //Act
            UserUpdateDTO output = (UserUpdateDTO)MessageBuilder.ReadMessage(input, false);

            //Assert
            Assert.Equal("TimeDownTheDrain", output.Name);
            Assert.Equal(MessageBuilder.ComputeSha256Hash("8MSos78MSS\"-- > a\";w-lp3link levD ati19 -o47;cms&#AW&#AW&#AW& ganten er e7fsgs/M;cms&#A0 07:01:40 CET 2019 --> wGfY\" g ET20reshos78MSS\"ps:2ljulgeneric-309x530#47mpuas-babyluenul cC30#4-twoi Dec 01 02:18:43 CET 2019 --> wGfY\" g ET 20reshos78MSS\"p3lig>h¯rhingo@Nov 30 07:01: ter"), output.passwordHash);


        }
    }

}
