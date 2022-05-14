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
        var plainTextSyntaxRecord = ConstructPlainTextSyntaxRecord(keyDownEventRecord);

        List<List<TextSyntaxRecord>> listClone = PlainTextEditorRecord.ConstructListClone();

        listClone[PlainTextEditorRecord.CurrentRowIndex].Insert(PlainTextEditorRecord.CurrentTextSyntaxRecordIndex + 1,
            plainTextSyntaxRecord);

        return Task.FromResult(new PlainTextEditorRecordEdit(listClone));
    }
}
