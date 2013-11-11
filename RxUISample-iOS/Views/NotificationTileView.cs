using System;
using System.Drawing;
using System.Reactive.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ReactiveUI.Cocoa;
using ReactiveUI;

namespace RxUISample
{
    public partial class NotificationTileView : ReactiveTableViewCell, IViewFor<NotificationTileViewModel>
    {
        public static readonly UINib Nib = UINib.FromName("NotificationTileView", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("NotificationTileView");

        public NotificationTileView(IntPtr handle) : base(handle)
        {

        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.WhenAny(x => x.ViewModel, x => x.Value)
                .Where(x => x != null)
                .Subscribe(x => { 
                title.Text = x.Title; 
            });
        }

        public static NotificationTileView Create()
        {
            return (NotificationTileView)Nib.Instantiate(null, null)[0];
        }

        NotificationTileViewModel _ViewModel;
        public NotificationTileViewModel ViewModel {
            get { return _ViewModel; }
            set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
        }

        object IViewFor.ViewModel {
            get { return _ViewModel; }
            set { ViewModel = (NotificationTileViewModel)value; }
        }
    }
}

