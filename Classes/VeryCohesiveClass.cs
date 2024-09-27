// We want to write high cohesive class and method. Or our goal is to 
// write maximally cohesive classes.
public class Stack
{
    private int _topOfStack = 0;
    private readonly List<int> _elements = new LinkedList<int>();

    // Only use one variable, so NOT maximally cohesive.
    public int Size() { return _topOfStack; }

    // This method use both variables as well, so maximally cohesive.
    public void Push(int element)
    {
        _topOfStack++;
        _elements.Add(element); // Add to the end
    }

    // This mothod uses both _topOfStack and _elements, so it's maximally cohesive.
    public int Pop() 
    {
        if (_topOfStack == 0) throw new PoppedWhenEmptyException();
        int element = _elements[--_topOfStack];
        _elements.RemoveLast(); // Remove from the end
        return element;
    } 
}
