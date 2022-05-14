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

            return InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(plainTextEditorRecord,
                plainTextEditorRecord.ConstructFabricatedDocumentClone(),
                ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord));
        }

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
}
