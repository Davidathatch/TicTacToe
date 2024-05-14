using Microsoft.JSInterop;

namespace TicTacToe.Services;

/// <summary>
/// Class containing methods that invoke JS functions.
/// </summary>
/// <param name="jS">Js runtime being used</param>
public class JsInteropFunctions(IJSRuntime jS)
{
    /// <summary>
    /// Takes the id of a svg object and sends it to the js runtime to
    /// have styles applied.
    ///
    /// This method will register an event listener to the given svg to have
    /// its styles updated once it is fully loaded. It cannot be styled
    /// before it's document has been loaded.
    /// </summary>
    /// <param name="svgId">The id of the svg object</param>
    /// <returns>True if successful</returns>
    public async Task<bool> RegisterSvgTile(string svgId)
    {
        return await jS.InvokeAsync<bool>("registerSvg", svgId);
    }

    /// <summary>
    /// Takes the desired theme value and sends it to the js runtime to have the
    /// app's styles updated accordingly.
    /// </summary>
    /// <param name="themeValue">
    /// Value correlating to the desired theme.
    /// </param>
    /// <returns>True if theme is successfully updated, false otherwise</returns>
    public async Task<bool> SetTheme(string themeValue)
    {
        return await jS.InvokeAsync<bool>("setTheme", themeValue);
    }

    /// <summary>
    /// Takes the id of a svg tile and applies a flipping animation to it.
    ///
    /// This animation takes place when a tile is claimed.
    /// </summary>
    /// <param name="svgId">ID of svg being animated</param>
    public async void InvokeTileFlipAnimation(string svgId)
    {
        await jS.InvokeVoidAsync("invokeTileFlip", svgId);
    }

    /// <summary>
    /// Resets a claimed tile in preparation for a new round.
    /// This will reset all changes made by the tile flip animation.
    /// </summary>
    /// <param name="svgId">Id of tile being reset</param>
    public async void ResetTile(string svgId)
    {
        await jS.InvokeVoidAsync("resetTile", svgId);
    }

    /// <summary>
    /// Calls the onGameStarted function within the js runtime. This function
    /// initiates looping animations to the game's decorative background images.
    /// </summary>
    public async void OnGameStarted()
    {
        await jS.InvokeVoidAsync("onGameStarted");
    }

    /// <summary>
    /// Called to animate the game over dialog in or out of view.
    /// </summary>
    /// <param name="entrance">True if animating the dialog into view, false if animating out of view.</param>
    public async void ToggleGameOverDialog(bool entrance)
    {
        await jS.InvokeVoidAsync("toggleGameOverDialog", entrance);
    }
}