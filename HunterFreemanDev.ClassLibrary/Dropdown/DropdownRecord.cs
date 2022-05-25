namespace HunterFreemanDev.ClassLibrary.Dropdown;

public record DropdownRecord(Guid DropdownId, List<DropdownRecord> ChildDropdownRecords);
