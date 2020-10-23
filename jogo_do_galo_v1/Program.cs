using System;

namespace jogo_do_galo_v1
{
    class Program
    {
        public static char[,] board = new char[3, 3];
        public static char player_one_piece, player_two_piece;

        static void initBoard() // Initialize Board
        {
            for(int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    board[r, c] = 'E'; //E = Empty
                }
            }
        }

        static void displayBoard() // Display Board
        {
            Console.WriteLine("   | 1 | 2 | 3 |");
            Console.WriteLine("----------------");
            for (int r = 0; r < 3; r++)
            {
                Console.Write(" {0}", r + 1);
                for (int c = 0; c < 3; c++)
                {
                    char piece;
                    if (board[r, c] == 'E')
                        piece = ' ';
                    else
                        piece = board[r, c];

                    if(c == 2)
                    {
                        Console.WriteLine(" | " + piece + " | ");
                    } else
                    {
                        Console.Write(" | " + piece);
                    }
                }
                Console.WriteLine("----------------");
            }
        }

        static int insertPiece(char piece, int row, int column) // Insert Piece in board
        {
            if(board[row - 1, column - 1] == 'E')
            {
                board[row - 1, column - 1] = piece;
                return 0;
            }

            return 2;
        }

        static bool checkHorizontal(char piece, int row) // Check Horizontally for three consecutive pieces
        {
            int aux = 0;
            for(int c = 0; c < 3; c++)
            {
                if (board[row, c] == piece)
                    aux++;
            }

            if (aux == 3)
                return true;
            else
                return false;
        }

        static bool checkVertical(char piece, int column) // Check Vertically for three consecutive pieces
        {
            int aux = 0;
            for(int r = 0; r < 3; r++)
            {
                if (board[r, column] == piece)
                    aux++;
            }

            if (aux == 3)
                return true;
            else
                return false;
        }

        static bool checkDiagonal(char piece) // Check Diagonally for three consecutive pieces
        {
            int aux = 0;
            int npieces;
            for(int r = 0; r < 3; r++)
            {
                npieces = 0;
                if(board[r, 0] == piece)
                {
                    for(int c = 0; c < 3; c++)
                    {
                        if (r == 0 && c != 0) { 
                            aux++; 
                        } else if (r == 2 && c != 0)
                        {
                            aux--;
                        }
                        if (board[r + (aux), c] == piece) { 
                            npieces++; 
                        }
                        if (npieces == 3)
                        {
                            return true;
                        }
                        else if(c == 2)
                        {
                            aux = 0;
                        }
                    }
                }
            }
            return false;
        }

        static bool win(char piece, int row, int column)
        {
            if(row == -1)
                return false;

            bool valid = false;
            if (checkVertical(piece, column) || checkHorizontal(piece, row) || checkDiagonal(piece))
                valid = true;
            
            return valid;
        }

        static void Main(string[] args)
        {
            initBoard();

            // Choose piece X or O
            int aux = 0; // Used for validations
            do
            {
                if (aux > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Piece is not available choose valid piece");
                }
                Console.WriteLine("Choose Your Piece X or O");
                Console.Write("Player One: ");
                player_one_piece = Console.ReadKey().KeyChar;
                aux++;
            } while (Char.ToUpper(player_one_piece) != 'X' && Char.ToUpper(player_one_piece) != 'O');

            Console.Clear();

            if (Char.ToUpper(player_one_piece) == 'X')
                player_two_piece = 'O';
            else
                player_two_piece = 'X';

            string turn = null;
            char piece;
            int row, column, nplays;
            
            aux = 0;
            nplays = 0; // number of plays to define if it's a tie

            // Play
            do
            {
                Console.Clear();
                row = -1;
                column = -1;

                if (turn == "Player Two" || turn is null)
                {
                    turn = "Player One";
                    piece = player_one_piece;
                }
                else
                {
                    turn = "Player Two";
                    piece = player_two_piece;
                }

                displayBoard();
                try
                {
                    if (aux == 1) // If aux value is 1 then there was a input error
                    {
                        nplays--;
                        Console.WriteLine("Invalid Input");
                    }
                    else if (aux == 2) // If aux value is 2 then the field choosed is filled player looses turn
                    {
                        nplays--;
                        Console.WriteLine("Field already filled");
                    }

                    Console.WriteLine("Your Turn {0}", turn);
                    Console.Write("Row: ");
                    row = Int32.Parse(Console.ReadLine());
                    Console.Write("Column: ");
                    column = Int32.Parse(Console.ReadLine());

                    aux = insertPiece(piece, row, column);
                    nplays++;
                } catch
                {
                    aux = 1;
                }
                
            } while (!win(piece, row - 1, column - 1) && nplays < 9);

            Console.Clear();
            displayBoard();
            if(nplays == 9)
                Console.WriteLine("It's a tie!");
            else
                Console.WriteLine("You Win {0}!", turn);
        }
    }
}
