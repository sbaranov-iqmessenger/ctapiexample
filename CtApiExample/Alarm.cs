using System;

namespace CtApiExample
{
    /// <summary>
    /// Class Alarm.
    /// </summary>
    public class Alarm
    {
        /// <summary>
        /// Gets or sets the timestamp occurrence.
        /// This is the converted "TIMETICKS" property.
        /// </summary>
        /// <value>The timestamp occurrence.</value>
        public DateTime TimestampOccurrence { get; set; }

        /// <summary>
        /// Gets or sets the timestamp transition.
        /// This is the converted "RECEIPTTIMETICKS" property.
        /// </summary>
        /// <value>The timestamp transition.</value>
        public DateTime TimestampTransition { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the equipment.
        /// </summary>
        /// <value>The equipment.</value>
        public string Equipment { get; set; }
        
        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public string Priority { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the alarm.
        /// </summary>
        /// <value>The type of the alarm.</value>
        public string AlarmType { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public AlarmState State { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is disabled.
        /// </summary>
        /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
        public bool IsDisabled => State.HasFlag(AlarmState.Disabled);

        /// <summary>
        /// Gets a value indicating whether this instance is acknowledged.
        /// </summary>
        /// <value><c>true</c> if this instance is acknowledged; otherwise, <c>false</c>.</value>
        public bool IsAcknowledged => State.HasFlag(AlarmState.Ack);

        /// <summary>
        /// Gets a value indicating whether this instance is good.
        /// </summary>
        /// <value><c>true</c> if this instance is good; otherwise, <c>false</c>.</value>
        public bool IsGood => State.HasFlag(AlarmState.Good);

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is on; otherwise, <c>false</c>.</value>
        public bool IsOn => State.HasFlag(AlarmState.On);

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id => Tag + TimestampOccurrence.Ticks;
    }
}
