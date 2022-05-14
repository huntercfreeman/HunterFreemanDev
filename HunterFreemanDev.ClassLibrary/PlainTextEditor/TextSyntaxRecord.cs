using HunterFreemanDev.ClassLibrary.Keyboard;
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
    
    public WhitespaceTextSyntaxRecord ConstructWhitespaceTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new WhitespaceTextSyntaxRecord(PlainTextEditorRecord, keyDownEventRecord);
    }

    public PlainTextEditorRecordEdit InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(List<List<TextSyntaxRecord>> fabricatedDocument, 
        TextSyntaxRecord textSyntaxRecord)
    {
        fabricatedDocument[PlainTextEditorRecord.CurrentRowIndex]
            .Insert(PlainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1, textSyntaxRecord);

        return new PlainTextEditorRecordEdit(PlainTextEditorRecord.PlainTextEditorRecordId,
            fabricatedDocument,
            PlainTextEditorRecord.CurrentRowIndex, 
            PlainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1);
    }
}
