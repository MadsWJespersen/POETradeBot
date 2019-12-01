using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POETestBot
{
    class Actor
    {
        private readonly Rectangle _tradeBoxBounds;
        private readonly Point _acceptButton;
        private readonly (int, int) tradeGrid;

        public Actor(Rectangle tradeBoxBounds,Point acceptButton)
        {
            this._tradeBoxBounds = tradeBoxBounds;
            this._acceptButton = acceptButton;
            this.tradeGrid = (12, 5);
        }
        [STAThread]
        public List<string> FindTradeItem()
        {
            double xCellDistance = _tradeBoxBounds.Width / (tradeGrid.Item1 - 1.0);
            double yCellDistance = _tradeBoxBounds.Height / (tradeGrid.Item2 - 1.0);
            var items = new List<string>();
            for (int i = 0; i < tradeGrid.Item1; i++)
            {
                for(int j = 0; j < tradeGrid.Item2; j++)
                {
                    Cursor.Position = new Point((int)(_tradeBoxBounds.Left + xCellDistance*i) , (int)(_tradeBoxBounds.Top + yCellDistance * j));
                    Process[] local = Process.GetProcessesByName("PathOfExile_x64Steam");
                    if (local.Length > 0)
                    {
                        Process p = local[0];
                        IntPtr h = p.MainWindowHandle;
                        SetForegroundWindow(h);
                        p.WaitForInputIdle();
                        SendKeys.SendWait("^(c)");
                        p.WaitForInputIdle();
                    }
                    if (Clipboard.ContainsText())
                    {
                        items.Add(Clipboard.GetText(TextDataFormat.Text));
                        Clipboard.Clear();
                    }
                    Thread.Sleep(100);
                    
                }
            }


            return items;
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        void ActivateApp(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);

            // Activate the first application we find with this name
            if (p.Count() > 0)
                SetForegroundWindow(p[0].MainWindowHandle);
        }
    }
}
