using System;

namespace TicTacToe;

public class TicTacToe
{
    const string Circle = "O";
    const string Cross = "X";


    public static void StartGame(string firstTurn)
    {
        var isCircleTurn = firstTurn == Circle;
        var field = new[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        var gameResult = GetGameResult(Cross, field);
        
        for (var i = 0; i < 9; i++)
        {
            var playerSign = isCircleTurn ? Circle : Cross;
            
            PrintField(field);
            Console.Write("\nХод " + playerSign + ": ");
            
            var turn = Console.ReadLine();

            if (turn != null && !CorrectTurn(turn, field, playerSign))
            {
                Console.Write("Неверный ход\n");
                continue;
            }
            isCircleTurn = !isCircleTurn;
            gameResult = GetGameResult(playerSign, field);
            if (gameResult != GameResult.Draw)
            {
                PrintField(field);
                break;
            }
        }
        
        Console.WriteLine(gameResult);
    }
    
    public static void PrintField(string[,] field)
    {
        Console.WriteLine(new string('-', 13));
        var row = field.GetUpperBound(0) + 1;
        var col = field.GetUpperBound(1) + 1;
        
        for (var i = 0; i < row; i++)
        {
            for (var j = 0; j < col; j++)
            {
                Console.Write("| ");
                if (field[i, j] == Circle)
                    Console.BackgroundColor = ConsoleColor.Blue;
                else if (field[i, j] == Cross)
                    Console.BackgroundColor = ConsoleColor.Red;
            
                Console.Write(field[i, j]);
                Console.ResetColor();
                Console.Write(" ");
            }
            Console.WriteLine("|\n" + new string('-', 13));
        }
    }

    public static bool CorrectTurn(string turn, string[,] field, string playerSign)
    {
        var row = field.GetUpperBound(0) + 1;
        var col = field.GetUpperBound(1) + 1;
        
        for (var i = 0; i < row; i++)
        {
            for (var j = 0; j < col; j++)
            {
                if (turn == field[i, j] && field[i, j] != playerSign)
                {
                    field[i, j] = playerSign;
                    return true;
                }
            }
        }
        return false;
    }

    public static GameResult GetGameResult(string turn, string[,] field)
    {
        if (HasWinSequence(field, turn) && turn == Cross) 
            return GameResult.CrossWin;
        if (HasWinSequence(field, turn) && turn == Circle) 
            return GameResult.CircleWin;
        
        return GameResult.Draw;
    }
    
    public static bool HasWinSequence(string[,] field, string turn)
    {
        for (var i = 0; i < 3; i++) {
            if (HasWinTrio(field[i, 0], field[i, 1], field[i, 2], turn)) 
                return true;
            
            if (HasWinTrio(field[0, i], field[1, i], field[2, i], turn)) 
                return true;
            
            if (HasWinTrio(field[0,0], field[1, 1], field[2,2], turn)) 
                return true;
            
            if (HasWinTrio(field[2,0], field[1, 1], field[0,2], turn)) 
                return true;
        }
        
        return false;
    }
    
    public static bool HasWinTrio(string mark1, string mark2, string mark3, string turn) 
    {
        return ((mark1 == mark2) && (mark1 == mark3) && (mark1 == turn));
    }
}