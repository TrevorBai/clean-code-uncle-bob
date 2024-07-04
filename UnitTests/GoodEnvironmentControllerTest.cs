@Test
public void TurnOnLoTempAlarmAtThreshold()
{
    // 1. Encapsulate all the unnecessary details
    WayTooCode();
    // 2. Combine all different states into one big state and assert that ONE state,
    // And the assert string is interesting and needs to be parsed properly. Where capital
    // letter indicates true, and lowercase denotes as false. 'H' means the heater state is
    // true, 'c' indicates cooler state is false.
    AsertEquals("HBchL", _hw.GetState());
}
