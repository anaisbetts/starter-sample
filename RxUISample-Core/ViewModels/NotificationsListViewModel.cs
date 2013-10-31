using System;
using System.Linq;
using ReactiveUI;
using Octokit;
using Akavache;

namespace RxUISample
{
    public class NotificationsListViewModel : ReactiveObject
    {
        public ReactiveList<NotificationTileViewModel> Notifications { get; protected set; }
        public ReactiveCommand LoadNotifications { get; protected set; }

        public NotificationsListViewModel(INotificationsClient notificationsApi)
        {
            Notifications = new ReactiveList<NotificationTileViewModel>();
            LoadNotifications = new ReactiveCommand();

            var loadedNotifications = LoadNotifications.RegisterAsync(_ => 
                BlobCache.LocalMachine.GetAndFetchLatest("notifications", () => notificationsApi.GetAllForCurrent()));

            loadedNotifications.Subscribe(newItems => {
                using (Notifications.SuppressChangeNotifications()) {
                    var toAdd = newItems
                        .Where(x => x.Repository.Owner.Id != 9919 && x.Repository.Owner.Id != 1089146)
                        .Select(x => new NotificationTileViewModel(x));

                    Notifications.Clear();
                    Notifications.AddRange(toAdd);
                }
            });
        }
    }

    public class NotificationTileViewModel : ReactiveObject
    {
        public Notification Model { get; protected set; }

        public NotificationTileViewModel(Notification model) 
        {
            Model = model; 
        }
    }
}

