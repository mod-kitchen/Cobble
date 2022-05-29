using Cobble.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cobble
{
    public static class Game
    {
        public static string Title { get; set; } = "Cobble";
        public static Platform Platform { get; private set; }
        public static Display Display { get; private set; }

        public static void Start(Platform platform)
        {
            Platform = platform;
            Display = new Display();
        }
    }
}
