using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class MobileNotifications : MonoBehaviour
{
    private static MobileNotifications instance = null;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyChildOnLoad(this.gameObject);

        // Make sure we have the perms to send notifications
        requestAuthorization();

        // Remove Notifications that have already been displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        // SetUp Notification Channel for Adnroid
        setupNotificationChannel();

        // Reminder Notifications
        reminderNotifications_24Hours();
        reminderNotifications_7Days();

    }

    private void requestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    private void setupNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notification Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

    }

    private void reminderNotifications_24Hours()
    {
        // Setup of Notification and its parameters!
        var notification = new AndroidNotification();
        notification.Title = "Hey! We Need Your Help!";
        notification.Text = "Come and help Save the princess! We need your help saving the world!";
        notification.FireTime = System.DateTime.Now.AddHours(24);

        // Send Notifcation Via Channel Id
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        // if the script is run and a message is already scheduled
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    private void reminderNotifications_7Days()
    {
        // Setup of Notification and its parameters!
        var notification = new AndroidNotification();
        notification.Title = "Hey! We Need Your Help!";
        notification.Text = "Hope is getting lost without you, our saviour!";
        notification.FireTime = System.DateTime.Now.AddDays(7);

        // Send Notifcation Via Channel Id
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        // if the script is run and a message is already scheduled
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    public void sendLivesFullNotification(double FiringInTime)
    {
        var notification = new AndroidNotification();
        notification.Title = "Your Lives Are Back!";
        notification.Text = "You now have 5 shinny lives! Come help save the princess!!!";
        notification.FireTime = System.DateTime.Now.AddSeconds(FiringInTime);

        // Send Notifcation Via Channel Id
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        // if the script is run and a message is already scheduled
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }

        return;
    }

    public void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform;

        while (parentTransform.parent != null)
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }

        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(parentTransform.gameObject);
        }
        else
        {
            Destroy(base.gameObject);
        }
    }
}
