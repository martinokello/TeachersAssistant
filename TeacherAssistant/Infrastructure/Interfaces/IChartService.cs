using System;
using System.Collections.Generic;
using TeacherAssistanct.Infrastructure;

namespace TeacherAssistanct.Infrastructure.Interfaces
{
    interface IChatUsersService
    {
        string GetInvitedClientUsername();
        MartinLayooIncChat GetChatResource();
        void InviteClient(Client clt);
        void ExitPublicRoom(Client clt);
        string IsInPrivateRoom(Client clt);
        void ExitPrivateRoom(Client clt);
        void ClearPrivateRoom();
        void AddExitMessagePrivateRoom(Client clt);
        void AddMessagePrivateRoom(Client clt);
        void AddMessagePublicRoom(Client clt);
        string[] GetUserList();
        string[] GetSecretUserList();
        string GetMessage(string room);
        bool BookSecretRoom(Client clt);
        bool SecretRoomIsFull(IList<Client> room);
        string GetBookerClient();

    }
}
