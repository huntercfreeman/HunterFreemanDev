using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public abstract record TextSyntaxRecord(int? IndexInContent)
{
    public Guid TextSyntaxRecordId { get; } = Guid.NewGuid();

    public abstract TextSyntaxRecordKind TextSyntaxRecordKind { get; }
    public abstract string ToPlainText { get; }

    public abstract Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(PlainTextEditorRecord plainTextEditorRecord, 
        KeyDownEventRecord keyDownEventRecord);

    public PlainTextSyntaxRecord ConstructPlainTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new PlainTextSyntaxRecord(keyDownEventRecord, null);
    }
    
    public WhitespaceTextSyntaxRecord ConstructWhitespaceTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new WhitespaceTextSyntaxRecord(keyDownEventRecord, null);
    }

    public PlainTextEditorRecordEdit InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(PlainTextEditorRecord plainTextEditorRecord, 
        List<List<TextSyntaxRecord>> fabricatedDocument, 
        TextSyntaxRecord textSyntaxRecord)
    {
        fabricatedDocument[plainTextEditorRecord.CurrentRowIndex]
            .Insert(plainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1, textSyntaxRecord with
            {
                IndexInContent = 0
            });

        return new PlainTextEditorRecordEdit(plainTextEditorRecord.PlainTextEditorRecordId,
            fabricatedDocument,
            plainTextEditorRecord.CurrentRowIndex,
            plainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1);
    }
}
