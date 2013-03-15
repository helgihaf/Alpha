using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema.Verification
{
    public class VerificationContext : IVerificationContext
    {
        private List<VerificationMessage> messages = new List<VerificationMessage>();



        public void Clear()
        {
            messages.Clear();
            if (MessagesCleared != null)
            {
                MessagesCleared(this, EventArgs.Empty);
            }
        }

        public void Add(VerificationMessage message)
        {
            messages.Add(message);
            if (MessageAdded != null)
            {
                MessageAdded(this, new MessageAddedEventArgs { Message = message });
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<VerificationMessage> Entries
        {
            get
            {
                return messages.AsReadOnly();
            }
        }

        public bool HasErrors
        {
            get
            {
                foreach (var message in messages)
                {
                    if (message.Severity == Severity.Error)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public event EventHandler<MessageAddedEventArgs> MessageAdded;
        public event EventHandler MessagesCleared;
    }

    public class MessageAddedEventArgs : EventArgs
    {
        public VerificationMessage Message { get; set; }
    }
}
