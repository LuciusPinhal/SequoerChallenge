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
    public partial class Base : Form
    {
        private Label titleLabel;

        public Base()
        {
            // Cria o título no formulário
            titleLabel = new Label
            {
                Dock = DockStyle.Top, 
                Padding = new Padding(40, 25, 0, 0), 
                ForeColor = Color.FromArgb(83, 126, 235), 
                Font = new Font("Sans Serif", 18.25F, FontStyle.Bold), 
                FlatStyle = FlatStyle.Flat, 
                TextAlign = ContentAlignment.MiddleLeft, 
                Height = 60, 
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
