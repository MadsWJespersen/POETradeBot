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
        private readonly Reader _reader;
        private readonly Rectangle _chatBoxBounds;

        public CommandLine(Reader reader, Rectangle chatBoxBounds)
        {
            this._reader = reader;
            this._chatBoxBounds = chatBoxBounds;
        }

        public void listenChat()
        {
            while (true)
            {
                Bitmap bmp = new Bitmap(_chatBoxBounds.Width, _chatBoxBounds.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(_chatBoxBounds.Left, _chatBoxBounds.Top, _chatBoxBounds.Right, _chatBoxBounds.Bottom, bmp.Size, CopyPixelOperation.SourceCopy);
                Page page = _reader.GetTesseract().Process(PixConverter.ToPix(bmp));

                Console.WriteLine(page.GetText());
                page.Dispose();
                Thread.Sleep(300);
            }
        }

        public static void Main(String[] args)
        {
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

            
            new CommandLine(new Reader(), new Rectangle(point1.X, point1.Y, point2.X - point1.X, point2.Y - point1.Y)).listenChat();
        }
    }
}
