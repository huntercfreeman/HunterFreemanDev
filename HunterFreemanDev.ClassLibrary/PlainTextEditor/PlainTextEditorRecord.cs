using HunterFreemanDev.ClassLibrary.KeyDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.PlainTextEditor;

public record PlainTextEditorRecord
{
    private StringBuilder _contentBuilder = new();

    public Task HandleKeyDownEvent(KeyDownEventRecord onKeyDownEventRecord)
    {
        _contentBuilder.Append(onKeyDownEventRecord.Key);

        return Task.CompletedTask;
    }
}
