namespace Lab4;

public class Solver
{
    private const int Seed = 40;
    private static readonly Random Random = new(Seed);
    
    private readonly List<Product> _products;
    private readonly int _n;

    public Solver(List<Product> products)
    {
        _products = products;
        _n = products.Count;
    }
    
    public Vector Solve(double epsilon, int maxErrorsCount, double stepStart, double stepRatio)
    {
        var step = stepStart;   // lambda
        
        var x = new Vector(_n);

        var iteration = 0;  // r
        var mistakes = 0;   // k

        do
        {
            Console.WriteLine($"Iteration {iteration}: X = {x} L(X) = {L(x)} Step = {step}");

            iteration_start:
            Vector y;
            do
            {
                var psi = GetRandomVector(_n);
                y = x;// + step * (psi / psi.Normalize());
            } while (y.Any(v => v < 0));


            if (L(y) < L(x))
            {
                if (mistakes > 0)
                {
                    Console.WriteLine($"Mistakes: {mistakes}");
                }

                Console.WriteLine("Success.");

                x = y;
                iteration++;
                mistakes = 0;
            }
            else
            {
                mistakes++;

                if (mistakes == maxErrorsCount)
                {
                    Console.WriteLine($"Mistakes: {mistakes}");

                    step *= stepRatio;

                    mistakes = 0;

                    if (step < epsilon)
                    {
                        Console.WriteLine($"Step = {step} < {epsilon}\r\nEnd.");
                        return x;
                    }
                }
                else
                {
                    goto iteration_start;
                }
            }
        } while (true);
    }

    public double L(Vector x)
    {
        ArgumentNullException.ThrowIfNull(x);
        
        if (x.Length != _n)
        {
            throw new ArgumentException("Invalid Q vector.", nameof(x));
        }
        
        var sum = 0.0;

        for (var i = 0; i < x.Length; i++)
        {
            sum += SubFunc(i);
        }

        return sum;
        
        double SubFunc(int index)
        {
            var product = _products[index];

            return product.K * product.V / x[index] + product.S * x[index] / 2;
        }
    }

    private static Vector GetRandomVector(int count)
    {
        return new Vector(Enumerable.Repeat(0.0, count).Select(_ => Random.NextDouble() * 2 - 1));
    }
}