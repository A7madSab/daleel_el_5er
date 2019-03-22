using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotificationHelper
{
    public class User
    {
        public string DeviceTokken { get; set; }
        public int badge { get; set; }
    }
    public class PushHelper
    {
        private const int IphoneTokenLength = 64;
        private readonly PushBroker _push = new PushBroker();

        public void Start(List<User> users, string title, string body, string type, long id)
        {
            string p12FilePath = "";
            string p12Password = "";
            string androidServerKey = "";
            //if (app == NotificationAppType.Doctor)
            //{
            //    p12FilePath = ConfigurationManager.AppSettings["p12FilePathDoctor"];
            //    p12Password = ConfigurationManager.AppSettings["p12FilePasswordDoctor"];
            //    androidServerKey = ConfigurationManager.AppSettings["pushAndroid_ServerKeyDoctor"];
            //}
            //  else if (app == NotificationAppType.Patient)
            //  {
            p12FilePath = ConfigurationManager.AppSettings["p12FilePath"];
            p12Password = ConfigurationManager.AppSettings["p12FilePassword"];
            androidServerKey = ConfigurationManager.AppSettings["pushAndroid_ServerKey"];
            //  }
            #region Wire up the events for all the services that the broker registers
            //Wire up the events for all the services that the broker registers
            _push.OnNotificationSent += NotificationSent;
            _push.OnChannelException += ChannelException;
            _push.OnServiceException += ServiceException;
            _push.OnNotificationFailed += NotificationFailed;
            _push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            _push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            _push.OnChannelCreated += ChannelCreated;
            _push.OnChannelDestroyed += ChannelDestroyed;
            //------------------------------------------------
            //IMPORTANT NOTE about Push Service Registrations
            //------------------------------------------------
            //Some of the methods in this sample such as 'RegisterAppleServices' depend on you referencing the correct
            //assemblies, and having the correct 'using PushSharp;' in your file since they are extension methods!!!

            // If you don't want to use the extension method helpers you can register a service like this:
            //push.RegisterService<WindowsPhoneToastNotification>(new WindowsPhonePushService());

            //If you register your services like this, you must register the service for each type of notification
            //you want it to handle.  In the case of WindowsPhone, there are several notification types!
            #endregion

            #region APPLE NOTIFICATIONS
            try
            {
                //Configure and start Apple APNS
                // IMPORTANT: Make sure you use the right Push certificate.  Apple allows you to generate one for connecting to Sandbox,
                //   and one for connecting to Production.  You must use the right one, to match the provisioning profile you build your
                //   app with!
                if (!string.IsNullOrWhiteSpace(p12FilePath))
                {
                    var appleCert =
                        File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, p12FilePath));
                    //IMPORTANT: If you are using a Development provisioning Profile, you must use the Sandbox push notification server
                    //  (so you would leave the first arg in the ctor of ApplePushChannelSettings as 'false')
                    //  If you are using an AdHoc or AppStore provisioning profile, you must use the Production push notification server
                    //  (so you would change the first arg in the ctor of ApplePushChannelSettings to 'true')
                    _push.RegisterAppleService(new ApplePushChannelSettings(false, appleCert, p12Password, true));
                }
            }
            catch { }

            #endregion

            #region   ANDROID GCM NOTIFICATIONS
            //Configure and start Android GCM
            //IMPORTANT: The API KEY comes from your Google APIs Console App, under the API Access section,
            //  by choosing 'Create new Server key...'
            //  You must ensure the 'Google Cloud Messaging for Android' service is enabled in your APIs Console
            _push.RegisterGcmService(new GcmPushChannelSettings(androidServerKey));
            #endregion

            #region Start Push notification
            for (int i = 0; i < users.Count; i++)
            {
                users[i].DeviceTokken = users[i].DeviceTokken.Replace(" ", "");
            }
            //Loop throw all divce and send message to each type of them *IOS, Android, WPhone*
            foreach (var user in users)
            {
            //   if (IsAndroidDevice(user.DeviceTokken))
            //        SendToAndroid(user.DeviceTokken, title, body, user.badge, type, id);
                if (IsIphoneDevice(user.DeviceTokken))
                    SendToIos(user.DeviceTokken, title, body, user.badge, type, id);
            }

            #endregion
        }

        private void SendToIos(string user, string title, string body, int badge, string type, long id)
        {
            //Extension method
            //Fluent construction of an iOS notification
            //IMPORTANT: For iOS you MUST MUST MUST use your own DeviceToken here that gets generated within your iOS app itself when the Application Delegate
            //  for registered for remote notifications is called, and the device token is passed back to you
            _push.QueueNotification(new AppleNotification()
                .ForDeviceToken("EC6F52C3B15AD049F3FF1A259958B42C1CDA60C1E9FADCE1FF0F9F13F6B7103A")
                .WithAlert(title)
                .WithCustomItem("body", body)
                .WithCustomItem("badge", badge)
                .WithCustomItem("type", type)
                .WithCustomItem("id", id)
                .WithSound("sound.caf"));
        }

        //private void SendToAndroid(string user, string title, string body, int badge, string type, long id)
        //{//title, body, badge ,Type ,Id
        //    //Fluent construction of an Android GCM Notification
        //    //IMPORTANT: For Android you MUST use your own RegistrationId here that gets generated within your Android app itself!
        //    var jsonData = "'title':'" + title + "','body':'" + body + "','badge':'" + badge + "','type':'" + type + "','id':'" + id + "', 'icon':'appicon'";
        //    jsonData = "{" + jsonData + "}";
        //    _push.QueueNotification(new GcmNotification()
        //    {
        //        RegistrationIds = new List<string> { user },
        //        JsonData = jsonData,
               
        //    });
        //    //_push.QueueNotification(new GcmNotification().ForDeviceRegistrationId(user.Device)
        //    //                      .WithJson("{\"alert\":\"Hello World!\",\"badge\":1,\"sound\":\"sound.caf\"}"));
        //}


        private static bool IsIphoneDevice(string device)
        {
            return device != null && device.Length == IphoneTokenLength;
        }

        private static bool IsAndroidDevice(string device)
        {
            return device != null && device.Length > 70;
        }

        private static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            //Currently this event will only ever happen for Android GCM
            Console.WriteLine(@"Device Registration Changed:  Old-> " + oldSubscriptionId + @"  New-> " + newSubscriptionId + @" -> " + notification);
        }
        private static void NotificationSent(object sender, INotification notification)
        {
            Console.WriteLine(@"Sent: " + sender + @" -> " + notification);
        }
        private static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
        {
            Console.WriteLine(@"Failure: " + sender + @" -> " + notificationFailureException.Message + @" -> " + notification);
        }
        private static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {
            Console.WriteLine(@"Channel Exception: " + sender + @" -> " + exception);
        }
        private static void ServiceException(object sender, Exception exception)
        {
            Console.WriteLine(@"Service Exception: " + sender + @" -> " + exception);
        }
        private static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
        {
            Console.WriteLine(@"Device Subscription Expired: " + sender + @" -> " + expiredDeviceSubscriptionId);
        }
        private static void ChannelDestroyed(object sender)
        {
            Console.WriteLine(@"Channel Destroyed for: " + sender);
        }
        private static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Console.WriteLine(@"Channel Created for: " + sender);
        }

    }
}
