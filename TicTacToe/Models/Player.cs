namespace TicTacToe.Models
{
    public class Player(char symbol)
    {
        /// <summary>
        /// The symbol representing this player. This is the symbol
        /// that will appear on tiles claimed by this player.
        /// </summary>
        public char Symbol { get; set; } = symbol;

        /// <summary>
        /// True if this player has won the current round.
        /// </summary>
        public bool Winner { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Player)) return false;
            return (obj as Player)!.Symbol == this.Symbol;
        }
    }
}
