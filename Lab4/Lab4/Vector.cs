using System.Collections;
using System.Globalization;

namespace Lab4;

public class Vector : IEnumerable<double>
{
    private readonly double[] _values;

    public int Length => _values.Length;

    public double this[int index]
    {
        get => _values[index];
        set => _values[index] = value;
    }

    public Vector(int length)
    {
        _values = new double[length];
    }

    public Vector(IEnumerable<double> values)
    {
        _values = values.ToArray();
    }

    public Vector Normalize() => this / Lengths;
    public double Lengths => Math.Sqrt(this.Select(v => v * v).Sum());

    public static Vector operator *(Vector vector, double value)
    {
        return new Vector(vector.Select(v => v * value));
    }

    public static Vector operator *(double value, Vector vector) => vector * value;
    
    public static Vector operator /(Vector vector, double value)
    {
        return new Vector(vector.Select(v => v / value));
    }
    
    public static Vector operator /(double value, Vector vector) => vector / value;
    
    public static Vector operator +(Vector vector1, Vector vector2)
    {
        if (vector1.Length != vector2.Length)
        {
            throw new ArgumentException();
        }

        return new Vector(Enumerable.Range(0, vector1.Length).Select(i => vector1[i] + vector2[i]));
    }

    public static Vector operator -(Vector vector)
    {
        return new Vector(vector.Select(v => -v));
    }

    public static Vector operator -(Vector vector1, Vector vector2) => vector1 + (-vector2);

    public IEnumerator<double> GetEnumerator() => ((IEnumerable<double>)_values).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"({string.Join(", ", _values.Select(v => v.ToString("0.00", CultureInfo.InvariantCulture)))})";
    }
}