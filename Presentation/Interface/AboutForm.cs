﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessMate.Presentation.Interface
{
	public partial class AboutForm : Form
	{
		/// <summary>
		/// Initializes the form.
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();
            Icon = new Icon($"{Application.StartupPath}\\Presentation\\Images\\form_icon.ico");
        }
    }
}
