namespace Tournamentz.BL.Core.Logging
{
    public enum LogSeverity
    {
        /// <summary>
        /// Severity of an event that is logged for debugging purposes
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Severity of an event that is expected; usually an executed user action
        /// </summary>
        Info = 2,

        /// <summary>
        /// Severity of an event that is not expected; usually a defect that does
        /// not affect the whole application
        /// </summary>
        Warn = 3,

        /// <summary>
        /// Severity of an event that is not expected; usually a defect that affects
        /// the whole application
        /// </summary>
        Error = 4
    }
}