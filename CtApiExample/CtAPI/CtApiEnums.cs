using System;

namespace CtApiExample.CtAPI
{
    /// <summary>
    ///     Defines supported scada types.
    /// </summary>
    public enum ScadaType
    {
        /// <summary>Citect SCADA system</summary>
        Citect = 1,
        /// <summary>iFix/Fix32 SCADA system</summary>
        iFix = 2,
        /// <summary>InTouch SCADA system</summary>
        WonderWare = 3,
        /// <summary>Unknown system</summary>
        Offline = 4
    }


    /// <summary>
    ///		This enum defines supported modes for opening the CtApi connection.
    /// </summary>
    [Flags]
    public enum CT_OPEN
    {
        /// <summary>use encryption</summary>
        CRYPT = 0x00000001,
        /// <summary>reconnect on failure</summary>
        RECONNECT = 0x00000002,
        /// <summary>read only mode</summary>
        READ_ONLY = 0x00000004,
        /// <summary>batch mode</summary>
        BATCH = 0x00000008,
        /// <summary>no license check</summary>
        NO_LICENSE = unchecked((int)0x80000000),
    }

    /// <summary>
    /// Event Mode used in the list query
    /// </summary>
    [Flags]
    public enum CT_LIST_MODE
    {
        /// <summary>
        /// list event mode
        /// </summary>
        EVENT = 0x00000001
    }

    /// <summary>
    /// Event Type used in the list query
    /// </summary>
    [Flags]
    public enum CT_LIST_EVENT_TYPE
    {
        /// <summary>
        /// get event for new tags
        /// </summary>
        NEW = 0x00000001,

        /// <summary>
        /// get events for status change
        /// </summary>
        STATUS = 0x00000002
    }

    /// <summary>
    ///		This enum defines supported modes for formatting tag values retrieved through ctListData
    /// </summary>
    [Flags]
    public enum CT_FORMAT
    {
        /// <summary>don't scale the variable</summary>
        NO_SCALE = 0x00000001,
        /// <summary>don't apply format</summary>
        NO_FORMAT = 0x00000002,
        /// <summary>get last value of data</summary>
        LAST = 0x00000004,
        /// <summary>range check the variable</summary>
        RANGE_CHECK = 0x00000008,
    }

    /// <summary>
    ///		This enum defines supported modes for seeking through ctFindScroll
    /// </summary>
    public enum CT_FIND
    {
        /// <summary>scroll to next record</summary>
        SCROLL_NEXT = 0x00000001,
        /// <summary>scroll to prev record</summary>
        SCROLL_PREV = 0x00000002,
        /// <summary>scroll to first record</summary>
        SCROLL_FIRST = 0x00000003,
        /// <summary>scroll to last record</summary>
        SCROLL_LAST = 0x00000004,
        /// <summary>scroll to absolute record</summary>
        SCROLL_ABSOLUTE = 0x00000005,
        /// <summary>scroll to relative record</summary>
        SCROLL_RELATIVE = 0x00000006,
    }

    /// <summary>
    ///		CtAPI data type identifiers.
    /// </summary>
    [Flags]
    public enum DbType
    {
        /// <summary>empty</summary>
        DBTYPE_EMPTY = 0,
        /// <summary>nothing</summary>
        DBTYPE_NULL = 1,
        /// <summary>short</summary>
        DBTYPE_I2 = 2,
        /// <summary>int</summary>
        DBTYPE_I4 = 3,
        /// <summary>float</summary>
        DBTYPE_R4 = 4,
        /// <summary>double</summary>
        DBTYPE_R8 = 5,
        /// <summary>currency</summary>
        DBTYPE_CY = 6,
        /// <summary>date</summary>
        DBTYPE_DATE = 7,
        /// <summary>basic string</summary>
        DBTYPE_BSTR = 8,
        /// <summary>IDispatch interface</summary>
        DBTYPE_IDISPATCH = 9,
        /// <summary>HRESULT</summary>
        DBTYPE_ERROR = 10,
        /// <summary>boolean</summary>
        DBTYPE_BOOL = 11,
        /// <summary>variant</summary>
        DBTYPE_VARIANT = 12,
        /// <summary>IUnknown interface</summary>
        DBTYPE_IUNKNOWN = 13,
        /// <summary>decimal value</summary>
        DBTYPE_DECIMAL = 14,
        /// <summary>byte</summary>
        DBTYPE_UI1 = 17,
        /// <summary>array flag</summary>
        DBTYPE_ARRAY = 0x2000,
        /// <summary>by reference flag</summary>
        DBTYPE_BYREF = 0x4000,
        /// <summary>signed byte</summary>
        DBTYPE_I1 = 16,
        /// <summary>unsigned short</summary>
        DBTYPE_UI2 = 18,
        /// <summary>unsigned int</summary>
        DBTYPE_UI4 = 19,
        /// <summary>long (64 bit)</summary>
        DBTYPE_I8 = 20,
        /// <summary>unsigned long (64 bit)</summary>
        DBTYPE_UI8 = 21,
        /// <summary>GUID struct</summary>
        DBTYPE_GUID = 72,
        /// <summary>vector flag</summary>
        DBTYPE_VECTOR = 0x1000,
        /// <summary>reserved</summary>
        DBTYPE_RESERVED = 0x8000,
        /// <summary>raw bytes</summary>
        DBTYPE_BYTES = 128,
        /// <summary>string</summary>
        DBTYPE_STR = 129,
        /// <summary>wide char string</summary>
        DBTYPE_WSTR = 130,
        /// <summary>numeric value</summary>
        DBTYPE_NUMERIC = 131,
        /// <summary>user defined type</summary>
        DBTYPE_UDT = 132,
        /// <summary>database date</summary>
        DBTYPE_DBDATE = 133,
        /// <summary>database time</summary>
        DBTYPE_DBTIME = 134,
        /// <summary>database timestamp</summary>
        DBTYPE_DBTIMESTAMP = 135
    };

