using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraughtsGamePerformanceAnalysisProject
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformanceAnalysis performanceAnalysis = new PerformanceAnalysis();

            performanceAnalysis.TwoStepMinMax();
            performanceAnalysis.FourStepMinMax();
            performanceAnalysis.EightStepMinMax();
            performanceAnalysis.UntilGameOverMinMax();
            performanceAnalysis.TwoStepMinMaxWithoutAlphaBetaPruning();
            performanceAnalysis.FourStepMinMaxWithoutAlphaBetaPruning();
            performanceAnalysis.EightstepMinMaxWithoutAlphaBetaPruning();            

            //Console.ReadLine();
        }
    }
}
