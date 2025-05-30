// dnSpy decompiler from Assembly-CSharp.dll class: Assets.SimpleAndroidNotifications.LocalNotficationAndroidNew
using System;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
	public class LocalNotficationAndroidNew : MonoBehaviour
	{
		private void Start()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				NotificationManager.CancelAll();
			}
			this.Number = UnityEngine.Random.Range(1, 3);
		}

		public void QuitYes()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				NotificationManager.CancelAll();
				this.NotifiSendCallFuncc();
			}
		}

		private void OnApplicationPause()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				NotificationManager.CancelAll();
				this.NotifiSendCallFuncc();
			}
		}

		private void NotifiSendCallFuncc()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				if (this.Number == 1)
				{
					NotificationParams notificationParams = new NotificationParams
					{
						Id = UnityEngine.Random.Range(0, int.MaxValue),
						Delay = TimeSpan.FromDays(1.0),
						Title = "Chance to win a new Train?",
						Message = "Come and Enjoy Driving Train",
						Ticker = "Ticker",
						Sound = true,
						Vibrate = true,
						Light = true,
						SmallIcon = NotificationIcon.Bell,
						SmallIconColor = new Color(0f, 0.5f, 0f),
						LargeIcon = "app_icon"
					};
					NotificationManager.SendCustom(notificationParams);
				}
				if (this.Number == 2)
				{
					NotificationParams notificationParams2 = new NotificationParams
					{
						Id = UnityEngine.Random.Range(0, int.MaxValue),
						Delay = TimeSpan.FromDays(1.0),
						Title = "You haven't completed today mission",
						Message = "Chance to win a new Train?",
						Ticker = "Ticker",
						Sound = true,
						Vibrate = true,
						Light = true,
						SmallIcon = NotificationIcon.Bell,
						SmallIconColor = new Color(0f, 0.5f, 0f),
						LargeIcon = "app_icon"
					};
					NotificationManager.SendCustom(notificationParams2);
				}
				if (this.Number == 3)
				{
					NotificationParams notificationParams3 = new NotificationParams
					{
						Id = UnityEngine.Random.Range(0, int.MaxValue),
						Delay = TimeSpan.FromDays(1.0),
						Title = "You haven't completed today mission?",
						Message = "Come and Enjoy Driving Train",
						Ticker = "Ticker",
						Sound = true,
						Vibrate = true,
						Light = true,
						SmallIcon = NotificationIcon.Bell,
						SmallIconColor = new Color(0f, 0.5f, 0f),
						LargeIcon = "app_icon"
					};
					NotificationManager.SendCustom(notificationParams3);
				}
				else
				{
					NotificationParams notificationParams4 = new NotificationParams
					{
						Id = UnityEngine.Random.Range(0, int.MaxValue),
						Delay = TimeSpan.FromDays(1.0),
						Title = "Train Driving is Amazing",
						Message = "Come and Enjoy Driving Train",
						Ticker = "Ticker",
						Sound = true,
						Vibrate = true,
						Light = true,
						SmallIcon = NotificationIcon.Bell,
						SmallIconColor = new Color(0f, 0.5f, 0f),
						LargeIcon = "app_icon"
					};
					NotificationManager.SendCustom(notificationParams4);
				}
				NotificationParams notificationParams5 = new NotificationParams
				{
					Id = UnityEngine.Random.Range(0, int.MaxValue),
					Delay = TimeSpan.FromDays(7.0),
					Title = "Train Driving is Amazing",
					Message = "Come and discover the most beautiful landscapes while driving the Train",
					Ticker = "Ticker",
					Sound = true,
					Vibrate = true,
					Light = true,
					SmallIcon = NotificationIcon.Bell,
					SmallIconColor = new Color(0f, 0.5f, 0f),
					LargeIcon = "app_icon"
				};
				NotificationManager.SendCustom(notificationParams5);
				NotificationParams notificationParams6 = new NotificationParams
				{
					Id = UnityEngine.Random.Range(0, int.MaxValue),
					Delay = TimeSpan.FromDays(15.0),
					Title = "Long Time No See?",
					Message = "New Trains are ready to unlock",
					Ticker = "Ticker",
					Sound = true,
					Vibrate = true,
					Light = true,
					SmallIcon = NotificationIcon.Bell,
					SmallIconColor = new Color(0f, 0.5f, 0f),
					LargeIcon = "app_icon"
				};
				NotificationManager.SendCustom(notificationParams6);
				NotificationParams notificationParams7 = new NotificationParams
				{
					Id = UnityEngine.Random.Range(0, int.MaxValue),
					Delay = TimeSpan.FromDays(30.0),
					Title = "Last Chance to win a new Train",
					Message = "Drive now and reach the destination in time so what are you waiting for?",
					Ticker = "Ticker",
					Sound = true,
					Vibrate = true,
					Light = true,
					SmallIcon = NotificationIcon.Bell,
					SmallIconColor = new Color(0f, 0.5f, 0f),
					LargeIcon = "app_icon"
				};
				NotificationManager.SendCustom(notificationParams7);
			}
		}

		private int Number;
	}
}
