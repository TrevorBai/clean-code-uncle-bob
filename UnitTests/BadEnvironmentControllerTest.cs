@Test
public void TurnOnLoTempAlarmAtThreashold() 
{
    // Too many details    
    _hw.SetTemp(WAY_TOO_COLD);
    _controller.tic();

    // Too many states to check
    AssertTrue(_hw.HeaterState());
    AssertTrue(_hw.BlowerState());
    AssertFalse(_hw.CoolerState());
    AssertFalse(_hw.HiTempAlarm());
    AssertTrue(_hw.LoTempAlarm());
}
