using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clients
{
    public partial class Allocator : Form
    {
        public Allocator()
        {
            InitializeComponent();
            var welcomeForm = new Welcome();
            welcomeForm.Show();
        }

        private void Allocator_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
