using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeacherAssistanct.Infrastructure
{
    public class Client
    {
        private string username;
        private string message;
        private DateTime timeStarted;
        public static Random random = new Random(DateTime.Now.Millisecond);

        public Client()
        {
            
        }

        public Client(string user, string mesg)
        {
            this.username = user;
            this.message = mesg;
        }
        public DateTime TimeStarted
        {
            get { return timeStarted; }
            set { timeStarted = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public override bool Equals(object obj)
        {
            if (this.Username.Equals((obj as Client).Username, StringComparison.InvariantCultureIgnoreCase))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return this.Username.Length;
        }
        public string ClientBooker
        {
            get;
            set;
        }
    }
}