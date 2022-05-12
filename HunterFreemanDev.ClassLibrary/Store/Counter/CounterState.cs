using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Counter;

[FeatureState]
public record CounterState(int Count)
{
    private CounterState() : this(0)
    {
    }
}
