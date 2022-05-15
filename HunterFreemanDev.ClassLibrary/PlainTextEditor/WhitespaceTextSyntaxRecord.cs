using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record WhitespaceTextSyntaxRecord(int? IndexInContent)
    : TextSyntaxRecord(IndexInContent)
{
    private readonly char _whitespaceCharacter;

    public WhitespaceTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord, int? IndexInContent) 
        : this(IndexInContent)
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
        List<List<TextSyntaxRecord>> fabricatedDocumentClone = plainTextEditorRecord.ConstructFabricatedDocumentClone();

        if (KeyboardFacts.IsWhitespaceKey(keyDownEventRecord))
        {
            if (KeyboardFacts.WhitespaceKeys.Enter == keyDownEventRecord.Code)
                return plainTextEditorRecord.InsertNewLine();

            return Task.FromResult(InsertAfterCurrentTextSyntaxRecordAndSetCurrent(plainTextEditorRecord,
                fabricatedDocumentClone,
                ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord)));
        }
        else if (KeyboardFacts.IsMovementKey(keyDownEventRecord))
        {
            return Task.FromResult(HandleMovementKey(plainTextEditorRecord,
                fabricatedDocumentClone,
                keyDownEventRecord));
        }

        return Task.FromResult(InsertAfterCurrentTextSyntaxRecordAndSetCurrent(plainTextEditorRecord,
            fabricatedDocumentClone,
            ConstructPlainTextSyntaxRecord(keyDownEventRecord)));
    }

    private PlainTextEditorRecordEdit HandleMovementKey(PlainTextEditorRecord plainTextEditorRecord,
        List<List<TextSyntaxRecord>> fabricatedDocumentClone,
        KeyDownEventRecord keyDownEventRecord)
    {
        switch (keyDownEventRecord.Code)
        {
            case KeyboardFacts.MovementKeys.ArrowLeft:
                return HandleArrowLeft(plainTextEditorRecord, fabricatedDocumentClone, keyDownEventRecord);
            case KeyboardFacts.MovementKeys.ArrowRight:
                return HandleArrowRight(plainTextEditorRecord, fabricatedDocumentClone, keyDownEventRecord);
            default:
                return new PlainTextEditorRecordEdit(plainTextEditorRecord);
        }
    }

    private PlainTextEditorRecordEdit HandleArrowLeft(PlainTextEditorRecord plainTextEditorRecord,
        List<List<TextSyntaxRecord>> fabricatedDocumentClone,
        KeyDownEventRecord keyDownEventRecord)
    {
        return SetPreviousTextSyntaxRecordCurrent(plainTextEditorRecord, fabricatedDocumentClone);
    }

    private PlainTextEditorRecordEdit HandleArrowRight(PlainTextEditorRecord plainTextEditorRecord,
        List<List<TextSyntaxRecord>> fabricatedDocumentClone,
        KeyDownEventRecord keyDownEventRecord)
    {
        return SetNextTextSyntaxRecordCurrent(plainTextEditorRecord, fabricatedDocumentClone);
    }
}