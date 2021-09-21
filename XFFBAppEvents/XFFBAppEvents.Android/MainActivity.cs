using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Java.Security;
using Prism;
using Prism.Ioc;
using Xamarin.Facebook.AppEvents;
using static Android.Content.PM.PackageManager;

namespace XFFBAppEvents.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));

            try
            {

                PackageInfo info = PackageManager.GetPackageInfo(
                            "ph.alonph.alon.divepro",
                            PackageInfoFlags.Signatures);
                foreach (var signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA");
                    md.Update(signature.ToByteArray());
                    Log.Debug("KeyHash:", Base64.EncodeToString(md.Digest(), Android.Util.Base64Flags.Default));
                    var s = Base64.EncodeToString(md.Digest(), Android.Util.Base64Flags.Default);
                }
            }
            catch (NameNotFoundException e)
            {

            }
            catch (NoSuchAlgorithmException e)
            {

            }

            AppEventsLogger logger = AppEventsLogger.NewLogger(this);
            logger.LogEvent("test_mark");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

