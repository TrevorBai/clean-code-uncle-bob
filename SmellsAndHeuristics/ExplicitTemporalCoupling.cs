// Hidden temporal coupling
public class MoogDiverBad
{
    Gradient gradient;
    List<Spline> splines;

    public void Dive(string reason)
    {
        // The order matters, but is NOT enforced.
        SaturateGradient();
        ReticulateSplines();
        DiveForMoog(reason);
    }

    // ...
}

// Explicit temporal coupling
// I should use this technique in cost sharing (split land use entities),
// but it increases the code complexity a bit.
public class MoogDiverGood
{
    Gradient gradient;
    List<Spline> splines;

    public void Dive()
    {
        // The calling order is enforced.
        Gradient gradient = SaturateGradient();
        List<Spline> splines = ReticulateSplines(gradient);
        DiveForMoog(splines, reason);
    }

    // ...
}
