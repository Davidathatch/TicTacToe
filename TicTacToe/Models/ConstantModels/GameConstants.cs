namespace TicTacToe.Models.ConstantModels;

/// <summary>
/// This class contains constant string values used throughout the app.
/// </summary>
public class GameConstants
{
    /// <summary>
    /// Values used to set the app's theme.
    /// </summary>
    public class ThemeValues
    {
        public static string PinkTheme = "pink";
        public static string BlueTheme = "blue";
        public static string OrangeTheme = "orange";
    }

    /// <summary>
    /// File paths used to access svgs
    /// </summary>
    public class SvgPaths
    {
        public static string BlankTile = "Assets/svgs/board-tile.svg";

        /// <summary>
        /// Returns the path for the svg containing the claimingPlayer's
        /// symbol.
        /// </summary>
        /// <param name="claimingPlayer">Player who will be represented by the returned tile</param>
        /// <returns>File path to appropriate svg</returns>
        public static string GetPathFor(Player claimingPlayer)
        {
            return $"Assets/svgs/claimed-square-{char.ToLower(claimingPlayer.Symbol)}";
        }
    }
}