using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TeacherAssistanct.Infrastructure;
using TeacherAssistanct.Infrastructure.Interfaces;

namespace TeacherAssistanct.Controllers
{
    //[Authorize(Roles ="Admin,Standard User,Guest")]
    public class ChatController : ApiController, IChatUsersService
    {
        private static string currMesg = null;
        private static string secCurrMesg = null;
        private static Client currentClient = null;
        private static string invitedClient = null;

        private MartinLayooIncChat ChatResource = null;

        private void Initialize()
        {
            ChatResource = HttpContext.Current.Application["ChatResource"] as MartinLayooIncChat;
            if (ChatResource == null)
            {
                var chatResource = new MartinLayooIncChat();

                chatResource.Initialize();

                HttpContext.Current.Application["ChatResource"] = chatResource;
                ChatResource = HttpContext.Current.Application["ChatResource"] as MartinLayooIncChat;
            }
        }
        private void DequeueMessages(MartinLayooIncChat.RoomType roomType)
        {
            Initialize();
            if (roomType == MartinLayooIncChat.RoomType.PublicRoom)
            {
                lock (ChatResource.Rooms[ChatResource.PublicRoom])
                {
                    if (ChatResource.Rooms[ChatResource.PublicRoom].Count > 0)
                    {
                        currentClient = ChatResource.Rooms[ChatResource.PublicRoom].Dequeue();
                        currMesg = "<span id='clientMsg' style='color:Red;font-weight:bold;'><b>" + currentClient.Username + ": </b></span> " + currentClient.Message;

                    }
                }
            }
            else//roomType == MartinLayooIncChat.RoomType.SecretRoom
            {
                lock (ChatResource.Rooms[ChatResource.SecretRoom])
                {
                    if (ChatResource.Rooms[ChatResource.SecretRoom].Count > 0)
                    {
                        currentClient = ChatResource.Rooms[ChatResource.SecretRoom].Dequeue();
                        if (ChatResource.userSecretList.Contains(currentClient))
                            secCurrMesg = "<span id='clientMsg' style='color:Red;font-weight:bold;'><b>" + currentClient.Username + ": </b></span> " + currentClient.Message;
                    }

                }
            }
        }
        
        public MartinLayooIncChat GetChatResource()
        {
            Initialize();
            return HttpContext.Current.Application["ChatResource"] as MartinLayooIncChat;
        }
        [Route("~/Chat/GetInvitedClientUsername")]
        public string GetInvitedClientUsername()
        {
            return invitedClient;
        }

        [Route("~/Chat/IsInPrivateRoom")]
        public string IsInPrivateRoom([FromBody] Client clt)
        {
            Initialize();
            return ChatResource.userSecretList.Contains(clt).ToString();
        }
        [Route("~/Chat/InviteClient")]
        public void InviteClient([FromBody] Client clt)
        {
            Initialize();
            MartinLayooIncChat.RoomType roomType = (MartinLayooIncChat.RoomType)Enum.Parse(typeof(MartinLayooIncChat.RoomType), MartinLayooIncChat.RoomType.SecretRoom.ToString());

            Client client = new Client(clt.Username, clt.Message);
            invitedClient = client.Username;
            AddMessagePublicRoom(clt);
            AddMessagePrivateRoom(clt);
        }

        [Route("~/Chat/ExitPublicRoom")]
        public void ExitPublicRoom([FromBody] Client clt)
        {
            Initialize();

            Client client = new Client(clt.Username, clt.Message);

            ChatResource.userList.Remove(client);
            AddExitMessagePublicRoom(clt);
        }
        [Route("~/Chat/ExitPrivateRoom")]
        public void ExitPrivateRoom([FromBody] Client clt)
        {

            Initialize();
            Client client = new Client(clt.Username, clt.Message);
            ChatResource.userSecretList.Remove(client);
            AddExitMessagePublicRoom(clt);
        }
        [Route("~/Chat/ClearPrivateRoom")]
        public void ClearPrivateRoom()
        {
            Initialize();
            if (ChatResource.userSecretList.Count > 0)
                ChatResource.userSecretList.Clear();
        }
        [Route("~/Chat/AddExitMessagePublicRoom")]
        public void AddExitMessagePublicRoom([FromBody] Client clt)
        {

            Initialize();
            Client client = new Client(clt.Username, clt.Message);
            Queue<Client> MessageQueue = ChatResource.Rooms["PublicRoom"];
            lock (MessageQueue)
            {
                MessageQueue.Enqueue(client);
            }
        }
        [Route("~/Chat/AddExitMessagePrivateRoom")]
        public void AddExitMessagePrivateRoom([FromBody] Client clt)
        {
            Initialize();

            Client client = new Client(clt.Username, clt.Message);
            Queue<Client> MessageQueue = ChatResource.Rooms["SecretRoom"];
            lock (MessageQueue)
            {
                MessageQueue.Enqueue(client);
            }
        }

