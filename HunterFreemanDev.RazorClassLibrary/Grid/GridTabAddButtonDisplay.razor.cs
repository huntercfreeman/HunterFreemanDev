using Fluxor;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.ClassLibrary.Store.Grid;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridTabAddButtonDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public GridItemRecordKey GridItemRecordKey { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public int NextAvailableTabIndex { get; set; }

    private void DispatchAddGridTabAction()
    {
        var addGridTabAction = new AddGridTabRecordAction(GridItemRecordKey, new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()), 
                typeof(GridTabAddFormDisplay),
                "Empty"),
            NextAvailableTabIndex);

        Dispatcher.Dispatch(addGridTabAction);
    }
}