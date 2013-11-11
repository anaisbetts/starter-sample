using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ReactiveUI;
using ReactiveUI.Cocoa;
using Octokit;

namespace RxUISample.Views
{
    public partial class NotificationsListViewController : ReactiveTableViewController, IViewFor<NotificationsListViewModel>
    {
        public NotificationsListViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            ViewModel = new NotificationsListViewModel(RxApp.DependencyResolver.GetService<INotificationsClient>());
            ViewModel.LoadNotifications.Execute(null);

            TableView.RegisterNibForCellReuse(NotificationTileView.Nib, NotificationTileView.Key);
            TableView.Source = new ReactiveTableViewSource(TableView, ViewModel.Notifications, NotificationTileView.Key, 64.0f, cell => {
                Console.WriteLine(cell);
            });
        }

        NotificationsListViewModel _ViewModel;
        public NotificationsListViewModel ViewModel {
            get { return _ViewModel; }
            set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
        }

        object IViewFor.ViewModel {
            get { return _ViewModel; }
            set { ViewModel = (NotificationsListViewModel)value; }
        }
    }
}

