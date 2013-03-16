using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Bus
{
    public interface IBus
    {
        void Publish(IMessage message);
        void Send(BusAddress destination, IMessage message);

        void InstallMessageHandler(IMessageHandler messageHandler);
        void UninstallMessageHandler(IMessageHandler messageHandler);

        void Subscribe(BusAddress address, Type messageType);
        void Unsubscribe(BusAddress address, Type messageType);
    }
}
