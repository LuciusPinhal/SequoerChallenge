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
    public partial class UC_Page : UserControl
    {
        public UC_Page()
        {
            InitializeComponent();
        }

        public string Title
        {
            get 
            { 
                return TitleLabel.Text; 
            }
            set
            {
                TitleLabel.Text = value;
            }
    }
    }
}
