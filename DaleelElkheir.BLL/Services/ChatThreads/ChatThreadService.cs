using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.ChatThreads
{
    public class ChatThreadService : IChatThreadService
    {
        private readonly IUnitOfWork unitOfWork;

        #region chatThread
        public ChatThreadService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public ChatThread GetChatThread(int id)
        {
            return unitOfWork.Repository<ChatThread>().GetById(id);
        }

        public List<ChatThread> GetChatThread(Expression<Func<ChatThread, bool>> Predicate)
        {
            return unitOfWork.Repository<ChatThread>().Get(Predicate);
        }

        public List<ChatThread> GetChatThreads()
        {
            return unitOfWork.Repository<ChatThread>().GetAll();
        }

        public List<ChatThread> GetChatThreads(Expression<Func<ChatThread,bool>> predicate)
        {
            return unitOfWork.Repository<ChatThread>().Get(predicate);
        }

        public void InsertChatThread(ChatThread _ChatThread)
        {
            unitOfWork.Repository<ChatThread>().Insert(_ChatThread);
            unitOfWork.Save();
        }

        public void UpdateChatThread(ChatThread _ChatThread)
        {
            unitOfWork.Repository<ChatThread>().Update(_ChatThread);
            unitOfWork.Save();
        }
        public void DeleteChatThread(int id)
        {
            unitOfWork.Repository<ChatThread>().Delete(id);
            unitOfWork.Save();
        }

        public void DeleteChatThreads(int caseID)
        {
            var threads = unitOfWork.Repository<ChatThread>().Get(w => w.CaseID == caseID);
            for (int i = 0; i < threads.Count; i++)
            {
                unitOfWork.Repository<ChatThread>().Delete(threads[i].ID);
            }
            unitOfWork.Save();
        }
        #endregion


        #region chatThreadMessage
        public ChatThreadMessage GetChatThreadMessage(int id)
        {
            return unitOfWork.Repository<ChatThreadMessage>().GetById(id);
        }

        public List<ChatThreadMessage> GetChatThreadMessage(Expression<Func<ChatThreadMessage, bool>> Predicate)
        {
            return unitOfWork.Repository<ChatThreadMessage>().Get(Predicate);
        }

        public List<ChatThreadMessage> GetChatThreadMessages()
        {
            return unitOfWork.Repository<ChatThreadMessage>().GetAll();
        }

        public void InsertChatThreadMessage(ChatThreadMessage _ChatThreadMessage)
        {
            unitOfWork.Repository<ChatThreadMessage>().Insert(_ChatThreadMessage);
            unitOfWork.Save();
        }

        public void UpdateChatThreadMessage(ChatThreadMessage _ChatThreadMessage)
        {
            unitOfWork.Repository<ChatThreadMessage>().Update(_ChatThreadMessage);
            unitOfWork.Save();
        }
        public void DeleteChatThreadMessage(int id)
        {
            unitOfWork.Repository<ChatThreadMessage>().Delete(id);
            unitOfWork.Save();
        }
        #endregion

    }
}
