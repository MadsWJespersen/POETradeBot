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

namespace POETradeBot
{
    class CommandLine
    {
        private readonly bool _debug;
        private readonly Reader _reader;
        private readonly Rectangle _chatBoxBounds;

        public CommandLine(Reader reader, Rectangle chatBoxBounds,bool debug)
        {
            this._reader = reader;
            this._chatBoxBounds = chatBoxBounds;
            this._debug = debug;
        }

        public void listenChat()
        {
            while (true)
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bmp = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                        using (Page page = _reader.GetTesseract().Process(bmp))
                        {

                            if (_debug)
                            {
                                bmp.Save("currentImage.png", System.Drawing.Imaging.ImageFormat.Png);
                                Console.WriteLine(page.GetText());
                                Console.ReadLine();
                            }
                            Console.WriteLine(page.GetText());
                        }
                        
                    }
                    
                }
                Thread.Sleep(300);
            }
        }

        public static void Main(String[] args)
        {
            Reader reader = new Reader();
            var thong = reader.CheckImage(@"D:\code\POETradeBot\POETradeBotTest\testdata\test4.PNG");
            Console.WriteLine(thong);

            var thing = ConsoleKey.Escape;
            while (thing != ConsoleKey.Enter)
            {
                Console.WriteLine("press enter while the cursor is over the bottom left corner of the chat");
                thing = Console.ReadKey().Key;
            }
            var point1 = Cursor.Position;

            thing = ConsoleKey.Escape;
            while (thing != ConsoleKey.Enter)
            {
                Console.WriteLine("press enter while the cursor is over the top right corner of the chat");
                thing = Console.ReadKey().Key;
            }
            var point2 = Cursor.Position;
            bool debug = false;
            if (Console.ReadLine().Equals("Debug")) debug = true;
            new CommandLine(new Reader(), new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y), debug).listenChat();
        }
    }
}
