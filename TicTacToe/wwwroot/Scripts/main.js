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
    applyStartStyling();
}

window.onload = function() {
    setTheme('pink');
}

/**
 * Goes through each tile and assigns to it the appropriate position in the grid.
 * @param boardSize The size of the grid (always square)
 */
window.applyGridStyling = (boardSize) => {
    var boardElement = document.getElementById('board-grid');
    var boardTileElements = boardElement.children;
    var currentRow = 1;
    var currentColumn = 1;

    for (let i = 0; i < boardTileElements.length; i++) {
        boardTileElements[i].style.gridRow = currentRow;
        boardTileElements[i].style.gridColumn = currentColumn;

        currentColumn++;

        //If all tiles in this row have been processed, move back to the first column
        if (currentColumn === boardSize + 1)
            currentColumn = 1;

        //If currentColumn is set to zero, the previous row has been processed
        if (currentColumn === 1)
            currentRow++;
    }
}

/**
 * Changes the appropriate tile such that it displays the symbol of the
 * player who claimed it.
 * @param tileId Id by which the tile element can be accessed
 * @param playerSymbol Symbol representing claiming player
 */
window.applyClaimedTileStyling = (tileId, playerSymbol) => {
    var claimedTile = document.getElementById(tileId);
    var maskUrl = `url(../Assets/TilePieces/clipping-tile-${playerSymbol.toUpperCase()})`;
    claimedTile.style.maskImage = maskUrl;
}

window.createSvgs = () => {
    var tileContainers = document.getElementsByClassName('tile-container');
    
    for (let i = 0; i < tileContainers.length; i++) {
        var newTileSvg = document.createElement('object');
        newTileSvg.setAttribute('data', 'Assets/svgs/claimed-square-x.svg');
        newTileSvg.setAttribute('type', 'image/svg+xml');
        newTileSvg.classList.add('board-tile');
        
        newTileSvg.addEventListener('load', (e) => {applyTileGradient(e.currentTarget)});
        
        tileContainers[i].appendChild(newTileSvg);
    }
}

/**
 * Given a tile svg element, edit its gradient to have the appropriate colors.
 * @param tile Tile being edited
 */
function applyTileGradient(tile) {
    var tileDoc = tile.contentDocument;
    tileDoc.getElementById("gradient-start-color").setAttribute('style', `stop-color:${currentTheme.primary}`);
    tileDoc.getElementById("gradient-end-color").setAttribute('style', `stop-color:white`);
}