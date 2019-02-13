using System;

namespace ticTacToeLogic
{
    public class logicEngine : logicEngineBase
    {

        private int score;
        private char[] gameBoardIncome;
        private logicEngineBase answer = new logicEngineBase();
        private char oppSymbol;
        private int[] locWinPos = new int[3];


        /*Create executemove function, 
         * this move will take json values as a char array and will 
         * check for any errors (3 in particular) if there are errors it will return
         * 
         * if no error, this code will execute the other code to determine the next 
         * move and will return the struct that contains the move and values that need 
         * to be returned. 
         * 
         * errors: 1) has to be x, o or ?
         *  2) cant have more than 9 values 
         *  3)cant have more than 3x or 3 os. 
         *  4)cant have continuous amount of x and no 0s at all. 
         *  
         *  http error status bad request 400
        */

        public logicEngineBase ExecuteMove(char[] inputPayload)
        {

            gameBoardIncome = inputPayload;
            this.answer.gameBoardResponse = inputPayload;

            symbolProperty(inputPayload);
            immediateWinner(inputPayload);

            int m = moveproperty(inputPayload);
            //the new move is registered and the move position is also estbalished in the response.
            inputPayload[m] = answer.azurePlayerSymbol;
            answer.move = m;

            gameBoardIncome = inputPayload;
            this.answer.gameBoardResponse = inputPayload;

            return this.answer;

        }


        //checking if someone has aalready won


        public bool moveLeft(char[] input)
        {
            //this checks if there is any empty spaces on the board. if none left then tie.
            for (int i = 0; i < 9; i++)
            {
                if (input[i] != '?')
                {
                    answer.winner = ' ';
                    return true;
                }
            }
            return false;
        }



        public int immediateWinner(char[] a)
        {
            // Checking for Rows for X or O victory. 
            for (int i = 0; i < 9; i += 3)
            {
                if (a[i] == a[i + 1] && a[i + 1] == a[i + 2])
                {
                    //int [] numb = [i, i + 1, i + 2];
                    //Todo i need to extract this repettition to winner property. 
                    //winPosition([i,i+1,i+2])
                    locWinPos[0] = i + 1;
                    locWinPos[1] = i + 2;
                    locWinPos[2] = i + 3;
                    answer.winPositions = locWinPos;

                    if (a[i] == this.answer.azurePlayerSymbol)
                    {
                        this.answer.winner = this.answer.azurePlayerSymbol;
                        return 100;
                    }
                    else
                    {
                        this.answer.winner = oppSymbol;
                        return -100;
                    }
                }
            }

            // Checking for Columns for X or O victory. 
            for (int j = 0; j < 3; j++)
            {
                if (a[j] == a[j + 3] && a[j + 3] == a[j + 6])
                {
                    locWinPos[0] = j + 1;
                    locWinPos[1] = j + 4;
                    locWinPos[2] = j + 7;
                    answer.winPositions = locWinPos;

                    if (a[j] == this.answer.azurePlayerSymbol)
                    {
                        this.answer.winner = this.answer.azurePlayerSymbol;
                        return 100;
                    }
                    else
                    {
                        this.answer.winner = oppSymbol;
                        return -100;
                    }
                }
            }

            // Checking for Diagonals for X or O victory. 

            if (a[0] == a[4] && a[4] == a[8])
            {

                locWinPos[0] = 1;
                locWinPos[1] = 5;
                locWinPos[2] = 9;
                answer.winPositions = locWinPos;

                if (a[0] == this.answer.azurePlayerSymbol)
                {
                    this.answer.winner = this.answer.azurePlayerSymbol;
                    return 100;
                }
                else
                {
                    this.answer.winner = oppSymbol;
                    return -100;
                }

            }

            if (a[2] == a[4] && a[4] == a[6])
            {

                locWinPos[0] = 3;
                locWinPos[1] = 5;
                locWinPos[2] = 7;
                answer.winPositions = locWinPos;


                if (a[2] == this.answer.azurePlayerSymbol)
                {
                    this.answer.winner = this.answer.azurePlayerSymbol;
                    return 100;
                }
                else
                {
                    this.answer.winner = oppSymbol;
                    return -100;
                }
            }
            return 0;
        }


        /*
         * the heuristics function assigns points to each space on a ticTicToe board and in the move property
         * we evaluate which space provides the highest number to make our next move.
         */

