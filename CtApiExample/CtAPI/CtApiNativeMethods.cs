using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CtApiExample.CtAPI
{
    ///<summary>
    ///</summary>
    internal class CtApiNativeMethods
    {
        private CtApiNativeMethods() { }

        #region Open / Close
        ///<summary>
        ///</summary>
        ///<param name="computer"></param>
        ///<param name="user"></param>
        ///<param name="password"></param>
        ///<param name="flags"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctOpen", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 Open(string computer, string user, string password, CT_OPEN flags);

        ///<summary>
        ///</summary>
        ///<param name="computer"></param>
        ///<param name="user"></param>
        ///<param name="password"></param>
        ///<param name="flags"></param>
        ///<param name="hCtapi"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctOpenEx", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 Open(string computer, string user, string password, CT_OPEN flags, Int32 hCtapi);

        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctClose", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int Close(Int32 hCtapi);

        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="destroy"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctCloseEx", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int Close(Int32 hCtapi, [MarshalAs(UnmanagedType.Bool)] bool destroy);
        #endregion

        #region CiCode
        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="cicode"></param>
        ///<param name="pointer"></param>
        ///<param name="mode"></param>
        ///<param name="value"></param>
        ///<param name="bufLength"></param>
        ///<param name="overlap"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctCicode", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RunCicode(Int32 hCtapi, string cicode, UInt32 pointer, UInt32 mode, StringBuilder value, UInt32 bufLength, IntPtr overlap);
        #endregion

        #region Lists
        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="mode"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctListNew", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ListNew(Int32 hCtapi, UInt32 mode);

        ///<summary>
        ///</summary>
        ///<param name="hCtList"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctListFree", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ListFree(Int32 hCtList);

        ///<summary>
        ///</summary>
        ///<param name="hCtList"></param>
        ///<param name="tag"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctListAdd", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ListAdd(Int32 hCtList, string tag);

        ///<summary>
        ///</summary>
        ///<param name="hCtList"></param>
        ///<param name="tag"></param>
        ///<param name="raw"></param>
        ///<param name="pollPeriodInMs"></param>
        ///<param name="deadband"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctListAddEx", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ListAdd(Int32 hCtList, string tag, [MarshalAs(UnmanagedType.Bool)] bool raw, Int32 pollPeriodInMs, double deadband);

        ///<summary>
        ///</summary>
        ///<param name="hCtTag"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctListDelete", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ListDelete(Int32 hCtTag);

        ///<summary>
        ///</summary>
        ///<param name="hCtList"></param>
        ///<param name="pointer"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctListRead", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ListRead(Int32 hCtList, IntPtr pointer);

        ///<summary>
        ///</summary>
        ///<param name="hCtTag"></param>
        ///<param name="value"></param>
        ///<param name="bufLength"></param>
        ///<param name="mode"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctListData", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ListData(Int32 hCtTag, StringBuilder value, UInt32 bufLength, CT_FORMAT mode);
        
        /// <summary>
        /// Returns the elements in the list which have changed state since they were last 
        /// read using the ctListRead() function. You must have created the list with 
        /// CT_LIST_EVENT mode in the ctListNew() function.
        /// </summary>
        /// <param name="hCtList">The handle to the CitectAPI as returned from ctListRead().</param>
        /// <param name="dwMode">
        /// The mode of the list event.  
        /// You must use the same mode for all calls to ctListEvent() until NULL is returned 
        /// before changing mode.
        /// CT_LIST_EVENT_NEW - Get notifications when tags are added to the list.  
        /// When this mode is used, you will get an event message when new tags added to the list.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value specifies a handle to a tags which 
        /// has changed state since the last time ctListRead was called.  
        /// If the function fails or there are no changes, the return value is NULL.  
        /// To get extended error information, call GetLastError().
        /// </returns>
        [DllImport("ctapi.dll", EntryPoint = "ctListEvent", SetLastError = true)]
        public static extern Int32 ListEvent(Int32 hCtList, int dwMode);	
        #endregion

        #region Finds
        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="tableName"></param>
        ///<param name="filter"></param>
        ///<param name="foundObject"></param>
        ///<param name="flags"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctFindFirst", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 FindFirst(Int32 hCtapi, string tableName, string filter, out Int32 foundObject, UInt32 flags);

        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="tableName"></param>
        ///<param name="filter"></param>
        ///<param name="cluster"></param>
        ///<param name="foundObject"></param>
        ///<param name="flags"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctFindFirstEx", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 FindFirst(Int32 hCtapi, string tableName, string filter, string cluster, out Int32 foundObject, UInt32 flags);

        ///<summary>
        ///</summary>
        ///<param name="searchHandle"></param>
        ///<param name="foundObject"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctFindNext", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNext(Int32 searchHandle, out Int32 foundObject);

        ///<summary>
        ///</summary>
        ///<param name="searchHandle"></param>
        ///<param name="foundObject"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctFindPrev", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindPrev(Int32 searchHandle, out Int32 foundObject);

        ///<summary>
        ///</summary>
        ///<param name="searchHandle"></param>
        ///<param name="mode"></param>
        ///<param name="offset"></param>
        ///<param name="foundObject"></param>
        ///<returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#"), DllImport("CTAPI.DLL", EntryPoint = "ctFindScroll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindScroll(Int32 searchHandle, UInt32 mode, Int32 offset, out Int32 foundObject);

        ///<summary>
        ///</summary>
        ///<param name="searchHandle"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctFindClose", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClose(Int32 searchHandle);

        ///<summary>
        ///</summary>
        ///<param name="hCtTag"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctFindNumRecords", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I8)]
        public static extern Int64 FindNumRecords(Int32 hCtTag);
        #endregion

        #region Tag Read/Write
        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="tagName"></param>
        ///<param name="variableState"></param>
        ///<param name="length"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctTagRead", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TagRead(Int32 hCtapi, string tagName, StringBuilder variableState, UInt32 length);

        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="tagName"></param>
        ///<param name="variableState"></param>
        ///<param name="length"></param>
        ///<param name="tagValueItems"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctTagReadEx", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TagRead(Int32 hCtapi, string tagName, StringBuilder variableState, UInt32 length, IntPtr tagValueItems);

        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="tagName"></param>
        ///<param name="variableState"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctTagWrite", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TagWrite(Int32 hCtapi, string tagName, string variableState);

        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="tagName"></param>
        ///<param name="variableState"></param>
        ///<param name="overlapped"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctTagWriteEx", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TagWrite(Int32 hCtapi, string tagName, string variableState, IntPtr overlapped);
        #endregion

        #region Misc
        ///<summary>
        ///</summary>
        ///<param name="hCtapi"></param>
        ///<param name="overlap"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctCancelIO", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CancelIO(Int32 hCtapi, IntPtr overlap);
        #endregion

        #region Property
        ///<summary>
        ///</summary>
        ///<param name="objectHandle"></param>
        ///<param name="propertyName"></param>
        ///<param name="valuePtr"></param>
        ///<param name="valueLength"></param>
        ///<param name="returnedValueLength"></param>
        ///<param name="type"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctGetProperty", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetProperty(Int32 objectHandle, string propertyName, IntPtr valuePtr, UInt32 valueLength, out UInt32 returnedValueLength, DbType type);

        ///<summary>
        ///</summary>
        ///<param name="objectHandle"></param>
        ///<param name="tagName"></param>
        ///<param name="propertyName"></param>
        ///<param name="valuePtr"></param>
        ///<param name="valueLength"></param>
        ///<param name="dwType"></param>
        ///<returns></returns>
        [DllImport("CTAPI.DLL", EntryPoint = "ctTagGetProperty", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TagGetProperty(Int32 objectHandle, string tagName, string propertyName, IntPtr valuePtr, UInt32 valueLength, UInt32 dwType);
        #endregion

        #region Unimplemented Extern Reference
        //extern	HANDLE	CTAPICALL	ctClientCreate();
        //extern	BOOL	CTAPICALL	ctClientDestroy(HANDLE);
        //extern	BOOL	CTAPICALL	ctPointWrite(HANDLE,HANDLE,void*,DWORD,CTOVERLAPPED*);		/* write to point handle	*/
        //extern	BOOL	CTAPICALL	ctPointRead(HANDLE,HANDLE,void*,DWORD,CTOVERLAPPED*);		/* read from point handle	*/
        //extern	HANDLE	CTAPICALL	ctTagToPoint(HANDLE,LPCSTR,DWORD,CTOVERLAPPED*);		/* convert tag into point handle*/
        //extern	BOOL	CTAPICALL	ctPointClose(HANDLE,HANDLE);					/* free a point handle		*/
        //extern	HANDLE	CTAPICALL	ctPointCopy(HANDLE);						/* copy a point handle		*/
        //extern	BOOL	CTAPICALL	ctPointGetProperty(HANDLE,LPCTSTR,void*,DWORD,DWORD*,DWORD);	/* get point property		*/
        //extern	DWORD	CTAPICALL	ctPointDataSize(HANDLE);					/* size of point data buffer	*/
        //extern	DWORD	CTAPICALL	ctPointBitShift(HANDLE);					/* calculate bit shift offset	*/
        //extern	BOOL	CTAPICALL	ctPointToStr(HANDLE,BYTE*,DWORD,BYTE*,DWORD,DWORD);		/* format point data to string	*/
        //extern	BOOL	CTAPICALL	ctStrToPoint(HANDLE,LPCSTR,DWORD,BYTE*,DWORD,DWORD);		/* format string data into point*/
        //extern	BOOL	CTAPICALL	ctEngToRaw(double*,double,CTSCALE*,DWORD);			/* scale from eng to raw	*/
        //extern	BOOL	CTAPICALL	ctRawToEng(double*,double,CTSCALE*,DWORD);			/* scale from raw to eng	*/
        //extern	BOOL	CTAPICALL	ctGetOverlappedResult(HANDLE,CTOVERLAPPED*,DWORD*,BOOL);	/* get overlapped result	*/
        //extern	BOOL	CTAPICALL	ctEngToRaw(double*,double,CTSCALE*,DWORD);			/* scale from eng to raw	*/
        //extern	BOOL	CTAPICALL	ctRawToEng(double*,double,CTSCALE*,DWORD);			/* scale from raw to eng	*/
        //extern	BOOL	CTAPICALL	ctListWrite(HANDLE,LPCSTR,CTOVERLAPPED*);									// write poll list item
        //extern	BOOL	CTAPICALL	ctListItem(HANDLE,DWORD,void*,DWORD,DWORD);									// get tag element extended data
        #endregion
    }
}

