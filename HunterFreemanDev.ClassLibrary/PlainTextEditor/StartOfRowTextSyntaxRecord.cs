using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record StartOfRowTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord) 
    : TextSyntaxRecord(PlainTextEditorRecord)
{
    public override TextSyntaxRecordKind TextSyntaxRecordKind => TextSyntaxRecordKind.StartOfRowText;
    public override string ToPlainText => TextSyntaxRecordId == PlainTextEditorRecord.StartOfDocumentTextSyntaxRecordId
        ? string.Empty
        : '\n'.ToString();

    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        TextSyntaxRecord textSyntaxRecord;

        if (KeyboardFacts.IsWhitespaceKey(keyDownEventRecord))
        {
            textSyntaxRecord = ConstructWhitespaceTextSyntaxRecord(keyDownEventRecord);
        }
        else
        {
            textSyntaxRecord = ConstructPlainTextSyntaxRecord(keyDownEventRecord);
        }

        List<List<TextSyntaxRecord>> fabricatedDocumentClone = PlainTextEditorRecord.ConstructFabricatedDocumentClone();

        return Task.FromResult(InsertAfterCurrentTextSyntaxRecordAndMakeCurrent(fabricatedDocumentClone, textSyntaxRecord));
    }
}
