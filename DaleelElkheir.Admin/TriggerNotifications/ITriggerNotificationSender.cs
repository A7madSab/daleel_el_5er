using NotificationHelper;
using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.Admin.TriggerNotifications
{
    public interface ITriggerNotificationSender
    {
        bool Send(int sender, IQueryable<UserDevice> devices, string title, string body,string bodyAr, int type);
        bool SendAllUsers(int sender, IQueryable<DeviceToken> devices, string title, string body, string bodyAr, int type);


    }
}
