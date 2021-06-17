
using System.Collections;

public class Range : IEnumerator, IEnumerable
{
    private int start;
    private int end;
    private int step;
    private int current;

    public Range(int start, int end, int step = 1)
    {
        this.start = start;
        this.end = end;
        this.step = step;
        current = start - step;
    }

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
        current += step;
        return (current <= end);
    }

    public void Reset()
    {
        current = start;
    }
}
