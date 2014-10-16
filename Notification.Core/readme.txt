To Install:

Build the Notification.WinService project
Copy bin/debug contents to c:\apps\notification-service
Open up a admin command prompt
> sc create NotificationService binPath=C:\apps\notification-service\Notification.WinService.exe start=auto
Go to computer management --> services
Change the user logon for the service to be me

To delete service: sc delete NotificationService

For debugging / development, delete service, and reinstall right from the bin/debug folder