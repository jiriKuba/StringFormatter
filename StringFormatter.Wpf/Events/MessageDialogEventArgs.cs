using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Events
{
    /// <summary>
    /// Message dialog arguments
    /// </summary>
    public class MessageDialogEventArgs : EventArgs
    {
        public string MessageHeader { get; private set; }

        public string MessageText { get; private set; }

        public MessageDialogStyle DialogStyle { get; private set; }

        public MetroDialogSettings Settings { get; private set; }

        public MessageDialogEventArgs(string messageHeader, string messageText, MessageDialogStyle dialogStyle)
            : base()
        {
            MessageHeader = messageHeader;
            MessageText = messageText;
            DialogStyle = dialogStyle;
        }

        public MessageDialogEventArgs(string messageHeader, string messageText, MessageDialogStyle dialogStyle, MetroDialogSettings setting)
            : this(messageHeader, messageText, dialogStyle)
        {
            Settings = setting;
        }
    }
}
