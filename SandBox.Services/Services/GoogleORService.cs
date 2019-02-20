using Google.OrTools.LinearSolver;
using SandBox.Services.Services.Interfaces;
using System;

namespace SandBox.Services.Services
{
    public class GoogleORService : IGoogleORService
    {
        //private readonly IIrisRepository _irisRepository;

        public GoogleORService(IServiceProvider serviceProvider)
        {
            //_irisRepository = serviceProvider.GetService<IIrisRepository>(); 
        }

        public void RunLinearProgrammingExample(String solverType)
        {
            Solver solver = Solver.CreateSolver("IntegerProgramming", solverType);
            // Create the variables x and y.
            Variable x = solver.MakeNumVar(0.0, 1.0, "x");
            Variable y = solver.MakeNumVar(0.0, 2.0, "y");
            // Create the objective function, x + y.
            Objective objective = solver.Objective();
            objective.SetCoefficient(x, 1);
            objective.SetCoefficient(y, 1);
            objective.SetMaximization();
            // Call the solver and display the results.
            solver.Solve();
            Console.WriteLine("Solution:");
            Console.WriteLine("x = " + x.SolutionValue());
            Console.WriteLine("y = " + y.SolutionValue());
        }
    }
}
