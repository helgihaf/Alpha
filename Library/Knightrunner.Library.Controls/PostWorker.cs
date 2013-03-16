using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Knightrunner.Library.Controls
{
    /// <summary>
    /// Provides the ability to do some work just before the application becomes idle.
    /// </summary>
    /// <remarks>
    /// Work must be requested by invoking the RequestWork() method. Implement the work by providing
    /// a DoWork event handler. Note that calling multiple RequestWork() before the application becomes
    /// idle results in only a single DoWork event. Once a DoWork event has been fired, no DoWork
    /// events are fired until RequestWork() is called again and the application becomes idle.
    /// </remarks>
    public class PostWorker : Component
    {
        public object syncRoot = new object();
        private bool workPending;

        /// <summary>
        /// Signal that work is requested once the application becomes idle.
        /// </summary>
        public void RequestWork()
        {
            lock (syncRoot)
            {
                if (!workPending)
                {
                    Application.Idle += new EventHandler(Application_Idle);
                    workPending = true;
                }
            }
        }

        /// <summary>
        /// Occurs when work has been requested and the application is just about to become idle.
        /// </summary>
        public event EventHandler<EventArgs> DoWork;



        private void Application_Idle(object sender, EventArgs e)
        {
            lock (syncRoot)
            {
                Application.Idle -= new EventHandler(Application_Idle);
                if (!workPending)
                    return;

                workPending = false;
            }

            if (DoWork != null && CanRaiseEvents)
            {
                DoWork(this, EventArgs.Empty);
            }
        }


    }

}
