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

        public static void AddMessage(string name, string message)
        {
            if (!_messageList.ContainsKey(name))
            {
                _messageList.Add(name, new List<string>());
            }

            _messageList[name].Add(message);
        }

        public static List<string> GetAllMessagesByClientName(string clientName)
        {
            if (_messageList.ContainsKey(clientName))
            {
                return _messageList[clientName];
            }

            return null;
        }

        public static Dictionary<string, List<string>> GetAllMessages()
        {
            return _messageList;
        }
    }
}
