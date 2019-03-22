using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.ChatThreads
{
    public interface IChatThreadService
    {
        #region chatThread
        ChatThread GetChatThread(int id);
        List<ChatThread> GetChatThread(Expression<Func<ChatThread, bool>> Predicate);

        List<ChatThread> GetChatThreads();
        List<ChatThread> GetChatThreads(Expression<Func<ChatThread, bool>> predicate);
        void InsertChatThread(ChatThread _ChatThread);
        void UpdateChatThread(ChatThread _ChatThread);
        void DeleteChatThread(int id);
        void DeleteChatThreads(int caseID);
        #endregion


        #region chatThreadMessage
        ChatThreadMessage GetChatThreadMessage(int id);
        List<ChatThreadMessage> GetChatThreadMessage(Expression<Func<ChatThreadMessage, bool>> Predicate);

        List<ChatThreadMessage> GetChatThreadMessages();
        void InsertChatThreadMessage(ChatThreadMessage _ChatThreadMessage);
        void UpdateChatThreadMessage(ChatThreadMessage _ChatThreadMessage);
        void DeleteChatThreadMessage(int id);
        #endregion

    }
}