        public int heuristics(char[] game, int depth, bool ismax)
        {
            int temp = immediateWinner(game);

            // If Maximizer has won the game return his/her 
            // evaluated score 
            if (temp == 100)
                return temp;

            // If Minimizer has won the game return his/her 
            // evaluated score 
            if (temp == -100)
                return temp;

            // If there are no more moves and no winner then 
            // it is a tie 
            if (moveLeft(game) == false)
                return 0;

            if (ismax)
            {
                 temp = -100;

                // Traverse all locations on the board.
                for (int i = 0; i < 9; i++)
                {
                    // Check if cell is empty 
                    if (game[i] == '?')
                    {
                        // Make the move 
                        game[i] = answer.azurePlayerSymbol;

                        // Call minimax recursively

                        temp = Math.Max(temp, (heuristics(game, depth + 1, !ismax)));

                        // Undo the move 
                        game[i] = '?';
                    }
                }
                return temp;
            }
            else
            {
                temp = 100;

                // Traverse all locations on the board.
                for (int j = 0; j < 9; j++)
                {
                    // Check if cell is empty 
                    if (game[j] == '?')
                    {
                        // Make the move 
                        game[j] = oppSymbol;

                        // Call minimax recursively

                        temp = Math.Min(temp, (heuristics(game, depth + 1, !ismax)));

                        // Undo the move 
                        game[j] = '?';
                    }
                }
                return temp;
            }
        }


        /*Move Property
         * 
         * This returns the position selected on the game board. That is the most. 
         * ideal position
         * 
         * if there is alredy a winner or tie this function will give null
         * 
         * if not won then the move should be placed and the winner property needs 
         * to be updated along the way
         * 
         */


        // This function returns true if there are moves 
        // remaining on the board. It returns false if 
        // there are no moves left to play. 

        int moveproperty(char[] games)
        {
            int bestmove = -100;
            int move = 0;

            for (int i = 0; i < 9; i++)
            {
                if (games[i] == '?')
                {
                    games[i] = this.answer.azurePlayerSymbol;

                    int mValue = heuristics(games, 0, false);

                    games[i] = '?';

                    if (mValue > bestmove)
                    {
                        move = i;
                        bestmove = mValue;
                    }
                }
            }
            return move;
        }


        /*Symbol Property
         * Compuuter will be x if moved first 
         * 
         * computer wil be o if moved second
         * 
         * if caller determines a x or o then computer will be the alternate 
         * 
         */

        public void symbolProperty(char[] input)
        {
            int tempX = 0;
            int tempO = 0;
            for (int i = 0; i < 9; i++)
            {
                if (input[i] == 'X')
                {
                    tempX++;
                }

                if (input[i] == 'O')
                {
                    tempO++;
                }
            }


            //assumed if odd then azure player moved first
            if (tempO % 2 == 0)
            {
                this.answer.azurePlayerSymbol = 'O';
            }
            else
            {
                this.answer.azurePlayerSymbol = 'X';
            }

            /*
             * assumed if even then azure moved first but if both o and x are event then priority is given to x
             * if both o and c are odd then azure symbol is o
             */
            if (tempX % 2 == 0)
            {
                this.answer.azurePlayerSymbol = 'X';
            }
            else
            {
                this.answer.azurePlayerSymbol = 'O';
            }

            //if there is no move then the first move will be azure player and it will be x
            if ((tempX == 0 || tempO == 0))
            {
                this.answer.azurePlayerSymbol = 'X';
            }

            if ((tempX == tempO))
            {
                this.answer.azurePlayerSymbol = 'X';
            }
            //the opposition symbol will be opposite
            if (this.answer.azurePlayerSymbol == 'X')
            {
                this.oppSymbol = 'O';
            }
            else if (this.answer.azurePlayerSymbol == 'O')
            {
                this.oppSymbol = 'X';
            }
            else
            {
                this.oppSymbol = 'N';
            }

        }


        /*Winner Property
         * 
         * indicates if its a tie, x win inconclusive or o win. This updates 
         * the struct
         * 
         */

        /*winPosition 
         * 
         * array of the win positions or null wil be written if no one wins
         * 
         * 
         */
        /*
                public void winPosition(int [] nums)
                {
                    for(int i = 0; i<4; i++)
                    {
                        this.answer.winPositions[i] = nums[i];
                    }  
                }

                /*gameboard property 
                 * 
                 * this is the final situation of the gameboard in json format that will 
                 * be filled into the struct above. Again this is the final position after
                 * the move 
                 * 
                 */
    }
}




