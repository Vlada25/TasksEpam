using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTCP_IP
{
    public class MessageBase
    {
        private static Dictionary<string, List<string>> _messageList = new Dictionary<string, List<string>>();

        /// <summary>
        /// Adding message to the dictionary by name
        /// </summary>
        /// <param name="name"> Sender name </param>
        /// <param name="message"> Message </param>
        public static void AddMessage(string name, string message)
        {
            if (!_messageList.ContainsKey(name))
            {
                _messageList.Add(name, new List<string>());
            }

            _messageList[name].Add(message);
        }

        /// <summary>
        /// Getting all messages by name
        /// </summary>
        /// <param name="senderName"> Sender name </param>
        /// <returns> List of messages </returns>
        public static List<string> GetAllMessagesBySenderName(string senderName)
        {
            if (_messageList.ContainsKey(senderName))
            {
                return _messageList[senderName];
            }

            return null;
        }

        /// <summary>
        /// Getting all messages
        /// </summary>
        /// <returns> All messages </returns>
        public static Dictionary<string, List<string>> GetAllMessages()
        {
            return _messageList;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
