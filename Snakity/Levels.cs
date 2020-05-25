using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Snakity
{
    public static class Levels
    {
        private static readonly string[] DefaultLevels =
        {
            @"
######################
#.........#..........#
#.........#..........#
#....................#
#....................#
#.........#..........#
#.........#..........#
######################",
            @"
#############################
#...........................#
#.....##....................#
#.....##.......##...........#
#..............##.....##....#
#........##...........##....#
#........##................##
#..........................##
###..............##.........#
###..............##.........#
#############################",
            @"
######################
#....................#
#....................#
#....####....####....#
#....####....####....#
#....................#
#....................#
#....####....####....#
#....####....####....#
#....................#
#....................#
######################"
        };
        
        public static string[] levels { get; private set; }

        private const string LevelsDir = "levels";

        public static void Load()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(LevelsDir)) Directory.CreateDirectory(LevelsDir);
            if (!Directory.GetFiles(LevelsDir).Any())
                for (int i = 0; i < DefaultLevels.Length; i++) File.WriteAllText(Path.Combine(LevelsDir, $"lvl_{i + 1}.lvl"), DefaultLevels[i]);
            levels = Directory.GetFiles(LevelsDir, "*.lvl").Select(File.ReadAllText).ToArray();
        }
    }
}