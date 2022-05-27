namespace HunterFreemanDev.ClassLibrary.Dimension;

public interface IViewportDimensionsService
{
    public Task<ViewportDimensionsModel> GetViewportDimensionsAsync();
}
