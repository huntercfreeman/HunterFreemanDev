using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record WhitespaceTextSyntaxRecord()
    : TextSyntaxRecord()
{
    private readonly char _whitespaceCharacter;

    public WhitespaceTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord) 
        : this()
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

    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(PlainTextEditorRecord plainTextEditorRecord,
        KeyDownEventRecord keyDownEventRecord)
    {
        TextSyntaxRecord textSyntaxRecord;

        if (KeyboardFacts.IsWhitespaceKey(keyDownEventRecord))
        {
            if (KeyboardFacts.WhitespaceKeys.Enter == keyDownEventRecord.Code)
                return plainTextEditorRecord.InsertNewLine();

            textSyntaxRecord = ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord);
        }
        else
        {
            textSyntaxRecord = ConstructPlainTextSyntaxRecord(keyDownEventRecord);
        }

        List<List<TextSyntaxRecord>> fabricatedDocumentClone = plainTextEditorRecord.ConstructFabricatedDocumentClone();

        return Task.FromResult(InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(plainTextEditorRecord, fabricatedDocumentClone, textSyntaxRecord));
    }
}