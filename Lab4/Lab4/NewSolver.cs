using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Lab4;

internal class NewSolver
{
    private readonly List<Product> _products;

    public NewSolver(List<Product> products)
    {
        _products = products;
    }

    public Vector GetDerivatives(Vector coords)
    {
        var result = new Vector(5)
        {
            [0] = 4 - (9000 + 4 * coords[0] * coords[0]) / (2 * coords[0] * coords[0]),
            [1] = 7 - (8000 + 7 * coords[1] * coords[1]) / (2 * coords[1] * coords[1]),
            [2] = 6 - (17600 + 6 * coords[2] * coords[2]) / (2 * coords[2] * coords[2]),
            [3] = 4 - (2800 + 4 * coords[3] * coords[3]) / (2 * coords[3] * coords[3]),
            [4] = 2 - (600 + 2 * coords[4] * coords[4]) / (2 * coords[4] * coords[4])
        };
        return result;
    }
    public (double funcValue, double lambda) MinFunc(Vector position, Vector gradient, double eps)
    {
        double lambda = eps, delta, prev = Calc(position), cur;
        do
        {
            cur = Calc(position + lambda * gradient);
            delta = prev - cur;
            prev = cur;
            lambda += eps;
            //Console.WriteLine($"{lambda}, {prev}, {cur} {Environment.NewLine}");
        } while (delta > 0);
        
        return (prev, lambda - eps);

        // Fibonacci
        //
        // int[] fibonacciNums = new int[19];
        //
        // fibonacciNums[0] = 1;
        // fibonacciNums[1] = 1;
        // for (int i = 2; i < 19; i++)
        // {
        //     fibonacciNums[i] += fibonacciNums[i - 1] + fibonacciNums[i-2];
        //     Console.WriteLine(fibonacciNums[i-1] + "  ");
        // }
        //
        // const int N = 16;
        // double a = 0, b = 100;
        // double[] x1, x2;
        // double lambda1, lambda2;
        // double f1, f2;
        // double epsilon = eps;
        //
        // if (epsilon < (b-a)/fibonacciNums[17])
        // {
        //     epsilon = (b - a) / fibonacciNums[17] - 0.000000000001;
        // }
        //
        // x1 = new double[15];
        // Console.WriteLine(epsilon);
        //
        // int k = 0;
        // for (int j = 1; j < 15; j++)
        // {
        //     x1[k] = a + fibonacciNums[N - j - 1] / fibonacciNums[N - j + 1] * (b - a) - ((-1) ^
        //          (N - j + 1)) / fibonacciNums[N - j + 1] * epsilon;
        //     Console.WriteLine(x1[k]);
        //     k++;
        // }
        
        // for (var i = 0; i < 100; i++)
        // {
        //     _writer.WriteLine($"{i} - {Calc(position + i * gradient):0.00}");
        // }
        // _writer.WriteLine();
        
        // double a = 0, b = 100;
        // double lambda1, lambda2;
        // double f1, f2;
        // const double F1 = 0.382, F2 = 0.618;
        // do
        // {
        //     lambda1 = a + F1 * (b - a);
        //     lambda2 = a + F2 * (b - a);
        //     f1 = Calc(position + (lambda1 * gradient));
        //     f2 = Calc(position + (lambda2 * gradient));
        //     if (f1 < f2)
        //         b = lambda2;
        //     else
        //         a = lambda1;
        // } while (lambda2 - lambda1 >= 1);
        // return ((f2 + f1) / 2, (lambda1 + lambda2) / 2);
    }

    public double Calc(Vector position)
    {
        double result = 0;
        for (var i = 0; i < 5; i++)
        {
            result += (2 * _products[i].K * _products[i].V
                  + _products[i].S * position[i] * position[i]) / (2 * position[i]);
        }
        return result;
    }
}