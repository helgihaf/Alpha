using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myname
{
	class Program
	{
		static void Main(string[] args)
		{
			string shortDomainName = Environment.UserDomainName;
			string fullDomainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
			string userName = Environment.UserName;
			string machineName = Environment.MachineName;

			Console.WriteLine("UPN:                   {0}@{1}", userName, fullDomainName);
			Console.WriteLine("Down-Level Logon Name: {0}\\{1}", shortDomainName, userName);
			Console.WriteLine("Full host name:        {0}.{1}", machineName, fullDomainName);
		}
	}
}
