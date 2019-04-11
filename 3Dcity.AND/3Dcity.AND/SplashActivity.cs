using Android.App;
using Android.Content.PM;
using Android.OS;

// http://developer.xamarin.com/guides/android/user_interface/creating_a_splash_screen
namespace WindowsGame
{
	[Activity(Label = "3D City"
		, MainLauncher = true
		, NoHistory = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, AlwaysRetainTaskState = true
		, ScreenOrientation = ScreenOrientation.SensorLandscape)]
	public class SplashActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			StartActivity(typeof(GameActivity));
		}
	}

}