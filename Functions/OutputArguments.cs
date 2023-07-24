/// <summary>
/// This is output arguments. This method would mutate the input argument
/// report and if you want to check the output, it would be the same argument
//  report. This programming style is not good and should be avoided.
/// </summary>
public void AppendFooter(StringBuffer report) {
    // ...
}

/// <summary>
/// We should call this method on the report object instead. That way
/// we know report object is mutated and it's good, we don't need rely
/// on output arguments because we don't have arguments here.
/// </summary>
report.AppendFooter();
