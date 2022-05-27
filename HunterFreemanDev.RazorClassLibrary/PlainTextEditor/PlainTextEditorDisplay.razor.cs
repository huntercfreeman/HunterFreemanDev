using Microsoft.AspNetCore.Components;
using Fluxor;
using System.Text;
using HunterFreemanDev.ClassLibrary.Store.Focus;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;
using HunterFreemanDev.RazorClassLibrary.Focus;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;
using HunterFreemanDev.ClassLibrary.Store.FileBuffer;
using HunterFreemanDev.ClassLibrary.Store.KeyDownEvent;

namespace HunterFreemanDev.RazorClassLibrary.PlainTextEditor;

public partial class PlainTextEditorDisplay : FluxorComponent
{
    [Inject]
    private IState<FileBufferStates> FileBufferStates { get; set; } = null!;
    [Inject]
    private IState<KeyDownEventState> KeyDownEventState { get; set; } = null!;
    [Inject]
    private IState<FocusState> FocusState { get; set; } = null!;
    [Inject]
    private IState<DebugCssClassesState> DebugCssClassesState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public GridRecord? GridRecord { get; set; }

    [Parameter, EditorRequired]
    public PlainTextEditorRecord PlainTextEditorRecord { get; set; } = new();
    [Parameter, EditorRequired]
    public FileDescriptorRecordKey? FileDescriptorRecordKey { get; set; }

    private FocusBoundaryDisplay? _focusBoundaryDisplay = null!;
    private FileDescriptorRecord? _cachedFileDescriptorRecord;
    private Guid? _previousFileDescriptorRecordSequenceId;

    private static readonly Guid[] DebugCssClasses = 
    {
        DebugCssClassInitialStates.PlainTextEditorDebugCssClassRecord.DebugCssClassId,
        DebugCssClassInitialStates.ColoredStartOfRowTextSyntaxRecordDisplaysDebugCssClassRecord.DebugCssClassId,
        DebugCssClassInitialStates.ColoredPlainTextSyntaxRecordDisplaysDebugCssClassRecord.DebugCssClassId,
        DebugCssClassInitialStates.ColoredWhitespaceTextSyntaxRecordDisplaysDebugCssClassRecord.DebugCssClassId,
    };

    protected override async Task OnInitializedAsync()
    {
        KeyDownEventState.StateChanged += KeyDownState_StateChanged;
        FileBufferStates.StateChanged += FileBufferStatesOnStateChanged;

        await CacheFileDescriptorRecord();

        await base.OnInitializedAsync();
    }

    private async void FileBufferStatesOnStateChanged(object? sender, EventArgs e)
    {
        await CacheFileDescriptorRecord();
    }

    private async Task CacheFileDescriptorRecord()
    {
        if (FileDescriptorRecordKey is not null)
        {
            try
            {
                _cachedFileDescriptorRecord = FileBufferStates.Value
                    .LookupFileDescriptor(FileDescriptorRecordKey);

                if (_previousFileDescriptorRecordSequenceId is null ||
                    _previousFileDescriptorRecordSequenceId != _cachedFileDescriptorRecord.FileDescriptorRecordSequenceId)
                {
                    foreach (var character in File.ReadAllText(_cachedFileDescriptorRecord.AbsoluteFilePath.GetAbsoluteFilePathString()))
                    {
                        var code = character switch
                        {
                            '\n' => KeyboardFacts.WhitespaceKeys.Enter,
                            ' ' => KeyboardFacts.WhitespaceKeys.Space,
                            '\t' => KeyboardFacts.WhitespaceKeys.Tab,
                            _ => string.Empty
                        };

                        PlainTextEditorRecord = await PlainTextEditorRecord.HandleKeyDownEventAsync(new KeyDownEventRecord(
                            character.ToString(), code, false, false, false));
                    }

                    await InvokeAsync(StateHasChanged);
                }

                _previousFileDescriptorRecordSequenceId = _cachedFileDescriptorRecord.FileDescriptorRecordSequenceId;
            }
            catch (KeyNotFoundException)
            {
            }
        }
    }

    private async void KeyDownState_StateChanged(object? sender, EventArgs e)
    {
        if (_focusBoundaryDisplay?.GetIsFocused() ?? false)
        {
            PlainTextEditorRecord = await PlainTextEditorRecord.HandleKeyDownEventAsync(KeyDownEventState.Value.OnKeyDownEventRecord);
        }

        await InvokeAsync(StateHasChanged);
    }

    private string GetDebugCssClasses()
    {
        var cssClassesBuilder = new StringBuilder();

        var enabledDebugCssClasses = DebugCssClassesState.Value.DebugCssClassRecordMap.Values
            .Where(cssClass => cssClass.IsEnabled && DebugCssClasses.Contains(cssClass.DebugCssClassId))
            .ToList();

        foreach (var cssClass in enabledDebugCssClasses)
        {
            cssClassesBuilder.Append($"{cssClass.CssClassString} ");
        }

        return cssClassesBuilder.ToString();
    }

    protected override void Dispose(bool disposing)
    {
        KeyDownEventState.StateChanged -= KeyDownState_StateChanged;
        FileBufferStates.StateChanged -= FileBufferStatesOnStateChanged; 

        base.Dispose(disposing);
    }
}