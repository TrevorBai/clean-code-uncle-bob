/// <summary>
/// This is a test for testing third-party library. It helps us learn how it works.
/// </summary>
public class LogTest
{
    private Logger _logger;

    // [Before] // Do not exist at NUnit framework
    public void Initialize()
    {
        _logger = Logger.GetLogger("logger");
        _logger.RemoveAllAppenders();
        Logger.GetRootLogger().RemoveAllAppenders();
    }

    [Test]
    public void BasicLogger()
    {
        BasicConfigurator.Configure();
        _logger.Info("basicLogger");
    }

    [Test]
    public void AddAppenderWithStream()
    {
        _logger.AddAppender(new ConsoleAppender(
            new PatternLayout("%p %t %m%n"),
            ConsoleAppender.SYSTEM_OUT));
        _logger.Info("addAppenderWithStream");  
    }

    [Test]
    public void AddAppenderWithoutStream()
    {
        _logger.AddAppender(new ConsoleAppender(
            new PatternLayout("%p %t %m%n"))); 
        _logger.Info("addAppenderWithoutStream");
    }   
}
