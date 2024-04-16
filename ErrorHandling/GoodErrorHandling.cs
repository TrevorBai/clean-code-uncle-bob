LocalPort port = new LocalPort(12);

try {
    port.Open();
} catch (PortDeviceFailure e) {
    ReportError(e);
    logger.Log(e.GetMessage(), e);
} finally {
    ...
}

// A wrapper class
public class LocalPort
{
    private readonly ACMEPort _innerPort;

    public LocalPort(int portNumber) 
    {
        _innerPort = new ACMEPort(portNumber);
    }

    public void Open()
    {
        try {
            _innerPort.Open();
        } catch (DeviceResponseException e) {
            throw new PortDeviceFailure(e);
        } catch (ATM1212UnlockedException e) {
            throw new PortDeviceFailure(e);
        } catch (GMXError e) {
            throw new PortDeviceFailure(e);
        }       
    }

    ...
}
