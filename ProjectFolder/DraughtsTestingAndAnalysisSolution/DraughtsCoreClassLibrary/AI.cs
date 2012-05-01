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
        public int numberOfMovesAhead, minCalls, maxCalls;
        private int alpha = 0, beta = Int32.MaxValue;
        public DateTime startTime, endTime;

        public void MiniMax(GameBoard board)
        {
            startTime = System.DateTime.Now;
            maxCalls = 0;
            minCalls = 0;
            int bestValue = 0;
            List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.Black);
            foreach (GamePieceMove move in PossibleMoves)
            {
                int currentValue = Min(CreateCurrentBoard(board, move), FIRSTSTEP, ref alpha, ref beta);
                board.UnDoMove(move);
                if (currentValue > bestValue)
                {
                    bestMove = move;
                    bestValue = currentValue;
                }
            }
            endTime = System.DateTime.Now;
        }

        private int Max(GameBoard board, int step, ref int alpha, ref int beta)
        {
            maxCalls++;
            if (board.IsGameOver() || IsDepthReached(step))
                return board.EvaluateBoard(PlayerColours.Black);
            else
            {                
                int bestValue = 0;
                List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.Black);
                foreach (GamePieceMove move in PossibleMoves)
                {
                    int currentValue = Min(CreateCurrentBoard(board, move), step + 1, ref alpha, ref beta);
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

        public int Min(GameBoard board, int step,ref int alpha,ref int beta)
        {
            minCalls++;
            if (board.IsGameOver() || IsDepthReached(step))
                return board.EvaluateBoard(PlayerColours.Black);
            else
            {                
                int bestValue = int.MaxValue;
                List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.White);
                foreach (GamePieceMove move in PossibleMoves)
                {
                    int currentValue = Max(CreateCurrentBoard(board, move), step + 1, ref alpha, ref beta);
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

        private GameBoard CreateCurrentBoard(GameBoard board, GamePieceMove move)
        {
            GameBoard currentBoard = new GameBoard();
            currentBoard = board.CloneBoard();
            currentBoard.ApplyMove(move);
            currentBoard = MakeAddtionalMoves(currentBoard, move).CloneBoard();
            return currentBoard;
        }

        public void MiniMaxWithoutAlphaBetaPruning(GameBoard board)
        {
            startTime = System.DateTime.Now;
            maxCalls = 0;
            minCalls = 0;
            int bestValue = 0;
            List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.Black);
            foreach (GamePieceMove move in PossibleMoves)
            {
                int currentValue = MinWithoutAlphaBetaPruning(CreateCurrentBoard(board, move), FIRSTSTEP);
                board.UnDoMove(move);
                if (currentValue > bestValue)
                {
                    bestMove = move;
                    bestValue = currentValue;
                }
            }
            endTime = System.DateTime.Now;
        }

        private int MaxWithoutAlphaBetaPruning(GameBoard board, int step)
        {
            maxCalls++;
            if (board.IsGameOver() || IsDepthReached(step))
                return board.EvaluateBoard(PlayerColours.Black);
            else
            {
                int bestValue = 0;
                List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.Black);
                foreach (GamePieceMove move in PossibleMoves)
                {
                    int currentValue = MinWithoutAlphaBetaPruning(CreateCurrentBoard(board, move), step + 1);
                    board.UnDoMove(move);
                    if (currentValue > bestValue)
                        bestValue = currentValue;                        
                }
                return bestValue;
            }
        }

        public int MinWithoutAlphaBetaPruning(GameBoard board, int step)
        {
            minCalls++;
            if (board.IsGameOver() || IsDepthReached(step))
                return board.EvaluateBoard(PlayerColours.Black);
            else
            {
                int bestValue = int.MaxValue;
                List<GamePieceMove> PossibleMoves = board.GetAllPossibleMoves(PlayerColours.White);
                foreach (GamePieceMove move in PossibleMoves)
                {
                    int currentValue = MaxWithoutAlphaBetaPruning(CreateCurrentBoard(board, move), step + 1);
                    board.UnDoMove(move);
                    if (currentValue < bestValue)
                        bestValue = currentValue;
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
            foreach (GamePieceMove moves in addtionalMoves)
            {
                GameBoard currentBoard = CreateCurrentBoard(board, moves);
                if (currentBoard.EvaluateBoard(PlayerColours.Black) > bestBoardValue)
                    bestBoard = currentBoard;
                board.UnDoMove(moves);
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
                if (currentBoard.EvaluateBoard(PlayerColours.Black) > bestBoardValue)
                    bestMoves.Add(m);
                bestMoves.AddRange(GetAddtionalMoves(currentBoard, m));
                board.UnDoMove(m);
            }
            board.UnDoMove(bestMove);
            return bestMoves;
        }

        public void SetGameDifficulty(Difficulty difficulty)
        {
            if (difficulty == Difficulty.Easy)
                numberOfMovesAhead = EASY;
            else if (difficulty == Difficulty.Normal)
                numberOfMovesAhead = NORMAL;
            else
                numberOfMovesAhead = HARD;
        }

        private Boolean IsDepthReached(int depth)
        {
            if (depth == numberOfMovesAhead)
                return true;
            else
                return false;
        }

        public List<GamePieceMove> GetAIMoves(GameBoard board)
        {   
            List<GamePieceMove> moves = new List<GamePieceMove>();
            MiniMax(board);
            moves.Add(bestMove);
            moves.AddRange(GetAddtionalMoves(board, bestMove));
            return moves;
        }
    }
}
