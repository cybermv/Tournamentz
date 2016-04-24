namespace Tournamentz.Host
{
    using BL.Core.Command;
    using BL.Core.Command.Interface;
    using BL.Core.Query;
    using BL.Core.Query.Interface;

    public static class ResultsExtensions
    {
        public static bool IsSuccessful(this ICommandResult result)
        {
            return result.Status == CommandResultStatus.Success;
        }

        public static bool IsSuccessful(this IQueryResult result)
        {
            return result.Status == QueryResultStatus.Success;
        }

        public static bool IsFailed(this ICommandResult result)
        {
            return result.Status != CommandResultStatus.Success;
        }

        public static bool IsFailed(this IQueryResult result)
        {
            return result.Status != QueryResultStatus.Success;
        }
    }
}