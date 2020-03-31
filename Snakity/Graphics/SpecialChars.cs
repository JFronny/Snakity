namespace Snakity.Graphics
{
    public static class SpecialChars
    {
        public const char Enemy = '@';
        public const char Space = '\0';

        public static class Wall
        {
            // 2 connectors
            public const char UpDown = '║';
            public const char LeftRight = '═';
            public const char DownRight = '╔';
            public const char UpRight = '╚';
            public const char DownLeft = '╗';

            public const char UpLeft = '╝';

            // 3 connectors
            public const char UpDownLeft = '╣';
            public const char UpDownRight = '╠';
            public const char UpLeftRight = '╩';

            public const char DownLeftRight = '╦';

            // 4 connectors
            public const char UpDownLeftRight = '╬';
        }

        public static class Player
        {
            // 1 connectors
            public const char Up = '╵';
            public const char Down = '╷';
            public const char Left = '╴';

            public const char Right = '╶';

            // 2 connectors
            public const char UpDown = '│';
            public const char LeftRight = '─';
            public const char DownRight = '┌';
            public const char UpRight = '└';
            public const char DownLeft = '┐';

            public const char UpLeft = '┘';

            // 3 connectors
            public const char UpDownLeft = '┤';
            public const char UpDownRight = '├';
            public const char UpLeftRight = '┴';

            public const char DownLeftRight = '┬';

            // 4 connectors
            public const char UpDownLeftRight = '┼';
        }
    }
}