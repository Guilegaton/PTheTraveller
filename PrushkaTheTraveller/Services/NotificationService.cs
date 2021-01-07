using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using System;

namespace PrushkaTheTraveller.Services
{
    public class NotificationService : IDisposable
    {
        public NotificationChannel NotificationChannel { get; private set; }

        private readonly NotificationManager _notificationManager;

        public NotificationService(NotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
            SetNotificationChanel("Prushka", "Prushka");
        }

        public void PushNotification(Context context, string title, string contentText)
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, NotificationChannel.Name)
                .SetSmallIcon(Resource.Drawable.ic_notification)
                .SetTicker("prushka")
                .SetWhen(0)
                .SetAutoCancel(true)
                .SetContentTitle(title)
                .SetStyle(new NotificationCompat.BigTextStyle().BigText(contentText))
                .SetContentText(contentText);

            // Build the notification:
            Notification notification = builder.Build();

            // Publish the notification:
            const int notificationId = 0;
            _notificationManager.Notify(notificationId, notification);
        }

        private void SetNotificationChanel(string chanelName, string chanelDescription)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = chanelName;
            var channelDescription = chanelDescription;
            NotificationChannel = new NotificationChannel(chanelName, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            _notificationManager.CreateNotificationChannel(NotificationChannel);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}