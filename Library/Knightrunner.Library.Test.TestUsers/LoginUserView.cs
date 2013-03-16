using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.Library.Test.TestUsers
{
	public partial class LoginUserView : UserControl
	{
		public LoginUserView()
		{
			InitializeComponent();
			UpdateOkButton();
		}

		private void textBoxUserName_TextChanged(object sender, EventArgs e)
		{
			UpdateOkButton();
		}

		private void textBoxPassword_TextChanged(object sender, EventArgs e)
		{
			UpdateOkButton();
		}

		private void UpdateOkButton()
		{
			buttonOk.Enabled = textBoxUserName.TextLength > 0 && textBoxPassword.TextLength > 0;
		}
	}
}
