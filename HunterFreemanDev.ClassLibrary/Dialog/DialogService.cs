using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Dialog;

namespace HunterFreemanDev.ClassLibrary.Dialog;

public class DialogService : IDialogService
{
    private readonly IDispatcher _dispatcher;

    public DialogService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void AddWindowManagerDialogRecord(DialogRecord windowManagerDialogRecord)
    {
        var action = new RegisterDialogAction(windowManagerDialogRecord);

        _dispatcher.Dispatch(action);
    }

    public void RemoveWindowManagerDialogRecord(Guid dialogRecordId)
    {
        var action = new UnregisterDialogAction(dialogRecordId);

        _dispatcher.Dispatch(action);
    }
}
