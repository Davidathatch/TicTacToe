//Dictionaries containing each themes colors as hex codes.
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

//Contains each available theme
const themes = {
    "blue": blueThemeColors,
    "pink": pinkThemeColors,
    "orange": orangeThemeColors
};

//Current theme, pink by default. Index zero represents the theme's key value, index 1 is the theme itself.
var currentTheme = ['pink', pinkThemeColors];

// .NET INVOKABLE FUNCTIONS

/**
 * Sets the value of currentTheme.
 *
 * Each theme is stored in the themes' dictionary. Keys correlating to
 * each theme can be accessed in .NET from the GameConstants class.
 * @param themeValue Key for desired theme
 */
window.setTheme = (themeValue) => {
    if (!themes.hasOwnProperty(themeValue))
        return false;

    //Remove the current theme from the body
    let body = document.getElementsByTagName('body')[0];
    body.classList.remove(getThemeClassName(currentTheme[0]));

    //Update currentTheme
    currentTheme = [themeValue, themes[themeValue]];
    body.classList.add(getThemeClassName(currentTheme[0]))

    return true;
}

/**
 * Retrieves the svg object with the given id and adds a 'load' event listener.
 * Once the svg has been loaded, it will call applyGradientToLoaded to have
 * a gradient fill added to itself. The gradient fill will match the current theme.
 * @param svgId id of the svg object
 */
window.registerSvg = (svgId) => {
    document.getElementById(svgId).addEventListener('load', (e) => { applyGradientToLoaded(svgId); });
    return true;
}

/**
 * Applies the correct gradient fill to a svg that has already been loaded.
 * @param {any} svgId Id of the svg being altered
 */
window.applyGradientToLoaded = (svgId) => {
    let svgDoc = document.getElementById(svgId).contentDocument;
    svgDoc.getElementById("gradient-start-color").setAttribute('style', `stop-color:${currentTheme[1]['primary']}`);
    svgDoc.getElementById("gradient-end-color").setAttribute('style', `stop-color:white`);
}

/**
 * Applies a card flipping animation to the tile with the given ID, along
 * with a sound effect.
 * @param {any} svgId ID of the tile
 */
window.invokeTileFlip = (svgId) => {
    //Get the tile being animated
    var tile = document.getElementById(svgId);

    //Perform tile flipping animation
    anime({
        targets: tile,
        scale: [{ value: 1 }, { value: 1.4 }, { value: 1, delay: 250 }],
        rotateY: { value: "+=180", delay: 200 },
        easing: "easeInOutSine",
        duration: 400,
        begin: function(anim) {
            let soundEffect = new Audio("Assets/audio/card-flip-audio.mp4");
            soundEffect.play();
        }
    });
}

/**
 * Resets changes made to a claimed tile by the tile flipping animation.
 * This is done in preparation for a new round.
 * @param {any} svgId ID of the tile being reset
 */
window.resetTile = (svgId) => {
    //Get the tile being reset
    var tile = document.getElementById(svgId);

    //Reset the y-axis rotation
    anime({
        targets: tile,
        rotateY: { value: "-=180" },
        duration: 0
    });
}

/**
 * Called when the game has started and the page has been rendered.
 * This function adds a subtle looping animation to the background svgs.
 */
window.onGameStarted = () => {
    var backgroundImages = document.getElementsByClassName("bg-image");
    var currentDuration = 3000;
    var currentYTranslation = 20;
    var currentXTranslation = 10;

    for (let i = 0; i < backgroundImages.length; i++) {
        anime({
            targets: backgroundImages[i],
            translateY: currentYTranslation,
            translateX: currentXTranslation,
            loop: true,
            direction: 'alternate',
            easing: 'easeInOutSine',
            duration: currentDuration
        });

        currentDuration += 500;
        var temp = currentXTranslation;
        currentXTranslation = currentYTranslation;
        currentYTranslation = temp;
    }
}

/**
 * Animates the game over dialog in or out of view.
 * @param {any} entrance True if dialog is being animated into view, false if being animated out of view.
 */
window.toggleGameOverDialog = (entrance) => {
    var gameOverDialog = document.getElementById('game-over-dialog');

    //If the dialog is being animated into view
    if (entrance) {
        gameOverDialog.style.right = 'auto';
        anime({
            targets: '#game-over-dialog',
            targets: '#game-over-dialog',
            left: '50%',
            duration: 3000,
            easing: 'easeInOutElastic(1, .6)'
        });
    }
    //If the dialog is being animated out of view.
    else {
        gameOverDialog.style.left = 'auto';
        anime({
            targets: '#game-over-dialog',
            right: '-100%',
            duration: 3000,
            easing: 'spring(1, 80, 10, 0)',
            complete: function(anim) {
                document.getElementById('game-over-dialog').style.left = '-100%';
            }
        });
    }
}


// PRIVATE FUNCTIONS

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
function applyTileGradientOnLoad(e) {
    applyGradientToLoaded(e.id);
}