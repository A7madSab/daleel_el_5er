using System;
using System.Collections.Generic;
using System.Linq;
using NotificationHelper;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.Admin.TriggerNotifications;
using DaleelElkheir.DAL.Repository;
using DaleelElkheir.Admin.Models;

namespace DaleelElkheir.Admin.TriggerNotifications
{
    public class TriggerNotificationSender:ITriggerNotificationSender
    {
        private IUnitOfWork uow;
        public TriggerNotificationSender(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public bool Send(int sender,IQueryable<UserDevice> devices,string title,string body,string bodyAr,int type)
        {
            try
            {
                PushHelper CurrentPusher = new PushHelper();
                List<NotificationHelper.User> AllUsers = devices.Select(q => new NotificationHelper.User { badge = 0, DeviceTokken = q.DeviceToken }).ToList();
                CurrentPusher.Start(AllUsers,title,body, type.ToString() , 0);


                devices.GroupBy(g=>g.UserID).Select(s=>s.Key).ToList().ForEach(f=>
                                                            uow.Repository<Notification>().Insert(new Notification
                                                            {
                                                                Body = body,
                                                                BodyAr= bodyAr,
                                                                Date = DateTime.Now,
                                                                FromUserID = f,
                                                                Title = title,
                                                                Type = type,
                                                                UserID = sender
                                                            }));
                uow.Save();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }            
        }

        public bool SendAllUsers(int sender, IQueryable<DeviceToken> devices, string title, string body, string bodyAr, int type)
        {
            try
            {
                PushHelper CurrentPusher = new PushHelper();
                List<NotificationHelper.User> AllUsers = devices.Select(s=>s.DeviceTokenKey).Distinct().Select(q => new NotificationHelper.User { badge = 0, DeviceTokken = q }).ToList();
                CurrentPusher.Start(AllUsers, title, body, type.ToString(), 0);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




    }
}
