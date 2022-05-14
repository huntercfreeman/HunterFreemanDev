using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record StartOfRowTextSyntaxRecord(bool IsStartOfDocument) 
    : TextSyntaxRecord()
{
    public override TextSyntaxRecordKind TextSyntaxRecordKind => TextSyntaxRecordKind.StartOfRowText;
    public override string ToPlainText => IsStartOfDocument
        ? string.Empty
        : '\n'.ToString();

    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(PlainTextEditorRecord plainTextEditorRecord,
        KeyDownEventRecord keyDownEventRecord)
    {
        TextSyntaxRecord textSyntaxRecord;

        if (KeyboardFacts.IsWhitespaceKey(keyDownEventRecord))
        {
            if (KeyboardFacts.WhitespaceKeys.Enter == keyDownEventRecord.Code)
                return plainTextEditorRecord.InsertNewLine();

            textSyntaxRecord = ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord);
        }
        else
        {
            textSyntaxRecord = ConstructPlainTextSyntaxRecord(keyDownEventRecord);
        }

        List<List<TextSyntaxRecord>> fabricatedDocumentClone = plainTextEditorRecord.ConstructFabricatedDocumentClone();

        return Task.FromResult(InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(plainTextEditorRecord,
            fabricatedDocumentClone, 
            textSyntaxRecord));
    }
}
