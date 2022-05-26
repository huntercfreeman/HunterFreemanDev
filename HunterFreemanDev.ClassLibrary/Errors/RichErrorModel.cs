namespace HunterFreemanDev.ClassLibrary.Errors;

public class RichErrorModel
{
    public RichErrorModel(string message, string hint)
    {
        Message = message;
        Hint = hint;
    }

    public RichErrorModel(string message, string hint, Func<bool> isResolved, Action<RichErrorModel> onIsResolvedAction)
    {
        Message = message;
        Hint = hint;
        IsResolved = isResolved;
        OnIsResolvedAction = onIsResolvedAction;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string Message { get; }
    public string Hint { get; }
    public Func<bool>? IsResolved { get; }
    public Action<RichErrorModel>? OnIsResolvedAction { get; }
}