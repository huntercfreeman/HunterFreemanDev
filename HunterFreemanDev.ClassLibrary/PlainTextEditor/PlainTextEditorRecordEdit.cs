namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record PlainTextEditorRecordEdit(Guid PlainTextEditorRecordId,
    List<List<TextSyntaxRecord>> Document,
    int CurrentRowIndex,
    int CurrentTextSyntaxRecordIndex)
{
    public PlainTextEditorRecordEdit(PlainTextEditorRecord plainTextEditorRecord) 
        : this(plainTextEditorRecord.PlainTextEditorRecordId,
              plainTextEditorRecord.ConstructFabricatedDocumentClone(),
              plainTextEditorRecord.CurrentRowIndex,
              plainTextEditorRecord.CurrentTextSyntaxRecordIndex)
    {

    }
}
