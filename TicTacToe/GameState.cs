using TicTacToe.Models;

namespace TicTacToe
{
    public class GameState
    {
        //TODO: Keep track of stats for successive games

        /// <summary>
        /// Player one.
        /// </summary>
        public Player PlayerOne { get; set; }

        /// <summary>
        /// Player two.
        /// </summary>
        public Player PlayerTwo { get; set; }

        /// <summary>
        /// Player whose turn it is currently.
        /// </summary>
        public Player CurrentPlayer { get; set; }

        /// <summary>
        /// If the game is over and a player won, this property will.
        /// reference the winner.
        /// </summary>
        public Player? Winner { get; set; }

        /// <summary>
        /// Represents the status of the game.
        /// </summary>
        public GameStatus Status { get; set; }

        /// <summary>
        /// The board the game is being played on.
        /// </summary>
        public Board GameBoard { get; set; }

        /// <summary>
        /// Invoked when this state is restarted.
        /// </summary>
        public event Action OnRestart;

        /// <summary>
        /// Create a new GameState
        /// </summary>
        /// <param name="playerOne">First player, will play first</param>
        /// <param name="playerTwo">Second player</param>
        /// <param name="boardSize">Size of the board</param>
        public GameState(Player playerOne, Player playerTwo, int boardSize)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            CurrentPlayer = playerOne;
            
            GameBoard = new(boardSize);
            GameBoard.OnGameOver += ProcessGameEnd;
            
            Status = new GameStatus.AwaitingStart();
        }
        
        /// <summary>
        /// Updates the current player and returns the player whose
        /// turn just ended.
        /// </summary>
        /// <returns>Player whose turn just ended</returns>
        public Player RegisterTurn()
        {
            if (Equals(CurrentPlayer, PlayerOne))
            {
                CurrentPlayer = PlayerTwo;
                return PlayerOne!;
            }

            CurrentPlayer = PlayerOne;
            return PlayerTwo!;
        }

        /// <summary>
        /// Process the game's end.
        /// </summary>
        private void ProcessGameEnd()
        {
            //If the winning component property is not null, there was a winner
            if (GameBoard.WinningComponent is not null)
            {
                Status = new GameStatus.Won(GameBoard.WinningComponent.ClaimedBy!);
                Winner = GameBoard.WinningComponent.ClaimedBy;
                return;
            }

            //Otherwise, the game ended in a tie.
            Status = new GameStatus.Tied();
        }

        /// <summary>
        /// Restarts this state in preparation for another game.
        /// </summary>
        /// <param name="onRestart">Method to be called once the state has been reset</param>
        public void Restart()
        {
            //Reset the starting player
            CurrentPlayer = PlayerOne;

            //Reset the board
            GameBoard.ResetBoard();

            //Set the status
            Status = new GameStatus.Ongoing();
            OnRestart?.Invoke();
        }
    }
}
