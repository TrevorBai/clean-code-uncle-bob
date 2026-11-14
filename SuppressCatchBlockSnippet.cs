// You can suppress the catch block, if you don't want to deal with the e,
// whichever the type is. Even tho it will trigger an IDE warning, that says
// variable e is defined but not been used. It's a legit way of suppressing
// a potential compiler error at runtime. In other words, cleaning up IDE 
// warnings DOESN'T make any sense even tho it looks ugly.


// first try parsing the string as an integer (1-12)...
try 
{
    result = Integer.parseInt(s);
}
catch (NumberFormatException e)
{
    // suppress
}
