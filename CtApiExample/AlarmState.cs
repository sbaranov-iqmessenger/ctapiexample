using System;

namespace CtApiExample
{
    /// <summary>
    /// Enum AlarmState
    /// </summary>
    [Flags]
    public enum AlarmState
    {
        /// <summary>
        /// Good
        /// </summary>
        Good = 1,
        /// <summary>
        /// Disabled
        /// </summary>
        Disabled = 2,
        /// <summary>
        /// On
        /// </summary>
        On = 8,
        /// <summary>
        /// ACK
        /// </summary>
        Ack = 16
    }
}