        [Route("~/Chat/AddMessagePrivateRoom")]
        public void AddMessagePrivateRoom([FromBody] Client clt)
        {
            Initialize();

            Client client = new Client(clt.Username, clt.Message);
            client.TimeStarted = DateTime.Now;
            Queue<Client> MessageQueue = ChatResource.Rooms["SecretRoom"];

            if (!string.IsNullOrEmpty(client.Username))
            {
                MessageQueue.Enqueue(client);
                if (!ChatResource.userSecretList.Contains(client))
                {
                    ChatResource.userSecretList.Add(client);
                }
                else
                {
                    ChatResource.userSecretList.Remove(client);
                    ChatResource.userSecretList.Add(client);
                }
            }
        }

        [Route("~/Chat/AddMessagePublicRoom")]
        public void AddMessagePublicRoom([FromBody] Client clt)
        {
            Initialize();

            Client client = new Client(clt.Username, clt.Message);
            client.TimeStarted = DateTime.Now;
            Queue<Client> MessageQueue = ChatResource.Rooms["PublicRoom"];
            lock (MessageQueue)
            {
                MessageQueue.Enqueue(client);
                if (!string.IsNullOrEmpty(client.Username))
                {
                    if (!ChatResource.userList.Contains(client))
                    {
                        ChatResource.userList.Add(client);
                    }
                    else
                    {
                        ChatResource.userList.Remove(client);
                        ChatResource.userList.Add(client);
                    }
                }
            }
        }

        [Route("~/Chat/GetUserList")]
        public string[] GetUserList()
        {
            Initialize();
            string[] result = new string[ChatResource.userList.Count];
            int i = 0;

            foreach (Client clt in ChatResource.userList)
            {
                result[i] = clt.Username;
                i++;
            }

            return result;
        }
        [Route("~/Chat/GetSecretUserList")]
        public string[] GetSecretUserList()
        {
            Initialize();
            string[] result = new string[ChatResource.userSecretList.Count];
            int i = 0;

            foreach (Client clt in ChatResource.userSecretList)
            {
                result[i] = clt.Username;
                i++;
            }

            return result;
        }
        [Route("~/Chat/GetMessage/{room}")]
        public string GetMessage(string room)
        {
            Initialize();

            MartinLayooIncChat.RoomType roomType = (MartinLayooIncChat.RoomType)Enum.Parse(typeof(MartinLayooIncChat.RoomType), room);

            string mesg = null;
            if (roomType == MartinLayooIncChat.RoomType.PublicRoom)
            {
                if (HttpContext.Current.Session["PreviousMesg"] == null) HttpContext.Current.Session["PreviousMesg"] = "";

                if ((currMesg != null && ((string)HttpContext.Current.Session["PreviousMesg"]) == currMesg) || currMesg == null)
                    DequeueMessages(roomType);

                if ((string)HttpContext.Current.Session["PreviousMesg"] != currMesg)
                {
                    mesg = currMesg;
                    HttpContext.Current.Session["PreviousMesg"] = mesg;
                }

                return mesg;
            }
            else
            {
                if (HttpContext.Current.Session["SecPreviousMesg"] == null) HttpContext.Current.Session["SecPreviousMesg"] = "";

                if ((secCurrMesg != null && ((string)HttpContext.Current.Session["SecPreviousMesg"]) == secCurrMesg) || secCurrMesg == null)
                    DequeueMessages(roomType);

                if ((string)HttpContext.Current.Session["SecPreviousMesg"] != secCurrMesg)
                {
                    mesg = secCurrMesg;
                    HttpContext.Current.Session["SecPreviousMesg"] = mesg;
                }

                return mesg;
            }
        }
        [Route("~/Chat/BookSecretRoom")]
        public bool BookSecretRoom([FromBody] Client clt)
        {
            Initialize();

            if (String.IsNullOrEmpty(clt.Username))
            {
                return false;
            }
            Client client = new Client(clt.Username, clt.Message);
            client.ClientBooker = clt.Username;
            client.TimeStarted = DateTime.Now;

            if (ChatResource.userSecretList.Count == 0 && !ChatResource.userSecretList.Contains(client))
            {
                //You can book the room and use:
                ChatResource.userSecretList.Add(client);
                return true;
            }
            else
            {
                return false;
            }
        }
        [Route("~/Chat/SecretRoomIsFull")]
        public bool SecretRoomIsFull([FromBody] IList<Client> room)
        {
            return room.Count >= 2;
        }
        [Route("~/Chat/GetBookerClient")]
        public string GetBookerClient()
        {
            Initialize();
            Client bclient = null;
            foreach (Client c in ChatResource.userSecretList)
            {
                if (!String.IsNullOrEmpty(c.ClientBooker))
                {
                    bclient = c;
                    break;
                }
            }
            if (bclient != null) return bclient.ClientBooker;
            else return null;
        }
    }

}
