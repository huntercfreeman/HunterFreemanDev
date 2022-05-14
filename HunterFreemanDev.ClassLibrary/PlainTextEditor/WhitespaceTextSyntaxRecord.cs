using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record WhitespaceTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord)
    : TextSyntaxRecord(PlainTextEditorRecord)
{
    private readonly char _whitespaceCharacter;

    public WhitespaceTextSyntaxRecord(PlainTextEditorRecord plainTextEditorRecord, KeyDownEventRecord keyDownEventRecord) 
        : this(plainTextEditorRecord)
    {
        switch (keyDownEventRecord.Code)
        {
            case KeyboardFacts.WhitespaceKeys.Space:
                _whitespaceCharacter = ' ';
                break;
            case KeyboardFacts.WhitespaceKeys.Tab:
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
        TextSyntaxRecord textSyntaxRecord;

        if (KeyboardFacts.IsWhitespaceKey(keyDownEventRecord))
        {
            textSyntaxRecord = ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord);
        }
        else
        {
            textSyntaxRecord = ConstructPlainTextSyntaxRecord(keyDownEventRecord);
        }

        List<List<TextSyntaxRecord>> fabricatedDocumentClone = PlainTextEditorRecord.ConstructFabricatedDocumentClone();

        return Task.FromResult(InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(fabricatedDocumentClone, textSyntaxRecord));
    }
}