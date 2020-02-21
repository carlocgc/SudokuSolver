using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace SudokuFu.Android
{
    [Activity(Label = "Game.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorPortrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class GameActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private AndroidGame _Game;

        private View _View;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _Game = new AndroidGame();
            _View = (View)_Game.Services.GetService(typeof(View));
            SetContentView(_View);
            _Game.Run();
        }

        #region Overrides of Activity

        public override void OnWindowFocusChanged(Boolean hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            if (hasFocus) SetImmersive();
        }

        #endregion

        private void SetImmersive()
        {
            _View.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutHideNavigation | SystemUiFlags.LayoutFullscreen | SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen | SystemUiFlags.ImmersiveSticky);
        }
    }
}

