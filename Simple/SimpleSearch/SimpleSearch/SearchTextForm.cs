//-------------------------------------------------------------------------------------------------
//
// SearchTextForm.cs -- The SearchTextForm form class.
//
// Copyright (c) 2008 Marel Food Systems. All rights reserved.
//
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleSearch
{
	//---------------------------------------------------------------------------------------------
	/// <summary>
	/// The SearchTextForm form class TODO: Describe class here
	/// </summary>
	public partial class SearchTextForm : Form
	{
		public SearchTextForm()
		{
			InitializeComponent();
		}


		public bool MultiText
		{
			get { return checkBoxMultiText.Checked; }
			set { checkBoxMultiText.Checked = value; }
		}

		public char SeperatorChar
		{
			get
			{
				if (textBoxSeperator.Text.Length > 0)
					return textBoxSeperator.Text[0];
				else
					return ' ';
			}

			set
			{
				textBoxSeperator.Text = value.ToString();
			}
		}


		public string SearchText
		{
			get
			{
				if (MultiText)
				{
					return Utils.LinesToSeparatedString(SeperatorChar, textBoxText.Lines);
				}
				else
				{
					return textBoxText.Text;
				}
			}

			set
			{
				if (MultiText)
				{
					textBoxText.Lines = value.Split(SeperatorChar);
				}
				else
				{
					textBoxText.Text = value;
				}
			}
		}

		private void checkBoxMultiText_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxMultiText.Checked && textBoxText.Text.Length > 0)
			{
				textBoxText.Lines = textBoxText.Text.Split(SeperatorChar);
			}
			else
			{
				textBoxText.Text = Utils.LinesToSeparatedString(SeperatorChar, textBoxText.Lines);
			}
		}


	}
}
