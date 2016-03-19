namespace Tournamentz.BL.Core.Command
{
    public enum CommandResultStatus
    {
        Success = 1,
        PermissionError = 2,
        BrokenRules = 3,
        SystemError = 4
    }
}