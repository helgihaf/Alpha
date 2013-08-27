using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace SimpleSearch
{
    internal class FileImageLoader : IDisposable
    {
        private class LoadRequest
        {
            public string FilePath { get; set; }
            public object Tag { get; set; }
        }

        private bool disposed;
        private Queue<LoadRequest> loadRequests = new Queue<LoadRequest>();
        private Thread workerThread;
        private bool enabled;

        public FileImageLoader()
        {
            ThreadStart threadStart = new ThreadStart(ThreadProc);
            workerThread = new Thread(threadStart);
            workerThread.IsBackground = true;
            workerThread.Start();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (workerThread.IsAlive)
                    {
                        // Signal to background thread that it should quit gracefully
                        lock (loadRequests)
                        {
                            loadRequests.Clear();
                            loadRequests.Enqueue(null);
                            if (enabled)
                            {
                                Monitor.PulseAll(loadRequests);
                            }
                        }
                        if (!workerThread.Join(2000))
                        {
                            workerThread.Abort();
                        }
                    }
                }
                disposed = true;
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                lock (loadRequests)
                {
                    Monitor.PulseAll(loadRequests);
                }
            }
        }
        public void AddRequest(string filePath, object tag)
        {
            lock (loadRequests)
            {
                loadRequests.Enqueue(new LoadRequest { FilePath = filePath, Tag = tag });
                Monitor.PulseAll(loadRequests);
            }
        }


        public void ClearRequests()
        {
            lock (loadRequests)
            {
                loadRequests.Clear();
            }
        }

        public event EventHandler<LoadCompletedEventArgs> LoadCompleted;


        private void ThreadProc()
        {
            while (true)
            {
                LoadRequest loadRequest;

                lock (loadRequests)
                {
                    while (loadRequests.Count == 0 || !enabled)
                    {
                        Monitor.Wait(loadRequests);
                    }
                    loadRequest = loadRequests.Dequeue();
                }

                if (loadRequest == null)
                {
                    break;
                }

                Image image;
                try
                {
                    image = Image.FromFile(loadRequest.FilePath);
                }
                catch (OutOfMemoryException)
                {
                    image = null;
                }

                if (image != null)
                {
                    OnLoadCompleted(loadRequest, image);
                }
            }
        }

        private void OnLoadCompleted(LoadRequest loadRequest, Image image)
        {
            if (LoadCompleted != null)
            {
                LoadCompletedEventArgs e = new LoadCompletedEventArgs
                {
                    FilePath = loadRequest.FilePath,
                    Tag = loadRequest.Tag,
                    Image = image
                };
                LoadCompleted(this, e);
            }
        }
    }


    internal class LoadCompletedEventArgs : EventArgs
    {
        public string FilePath { get; set; }
        public object Tag { get; set; }
        public Image Image { get; set; }
    }
}
