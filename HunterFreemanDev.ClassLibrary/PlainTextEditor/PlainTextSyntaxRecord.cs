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

    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        var nextPlainTextSyntaxRecord = new PlainTextSyntaxRecord(PlainTextEditorRecord, 
            PlainText + keyDownEventRecord.Key);

        List<List<TextSyntaxRecord>> listClone = PlainTextEditorRecord.ConstructListClone();

        listClone[PlainTextEditorRecord.CurrentRowIndex][PlainTextEditorRecord.CurrentTextSyntaxRecordIndex]
            = nextPlainTextSyntaxRecord;

        return Task.FromResult(new PlainTextEditorRecordEdit(listClone));
    }
}