    /// <summary>
    ///		Data types defined in Citect.
    /// </summary>
    public enum RDT
    {
        /// <summary>
        ///		Placeholder value for unknown Raw data type.
        /// </summary>
        /// <remarks>
        ///		This is just to indicate to us that we need to get the actual raw
        ///		data type from Citect when available.
        /// </remarks>
        RDT_UNKNOWN = -1,

        /// <summary>
        ///		Raw data type for binary type
        /// </summary>
        RDT_DIGITAL = 0,

        /// <summary>
        ///		Raw data type integer
        /// </summary>
        RDT_INTEGER = 1,

        /// <summary>
        ///		Raw data type for floating point
        /// </summary>
        RDT_REAL = 2,

        /// <summary>
        ///		Binay coded decimal type
        /// </summary>
        RDT_BCD = 3,

        /// <summary>
        ///		Raw data type for long
        /// </summary>
        RDT_LONG = 4,

        /// <summary>
        ///		Long binay coded decimal type
        /// </summary>
        RDT_LONG_BCD = 5,

        /// <summary>
        ///		Raw data type for long real
        /// </summary>
        RDT_LONG_REAL = 6,

        /// <summary>
        ///		Raw data type string
        /// </summary>
        RDT_STRING = 7,

        /// <summary>
        ///		Raw data type for byte
        /// </summary>
        RDT_BYTE = 8,

        /// <summary>
        ///		Void data type
        /// </summary>
        RDT_VOID = 9,

        /// <summary>
        ///		Raw data type for unsigned integer
        /// </summary>
        RDT_UINTEGER = 10,

        /// <summary>
        ///		Raw data type for unresolved type
        /// </summary>
        RDT_VALUE = 11,

        /// <summary>
        ///		Raw data type for object handle
        /// </summary>
        RDT_HOBJECT = 12,

        /// <summary>
        ///		Raw data type for anything
        /// </summary>
        RDT_VARIANT = 13,

        /// <summary>
        ///		Argument list of anything
        /// </summary>
        RDT_VARARG = 16,

        /// <summary>
        ///		Named variable tag
        /// </summary>
        RDT_TAG = 17,
    };

    /// <summary>
    ///		Error code from citect scada.
    /// </summary>
    public enum CitectScadaError
    {
        /// <summary>Device offline, cannot talk</summary>
        DEVICE_OFFLINE = 12,
        /// <summary>Cannot cancel command</summary>
        GENERIC_CANNOT_CANCEL = 26,
        /// <summary>No server found. This error can result when calling ctFindFirst on trends or alarms when their servers are unreachable.</summary>
        NO_SERVER_FOUND = 281,
        /// <summary>Name does not exist</summary>
        NAME_NO_EXIST = 289,
        /// <summary>Object not found</summary>
        OBJECT_NOT_FOUND = 347,
        /// <summary>Out of range</summary>
        OUT_OF_RANGE = 257,
        /// <summary>Data browse no next cluster. This error can result when calling ctFindFirst on trends or alarms when their servers have not been created.</summary>
        DATA_BROWSE_NO_NEXT_CLUSTER = 416,
        /// <summary>Subscription to a list of tags is in pending state (SCADA v7 or higher)</summary>
        SUBSCRIPTION_PENDING = 423,
        /// <summary>Tag does not exist (SCADA v7 or higher)</summary>
        TAG_NOT_FOUND = 424
    }

    /// <summary>
    ///		Error code from ctAPI connection.
    /// </summary>
    public enum Win32Error
    {
        /// <summary>Invalid Parameter</summary>
        ERROR_INVALID_PARAMETER = 87,
        /// <summary>Pipe not connected</summary>
        ERROR_PIPE_NOT_CONNECTED = 233,
    }

    ///<summary>
    ///</summary>
    public enum FakeDataInterval
    {
        ///<summary>
        ///</summary>
        Hour = 0,
        ///<summary>
        ///</summary>
        Day = 1,
        ///<summary>
        ///</summary>
        Month = 2,
        ///<summary>
        ///</summary>
        Year = 3
    }
}
