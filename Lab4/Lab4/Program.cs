using Lab4;

var products = new List<Product>
{
    new(900, 5, 4),
    new(400, 10, 7),
    new(800, 11, 6),
    new(200, 7, 4),
    new(150, 2, 2)
};

var solver = new NewSolver(products);
var position = new Vector(new List<double> { 100, 100, 100, 100, 100 });
double func = 0, lambda, prev;
double eps = 0.1;

List<(double func, Vector vector, Vector grad)> logs = 
    new List<(double func, Vector vector, Vector grad)>();

do
{
    prev = func;
    var gradient = -solver.GetDerivatives(position).Normalize();
    (func, lambda) = solver.MinFunc(position, gradient, eps/100);
    position += lambda * gradient;
    logs.Add((func, position, gradient));
} while (func - prev >= eps || func - prev <= -eps);

Console.WriteLine("function: ");
foreach (var log in logs)
{
    Console.WriteLine(log.func);
}
Console.WriteLine();

for (var i = 0; i < 5; i++)
{
    Console.WriteLine($"position[{i}]: ");
    foreach (var log in logs)
    {
        Console.WriteLine(log.vector[i]);
    }
    Console.WriteLine();
    
    Console.WriteLine($"gradient[{i}]: ");
    foreach (var log in logs)
    {
        Console.WriteLine(log.grad[i]);
    }
    Console.WriteLine();
}