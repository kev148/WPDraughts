using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPDraughts;

namespace DraughtsGamePerformanceAnalysisProject
{
    class PerformanceAnalysis
    {
        AI ai = new AI();
        GameBoard gameBoard;

        public void TwoStepMinMax()
        {
            Console.WriteLine(" Two Step Minmax method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = 2;
            ai.MiniMax(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }

        public void FourStepMinMax()
        {
            Console.WriteLine(" Four Step Minmax method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = 4;
            ai.MiniMax(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }

        public void EightStepMinMax()
        {
            Console.WriteLine(" Eight Step Minmax method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = 8;
            ai.MiniMax(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }

        public void UntilGameOverMinMax()
        {
            Console.WriteLine(" Until Gameover Minmax method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = int.MaxValue;
            ai.MiniMax(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }

        public void TwoStepMinMaxWithoutAlphaBetaPruning()
        {
            Console.WriteLine(" Two Step Minmax without Alpha Beta Pruning method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = 2;
            ai.MiniMaxWithoutAlphaBetaPruning(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }

        public void FourStepMinMaxWithoutAlphaBetaPruning()
        {
            Console.WriteLine(" Four Step Minmax without Alpha Beta Pruning method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = 4;
            ai.MiniMaxWithoutAlphaBetaPruning(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }

        public void EightstepMinMaxWithoutAlphaBetaPruning()
        {
            Console.WriteLine(" Eight Step Minmax without Alpha Beta Pruning method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = 8;
            ai.MiniMaxWithoutAlphaBetaPruning(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }

        public void UntilGameOverMinMaxWithoutAlphaBetaPruning()
        {
            Console.WriteLine(" Until Gameover Minmax without Alpha Beta Pruning method executed");
            gameBoard = new GameBoard();
            ai.numberOfMovesAhead = int.MaxValue;
            ai.MiniMaxWithoutAlphaBetaPruning(gameBoard);
            Console.WriteLine("Calls to Min method " + ai.minCalls);
            Console.WriteLine("Calls to Max method " + ai.maxCalls);
            Console.WriteLine("Time taken " + (ai.endTime - ai.startTime));
        }
    }
}


