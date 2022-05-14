﻿using HunterFreemanDev.ClassLibrary.Focus;
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
                new StartOfRowTextSyntaxRecord(this)
            }
        };
    }

    public List<TextSyntaxRecord> CurrentRow => _document[CurrentRowIndex];
    public TextSyntaxRecord CurrentTextSyntaxRecord => _document[CurrentRowIndex][CurrentTextSyntaxRecordIndex];
    public Guid StartOfDocumentTextSyntaxRecordId => _document.First().First().TextSyntaxRecordId;
    
    public ImmutableArray<ImmutableArray<TextSyntaxRecord>> GetDocument()
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
        var documentEdit = await CurrentTextSyntaxRecord.HandleKeyDownEventRecordAsync(onKeyDownEventRecord);

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

            foreach (var immutableTextSyntaxRecord in immutableRow)
            {
                listCloneRow.Add(immutableTextSyntaxRecord);
            }

            listClone.Add(listCloneRow);
        }

        return listClone;
    }
}
