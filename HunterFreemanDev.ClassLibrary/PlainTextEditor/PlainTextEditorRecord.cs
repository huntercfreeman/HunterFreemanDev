using HunterFreemanDev.ClassLibrary.Focus;
using HunterFreemanDev.ClassLibrary.KeyDown;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record PlainTextEditorRecordEdit(List<List<TextSyntaxRecord>> Document);

public record PlainTextEditorRecord(Guid PlainTextEditorRecordId,
    int CurrentRowIndex,
    int CurrentTokenIndex,
    Stack<PlainTextEditorRecordEdit> UndoDocumentEditStack,
    Stack<PlainTextEditorRecordEdit> RedoDocumentEditStack,
    Stack<PlainTextEditorRecordEdit> RedoJumpMovementStack,
    Stack<PlainTextEditorRecordEdit> UndoJumpMovementStack)
{
    private List<List<TextSyntaxRecord>> _document = new();

    public List<TextSyntaxRecord> CurrentRow => _document[CurrentRowIndex];
    public TextSyntaxRecord CurrentToken => _document[CurrentRowIndex][CurrentTokenIndex];
    
    public ImmutableArray<ImmutableArray<TextSyntaxRecord>> GetDocument()
    {
        ImmutableArray<TextSyntaxRecord>[] temporaryRows = new ImmutableArray<TextSyntaxRecord>[_document.Count];

        for (int i = 0; i < _document.Count; i++)
        {
            List<TextSyntaxRecord>? documentRow = _document[i];

            var immutableRow = new TextSyntaxRecord[documentRow.Count]
                .ToImmutableArray();

            temporaryRows[i] = immutableRow;
        }

        return temporaryRows.ToImmutableArray();
    }

    public async Task<PlainTextEditorRecord> HandleKeyDownEventAsync(KeyDownEventRecord onKeyDownEventRecord)
    {
        var documentEdit = await CurrentToken.HandleKeyDownEventRecordAsync(onKeyDownEventRecord);

        UndoDocumentEditStack.Push(new PlainTextEditorRecordEdit(_document));

        return this with
        {
            _document = documentEdit.Document,
            UndoDocumentEditStack = UndoDocumentEditStack
        };
    }

    public List<List<TextSyntaxRecord>> ConstructListClone()
    {
        List<List<TextSyntaxRecord>> listClone = new();

        foreach (var immutableRow in GetDocument())
        {

            var listCloneRow = new List<TextSyntaxRecord>();

            foreach (var immutableToken in immutableRow)
            {
                listCloneRow.Add(immutableToken);
            }

            listClone.Add(listCloneRow);
        }

        return listClone;
    }
}

public abstract record TextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord)
{
    public Guid TextSyntaxRecordId { get; } = Guid.NewGuid();

    public abstract Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord);

    public PlainTextSyntaxRecord ConstructPlainTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new PlainTextSyntaxRecord(PlainTextEditorRecord, keyDownEventRecord);
    }
}

public record StartOfRowTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord) 
    : TextSyntaxRecord(PlainTextEditorRecord)
{
    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        var plainTextSyntaxRecord = ConstructPlainTextSyntaxRecord(keyDownEventRecord);

        List<List<TextSyntaxRecord>> listClone = PlainTextEditorRecord.ConstructListClone();

        listClone[PlainTextEditorRecord.CurrentRowIndex].Insert(PlainTextEditorRecord.CurrentTokenIndex,
            plainTextSyntaxRecord);

        return Task.FromResult(new PlainTextEditorRecordEdit(listClone));
    }
}

public record PlainTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord, string PlainText)
    : TextSyntaxRecord(PlainTextEditorRecord)
{
    public PlainTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord, KeyDownEventRecord keyDownEventRecord) 
        : this(PlainTextEditorRecord, keyDownEventRecord.Key 
              ?? throw new ApplicationException($"{nameof(PlainTextSyntaxRecord)} was attempted " +
                  $"to be constructed with a {nameof(keyDownEventRecord.Key)} that was null."))
    {
    }

    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        var nextPlainTextSyntaxRecord = new PlainTextSyntaxRecord(PlainTextEditorRecord, 
            PlainText + keyDownEventRecord.Key);

        List<List<TextSyntaxRecord>> listClone = PlainTextEditorRecord.ConstructListClone();

        listClone[PlainTextEditorRecord.CurrentRowIndex][PlainTextEditorRecord.CurrentTokenIndex]
            = nextPlainTextSyntaxRecord;

        return Task.FromResult(new PlainTextEditorRecordEdit(listClone));
    }
}

public record WhitespaceTextSyntaxRecord(PlainTextEditorRecord PlainTextEditorRecord)
    : TextSyntaxRecord(PlainTextEditorRecord)
{
    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        throw new NotImplementedException();
    }
}