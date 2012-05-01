using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPDraughts
{
    public class AI
    {
        private GamePieceMove bestMove;
        private const int EASY = 2, NORMAL = 4, HARD = 8, FIRSTSTEP = 1;
        private int numberOfMovesAhead;
        private int alpha = 0, beta = Int32.MaxValue;

        public void SetGameDifficulty(Difficulty difficulty)
        {
            if (difficulty == Difficulty.Easy)
                numberOfMovesAhead = EASY;
            else if (difficulty == Difficulty.Normal)
                numberOfMovesAhead = NORMAL;
            else
                numberOfMovesAhead = HARD;
        }

        public List<GamePieceMove> GetAIMoves(GameBoard board)
        {            
            List<GamePieceMove> moves = new List<GamePieceMove>();
            MiniMax(board);
            moves.Add(bestMove);
            moves.AddRange(GetAddtionalMoves(board, bestMove));
            return moves;
        }

        private void MiniMax(GameBoard board)
        {            
            
            int bestValue = 0;
            List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.Black);
            foreach (GamePieceMove move in PossibleMoves)
            {
                GameBoard currentBoard = board.CloneBoard();
                currentBoard.ApplyMove(move);
                currentBoard = MakeAddtionalMoves(currentBoard, move).CloneBoard();                
                int currentValue = Min(currentBoard, FIRSTSTEP,ref alpha,ref beta);
                board.UnDoMove(move);
                if (currentValue > bestValue)
                {
                    bestMove = move;
                    bestValue = currentValue;
                }
            }
        }

        private int Max(GameBoard board, int step, ref int alpha, ref int beta)
        {            
            if (board.IsGameOver() || IsDepthReached(step))
                return board.EvaluateBoard(PlayerColours.Black);
            else
            {                
                int bestValue = 0;
                List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.Black);
                foreach (GamePieceMove move in PossibleMoves)
                {
                    GameBoard currentBoard = new GameBoard();
                    currentBoard = board.CloneBoard();
                    currentBoard.ApplyMove(move);
                    currentBoard = MakeAddtionalMoves(currentBoard, move).CloneBoard(); 
                    int currentValue = Min(currentBoard, step + 1, ref alpha, ref beta);
                    board.UnDoMove(move);
                    if (currentValue > bestValue)
                    {
                        bestValue = currentValue;
                        alpha = bestValue;
                    }
                    if (beta >= alpha)
                        return bestValue;
                }
                return bestValue;
            }
        }

        private int Min(GameBoard board, int step,ref int alpha,ref int beta)
        {
         if (board.IsGameOver() || IsDepthReached(step))
                return board.EvaluateBoard(PlayerColours.Black);
            else
            {                
                int bestValue = int.MaxValue;
                List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.White);
                foreach (GamePieceMove move in PossibleMoves)
                {
                    GameBoard currentBoard = new GameBoard();
                    currentBoard = board.CloneBoard();
                    currentBoard.ApplyMove(move);
                    currentBoard = MakeAddtionalMoves(currentBoard, move).CloneBoard(); 
                    int currentValue = Max(currentBoard, step + 1, ref alpha, ref beta);
                    board.UnDoMove(move);
                    if (currentValue < bestValue)
                    {
                        bestValue = currentValue;
                        beta = currentValue;
                    }
                    if (beta <= alpha)
                        return bestValue;

                }
                return bestValue;
            }
        }

        private GameBoard MakeAddtionalMoves(GameBoard board, GamePieceMove move)
        {
            List<GamePieceMove> addtionalMoves = new List<GamePieceMove>();
            GameBoard bestBoard = board.CloneBoard();
            int bestBoardValue = bestBoard.EvaluateBoard(PlayerColours.Black);
            
            if (move.IsJump && board.IsThereASecondJump(move))
                addtionalMoves.AddRange(board.GetAllPossibleJumpsForThisPiece(move.MovingPiece));
            foreach (GamePieceMove m in addtionalMoves)
            {
                GameBoard currentBoard = new GameBoard();
                currentBoard = board.CloneBoard();
                currentBoard.ApplyMove(m);
                currentBoard = MakeAddtionalMoves(currentBoard, m);
                if (currentBoard.EvaluateBoard(PlayerColours.Black) > bestBoardValue)
                    bestBoard = currentBoard;
                board.UnDoMove(m);
            }
            return bestBoard;
        }

        private List<GamePieceMove> GetAddtionalMoves(GameBoard board, GamePieceMove bestMove)
        {
            List<GamePieceMove> moves = new List<GamePieceMove>();
            List<GamePieceMove> bestMoves = new List<GamePieceMove>();
            int bestBoardValue = board.EvaluateBoard(PlayerColours.Black);

            GameBoard nextBoard = new GameBoard();
            nextBoard = board.CloneBoard();
            nextBoard.ApplyMove(bestMove);

            if (bestMove.IsJump)
                moves.AddRange(board.GetAllPossibleJumpsForThisPiece(bestMove.MovingPiece));
            foreach (GamePieceMove m in moves)
            {
                GameBoard currentBoard = new GameBoard();
                currentBoard = nextBoard.CloneBoard();
                currentBoard.ApplyMove(m);
                //currentBoard = MakeAddtionalMoves(currentBoard, m);
                if (currentBoard.EvaluateBoard(PlayerColours.Black) > bestBoardValue)
                    bestMoves.Add(m);
                bestMoves.AddRange(GetAddtionalMoves(currentBoard, m));
                board.UnDoMove(m);
            }
            board.UnDoMove(bestMove);
            return bestMoves;
        }        

        private Boolean IsDepthReached(int depth)
        {
            if (depth == numberOfMovesAhead)
                return true;
            else
                return false;
        }        
    }
}
