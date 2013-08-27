//-------------------------------------------------------------------------------------------------
//
// Wildcard.cs -- The Wildcard class.
//
// Copyright (c) Marel hf. 2006. All rights reserved.
//
//-------------------------------------------------------------------------------------------------

#region File history
/*
	$History: Wildcard.cs $
 * 
 * *****************  Version 3  *****************
 * User: Helgihaf     Date: 15.03.08   Time: 11:39
 * Updated in $/G5/Marel/Common/Current/Marel.Common/Utilities
 * 
 * *****************  Version 2  *****************
 * User: Helgihaf     Date: 10.03.08   Time: 12:26
 * Updated in $/G5/Marel/Common/Current/Marel.Common/Utilities
 * 
 * *****************  Version 1  *****************
 * User: Helgihaf     Date: 3.12.07    Time: 17:22
 * Created in $/G5/Marel/Common/Current/Marel.Common/Utilities
*/
#endregion

using System;

namespace SimpleSearch
{
	//-----------------------------------------------------------------------------------------
	/// <summary>
	/// Class providing wildcard string matching as found in standard file system wildcards.
	/// </summary>
	public class Wildcard
	{
		private Wildcard()
		{
		}

		/// <summary>
		/// Array of valid wildcards
		/// </summary>
		private static char[] wildcards = new char[] { '*', '?' };

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Returns true if the string matches the pattern which may contain * and ? wildcards.
		/// Matching is done without regard to case.
		/// </summary>
		/// <param name="pattern">The wildcard pattern to search for. Can be without any wildcards.</param>
		/// <param name="s">The string to search in.</param>
		/// <returns>True if the specified string matches the pattern, false otherwise.</returns>
		public static bool Match(string pattern, string s)
		{
			return Match(pattern, s, false);
		}

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Returns true if the specified string matches the pattern which may contain * and ? wildcards.
		/// </summary>
		/// <param name="pattern">The wildcard pattern to search for.</param>
		/// <param name="s">The string to search in.</param>
		/// <param name="caseSensitive">True if match should be case sensitive, false otherwise.</param>
		/// <returns>True if the specified string matches the pattern, false otherwise.</returns>
		public static bool Match(string pattern, string s, bool caseSensitive)
		{
			// if not concerned about case, convert both string and pattern
			// to lower case for comparison
			if (!caseSensitive)
			{
				pattern = pattern.ToLower();
				s = s.ToLower();
			}

			// if pattern doesn't actually contain any wildcards, use simple equality
			if (pattern.IndexOfAny(wildcards) == -1)
				return (s == pattern);

			// otherwise do pattern matching
			int i = 0;
			int j = 0;
			while (i < s.Length && j < pattern.Length && pattern[j] != '*')
			{
				if ((pattern[j] != s[i]) && (pattern[j] != '?'))
				{
					return false;
				}
				i++;
				j++;
			}

			// if we have reached the end of the pattern without finding a * wildcard,
			// the match must fail if the string is longer or shorter than the pattern
			if (j == pattern.Length)
				return s.Length == pattern.Length;

			int cp = 0;
			int mp = 0;
			while (i < s.Length)
			{
				if (j < pattern.Length && pattern[j] == '*')
				{
					if ((j++) >= pattern.Length)
					{
						return true;
					}
					mp = j;
					cp = i + 1;
				}
				else if (j < pattern.Length && (pattern[j] == s[i] || pattern[j] == '?'))
				{
					j++;
					i++;
				}
				else
				{
					j = mp;
					i = cp++;
				}
			}

			while (j < pattern.Length && pattern[j] == '*')
			{
				j++;
			}

			return j >= pattern.Length;
		}


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Checks if the specified string contains any wildcard characters.
		/// </summary>
		/// <param name="s">The string to check for wildcard characters.</param>
		/// <returns>True if the specified string contains one or more wildcard characters, false otherwise.</returns>
		public static bool ContainsWildcard(string s)
		{
			return s.IndexOfAny(wildcards) != -1;
		}
	}
}
