/*
 *  Everyone knows StringBuilder is more efficient than just use regular string concatenation. 
 *  However, uncle bob says in production code, he will avoid using it because it looks ugly.
 *  And uncle bob thinks the cost of using regular string concatenation is small, interesting!
 *  As long as it's not an embedded real-time system like programming in chips/robots/fridge/
 *  sweeping robot that kind of things, no need to use StringBuilder. Wow. Our desktop application
 *  at my current company doesn't have memory constraint, so I should not use StringBuilder from
 *  now on. Even we use double instead of float as well. Similar ideas.
 *
 *  One thing I favor to use StringBuilder is when we have a lot of char, int concatenation stuff.
 *  It's easy to miss calculating the result.
 *
 *  To summarize, we should avoid using StringBuilder.
 */



using System;

public string GetState()
{
    // The solution the book provides doesn't use a StringBuilder, because the book
    // says, this code is used for testing which is not likely to be constrained at all.
    // The book says, usually embedded real-time system has constrained memory.
    string state = "";
    state += heater ? "H" : "h";
    state += blower ? "B" : "b";
    state += cooler ? "C" : "c";
    state += hiTempAlarm ? "H" : "h";
    state += loTempAlarm ? "L" : "l";
    return state;
}
