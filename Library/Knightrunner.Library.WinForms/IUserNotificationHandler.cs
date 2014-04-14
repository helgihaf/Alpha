using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.WinForms
{
	public interface IUserNotificationHandler
	{
		void Notify(UserNotification notification);
	}
}
