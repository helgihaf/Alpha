//-------------------------------------------------------------------------------------------------
//
// Utils.cs -- The Utils class.
//
// Copyright (c) 2008 Marel Food Systems. All rights reserved.
//
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSearch
{
	//---------------------------------------------------------------------------------------------
	/// <summary>
	/// The Utils class TODO: Describe class here
	/// </summary>
	internal static class Utils
	{
		public static string LinesToSeparatedString(char seperator, string[] lines)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string line in lines)
			{
				if (sb.Length > 0)
					sb.Append(seperator);
				sb.Append(line);
			}
			return sb.ToString();
		}


	}
}
