using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record StartOfRowTextSyntaxRecord(int? IndexInContent) 
    : TextSyntaxRecord(IndexInContent)
{
    public override TextSyntaxRecordKind TextSyntaxRecordKind => TextSyntaxRecordKind.StartOfRowText;
    public override string ToPlainText => '\n'.ToString();

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
        else if(KeyboardFacts.IsMovementKey(keyDownEventRecord))
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
        switch(keyDownEventRecord.Code)
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
