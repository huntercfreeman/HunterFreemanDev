using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record PlainTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord, string PlainText)
    : TextSyntaxRecord(PlainTextEditorRecord)
{
    public PlainTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord, KeyDownEventRecord keyDownEventRecord) 
        : this(PlainTextEditorRecord, keyDownEventRecord.Key 
              ?? throw new ApplicationException($"{nameof(PlainTextSyntaxRecord)} was attempted " +
                  $"to be constructed with a {nameof(keyDownEventRecord.Key)} that was null."))
    {
    }

    public override TextSyntaxRecordKind TextSyntaxRecordKind => TextSyntaxRecordKind.PlainText;
    public override string ToPlainText => PlainText;

    public override async Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        if(KeyboardFacts.IsWhitespaceKey(keyDownEventRecord))
        {
            if(KeyboardFacts.WhitespaceKeys.Enter == keyDownEventRecord.Code)
            {
                return await PlainTextEditorRecord.InsertNewLine();
            }

            return InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(PlainTextEditorRecord.ConstructFabricatedDocumentClone(),
                ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord));
        }    

        var nextPlainTextSyntaxRecord = new PlainTextSyntaxRecord(PlainTextEditorRecord, 
            PlainText + keyDownEventRecord.Key);

        List<List<TextSyntaxRecord>> fabricatedDocumentClone = PlainTextEditorRecord.ConstructFabricatedDocumentClone();

        fabricatedDocumentClone[PlainTextEditorRecord.CurrentRowIndex][PlainTextEditorRecord.CurrentTextSyntaxRecordIndex]
            = nextPlainTextSyntaxRecord;

        return new PlainTextEditorRecordEdit(PlainTextEditorRecord.PlainTextEditorRecordId,
            fabricatedDocumentClone,
            PlainTextEditorRecord.CurrentRowIndex,
            PlainTextEditorRecord.CurrentTextSyntaxRecordIndex);
    }
}
