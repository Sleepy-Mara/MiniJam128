using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
    AndroidNotificationChannel notifChannel;
    [SerializeField] private List<String> notifMessages;
    [SerializeField] private List<String> notifTitle;
    [SerializeField] private int notifTime;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_channel",
            Name = "Reminder Notifications",
            Description = "This is my description",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        string nextNotifTitle = notifTitle[UnityEngine.Random.Range(0,notifTitle.Count)], nextNotifMessage = notifMessages[UnityEngine.Random.Range(0, notifMessages.Count)];
        DisplayNotification(nextNotifTitle, nextNotifMessage, DateTime.Now.AddSeconds(notifTime));
    }
    public int DisplayNotification(string title, string text, DateTime fireTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = "";
        notification.LargeIcon = "";
        notification.FireTime = fireTime;

        return AndroidNotificationCenter.SendNotification(notification, notifChannel.Id);
    }
    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }
}
