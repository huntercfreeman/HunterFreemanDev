using HunterFreemanDev.ClassLibrary.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Dialog;

public record SetIsMinimizedDialogAction(DialogRecord DialogRecord, bool IsMinimized);
