using System;
namespace WeGameSdk.Api
{
    public class ExitEventArgs  : EventArgs
    {
        public static int EXIT_TYPE_CHANNEL = 0;
        public static int EXIT_TYPE_GAME = 1;

        public int Type { get; set; }
    }
}
