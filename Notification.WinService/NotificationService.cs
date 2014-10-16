using Notification.Core;
using System.ServiceProcess;

namespace Notification.WinService
{
    public partial class NotificationService : ServiceBase
    {
        NotificationControlCenter _notificationControlCenter;
        public NotificationService()
        {
            Logger.Log("Starting up the windows service");
            InitializeComponent();
            _notificationControlCenter = new NotificationControlCenter("Toast Notification Service", "Toast.Notification.Service");
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("OnStart");
            _notificationControlCenter.StartConnection();
        }

        protected override void OnStop()
        {
            Logger.Log("OnStop");
            _notificationControlCenter.StopConnection();
        }
    }
}
