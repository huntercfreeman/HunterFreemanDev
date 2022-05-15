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

    public PlainTextEditorRecordEdit InsertAfterCurrentTextSyntaxRecordAndSetCurrent(PlainTextEditorRecord plainTextEditorRecord, 
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

    public PlainTextEditorRecordEdit SetPreviousTextSyntaxRecordCurrent(PlainTextEditorRecord plainTextEditorRecord,
        List<List<TextSyntaxRecord>> fabricatedDocument)
    {
        if(plainTextEditorRecord.CurrentTextSyntaxRecordIndex == 0)
        {
            if (plainTextEditorRecord.CurrentRowIndex != 0)
            {
                var previousFinalTextSyntaxRecordOfPreviousRow = fabricatedDocument[plainTextEditorRecord.CurrentRowIndex - 1]
                    .Last();

                fabricatedDocument[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1] = previousFinalTextSyntaxRecordOfPreviousRow with
                {
                    IndexInContent = previousFinalTextSyntaxRecordOfPreviousRow.ToPlainText.Length - 1
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

            fabricatedDocument[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1] = previousTextSyntaxRecord with
            {
                IndexInContent = previousTextSyntaxRecord.ToPlainText.Length - 1
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
    
    public PlainTextEditorRecordEdit SetNextTextSyntaxRecordCurrent(PlainTextEditorRecord plainTextEditorRecord,
        List<List<TextSyntaxRecord>> fabricatedDocument)
    {
        if(plainTextEditorRecord.CurrentTextSyntaxRecordIndex == plainTextEditorRecord.CurrentRow.Length - 1)
        {
            if (plainTextEditorRecord.CurrentRowIndex != fabricatedDocument.Count - 1)
            {
                var nextFirstTextSyntaxRecordOfNextRow = fabricatedDocument[plainTextEditorRecord.CurrentRowIndex + 1]
                    .First();

                fabricatedDocument[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1] = nextFirstTextSyntaxRecordOfNextRow with
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
            var nextTextSyntaxRecord = fabricatedDocument[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1];

            fabricatedDocument[plainTextEditorRecord.CurrentRowIndex][plainTextEditorRecord.CurrentTextSyntaxRecordIndex - 1] = nextTextSyntaxRecord with
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
