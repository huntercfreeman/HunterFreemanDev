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
}

public abstract record TextSyntaxRecord()
{
    public Guid TextSyntaxRecordId { get; } = Guid.NewGuid();

    public abstract Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord);

    public PlainTextSyntaxRecord ConstructPlainTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord)
    {
        return new PlainTextSyntaxRecord(keyDownEventRecord);
    }
}

public record StartOfRowTextSyntaxRecord() : TextSyntaxRecord
{
    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        throw new NotImplementedException();
    }
}

public record PlainTextSyntaxRecord(string PlainText) : TextSyntaxRecord
{
    public PlainTextSyntaxRecord(KeyDownEventRecord keyDownEventRecord) 
        : this(keyDownEventRecord.Key 
              ?? throw new ApplicationException($"{nameof(PlainTextSyntaxRecord)} was attempted " +
                  $"to be constructed with a {nameof(keyDownEventRecord.Key)} that was null."))
    {
    }

    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        return _
    }
}

public record WhitespaceTextSyntaxRecord() : TextSyntaxRecord
{
    public override Task<PlainTextEditorRecordEdit> HandleKeyDownEventRecordAsync(KeyDownEventRecord keyDownEventRecord)
    {
        throw new NotImplementedException();
    }
}