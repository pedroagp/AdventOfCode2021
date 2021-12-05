namespace AdventOfCode2021.Common {
    internal static class Utils {
        internal static bool IsStraightLine(int[] coords) {
            return (coords[0] == coords[2] || coords[1] == coords[3]);
        }

    }
}
