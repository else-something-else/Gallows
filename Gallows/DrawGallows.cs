using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallows
{
    public static class DrawGallows
    {
        private static readonly string[][] gallows =
    {
        new string[]
        {
            @"  _______",
            "  |     |",
            "  |",
            "  |",
            "  |",
            "  |",
            "__|__"
        },
        new string[]
        {
            @"  _______",
            "  |     |",
            "  |     O",
            "  |",
            "  |",
            "  |",
            "__|__"
        },
        new string[]
        {
            @"  _______",
            "  |     |",
            "  |     O",
            "  |     |",
            "  |     |",
            "  |",
            "__|__"
        },
        new string[]
        {
            @"  _______",
            "  |     |",
            "  |     O",
            "  |    /|",
            "  |     |",
            "  |",
            "__|__"
        },
        new string[]
        {
            "  _______",
            "  |     |",
            "  |     O",
            "  |    /|\\",
            "  |     |",
            "  |",
            "__|__"
        },
        new string[]
        {
            @"  _______",
            "  |     |",
            "  |     O",
            "  |    /|\\",
            "  |     |",
            "  |    /",
            "__|__"
        },
        new string[]
        {
            @"  _______",
            "  |     |",
            "  |     O",
            "  |    /|\\",
            "  |     |",
            "  |    / \\",
            "__|__"
        }
    };

        public static void Draw(int errorCount)
        {
            int index = Math.Min(errorCount, gallows.Length - 1);
            foreach (var line in gallows[index])
            {
                Console.WriteLine(line);
            }
        }
    }
}
