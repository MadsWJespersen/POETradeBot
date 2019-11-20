using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Tesseract;

namespace POETradeBot
{
    public class Reader
    {
        private readonly TesseractEngine _ocr;
        public Reader()
        {
            _ocr = new TesseractEngine(@"D:\code\POETradeBot\POETestBot\tessdata", "eng", EngineMode.Default);
        }

        public string CheckImage(string path)
        {
            var words = _ocr.Process(PixConverter.ToPix(new Bitmap(path)));
            StringBuilder output = new StringBuilder();

            output.Append(words.GetText());
            

            return output.ToString();
        }

        public TesseractEngine GetTesseract()
        {
            return _ocr;
        }
    }
}
