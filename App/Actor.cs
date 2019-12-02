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
        private readonly Rectangle _stashBoxBounds;
        private readonly Point _stash;
        private readonly (int, int) tradeGrid;
        private readonly (int, int) stashGrid;

        public Actor(Rectangle tradeBoxBounds,Point acceptButton, Rectangle stashBoxBounds, Point stash)
        {
            this._tradeBoxBounds = tradeBoxBounds;
            this._acceptButton = acceptButton;
            this._stashBoxBounds = stashBoxBounds;
            this._stash = stash;
            this.tradeGrid = (12, 5);
            this.stashGrid = (24, 24);
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
                    pressKey("^(c)");
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

        [STAThread]
        public string GetStashItem(int x, int y)
        {

            double xCellDistance = _stashBoxBounds.Width / (stashGrid.Item1 - 1.0);
            double yCellDistance = _stashBoxBounds.Height / (stashGrid.Item2 - 1.0);
            var item = "";

            //subtract by 1 because the coords are given in 1 indexed coords and actor uses 0 indexed coords
            Cursor.Position = new Point((int)(_stashBoxBounds.Left + xCellDistance * (x - 1)), (int)(_stashBoxBounds.Top + yCellDistance * (y - 1)));
            pressKey("^(c)");
            keybd_event(VK_CONTROL, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);

            if (Clipboard.ContainsText())
            {
                item = Clipboard.GetText(TextDataFormat.Text);
                Clipboard.Clear();
            }
            
            return item;
        }

        public void AcceptTrade()
        {
            Cursor.Position = _acceptButton;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        private void pressKey(string key)
        {
            Process[] local = Process.GetProcessesByName("PathOfExile_x64Steam");

            if (local.Length > 0)
            {
                Process p = local[0];
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                p.WaitForInputIdle();
                SendKeys.SendWait(key);
                p.WaitForInputIdle();
            }
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const byte KEYEVENTF_KEYUP = 0x02;
        public const byte VK_CONTROL = 0x11;

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
    }
}
