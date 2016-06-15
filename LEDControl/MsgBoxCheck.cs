using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MsdnMag;			// For LocalCbtHook
using Microsoft.Win32;	// For RegKey

namespace LEDControl
{
    public class MessageBoxEx
    {
        protected LocalCbtHook m_cbt;
        protected IntPtr m_hwnd = IntPtr.Zero;
        protected IntPtr m_hwndBtn = IntPtr.Zero;
        protected bool m_bInit = false;
        protected bool m_bCheck = false;
        protected string m_strCheck;

        public MessageBoxEx()
        {
            m_cbt = new LocalCbtHook();
            m_cbt.WindowCreated += new LocalCbtHook.CbtEventHandler(WndCreated);
            m_cbt.WindowDestroyed += new LocalCbtHook.CbtEventHandler(WndDestroyed);
            m_cbt.WindowActivated += new LocalCbtHook.CbtEventHandler(WndActivated);
        }

        public DialogResult Show(string strKey, string strValue, DialogResult dr, string strCheck, string strText, string strTitle, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            m_strCheck = strCheck;
            m_cbt.Install();
            dr = System.Windows.Forms.MessageBox.Show(strText, strTitle, buttons, icon);
            m_cbt.Uninstall();

            Properties.Settings.Default[strKey] = m_bCheck;
            Properties.Settings.Default.Save();

            return dr;
        }

        public DialogResult Show(string strKey, string strValue, DialogResult dr, string strCheck, string strText, string strTitle, MessageBoxButtons buttons)
        {
            return Show(strKey, strValue, dr, strCheck, strText, strTitle, buttons, MessageBoxIcon.None);
        }

        public DialogResult Show(string strKey, string strValue, DialogResult dr, string strCheck, string strText, string strTitle)
        {
            return Show(strKey, strValue, dr, strCheck, strText, strTitle, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public DialogResult Show(string strKey, string strValue, DialogResult dr, string strCheck, string strText)
        {
            return Show(strKey, strValue, dr, strCheck, strText, "", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void WndCreated(object sender, CbtEventArgs e)
        {
            if (e.IsDialogWindow)
            {
                m_bInit = false;
                m_hwnd = e.Handle;
            }
        }

        private void WndDestroyed(object sender, CbtEventArgs e)
        {
            if (e.Handle == m_hwnd)
            {
                m_bInit = false;
                m_hwnd = IntPtr.Zero;
                if (BST_CHECKED == (int)SendMessage(m_hwndBtn, BM_GETCHECK, IntPtr.Zero, IntPtr.Zero))
                    m_bCheck = true;
            }
        }

        private void WndActivated(object sender, CbtEventArgs e)
        {
            if (m_hwnd != e.Handle)
                return;

            // Not the first time
            if (m_bInit)
                return;
            else
                m_bInit = true;

            // Get the current font, either from the static text window
            // or the message box itself
            IntPtr hFont;
            IntPtr hwndText = GetDlgItem(m_hwnd, 0xFFFF);
            if (hwndText != IntPtr.Zero)
                hFont = SendMessage(hwndText, WM_GETFONT, IntPtr.Zero, IntPtr.Zero);
            else
                hFont = SendMessage(m_hwnd, WM_GETFONT, IntPtr.Zero, IntPtr.Zero);
            Font fCur = Font.FromHfont(hFont);

            // Get the x coordinate for the check box.  Align it with the icon if possible,
            // or one character height in
            int x = 0;
            IntPtr hwndIcon = GetDlgItem(m_hwnd, 0x0014);
            if (hwndIcon != IntPtr.Zero)
            {
                RECT rcIcon = new RECT();
                GetWindowRect(hwndIcon, rcIcon);
                POINT pt = new POINT();
                pt.x = rcIcon.left;
                pt.y = rcIcon.top;
                ScreenToClient(m_hwnd, pt);
                x = pt.x;
            }
            else
                x = (int)fCur.GetHeight();

            // Get the y coordinate for the check box, which is the bottom of the
            // current message box client area
            RECT rc = new RECT();
            GetClientRect(m_hwnd, rc);
            int y = rc.bottom - rc.top + 10;

            // Resize the message box with room for the check box
            GetWindowRect(m_hwnd, rc);
            MoveWindow(m_hwnd, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top + (int)fCur.GetHeight() * 2 + 10, true);

            m_hwndBtn = CreateWindowEx(0, "button", m_strCheck, BS_AUTOCHECKBOX | WS_CHILD | WS_VISIBLE | WS_TABSTOP,
                x, y, rc.right - rc.left - x, (int)fCur.GetHeight() + 10,
                m_hwnd, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            SendMessage(m_hwndBtn, WM_SETFONT, hFont, new IntPtr(1));
        }

        #region Win32 Imports
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;
        private const int WS_TABSTOP = 0x00010000;
        private const int WM_SETFONT = 0x00000030;
        private const int WM_GETFONT = 0x00000031;
        private const int BS_AUTOCHECKBOX = 0x00000003;
        private const int BM_GETCHECK = 0x00F0;
        private const int BST_CHECKED = 0x0001;

        [DllImport("user32.dll")]
        protected static extern void DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetDlgItem(IntPtr hwnd, int id);

        [DllImport("user32.dll")]
        protected static extern int GetWindowRect(IntPtr hwnd, RECT rc);

        [DllImport("user32.dll")]
        protected static extern int GetClientRect(IntPtr hwnd, RECT rc);

        [DllImport("user32.dll")]
        protected static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        protected static extern int ScreenToClient(IntPtr hwnd, POINT pt);

        [DllImport("user32.dll", EntryPoint = "MessageBox")]
        protected static extern int _MessageBox(IntPtr hwnd, string text, string caption,
            int options);

        [DllImport("user32.dll")]
        protected static extern IntPtr SendMessage(IntPtr hwnd,
            int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        protected static extern IntPtr CreateWindowEx(
            int dwExStyle,          // extended window style
            string lpClassName,     // registered class name
            string lpWindowName,    // window name
            int dwStyle,            // window style
            int x,                  // horizontal position of window
            int y,                  // vertical position of window
            int nWidth,             // window width
            int nHeight,            // window height
            IntPtr hWndParent,      // handle to parent or owner window
            IntPtr hMenu,           // menu handle or child identifier
            IntPtr hInstance,       // handle to application instance
            IntPtr lpParam          // window-creation data
            );

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #endregion
    }
}
