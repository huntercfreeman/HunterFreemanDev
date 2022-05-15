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

    public PlainTextEditorRecordEdit MakePreviousTextSyntaxRecordCurrent(PlainTextEditorRecord plainTextEditorRecord,
        List<List<TextSyntaxRecord>> fabricatedDocument)
    {
        if(plainTextEditorRecord.CurrentTextSyntaxRecordIndex == 0)
        {
            if (plainTextEditorRecord.CurrentRowIndex != 0)
            {
                var previousFinalTokenOfPreviousRow = fabricatedDocument[plainTextEditorRecord.CurrentRowIndex - 1]
                    .Last();

                plainTextEditorRecord.CurrentRow[plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1] = previousFinalTokenOfPreviousRow with
                {
                    IndexInContent = 0
                };

                fabricatedDocument[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex] = this with
                {
                    IndexInContent = null
                };

                return new PlainTextEditorRecordEdit(plainTextEditorRecord.PlainTextEditorRecordId,
                    fabricatedDocument,
                    plainTextEditorRecord.CurrentRowIndex - 1,
                    fabricatedDocument[plainTextEditorRecord.CurrentRowIndex - 1].Count - 1);
            }

            return new PlainTextEditorRecordEdit(plainTextEditorRecord);
        }
        else
        {
            var previousTextSyntaxRecord = plainTextEditorRecord.CurrentRow[plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1];

            plainTextEditorRecord.CurrentRow[plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1] = previousTextSyntaxRecord with
            {
                IndexInContent = 0
            };

            fabricatedDocument[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex] = this with
            { 
                IndexInContent = null
            };

            return new PlainTextEditorRecordEdit(plainTextEditorRecord.PlainTextEditorRecordId,
                fabricatedDocument,
                plainTextEditorRecord.CurrentRowIndex,
                plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1);
        }
    }
}
