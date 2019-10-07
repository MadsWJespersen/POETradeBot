using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;

namespace POETradeBot
{
    public class Reader
    {
        private AutoOcr ocr;
        public Reader()
        {
            ocr = new AutoOcr();
        }

        public string checkImage(string path)
        {
            var output = ocr.Read(path);
            return output.Text;
        }
    }
}
