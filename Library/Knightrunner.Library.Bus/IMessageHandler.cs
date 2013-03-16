using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Bus
{
    public interface IMessageHandler
    {
        IBus Bus { get; }
    }

    public interface IMessageHandler<TMessage> : IMessageHandler
        where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
