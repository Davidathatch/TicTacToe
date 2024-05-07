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

//Current theme, pink by default. Index zero represents the theme's name
var currentTheme = ['pink', pinkThemeColors];

// .NET INVOKABLE FUNCTIONS

/**
 * Sets the value of currentTheme.
 * 
 * Each theme is stored in the themes dictionary:
 * Key = name of theme's color (ex: pink theme's key is 'pink')
 * @param theme Key for desired key
 */
window.setTheme = (theme) => {
    if (!themes.hasOwnProperty(theme))
        return false;
    
    //Remove the current theme from the body
    let body = document.getElementsByTagName('body')[0];
    body.classList.remove(getThemeClassName(currentTheme[0]));
    
    //Update currentTheme
    currentTheme = [theme, themes[theme]];
    body.classList.add(getThemeClassName(currentTheme[0]))
    
    return true;
}

/**
 * Retrieves the svg object with the given id and sets it to call a method
 * once fully loaded. The method being called will apply the appropriate
 * gradient fill.
 * @param svgId id of the svg object
 */
window.registerSvg = (svgId) => {
    document.getElementById(svgId).addEventListener('load', applyTileGradient);
    return true;
}


// PRIVATE FUNCTIONS

/**
 * Returns the file path of the svg representing a tile claimed by the player
 * with the given symbol.
 * @param playerSymbol Symbol representing player claiming this tile
 * @returns {string} File path the appropriate svg
 */
function getTileSvgPath(playerSymbol) {
    return `Assets/svgs/claimed-square-${playerSymbol.toLowerCase()}`;
}

/**
 * Returns the class name that contains the colors for a desired theme in styles.css.
 * @param themeName Name of the theme
 * @returns {string} Class name correlating to desired theme
 */
function getThemeClassName(themeName) {
    return `${themeName}-theme`;
}

/**
 * This event is called once a svg object is loaded. It will apply the 
 * correct gradient fill.
 * @param e Event the invoked this function
 */
function applyTileGradient(e) {
    var tileDoc = e.currentTarget.contentDocument;
    tileDoc.getElementById("gradient-start-color").setAttribute('style', `stop-color:${currentTheme[1].primary}`);
    tileDoc.getElementById("gradient-end-color").setAttribute('style', `stop-color:white`);
}