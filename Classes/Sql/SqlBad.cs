// A class that must be opened for change
public class Sql
{
    // My first intuition is there are a lot of methods in this class,
    // could be separated as multiple classes.
    public Sql(string table, Column[] columns) {}

    public string Create() { ... }

    public string Insert(object[] fields) { ... }

    public string SelectAll() { ... }

    public string FindByKey(string keyColumn, string keyValue) { ... }

    public string Select(Column column, string pattern) { ... }

    public string Select(Criteria criteria) { ... }

    public string PreparedInsert() { ... }

    private string ColumnList(Column[] columns) { ... }

    // Java version is final Column[] columns, not sure readonly in c# triggers compiler errors or not.
    private string ValuesList(object[] fields, readonly Column[] columns) { ... }

    private string SelectWithCriteria(string criteria) { ... }

    private string PlaceholderList(Column[] columns) { ... }  
}
