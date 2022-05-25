using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record PlainTextSyntaxRecord(string PlainText, int? IndexInContent)
    : TextSyntaxRecord(IndexInContent)
{
    public PlainTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord, int? IndexInContent) 
        : this(keyDownEventRecord.Key 
              ?? throw new ApplicationException($"{nameof(PlainTextSyntaxRecord)} was attempted " +
                  $"to be constructed with a {nameof(keyDownEventRecord.Key)} that was null."),
              IndexInContent)
    {
    }

    public override TextSyntaxRecordKind TextSyntaxRecordKind => TextSyntaxRecordKind.PlainText;
    public override string ToPlainText => PlainText;

    public override async Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(PlainTextEditorRecord plainTextEditorRecord,
        KeyDownEventRecord keyDownEventRecord)
    {
        if(KeyboardFacts.IsWhitespaceKey(keyDownEventRecord))
        {
            if(KeyboardFacts.WhitespaceKeys.Enter == keyDownEventRecord.Code)
                return await plainTextEditorRecord.InsertNewLine();

            return InsertAfterCurrentTextSyntaxRecordAndSetCurrent(plainTextEditorRecord,
                plainTextEditorRecord.ConstructFabricatedDocumentClone(),
                ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord));
        }
        
        if(KeyboardFacts.IsMovementKey(keyDownEventRecord))
            return HandleMovementKey(plainTextEditorRecord, keyDownEventRecord);

        var nextPlainTextSyntaxRecord = this with
        {
            IndexInContent = IndexInContent + 1,
            PlainText = PlainText + keyDownEventRecord.Key
        };

        List<List<TextSyntaxRecord>> fabricatedDocumentClone = plainTextEditorRecord.ConstructFabricatedDocumentClone();

        fabricatedDocumentClone[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex]
            = nextPlainTextSyntaxRecord;

        return new PlainTextEditorRecordEdit(plainTextEditorRecord.PlainTextEditorRecordId,
            fabricatedDocumentClone,
            plainTextEditorRecord.CurrentRowIndex,
            plainTextEditorRecord.CurrentTextSyntaxRecordIndex);
    }

    private PlainTextEditorRecordEdit HandleMovementKey(PlainTextEditorRecord plainTextEditorRecord, KeyDownEventRecord keyDownEventRecord)
    {
        switch(keyDownEventRecord.Code)
        {
            case KeyboardFacts.MovementKeys.ArrowLeft:
                 return HandlePlainTextSyntaxRecordArrowLeft(plainTextEditorRecord, keyDownEventRecord);
            case KeyboardFacts.MovementKeys.ArrowRight:
                return HandlePlainTextSyntaxRecordArrowRight(plainTextEditorRecord, keyDownEventRecord);
        }

        return new PlainTextEditorRecordEdit(plainTextEditorRecord);
    }

    private PlainTextEditorRecordEdit HandlePlainTextSyntaxRecordArrowLeft(PlainTextEditorRecord plainTextEditorRecord, KeyDownEventRecord keyDownEventRecord)
    {
        List<List<TextSyntaxRecord>> fabricatedDocumentClone = plainTextEditorRecord.ConstructFabricatedDocumentClone();

        if (IndexInContent == 0)
            return SetPreviousTextSyntaxRecordCurrent(plainTextEditorRecord, fabricatedDocumentClone);
        
        PlainTextSyntaxRecord copyPlainTextSyntaxRecord = this with
        {
            IndexInContent = IndexInContent - 1
        };

        fabricatedDocumentClone[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex]
            = copyPlainTextSyntaxRecord;

        return new PlainTextEditorRecordEdit(plainTextEditorRecord.PlainTextEditorRecordId,
            fabricatedDocumentClone,
            plainTextEditorRecord.CurrentRowIndex,
            plainTextEditorRecord.CurrentTextSyntaxRecordIndex);
    }
    
    private PlainTextEditorRecordEdit HandlePlainTextSyntaxRecordArrowRight(PlainTextEditorRecord plainTextEditorRecord, KeyDownEventRecord keyDownEventRecord)
    {
        List<List<TextSyntaxRecord>> fabricatedDocumentClone = plainTextEditorRecord.ConstructFabricatedDocumentClone();

        if (IndexInContent == PlainText.Length - 1)
            return SetNextTextSyntaxRecordCurrent(plainTextEditorRecord, fabricatedDocumentClone);
        
        PlainTextSyntaxRecord copyPlainTextSyntaxRecord = this with
        {
            IndexInContent = IndexInContent + 1
        };

        fabricatedDocumentClone[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex]
            = copyPlainTextSyntaxRecord;

        return new PlainTextEditorRecordEdit(plainTextEditorRecord.PlainTextEditorRecordId,
            fabricatedDocumentClone,
            plainTextEditorRecord.CurrentRowIndex,
            plainTextEditorRecord.CurrentTextSyntaxRecordIndex);
    }
}
