using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace TeacherAssistanct.Infrastructure
{
    public partial class MartinLayooIncChat
    {
        public  Dictionary<string, Queue<Client>> Rooms = new Dictionary<string, Queue<Client>>();
        public  string PublicRoom = "PublicRoom";
        public  string SecretRoom = "SecretRoom";
        public  IList<Client> userList = new List<Client>();
        public  IList<Client> userSecretList = new List<Client>();


        public enum RoomType { PublicRoom = 0, SecretRoom = 1 };
        public enum BookRoom { RoomEmpty = 0, RoomFull = 1 };

        public void Initialize()
        {
            Rooms.Add(PublicRoom, new Queue<Client>());
            Rooms.Add(SecretRoom, new Queue<Client>());

            //Thread Cleanup Rooms:
            ThreadStart publicCleanUp = new ThreadStart(CleanUpPublicRoom);
            ThreadStart secretCleanUp = new ThreadStart(CleanUpSecretRoom);

            Thread PublicRoomKeeper = new Thread(publicCleanUp);
            Thread SecretRoomKeeper = new Thread(secretCleanUp);
            PublicRoomKeeper.IsBackground = true;
            SecretRoomKeeper.IsBackground = true;

            PublicRoomKeeper.Start();
            SecretRoomKeeper.Start();
        }

        private  void CleanUpPublicRoom()
        {
            for (int i = 0; i < userList.Count; i++)
            {
                TimeSpan span = DateTime.Now - userList[i].TimeStarted;
                if (span.Minutes >= 30) userList.Remove(userList[i]);
            }

            Thread.Sleep(20000);
        }
        private  void CleanUpSecretRoom()
        {
            for (int i = 0; i < userSecretList.Count; i++)
            {
                TimeSpan span = DateTime.Now - userSecretList[i].TimeStarted;
                if (span.Minutes >= 15) userSecretList.Remove(userSecretList[i]);
            }

            Thread.Sleep(20000);
        }
    }
}