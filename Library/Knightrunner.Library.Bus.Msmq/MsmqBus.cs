using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Bus.Msmq
{
    public class MsmqBus : IBus
    {
        private MessageQueue receiveQueue;
        private bool started;
        private List<IMessageHandler> handlers = new List<IMessageHandler>();

        public void Initialize(string queueName)
        {
            var receiveQueuePath = ".\\Private$\\" + queueName;
            receiveQueue = new System.Messaging.MessageQueue(receiveQueuePath, true);
            receiveQueue.PeekCompleted += receiveQueue_PeekCompleted;
        }

        public void Start()
        {
            if (!started)
            {
                receiveQueue.BeginPeek();
            }
            else
            {
                throw new InvalidOperationException("Already started");
            }
        }

        public void Stop()
        {
            started = false;
        }

        public void Publish(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void Send(BusAddress destination, IMessage message)
        {
            throw new NotImplementedException();
        }

        public void InstallMessageHandler(IMessageHandler messageHandler)
        {
            throw new NotImplementedException();
        }

        public void UninstallMessageHandler(IMessageHandler messageHandler)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(BusAddress address, Type messageType)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(BusAddress address, Type messageType)
        {
            throw new NotImplementedException();
        }

        private void receiveQueue_PeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            MessageQueue mq = (MessageQueue)sender;

            // End the asynchronous Receive operation.
            mq.EndPeek(e.AsyncResult);

            if (!started)
            {
                return;
            }

            var message = mq.Receive();
            DispatchMessage(message);

            mq.BeginPeek();
        }

        private void DispatchMessage(Message message)
        {
            throw new NotImplementedException();
        }




    }
}
