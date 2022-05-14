using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public abstract record TextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord)
{
    public Guid TextSyntaxRecordId { get; } = Guid.NewGuid();

    public abstract TextSyntaxRecordKind TextSyntaxRecordKind { get; }
    public abstract string ToPlainText { get; }

    public abstract Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord);

    public PlainTextSyntaxRecord ConstructPlainTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new PlainTextSyntaxRecord(PlainTextEditorRecord, keyDownEventRecord);
    }
    
    public PlainTextEditorRecordEdit InsertAfterCurrentTextSyntaxTokenAndMakeCurrent(List<List<TextSyntaxRecord>> fabricatedDocument, 
        TextSyntaxRecord textSyntaxRecord)
    {
        List<List<TextSyntaxRecord>> fabricatedDocumentClone = PlainTextEditorRecord.ConstructFabricatedDocumentClone();

        fabricatedDocumentClone[PlainTextEditorRecord.CurrentRowIndex]
            .Insert(PlainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1, textSyntaxRecord);

        return new PlainTextEditorRecordEdit(fabricatedDocumentClone);
    }
}
