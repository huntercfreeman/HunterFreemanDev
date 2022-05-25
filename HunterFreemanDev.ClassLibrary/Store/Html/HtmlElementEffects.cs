using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.Html;

public class HtmlElementEffects
{
    private readonly SemaphoreSlim _zIndexSemaphoreSlim = new(1, 1);
    private int _zIndexCounter = 1;

    [EffectMethod]
    public async Task HandleZIndexRequestAction(ZIndexRequestAction zIndexRequestAction, IDispatcher dispatcher)
    {
        try
        {
            await _zIndexSemaphoreSlim.WaitAsync();
            
            var nextZIndex = _zIndexCounter++;

            if (_zIndexCounter == int.MaxValue)
            {
                // TODO: Reset all the element ZIndexes (ensure this doesn't cause an infinite loop
                // when there are int.MaxValue html elements asking for a ZIndex
            }
            else
            {
                var responseAction = new ZIndexRequestAction(zIndexRequestAction.HtmlElementRecordKey);

                dispatcher.Dispatch(responseAction);
            }
        }
        finally
        {
            _zIndexSemaphoreSlim.Release();
        }
    }
}
