using System.Diagnostics;

namespace TicTacToe.Models.ConstantModels;

/// <summary>
/// This class contains constant string values used throughout the app.
/// </summary>
public class GameConstants
{
    /// <summary>
    /// Values used to set the app's theme.
    ///
    /// Each app theme has a correlating value (typically the name of its primary color).
    /// Passing this theme value to the JS runtime will set the app to the appropriate
    /// theme.
    /// </summary>
    public class ThemeValues
    {
        public static string PinkTheme = "pink";
        public static string BlueTheme = "blue";
        public static string OrangeTheme = "orange";

        //The default theme will be applied initially
        public static string Default = "orange";
    }

    /// <summary>
    /// File paths used to access svgs
    /// </summary>
    public class SvgPaths
    {
        /// <summary>
        /// File path to the svg representing an unclaimed tile.
        /// </summary>
        public static string BlankTile = "Assets/svgs/board-tile.svg";

        /// <summary>
        /// Returns the path for the svg containing the claimingPlayer's
        /// symbol. This will be used to represent a tile that has been claimed.
        /// </summary>
        /// <param name="claimingPlayer">Player who this tile will represent</param>
        /// <returns>File path to appropriate svg</returns>
        public static string GetTilePathFor(Player claimingPlayer)
        {
            return $"Assets/svgs/claimed-square-{char.ToLower(claimingPlayer.Symbol)}.svg";
        }

        /// <summary>
        /// Given the value of a theme, return a list of the decorative svgs designed
        /// for that theme.
        /// </summary>
        /// <param name="themeValue">Value of the theme being applied</param>
        /// <returns>A list of file paths to local svgs</returns>
        public static List<string>? GetDecorativeSvgPaths(string themeValue)
        {
            switch (themeValue)
            {
                case "pink":
                    return
                    [
                        "Assets/decorative/pink-theme-decorative-squiggle.svg",
                        "Assets/decorative/pink-theme-decorative-donut.svg"
                    ];
                    break;

                case "blue":
                    return
                    [
                        "Assets/decorative/blue-theme-decorative-circle.svg",
                        "Assets/decorative/blue-theme-decorative-square.svg"
                    ];
                    break;

                case "orange":
                    return
                    [
                        "Assets/decorative/orange-theme-decorative-circle.svg",
                        "Assets/decorative/orange-theme-decorative-star.svg"
                    ];
                    break;

                default:
                    return new List<string>();
                    break;
            }
        }
    }
}