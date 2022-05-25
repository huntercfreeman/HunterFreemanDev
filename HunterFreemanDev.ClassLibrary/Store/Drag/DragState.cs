using Fluxor;
using Microsoft.AspNetCore.Components.Web;

namespace HunterFreemanDev.ClassLibrary.Store.Drag;

[FeatureState]
public record DragState(double DeltaX, double DeltaY, MouseEventArgs? MouseEventArgs)
{
    public DragState() : this(0, 0, null)
    {

    }
}
