using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        const int WM_QUERYENDSESSION = 0x0011;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_QUERYENDSESSION:
                    //MessageBox.Show("I don't want to shutdown!");
                    m.Result = new IntPtr(0);
                    break;
                default:
                    break;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (var context = new PreventShutdownContext(this, "Warning! Don't shutdown forcely."))
            {
                await Task.Delay(3 * 60 * 1000);
            }
        }
    }
}
