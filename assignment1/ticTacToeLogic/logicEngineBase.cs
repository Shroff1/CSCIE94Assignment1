using System;
using System.Collections.Generic;
using System.Text;

namespace ticTacToeLogic
{
    /*Create a class with the following tempalte. 
         * { "move": 7, 
         *   "azurePlayerSymbol": "X", 
         *   "winner": "X", 
         *   "winPositions": [ 1, 4, 7 ], 
         *   "gameBoard":[ "O", "X", "O", "?", "X", "O", "?", "X", "?" ] 
         *   }
    */
  
    public class logicEngineBase
    {
        /// <summary>
        /// Gets the next move on the TicTacToe Gameboard.
        /// </summary>
        /// <value>
        /// The next move number position.
        /// </value>
        public int move { get; set; }

        /// <summary>
        /// Mentions whether the computer is a 'X' or 'O'
        /// </summary>
        /// <value>
        /// Mentions whether the computer is a 'X' or 'O'
        /// </value>
        public char azurePlayerSymbol { get; set; }

        /// <summary>
        /// Gives the winner based on the existing board situation
        /// </summary>
        /// <value>
        /// Gives the winner based on the existing board situation
        /// </value>
        public char winner { get; set; }

        /// <summary>
        /// Gives the three numberical position that shows the win
        /// </summary>
        /// <value>
        /// Gives the three numberical position that shows the win
        /// </value>
        public int[] winPositions { get; set; }

        /// <summary>
        /// Gives the new state of the gameboard once the move has been done. 
        /// </summary>
        /// <value>
        /// Gives the new state of the gameboard once the move has been done. 
        /// </value>
        public char[]  gameBoardResponse { get; set; }
    }
}
