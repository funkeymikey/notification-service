using Microsoft.AspNet.SignalR.Client;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Timers;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Notification.Core
{
    public class NotificationControlCenter
    {
        private String _appID;
        private String _appName;
        private HubConnection _hubConnection;
        private IHubProxy _proxy;
				//private Timer _heartbeatTimer;        
        
        public NotificationControlCenter(String appName, String appID)
        {
            //maybe the name can be the same as the id - i'm not sure
            _appID = appID;
            _appName = appName;

            //every thirty seconds, try and connect with the hub
						//_heartbeatTimer = new Timer(30 * 1000);
						//_heartbeatTimer.Elapsed += CheckHeartbeat;

            //put the shortcut on start menu if not there already
            ShortcutHelper.InitializeShortcut(_appName, _appID);

            Logger.Log("Proxy Loaded");
        }

        public async Task StartConnection()
        {
            try
            {
                Logger.Log(ConfigurationManager.AppSettings["service_url"]);
                //create the connection
                _hubConnection = new HubConnection(ConfigurationManager.AppSettings["service_url"]);
                _proxy = _hubConnection.CreateHubProxy("NotificationsHub");

                //when we get "notify", call AddNotification
                _proxy.On<NotificationMessage>("notify", AddNotification);

                //connect
                await _hubConnection.Start();

                //start the timer if it's not started
								//if (!_heartbeatTimer.Enabled)
								//		_heartbeatTimer.Start();

                Logger.Log("Connection started");
            }
            catch (Exception) { }
        }

        public void StopConnection()
        {
            _hubConnection.Dispose();
						//_heartbeatTimer.Dispose();

            Logger.Log("Connection closed");
        }

				//private async void CheckHeartbeat(object sender, ElapsedEventArgs e)
				//{
				//		//don't do anything if we're attempting to connect
				//		if (_hubConnection.State == ConnectionState.Connecting || _hubConnection.State == ConnectionState.Reconnecting)
				//				return;

				//		try
				//		{
				//				//try to contact the server
				//				await _proxy.Invoke("Heartbeat");
				//		}
				//		catch (Exception)
				//		{
				//				//failure, so toss the current connection in the garbage
				//				_hubConnection.Dispose();

				//				//try to reconnect
				//				this.StartConnection();
				//		}
				//}

        //invoked when the hub says "notify"
        private void AddNotification(NotificationMessage newNotification)
        {
            try
            {
                if (newNotification == null)
                    return;

                Logger.Log(DateTime.Now + " - " + newNotification);
                this.ShowToastNotification(newNotification);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }

        public void ShowToastNotification(NotificationMessage newNotification)
        {
            // Get a toast XML template
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            if (newNotification.Person != null)
                stringElements[0].AppendChild(toastXml.CreateTextNode(newNotification.Person));
            if (newNotification.Action != null)
                stringElements[1].AppendChild(toastXml.CreateTextNode(newNotification.Action));

            // Create the toast and show it
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier(_appID).Show(toast);
        }
        
    }
}
