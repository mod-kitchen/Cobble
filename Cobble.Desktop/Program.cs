using System.Runtime.InteropServices;

Cobble.System.Platform platform;
bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
bool isMac = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
bool isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
if (isMac) platform = Cobble.System.Platform.MAC;
else if (isLinux) platform = Cobble.System.Platform.LINUX;
else if (isWindows) platform = Cobble.System.Platform.WINDOWS;
else platform = Cobble.System.Platform.UNKNOWN;

Cobble.Game.Start(platform);
return 0;