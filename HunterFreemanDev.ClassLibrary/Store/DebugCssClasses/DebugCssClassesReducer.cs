using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;

public class DebugCssClassesReducer
{
    [ReducerMethod]
    public static DebugCssClassesState ReduceInitializeDebugCssClassesStateAction(DebugCssClassesState previousDebugCssClassesState,
        InitializeDebugCssClassesStateAction initializeDebugCssClassesStateAction)
    {
        var nextDebugCssClassesState = new DebugCssClassesState(
            new Dictionary<Guid, ClassLibrary.DebugCssClasses.DebugCssClassRecord>(previousDebugCssClassesState.DebugCssClassRecordMap));

        foreach(var debugCssClassRecord in initializeDebugCssClassesStateAction.DebugCssClassRecords)
        {
            try
            {
                nextDebugCssClassesState.DebugCssClassRecordMap.Add(debugCssClassRecord.DebugCssClassId,
                    debugCssClassRecord);
            }
            catch (ArgumentException)
            {
                // Duplicate Key Errors are handled with a "try and handle" mindset like accessing a file system item.
                // Checking to see if a key exists would introduce the need for locking logic
            }
        }

        return nextDebugCssClassesState;
    }
    
    [ReducerMethod]
    public static DebugCssClassesState ReduceSetIsEnabledDebugCssClassRecordAction(DebugCssClassesState previousDebugCssClassesState,
        SetIsEnabledDebugCssClassRecordAction setIsEnabledDebugCssClassRecordAction)
    {
        var nextDebugCssClassesState = new DebugCssClassesState(
            new Dictionary<Guid, ClassLibrary.DebugCssClasses.DebugCssClassRecord>(previousDebugCssClassesState.DebugCssClassRecordMap));

        if(nextDebugCssClassesState.DebugCssClassRecordMap
            .TryGetValue(setIsEnabledDebugCssClassRecordAction.DebugCssClassRecordId, out var previousDebugCssClassRecord))
        {
            nextDebugCssClassesState.DebugCssClassRecordMap[setIsEnabledDebugCssClassRecordAction.DebugCssClassRecordId] = previousDebugCssClassRecord with
            {
                IsEnabled = setIsEnabledDebugCssClassRecordAction.IsEnabled
            };
        }

        return nextDebugCssClassesState;
    }
    
    [ReducerMethod]
    public static DebugCssClassesState ReduceRegisterDebugCssClassRecordAction(DebugCssClassesState previousDebugCssClassesState,
        RegisterDebugCssClassRecordAction registerDebugCssClassRecordAction)
    {
        var nextDebugCssClassesState = new DebugCssClassesState(
            new Dictionary<Guid, ClassLibrary.DebugCssClasses.DebugCssClassRecord>(previousDebugCssClassesState.DebugCssClassRecordMap));

        try
        {
            nextDebugCssClassesState.DebugCssClassRecordMap.Add(registerDebugCssClassRecordAction.DebugCssClassRecord.DebugCssClassId,
                registerDebugCssClassRecordAction.DebugCssClassRecord);
        }
        catch (ArgumentException)
        {
            // Duplicate Key Errors are handled with a "try and handle" mindset like accessing a file system item.
            // Checking to see if a key exists would introduce the need for locking logic
        }

        return nextDebugCssClassesState;
    }
}
