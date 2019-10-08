using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace POETradeBot
{
    public class Reader
    {
        private tessnet2.Tesseract ocr;
        public Reader()
        {
            ocr = new tessnet2.Tesseract();
            ocr.Init(@"D:\code\POETradeBot\POETradeBot\packages\NuGet.Tessnet2.1.1.1\content\Content\tessdata","eng",false);
        }

        public string CheckImage(string path)
        {
            var words = ocr.DoOCR(new Bitmap( path),Rectangle.Empty);
            StringBuilder output = new StringBuilder();
            foreach (var word in words)
            {
                output.Append(" " + word.Text);
            }

            return output.ToString();
        }
    }
}
