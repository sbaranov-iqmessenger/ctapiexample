using System;
using System.Diagnostics.CodeAnalysis;

namespace CtApiExample.CtAPI
{
    ///<summary>
    /// Static methods to help with ctAPI
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("CitectStandard", "1.0.0.0")]
    [ExcludeFromCodeCoverage]
    public class CtApiStaticMethods
    {
        #region DateTime Related
        /// <summary>
        /// Converts a DateTime object to Unix-style ticks (seconds since 1/1/70)
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>Citect ticks</returns>
        public static Int32 DateTimeToCitectTicks(DateTime time)
        {
            const Int64 ticksAtEpoch = 621355968000000000;
            const Int64 ticksPerCitectTick = 10000000;
            return (Int32)((time.Ticks - ticksAtEpoch) / ticksPerCitectTick);
        }

        /// <summary>
        /// Converts Unix-style ticks to a new DateTime object
        /// </summary>
        /// <param name="ticks">The ticks.</param>
        /// <param name="ms">The ms.</param>
        /// <returns>DateTime object.</returns>
        public static DateTime CitectTicksToDateTime(Int32 ticks, Int32 ms)
        {
            const Int64 ticksAtEpoch = 621355968000000000;
            const Int64 ticksPerCitectTick = 10000000;
            var newDate = new DateTime((ticks * ticksPerCitectTick) + ticksAtEpoch, DateTimeKind.Utc);
            newDate = newDate.AddMilliseconds(ms);
            return newDate;
        }
        #endregion

        #region Error Related
        /// <summary>
        ///		Convert a Citect Error code to a Win32 error code
        /// </summary>
        /// <param name="citectError">
        ///		The Citect Error code to convert
        /// </param>
        /// <returns>
        ///		The Translated Windows Error code
        /// </returns>
        public static int CitectToWin32Error(CitectScadaError citectError)
        {
            const int errorUserDefinedBase = 0x10000000;
            return (int)citectError + errorUserDefinedBase;
        }

        /// <summary>
        ///		Convert a Win32 error code  to a Citect Error code
        /// </summary>
        /// <param name="win32Error">
        ///		The Win32 Error code to convert
        /// </param>
        /// <returns>
        ///		The Translated Citect Error code
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "win32Error-268435456", Justification = "Citect Original Code, Clear this up later.")]
        public static CitectScadaError Win32ToCitectError(int win32Error)
        {
            const int errorUserDefinedBase = 0x10000000;
            return (CitectScadaError)(win32Error - errorUserDefinedBase);
        }

        /// <summary>
        ///		Determine if the given error is a Citect error
        /// </summary>
        /// <param name="error">
        ///		The error
        /// </param>
        /// <returns>
        ///		true if a Citect error, false otherwise
        /// </returns>
        public static bool IsCitectError(int error)
        {
            const int errorUserDefinedBase = 0x10000000;
            return ((error & errorUserDefinedBase) == errorUserDefinedBase);
        }

        /// <summary>
        ///		Retrieves the last Ctapi error and throws it as an exception.
        /// </summary>
        /// <param name="functionName">
        ///		Name of the function that failed.
        /// </param>
        public static void ThrowLastCtapiError(string functionName)
        {
            int error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();

            if (IsCitectError(error))
            {
                CitectScadaError citectScadaError = Win32ToCitectError(error);
                throw new Exception(String.Format("{0} failed giving citect error: {1}.", functionName, citectScadaError));
            }
            throw new Exception(String.Format("{0} failed giving win32 error: {1}.", functionName, error));
        }
        #endregion
    }
}
