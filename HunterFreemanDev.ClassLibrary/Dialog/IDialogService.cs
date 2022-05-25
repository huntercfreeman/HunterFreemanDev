namespace HunterFreemanDev.ClassLibrary.Dialog;

public interface IDialogService
{
    public void AddWindowManagerDialogRecord(DialogRecord windowManagerDialogRecord);
    public void RemoveWindowManagerDialogRecord(Guid windowManagerDialogRecordId);
}
