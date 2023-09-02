using System.Collections.Generic;

namespace Casino.Domain.Utils
{
    public static class Constants
    {
        public static List<int> GetRedNumbers() => new() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

        public static List<int> GetBlackNumbers() => new() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

        public static List<int> GetFirstDozen() => new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        public static List<int> GetSecondDozen() => new() { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

        public static List<int> GetThirdDozen() => new() { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
    }
}
