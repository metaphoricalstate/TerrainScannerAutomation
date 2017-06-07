﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Terrain_Scanner_Automation
{
    public partial class AutomationDisplay : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys key);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        const int START_HOTKEY_ID = 1;
        const int STOP_HOTKEY_ID = 2;
        Automator _auto;

        public AutomationDisplay()
        {
            InitializeComponent();

            if (!RegisterHotKey(this.Handle, START_HOTKEY_ID, 0, Keys.NumPad7))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!RegisterHotKey(this.Handle, STOP_HOTKEY_ID, 0, Keys.NumPad8))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            _auto = new Automator();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == START_HOTKEY_ID && _auto.MinecraftProcessID != 0)
            {
                Console.WriteLine("Start");
                _auto.Running = true;
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == STOP_HOTKEY_ID)
            {
                Console.WriteLine("Stop");
                _auto.Running = false;
            }

            base.WndProc(ref m);
        }

        private void btnSetProcessID_Click(object sender, EventArgs e)
        {
            _auto.MinecraftProcessID = Int32.Parse(tbProcessID.Text);
        }
        
    }
}