using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagerAPP
{
    public partial class Frm_Base : Form
    {
        private Label titleLabel;
        public Frm_Base()
        {
            titleLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(10, 10), // Ajuste conforme necessário
                Text = "Default Title"
            };
            Controls.Add(titleLabel);
        }

        public string Title
        {
            get => titleLabel.Text;
            set => titleLabel.Text = value;
        }
    }
}

