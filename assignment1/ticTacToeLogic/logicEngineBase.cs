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
            public int move { get; set; }
            public char azurePlayerSymbol { get; set; }
            public char winner { get; set; }
            public int[] winPositions { get; set; }
            public char[] gameBoardResponse { get; set;}
    }
}
