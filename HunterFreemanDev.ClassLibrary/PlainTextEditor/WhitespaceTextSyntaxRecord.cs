using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record WhitespaceTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord)
    : TextSyntaxRecord(PlainTextEditorRecord)
{
    private readonly char _whitespaceCharacter;

    public WhitespaceTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord, PlainTextEditorRecord plainTextEditorRecord) 
        : this(plainTextEditorRecord)
    {
        switch (keyDownEventRecord.Code)
        {
            case KeyboardKeyFacts.WhitespaceKeys.Space:
                _whitespaceCharacter = ' ';
                break;
            case KeyboardKeyFacts.WhitespaceKeys.Tab:
                _whitespaceCharacter = '\t';
                break;
            default:
                throw new ApplicationException($"Unrecognized whitespace code, '{keyDownEventRecord.Code}' when " +
                    $"constructing a {nameof(WhitespaceTextSyntaxRecord)}.");
        }
    }

    public override TextSyntaxRecordKind TextSyntaxRecordKind => TextSyntaxRecordKind.WhitespaceText;
    public override string ToPlainText => _whitespaceCharacter.ToString();

    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        throw new NotImplementedException();
    }
}