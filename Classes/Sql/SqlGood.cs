/// A set of closed classes

public abstract class Sql 
{
    public Sql(string table, Column[] columns) {}
    public abstract string Generate();
}

public class CreateSql : Sql
{
    public CreateSql(string table, Column[] columns) : base(table, columns) {}
    public override string Generate() 
    {
        // ...
    }
}

public class SelectSql : Sql
{
    public SelectSql(string table, Column[] columns) : base(table, columns) {}
    public override string Generate {
        //... 
    }
}

public class InsertSql : Sql
{
    public InsertSql(string table, Column[] columns) : base(table, columns) {}
    public override string Generate {
        //... 
    }

    private string ValuesList(object[] fields, readonly Column[] columns)
    {
        // ...
    }
}

public class SelectWithCriteriaSql : Sql
{
    public SelectWithCriteriaSql(string table, Column[] columns, Criteria criteria) : base(table, columns) {}
    public override string Generate {
        //... 
    }
}

public class SelectWithMatchSql : Sql
{
    public SelectWithMatchSql(string table, Column[] columns,
        Column column, string pattern) : base(table, columns) {}
    public override string Generate {
        //... 
    }
}

public class FindByKeySql : Sql
{
    public FindByKeySql(string table, Column[] columns,
        string keyColumn, string keyValue) : base(table, columns) {}
    public override string Generate {
        //... 
    }
}

public class PreparedInsertSql : Sql
{
    public PreparedInsertSql(string table, Column[] columns) : base(table, columns) {}
    public override string Generate {
        //... 
    }
    private string PlaceholderList(Column[] columns)
    {
        // ...
    }
}

public class Where
{
    public Where(string criteria) {}
    public string Generate()
    {
        // ...
    }
}

public class ColumnList
{
    public ColumnList(Column[] columns) {}
    public string Generate()
    {
        // ...
    }
}
