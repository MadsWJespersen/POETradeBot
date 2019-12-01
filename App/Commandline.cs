using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Tesseract;
using System.Runtime.InteropServices;
using System.Windows;
using POETestBot;

namespace POETradeBot
{
    class CommandLine
    {
        public readonly bool _debug;
        private readonly Reader _reader;
        private readonly Rectangle _chatBoxBounds;
        private readonly Actor _actor;

        public CommandLine(Reader reader, Rectangle chatBoxBounds,Rectangle tradeBoxBounds, bool debug)
        {
            this._reader = reader;
            this._chatBoxBounds = chatBoxBounds;
            this._actor = new Actor (tradeBoxBounds,Point.Empty);
            this._debug = debug;
        }
        [STAThread]
        public void listenChat()
        {
            while (true)
            {
                using (Bitmap bmp = new Bitmap(_chatBoxBounds.Width, _chatBoxBounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(new Point(_chatBoxBounds.Left, _chatBoxBounds.Top), Point.Empty, bmp.Size, CopyPixelOperation.SourceCopy);
                        using (Page page = _reader.GetTesseract().Process(bmp))
                        {

                            if (_debug)
                            {
                                bmp.Save("currentImage.png", System.Drawing.Imaging.ImageFormat.Png);
                                Console.ReadLine();
                            }

                            Console.WriteLine(page.GetText());
                            var text = page.GetText().Replace("\n", " ");
                            Console.WriteLine(text);
                            Console.WriteLine(text.Replace("\"", ""));
                            var textAfter = new StringBuilder();
                            for (int i = 0; i < text.Length; i++)
                            {
                                char character = text[i];
                                if (character != '”')
                                {
                                    textAfter.Append(text[i]);
                                }
                            }
                            Console.WriteLine(textAfter.ToString());
                            var arr = textAfter.ToString().Split('@');
                            foreach (string message in arr)
                            {
                                Console.WriteLine(message);
                                Console.WriteLine(MessageBuilder.ReadMessage(message, _debug));
                            }
                        }

                    }

                }

                var bong = _actor.FindTradeItem();
                foreach(var thing in bong)
                {
                    Console.WriteLine(thing);
                }
                Thread.Sleep(300);
            }
        }

        public static double GetWindowsScaling()
        {
            return Screen.PrimaryScreen.Bounds.Width / SystemParameters.PrimaryScreenWidth;
        }

        [STAThread]
        public static void Main(String[] args)
        {
            var thing = ConsoleKey.Escape;
            while (thing != ConsoleKey.Enter)
            {
                Console.WriteLine("press enter while the cursor is over the top left corner of the chat");
                thing = Console.ReadKey().Key;
            }
            var point1 = Cursor.Position;
            

            thing = ConsoleKey.Escape;
            while (thing != ConsoleKey.Enter)
            {
                Console.WriteLine("press enter while the cursor is over the bottom right corner of the chat");
                thing = Console.ReadKey().Key;
            }
            var point2 = Cursor.Position;
            
            
            //Adjust the captured points for screen zoom
            var zoomfactor = GetWindowsScaling();
            point1.X = (int) (point1.X * zoomfactor);
            point1.Y = (int) (point1.Y * zoomfactor);
            point2.X = (int) (point2.X * zoomfactor);
            point2.Y = (int) (point2.Y * zoomfactor);

            var chatBoxBounds = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);

            thing = ConsoleKey.Escape;
            while (thing != ConsoleKey.Enter)
            {
                Console.WriteLine("press enter while the cursor is over the top left corner of the trade window");
                thing = Console.ReadKey().Key;
            }
            point1 = Cursor.Position;
            Console.WriteLine("true top left " + point1.X + "," + point1.Y);
            thing = ConsoleKey.Escape;
            while (thing != ConsoleKey.Enter)
            {
                Console.WriteLine("press enter while the cursor is over the bottom right corner of the trade window");
                thing = Console.ReadKey().Key;
            }
            point2 = Cursor.Position;
            Console.WriteLine("true bottom right " + point2.X + "," + point2.Y);


            //Adjust the captured points for screen zoom
            point1.X = (int)(point1.X );
            point1.Y = (int)(point1.Y );
            point2.X = (int)(point2.X );
            point2.Y = (int)(point2.Y );

            var tradeBoxBounds = new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y);

            bool debug = false;
            if (Console.ReadLine().Equals("Debug")) debug = true;
            new CommandLine(new Reader(), chatBoxBounds,tradeBoxBounds, debug).listenChat();
        }
    }
}
