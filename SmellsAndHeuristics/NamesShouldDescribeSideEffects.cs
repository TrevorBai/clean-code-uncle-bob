// This technique is very useful in my daily job,
// I actually might already apply to this principle already.
public ObjectOutputStream GetOosBad()
{
    if (_oos == null) _oos = new ObjectOutputStream(_socket.GetOutputStream());
    return _oos;    
}

// This method is so simple, but it does 2 things.
// So it should have a better name, even tho smart dev can read what it does.
// But smart dev should be lazy to NOT read as much as possible imo.
// I like Uncle bob's naming convention a lot, it just makes sense as an art.
// He just treats writing code as drawing an art, it's just beautiful and elegent.
// I sometimes feel like writing code is like wrting an english essay or poem, you
// are expressing yourself in a different language in the end. If you are writing in
// english, first of all you better are good at english right? Here, you can't write
// code in Chinese yet... And I think Chinese is so difficult to express things accurately
// and simple.
public ObjectOutputStream CreateOrReturnOos()
{
    if (_oos == null) _oos = new ObjectOutputStream(_socket.GetOutputStream());
    return _oos;
}
