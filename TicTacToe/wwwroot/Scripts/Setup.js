window.applyGridStyling = (boardSize) => {
    var boardElement = document.getElementById('board-grid');
    var boardTileElements = boardElement.children;
    var currentRow = 1;
    var currentColumn = 1;

    for (let i = 0; i <= boardTileElements.length; i++) {
        boardTileElements[i].style.gridRow = currentRow;
        boardTileElements[i].style.gridColumn = currentColumn;

        currentColumn++;

        //If all tiles in this row have been processed, move back to the first column
        if (currentColumn === boardSize + 1)
            currentColumn = 1 ;

        //If currentColumn is set to zero, the previous row has been processed
        if (currentColumn === 1)
            currentRow++;
    }
}