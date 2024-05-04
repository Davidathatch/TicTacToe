const blueThemeColors = {
    "primary": "#B3C8CF",
    "primaryLight": "#D6E8EE",
    "secondary": "#99BC85",
    "secondaryLight": "#E1F0DA"
};

const pinkThemeColors = {
    "primary": "#D77FA1",
    "primaryLight": "#F0C1D3",
    "secondary": "#8E7AB5",
    "secondaryLight": "#DFCCFB"
};

const orangeThemeColors = {
    "primary": "#EFB495",
    "primaryLight": "#FFDFCE",
    "secondary": "#BB8493",
    "secondaryLight": "#F3D0D7"
}

const themes = {
    "blue": blueThemeColors,
    "pink": pinkThemeColors,
    "orange": orangeThemeColors
};

var currentTheme = blueThemeColors;

window.setTheme = (theme) => {
    currentTheme = themes[theme];
    // applyStartStyling();
}

window.onload = function() {
    setTheme('pink');
}

/**
 * Returns the file path of the svg representing a tile claimed by the player
 * with the given symbol.
 * @param playerSymbol Symbol representing player claiming this tile
 * @returns {string} File path the appropriate svg
 */
function getTileSvg(playerSymbol) {
    return `Assets/svgs/claimed-square-${playerSymbol}`;
}

/**
 * Retrieves the svg object with the given id and sets it to call a method
 * once fully loaded. The method being called will apply the appropriate
 * gradient fill.
 * @param svgId id of the svg object
 */
window.registerSvg = (svgId) => {
    document.getElementById(svgId).addEventListener('load', applyTileGradient);
}

/**
 * This event is called once a svg object is loaded. It will apply the 
 * correct gradient fill.
 * @param e Event the invoked this function
 */
function applyTileGradient(e) {
    var tileDoc = e.currentTarget.contentDocument;
    tileDoc.getElementById("gradient-start-color").setAttribute('style', `stop-color:${currentTheme.primary}`);
    tileDoc.getElementById("gradient-end-color").setAttribute('style', `stop-color:white`);
}