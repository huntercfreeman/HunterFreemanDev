using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public abstract record TextSyntaxRecord()
{
    public Guid TextSyntaxRecordId { get; } = Guid.NewGuid();

    public abstract TextSyntaxRecordKind TextSyntaxRecordKind { get; }
    public abstract string ToPlainText { get; }

    public abstract Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(PlainTextEditorRecord plainTextEditorRecord, 
        KeyDownEventRecord keyDownEventRecord);

    public PlainTextSyntaxRecord ConstructPlainTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new PlainTextSyntaxRecord(keyDownEventRecord);
    }
    
    public WhitespaceTextSyntaxRecord ConstructWhitespaceTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new WhitespaceTextSyntaxRecord(keyDownEventRecord);
    }

    public PlainTextEditorRecordEdit InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(PlainTextEditorRecord plainTextEditorRecord, 
        List<List<TextSyntaxRecord>> fabricatedDocument, 
        TextSyntaxRecord textSyntaxRecord)
    {
        fabricatedDocument[plainTextEditorRecord.CurrentRowIndex]
            .Insert(plainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1, textSyntaxRecord);

        return new PlainTextEditorRecordEdit(plainTextEditorRecord.PlainTextEditorRecordId,
            fabricatedDocument,
            plainTextEditorRecord.CurrentRowIndex,
            plainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1);
    }
}
