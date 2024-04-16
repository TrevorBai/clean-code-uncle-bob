/*
 * Without any formal trainings, you can feel we have a lot of parallel catches, which is not good.
 * In fact, it IS not good. This piece of code comes from a third-party library call referenced from
 * the book Clean Code by uncle Bob.
 */

ACMEPort port = new ACMEPort(12);

try {
    port.Open();
} catch (DeviceResponseException e) {
    ReportPortError(e);
    logger.Log("Device response exception", e);
} catch (ATM1212UnlockedException e) {
    ReportPortError(e);
    logger.Log("Unlock exception", e);
} catch (GMXError e) {
    ReportPortError(e);
    logger.Log("Device response exception");  
} finally {
    ...
}
