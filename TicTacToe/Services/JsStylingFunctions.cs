using System.Diagnostics;
using Microsoft.JSInterop;

namespace TicTacToe.Services;

/// <summary>
/// Class containing methods that invoke JS functions used to update
/// themes and styles in the app.
/// </summary>
/// <param name="jS">Js runtime being used</param>
public class JsStylingFunctions(IJSRuntime jS)
{
    /// <summary>
    /// Takes the id of a svg object and sends it to the js runtime to
    /// have styles applied.
    /// </summary>
    /// <param name="svgId">The id of the svg object</param>
    /// <returns>True if successful</returns>
    public async Task<bool> RegisterSvgTile(string svgId)
    {
        return await jS.InvokeAsync<bool>("registerSvg",  svgId);
    }

    /// <summary>
    /// Takes the desired theme value and sends it to the js runtime
    /// to be updated.
    /// </summary>
    /// <param name="themeValue">
    /// Value used to access desired theme, usually the name of the
    /// theme's color.
    /// </param>
    /// <returns>True if theme is successfully updated, false otherwise</returns>
    public async Task<bool> SetTheme(string themeValue)
    {
        return await jS.InvokeAsync<bool>("setTheme", themeValue);
    }
}