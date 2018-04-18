using Park.Authorization.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Authorization.Users
{
    public interface IUserEmailer
    {
        Task SendEmailActivationLinkAsync(User user, string plainPassword = null);

        Task SendPasswordResetLinkAsync(User user);

        void TryToSendCharMessageMail(User user, string senderUsername, string senderTenantyName, ChatMessage chatMessage);
    }
}
