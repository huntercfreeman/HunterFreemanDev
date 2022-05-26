using Fluxor;
using HunterFreemanDev.ClassLibrary.Dialog;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using HunterFreemanDev.ClassLibrary.Html;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.InputFile;

public partial class InputFileDialogEntryPointDisplay : ComponentBase
{
    [Inject]
    private IState<DialogStates> DialogStates { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set;} = null!;

    [Parameter, EditorRequired]
    public Action<IAbsoluteFilePath> OnFileSelectedAction { get; set; } = null!;

    private IAbsoluteFilePath _rootAbsoluteFilePath =
        new AbsoluteFilePath(System.IO.Path.DirectorySeparatorChar.ToString(), true);

    private Guid _inputFileDialogRecordId = Guid.NewGuid();

    private void DispatchRegisterDialogAction()
    {
        if (!DialogStates.Value.DialogRecordMap.ContainsKey(_inputFileDialogRecordId))
        {
            var inputFileDialogParameters = new Dictionary<string, object>
            {
                { "OnFileSelectedAction", OnFileSelectedAction },
                { "InputFileDialogRecordId", _inputFileDialogRecordId },
            };

            var inputFileDialogRecord = new DialogRecord(_inputFileDialogRecordId, 
                "Input File Dialog", 
                typeof(InputFileDialogDisplay),
                inputFileDialogParameters,
                new HtmlElementRecordKey(Guid.NewGuid()));

            var action = new RegisterDialogAction(inputFileDialogRecord);

            Dispatcher.Dispatch(action);
        }
    }
}