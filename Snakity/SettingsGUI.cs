using System;

namespace Snakity
{
    public static class SettingsGui
    {
        public static void Show()
        {
            int currentSetting = 0;
            bool settingVals = true;
            while (settingVals)
            {
                Console.ResetColor();
                Console.Clear();
                DrawSwitch("Smooth Player", SettingsMan.SmoothPlayer, currentSetting == 0);
                DrawSwitch("Smooth Terrain", SettingsMan.SmoothTerrain, currentSetting == 1);
                DrawSwitch("Use Color", SettingsMan.Color, currentSetting == 2);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Enter:
                    case ConsoleKey.OemPlus:
                    case ConsoleKey.OemMinus:
                        settingVals = false;
                        break;
                        ;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.H:
                    case ConsoleKey.L:
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.NumPad6:
                        switch (currentSetting)
                        {
                            case 0:
                                SettingsMan.SmoothPlayer = !SettingsMan.SmoothPlayer;
                                break;
                            case 1:
                                SettingsMan.SmoothTerrain = !SettingsMan.SmoothTerrain;
                                break;
                            case 2:
                                SettingsMan.Color = !SettingsMan.Color;
                                break;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.K:
                    case ConsoleKey.NumPad8:
                        currentSetting--;
                        if (currentSetting < 0)
                            currentSetting = 2;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.J:
                    case ConsoleKey.Tab:
                    case ConsoleKey.NumPad2:
                        currentSetting++;
                        if (currentSetting > 2)
                            currentSetting = 0;
                        break;
                }
            }

            Console.ResetColor();
        }

        private static void DrawSwitch(string text, bool enabled, bool selected)
        {
            Console.ForegroundColor =
                selected ? ConsoleColor.Black : ConsoleColor.White;
            Console.BackgroundColor =
                selected ? ConsoleColor.White : ConsoleColor.Black;
            Console.WriteLine(text);
            Console.ForegroundColor =
                enabled ? ConsoleColor.Black : ConsoleColor.White;
            Console.BackgroundColor =
                enabled ? ConsoleColor.White : ConsoleColor.Black;
            Console.Write("Yes ");
            Console.ForegroundColor =
                enabled ? ConsoleColor.White : ConsoleColor.Black;
            Console.BackgroundColor =
                enabled ? ConsoleColor.Black : ConsoleColor.White;
            Console.WriteLine("No");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}