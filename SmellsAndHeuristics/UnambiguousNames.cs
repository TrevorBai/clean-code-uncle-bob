// Names are very bad
private string DoRename()
{
    if (_refactorReferences) RenameReferences();
    RenamePage();

    _pathToRename.RemoveNameFromEnd();
    _pathToRename.AddNameToEnd(_newName);
    return PathParser.Render(_pathToRename);
}

// Names are very good, I like how Uncle bob express himself.
// Uncle bob says the name is long, but it's only called from one place in the module,
// so its explanatory value outweighs the length.
// You can tell how the author/developer is trying to express himself in a professionally way
// and it IS somewhat the effort pays off.
private string RenamePageAndOptionallyAllReferences()
{
    if (_refactorReferences) RenameReferences();
    RenamePage();

    // The method name doesn't include the following context, no?
    _pathToRename.RemoveNameFromEnd();
    _pathToRename.AddNameToEnd(_newName);
    return PathParser.Render(_pathToRename); 
}
