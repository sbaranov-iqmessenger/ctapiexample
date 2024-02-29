using CtApiExample.CtAPI;

namespace CtApiExample
{
    /// <summary>
    /// The form
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Form1 : Form
    {
        #region Private Members

        /// <summary>
        /// The _citect handle for use across application
        /// </summary>
        private int _citectHandle;

        /// <summary>
        /// The _code page for translations
        /// </summary>
        private int _codePage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Load / Open Connection

        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _codePage = new CtApiCharsetHelper().GetCodePage(0);
            // codePage=0 (default) maps to local machines regional setting codepage, or Encoding.Default
            OpenConnection();

            //load the tags dropdown
            var tags = GetTags();
            foreach (var tag in tags)
            {
                cmbTag.Items.Add(tag);
            }

            if (cmbTag.Items.Count > 0) cmbTag.SelectedIndex = 0;

            if (cmbTag.Items.Count == 0)
            {
                MessageBox.Show(
                    @"The application failed to retrieve tags from Citect.  This application must be run in the bin folder.");
                Application.Exit();
            }

            //load the devices dropdown
            var devices = GetDevices();
            foreach (var device in devices)
            {
                cmbDevice.Items.Add(device);
            }

            if (cmbDevice.Items.Count > 0) cmbDevice.SelectedIndex = 0;

            tmrAlarm.Enabled = true;
        }


        /// <summary>
        /// Opens the connection to ctAPI
        /// </summary>
        private void OpenConnection()
        {
            try
            {
                _citectHandle = CtApiNativeMethods.Open("", "", "", CT_OPEN.RECONNECT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    @"The application failed to connect to Citect.  This application must be run in the bin folder.  Read the documentation for more details.  More info: " +
                    ex.Message);
                Application.Exit();
            }
        }

        /// <summary>
        /// Gets the tag list
        /// </summary>
        private Collection<string> GetTags()
        {
            var tags = new Collection<string>();
            int objectHandle;

            var searchHandle =
                CtApiNativeMethods.FindFirst(_citectHandle, "TAG", "*", string.Empty, out objectHandle, 0);

            if (searchHandle == 0) return tags; //no tags, return empty list

            do
            {
                //get the tag name (or any other property) like this:
                var name = GetPropertyAsString(objectHandle, "TAG", 255);
                tags.Add(name);
            } while (CtApiNativeMethods.FindNext(searchHandle, out objectHandle));

            CtApiNativeMethods.FindClose(searchHandle);

            return tags;
        }

        /// <summary>
        /// Gets the device list
        /// </summary>
        private Collection<string> GetDevices()
        {
            var devices = new Collection<string>();
            int objectHandle;

            var searchHandle = CtApiNativeMethods.FindFirst(_citectHandle, "EQUIP", "*", string.Empty, out objectHandle,
                0);

            if (searchHandle == 0) return devices; //no tags, return empty list

            do
            {
                //get the tag name (or any other property) like this:
                var name = GetPropertyAsString(objectHandle, "IODEVICE", 255);
                devices.Add(name);
            } while (CtApiNativeMethods.FindNext(searchHandle, out objectHandle));

            CtApiNativeMethods.FindClose(searchHandle);

            return devices;
        }

        /// <summary>
        /// Helper method to get a property as a string
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string GetPropertyAsString(int objectHandle, string propertyName, uint length)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            var valueBuffer = new byte[length + 1];

            var valueBufferHandle = GCHandle.Alloc(valueBuffer, GCHandleType.Pinned);

            try
            {
                uint returnedLength;
                var returnValue = CtApiNativeMethods.GetProperty(objectHandle, propertyName,
                    valueBufferHandle.AddrOfPinnedObject(), length, out returnedLength, DbType.DBTYPE_STR);

                if (returnValue)
                {
                    return Encoding.GetEncoding(_codePage).GetString(valueBuffer, 0, (int)returnedLength);
                }
            }
            finally
            {
                valueBufferHandle.Free();
            }

