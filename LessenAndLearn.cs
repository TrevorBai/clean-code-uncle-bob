/// <summary>
/// Case study number 1
/// </summary>
namespace PlanViewObjs.Grading.Envelope
{
    public class BuildingEnvelope : AbstractPlanViewObject
    {
        private List<Curve> _sides;
    
        public Curve FrontSide { get; set; }
        public Curve BackSide { get; set; }

        // This LeftAndRight getter is very confusing, not only it depends on
        // private field _sides, but also FrontSide and BackSide. In other words,
        // if user populates _sides, FrontSide and BackSide, LeftAndRight would be
        // automatically populated, it's kinda confusing. There are too many dependencies
        // for a public field, it's better separating these complex dependencies to make
        // it an independent public field.
        public List<Curve> LeftAndRight 
        {
            get
            {
                if (_sides == null || !_sides.Any()) return new List<Curve>();

                // Method Foo() is dependent on FrontSide and BackSide,
                // i.e. if FrontSide or BackSide is null, LeftAndRight is an empty list.
                var leftAndRight = _sides.Where(
                    side => Foo(side)
                ).ToList();
                return leftAndRight;
            }        
        }
    
    }
}
