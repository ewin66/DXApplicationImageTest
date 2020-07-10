using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXApplicationImageMemory
{
    public partial class XtraFormMain : DevExpress.XtraEditors.XtraForm
    {
        public XtraFormMain()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new FormException().Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            new FormOK().Show();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }
    }
}