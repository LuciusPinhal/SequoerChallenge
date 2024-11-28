using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerAPP.Models
{
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

