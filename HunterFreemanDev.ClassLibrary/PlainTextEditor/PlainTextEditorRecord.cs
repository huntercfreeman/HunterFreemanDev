using HunterFreemanDev.ClassLibrary.Focus;
using HunterFreemanDev.ClassLibrary.Keyboard;
using HunterFreemanDev.ClassLibrary.KeyDown;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record PlainTextEditorRecord(Guid PlainTextEditorRecordId,
    int CurrentRowIndex,
    int CurrentTextSyntaxRecordIndex,
    Stack<PlainTextEditorRecordEdit> UndoDocumentEditStack,
    Stack<PlainTextEditorRecordEdit> RedoDocumentEditStack,
    Stack<PlainTextEditorRecordEdit> RedoJumpMovementStack,
    Stack<PlainTextEditorRecordEdit> UndoJumpMovementStack)
{
    private List<List<TextSyntaxRecord>> _document;

    public PlainTextEditorRecord() : this(Guid.NewGuid(),
        0,
        0,
        new Stack<PlainTextEditorRecordEdit>(),
        new Stack<PlainTextEditorRecordEdit>(),
        new Stack<PlainTextEditorRecordEdit>(),
        new Stack<PlainTextEditorRecordEdit>())
    {
        _document = new()
        {
            new List<TextSyntaxRecord>
            {
                new StartOfRowTextSyntaxRecord(true, 0)
            }
        };
    }

    public ImmutableArray<TextSyntaxRecord> CurrentRow => _document[CurrentRowIndex].ToImmutableArray();
    public TextSyntaxRecord CurrentTextSyntaxRecord => _document[CurrentRowIndex][CurrentTextSyntaxRecordIndex];
    public Guid StartOfDocumentTextSyntaxRecordId => _document.First().First().TextSyntaxRecordId;
    
    public ImmutableArray<ImmutableArray<TextSyntaxRecord>> GetImmutableDocument()
    {
        ImmutableArray<TextSyntaxRecord>[] temporaryRows = new ImmutableArray<TextSyntaxRecord>[_document.Count];

        for (int i = 0; i < _document.Count; i++)
        {
            List<TextSyntaxRecord>? documentRow = _document[i];

            var temporaryRow = new TextSyntaxRecord[documentRow.Count];

            for (int j = 0; j < documentRow.Count; j++)
            {
                temporaryRow[j] = documentRow[j];
            }

            temporaryRows[i] = temporaryRow.ToImmutableArray();
        }

        return temporaryRows.ToImmutableArray();
    }

    public async Task<PlainTextEditorRecord> HandleKeyDownEventAsync(KeyDownEventRecord onKeyDownEventRecord)
    {
        if(KeyboardFacts.IsMetaKey(onKeyDownEventRecord) &&
            !KeyboardFacts.IsMovementKey(onKeyDownEventRecord) &&
            !KeyboardFacts.IsWhitespaceKey(onKeyDownEventRecord))
            return this;

        var documentEdit = await CurrentTextSyntaxRecord.HandleKeyDownEventRecordAsync(this,
            onKeyDownEventRecord);

        UndoDocumentEditStack.Push(new PlainTextEditorRecordEdit(this));

        return this with
        {
            _document = documentEdit.Document,
            UndoDocumentEditStack = UndoDocumentEditStack,
            CurrentRowIndex = documentEdit.CurrentRowIndex,
            CurrentTextSyntaxRecordIndex = documentEdit.CurrentTextSyntaxRecordIndex,
        };
    }

    public List<List<TextSyntaxRecord>> ConstructFabricatedDocumentClone()
    {
        List<List<TextSyntaxRecord>> fabricatedDocumentClone = new();

        foreach (var immutableRow in GetImmutableDocument())
        {
            var listCloneRow = new List<TextSyntaxRecord>();

            foreach (var immutableTextSyntaxRecord in immutableRow)
            {
                listCloneRow.Add(immutableTextSyntaxRecord);
            }

            fabricatedDocumentClone.Add(listCloneRow);
        }

        return fabricatedDocumentClone;
    }

    public Task<PlainTextEditorRecordEdit> InsertNewLine()
    {
        // TODO: If inserting new line in the middle of a row it is necessary to split the line and possibly a TextEditorRecord that the user was within
        List<List<TextSyntaxRecord>> fabricatedDocumentClone = ConstructFabricatedDocumentClone();

        fabricatedDocumentClone.Insert(CurrentRowIndex + 1, new List<TextSyntaxRecord>
        {
            new StartOfRowTextSyntaxRecord(false, 0)
        });

        return Task.FromResult(new PlainTextEditorRecordEdit(PlainTextEditorRecordId,
            fabricatedDocumentClone,
            CurrentRowIndex + 1,
            0));
    }
}
