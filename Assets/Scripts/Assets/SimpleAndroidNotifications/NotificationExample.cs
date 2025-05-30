// dnSpy decompiler from Assembly-CSharp.dll class: Assets.SimpleAndroidNotifications.NotificationExample
using System;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
	public class NotificationExample : MonoBehaviour
	{
		public void Rate()
		{
			Application.OpenURL("http://u3d.as/y6r");
		}

		public void OpenWiki()
		{
			Application.OpenURL("https://github.com/hippogamesunity/SimpleAndroidNotificationsPublic/wiki");
		}

		public void ScheduleSimple()
		{
			NotificationManager.Send(TimeSpan.FromSeconds(5.0), "Simple notification", "Customize icon and color", new Color(1f, 0.3f, 0.15f), NotificationIcon.Bell);
		}

		public void ScheduleNormal()
		{
			NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(5.0), "Notification", "Notification with app icon", new Color(0f, 0.6f, 1f), NotificationIcon.Message);
		}

		public void ScheduleCustom()
		{
			NotificationParams notificationParams = new NotificationParams
			{
				Id = UnityEngine.Random.Range(0, int.MaxValue),
				Delay = TimeSpan.FromSeconds(5.0),
				Title = "Custom notification",
				Message = "Message",
				Ticker = "Ticker",
				Sound = true,
				Vibrate = true,
				Light = true,
				SmallIcon = NotificationIcon.Heart,
				SmallIconColor = new Color(0f, 0.5f, 0f),
				LargeIcon = "app_icon"
			};
			NotificationManager.SendCustom(notificationParams);
		}

		public void CancelAll()
		{
			NotificationManager.CancelAll();
		}
	}
}
