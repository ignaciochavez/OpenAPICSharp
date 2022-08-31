using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tool
{
    public class MessageVO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Messages { get; set; }

        public MessageVO()
        {
            Id = 0;
            Title = string.Empty;
            Messages = new List<string>();
        }

        public MessageVO(int id, string title, string messages)
        {
            Id = id;
            Title = title;
            Messages = new List<string>();
            Messages.Add(messages);
        }

        public void SetMessage(int id, string title, string messages)
        {
            Id = id;
            Title = title;
            Messages = new List<string>();
            Messages.Add(messages);
        }

        public void SetMessage(int id, string title, List<string> messages)
        {
            Id = id;
            Title = title;
            Messages = new List<string>();
            Messages = messages;
        }

        public void SetIdTitle(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