            return string.Empty;
        }

        #endregion

        #region Read / Write Tags

        /// <summary>
        /// Handles the Click event of the cmdRead control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmdRead_Click(object sender, EventArgs e)
        {
            txtValue.Text = string.Empty;

            var returnValue = new StringBuilder(255);
            if (CtApiNativeMethods.TagRead(_citectHandle, cmbTag.SelectedItem.ToString(), returnValue, 255))
            {
                txtValue.Text = returnValue.ToString();
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdWrite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmdWrite_Click(object sender, EventArgs e)
        {
            float val;
            if (string.IsNullOrEmpty(txtValue.Text) || !float.TryParse(txtValue.Text, out val))
            {
                MessageBox.Show(@"This example only supports numeric types.  Enter a numeral.");
                return;
            }

            if (
                !CtApiNativeMethods.TagWrite(_citectHandle, cmbTag.SelectedItem.ToString(),
                    val.ToString(CultureInfo.InvariantCulture)))
            {
                MessageBox.Show(@"Failed to write to " + cmbTag.SelectedItem);
                return;
            }

            txtValue.Text = string.Empty;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbTag control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValue.Text = string.Empty;
        }

        #endregion

        #region Device Status

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDevice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear all labels
            lblOffline.Visible = false;
            lblOnline.Visible = false;
            lblUnknown.Visible = false;

            var cicode = "IODeviceInfo(\"" + cmbDevice.SelectedItem + "\",10)";
            var returnValue = new StringBuilder(256);
            var result = CtApiNativeMethods.RunCicode(_citectHandle, cicode, 0, 0, returnValue,
                (uint)returnValue.Capacity, IntPtr.Zero);

            if (!result)
            {
                //the cicode failed, show unknown
                lblUnknown.Visible = true;
                return;
            }

            if (returnValue.ToString().Trim() == "1")
            {
                lblOnline.Visible = true;
            }
            else
            {
                lblOffline.Visible = true;
            }
        }

        #endregion

        #region Alarms

        /// <summary>
        /// Polls for alarms
        /// </summary>
        private ReadOnlyCollection<Alarm> PollForAlarms()
        {
            int soeQueryObject;
            var startQueryTime = DateTime.Now.AddDays(-7); //get the last week of alarms
            var endQueryTime = DateTime.Now.AddDays(1);
            var priorityMin = 0;
            var priorityMax = 255;

            const string commandFormat = "SOEQUERY,{0},{1}";
            const string filterBaseString = "({0} >= {1} AND {0} <= {2})";
            var priorityString = string.Format(filterBaseString, "PRIORITY", priorityMin, priorityMax);
            var filterString = string.Format(
                "TIMEDATE >= {0};NOT (TAG = \"\");NOT (CLASSIFICATION = \"Configuration\");NOT (CLASSIFICATION = \"Interface\");NOT (CLASSIFICATION = \"System\");NOT (CLASSIFICATION = \"Comment\");{1}",
                CtApiStaticMethods.DateTimeToCitectTicks(startQueryTime.AddDays(-1)), priorityString);

            var currentQuerySampleCount = 0;

            var query = string.Format(commandFormat, startQueryTime.ToFileTimeUtc(), endQueryTime.ToFileTimeUtc());
            var searchHandle = CtApiNativeMethods.FindFirst(_citectHandle, query, filterString, string.Empty,
                out soeQueryObject, 0);
            var validHandle = searchHandle > 0;

            var alarms = new List<Alarm>();

            try
            {
                while (validHandle)
                {
                    currentQuerySampleCount++;

                    //Get the SOE properties and generate a new alarm object
                    //The SOE properties must be retrieved for each instance
                    var alarm = new Alarm()
                    {
                        Tag = GetPropertyAsString(soeQueryObject, "TAG", 1000),

                        TimestampOccurrence =
                            DateTime.FromFileTimeUtc(
                                long.Parse(GetPropertyAsString(soeQueryObject, "TIMETICKS", 1000))),
                        TimestampTransition =
                            DateTime.FromFileTimeUtc(
                                long.Parse(GetPropertyAsString(soeQueryObject, "RECEIPTTIMETICKS", 1000))),
                        State = (AlarmState)int.Parse(GetPropertyAsString(soeQueryObject, "ALMQUERYVALUE", 1000))
                    };

                    //Get the properties for the alarm tag
                    //Note: You can optimize this code by caching (these properties do not change with each instance)
                    int alarmQueryObject = 0;
                    CtApiNativeMethods.FindFirst(_citectHandle, "Alarm", "TAG=" + alarm.Tag, out alarmQueryObject, 0);
                    alarm.Name = GetPropertyAsString(alarmQueryObject, "NAME", 1000);
                    alarm.Priority = GetPropertyAsString(alarmQueryObject, "PRIORITY", 1000);
                    alarm.AlarmType = GetPropertyAsString(alarmQueryObject, "ALARMTYPE", 1000);
                    alarm.Description = GetPropertyAsString(alarmQueryObject, "DESC", 1000);
                    alarm.Equipment = GetPropertyAsString(alarmQueryObject, "EQUIPMENT", 1000);

                    alarms.Add(alarm);
                    validHandle = CtApiNativeMethods.FindNext(searchHandle, out soeQueryObject);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (searchHandle > 0)
                {
                    CtApiNativeMethods.FindClose(searchHandle);
                }
            }

            Debug.WriteLine("Alarms SOEQuery Sample Count: " + currentQuerySampleCount);
            return alarms.AsReadOnly();
        }

        /// <summary>
        /// Displays the alarms in the listbox.
        /// </summary>
        /// <param name="alarms">The alarms.</param>
        private void UpdateAlarms(ReadOnlyCollection<Alarm> alarms)
        {
            var itemAdded = false;

            //process alarms in order
            var sortedAlarms = alarms.OrderBy(d => d.TimestampTransition.Ticks);

            foreach (var alarm in sortedAlarms)
            {
                //see if the item exists
                var existing = lstAlarms.Items.Find(alarm.Id, false);
                if (existing.Any())
                {
                    var selectedItem = existing[0];
                    selectedItem.SubItems[3].Text = alarm.IsOn ? "Active" : "Inactive";
                    selectedItem.SubItems[4].Text = alarm.IsAcknowledged ? "Ack" : "Unack";
                }
                else
                {
                    //add a new entry
                    var item = new ListViewItem(alarm.TimestampOccurrence.ToLocalTime()
                        .ToString("MM/dd/yy  hh:mm:ss.fff tt")) { Tag = alarm, Name = alarm.Id };
                    item.SubItems.Add(alarm.Name);
                    item.SubItems.Add(alarm.Equipment);
                    item.SubItems.Add(alarm.IsOn ? "Active" : "Inactive");
                    item.SubItems.Add(alarm.IsAcknowledged ? "Ack" : "Unack");
                    lstAlarms.Items.Add(item);
                    itemAdded = true;
                }
            }

            if (itemAdded)
            {
                lstAlarms.Sort(); //sort based on timestamp
            }
        }

        /// <summary>
        /// Handles the Tick event of the tmrAlarm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrAlarm_Tick(object sender, EventArgs e)
        {
            UpdateAlarms(PollForAlarms());
        }

        #endregion
    }
}