using Microsoft.AspNetCore.Components;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using HunterFreemanDev.ClassLibrary.Dialog;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Html;

namespace HunterFreemanDev.RazorClassLibrary.Settings;

public partial class SettingsDialogEntryPointDisplay : ComponentBase
{
    [Inject]
    private IState<DialogStates> DialogStates { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set;} = null!;

    private readonly Guid _settingsDialogRecordId = Guid.NewGuid();
    private readonly Type _settingsDialogRecordType = typeof(SettingsDisplay);

    private async Task DispatchConstructDialogOnClick()
    {
        if (!DialogStates.Value.DialogRecordMap.ContainsKey(_settingsDialogRecordId))
        {
            var defaultDimensionsRecordForDialog = await DialogRecord.ConstructDefaultDimensionsRecord(ViewportDimensionsService);

            var action = new RegisterDialogAction(new DialogRecord(_settingsDialogRecordId,
                "Settings Display",
                _settingsDialogRecordType,
                null,
                new HtmlElementRecordKey(Guid.NewGuid())));

            Dispatcher.Dispatch(action);
        }
    }
}