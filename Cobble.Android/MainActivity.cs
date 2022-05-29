using System.Diagnostics;
using Android.App;
using Silk.NET.Windowing.Sdl.Android;

namespace Cobble.Android;

[Activity(MainLauncher = true)]
public class MainActivity : SilkActivity
{
    public static MainActivity Activity { get; private set; }
    protected override void OnRun()
    {
        Activity = this;
        Game.Start(System.Platform.ANDROID);
    }
}