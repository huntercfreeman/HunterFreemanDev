using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.KeyDown;

public record KeyDownEventRecord(string? Key,
    string? Code,
    bool CtrlWasPressed,
    bool ShiftWasPressed,
    bool AltWasPressed)
{
    public static KeyDownEventRecord CloneWithoutCtrlModifier(KeyDownEventRecord onKeyDownEventArgs)
    {
        return onKeyDownEventArgs with
        {
            CtrlWasPressed = false
        };
    }
}