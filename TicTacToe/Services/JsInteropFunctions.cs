using System.Diagnostics;
using Microsoft.JSInterop;

namespace TicTacToe.Services;

/// <summary>
/// Class containing methods that invoke JS functions used to update
/// themes and styles in the app.
/// </summary>
/// <param name="jS">Js runtime being used</param>
public class JsInteropFunctions(IJSRuntime jS)
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

    /// <summary>
    /// Takes the id of a svg object that has already loaded on the client and applies
    /// a gradient fill.
    /// </summary>
    /// <param name="svgId">Id of svg being altered</param>
    public async void ApplyGradientToLoaded(string svgId)
    {
        await jS.InvokeVoidAsync("applyGradientToLoaded", svgId);
    }

    /// <summary>
    /// Takes the id of an svg tile and begins a flipping animation.
    /// </summary>
    /// <param name="svgId">Id of svg being animated</param>
    public async void InvokeTileFlipAnimation(string svgId)
    {
        await jS.InvokeVoidAsync("invokeTileFlipAnimation", svgId);
    }

    /// <summary>
    /// Resets a claimed tile in preparation for a new round.
    /// </summary>
    /// <param name="svgId">Id of tile being reset</param>
    public async void ResetTile(string svgId)
    {
        await jS.InvokeVoidAsync("resetTile", svgId);
    }
}