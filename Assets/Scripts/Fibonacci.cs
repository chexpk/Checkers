
using System.Collections;

public class Fibonacci : IEnumerator, IEnumerable
{
    private int prevPrev = -1;
    private int prev = 1;
    private int current;

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    public object Current
    {
        get
        {
            return current;
        }
    }

    public bool MoveNext()
    {
        current = prevPrev + prev;
        prevPrev = prev;
        prev = current;
        return true;
    }

    public void Reset()
    {
        prevPrev = -1;
        prev = 1;
    }
}
