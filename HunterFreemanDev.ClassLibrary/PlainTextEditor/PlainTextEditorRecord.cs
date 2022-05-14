using HunterFreemanDev.ClassLibrary.Focus;
using HunterFreemanDev.ClassLibrary.KeyDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record PlainTextEditorRecord(Guid PlainTextEditorRecordId)
{
    private StringBuilder _contentBuilder = new();

    public StringBuilder ContentBuilder => _contentBuilder;

    public Task HandleKeyDownEventAsync(KeyDownEventRecord onKeyDownEventRecord)
    {
        _contentBuilder.Append(onKeyDownEventRecord.Key);

        return Task.CompletedTask;
    }
}
