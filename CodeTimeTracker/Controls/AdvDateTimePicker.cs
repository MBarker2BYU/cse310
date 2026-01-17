// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-15-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="AdvDateTimePicker.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace CodeTimeTracker.Controls
{
    /// <summary>
    /// Class AdvDateTimePicker.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class AdvDateTimePicker : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvDateTimePicker"/> class.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Date and Time pickers must be added in designer.</exception>
        public AdvDateTimePicker()
        {
            InitializeComponent();

            // Make sure controls exist from designer
            if (dtpDate == null || dtpTime == null)
                throw new InvalidOperationException("Date and Time pickers must be added in designer.");

            // Set up initial behavior
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpTime.Format = DateTimePickerFormat.Custom;
            dtpTime.CustomFormat = "HH:mm:ss";
            dtpTime.ShowUpDown = true;

            // Wire value change events
            dtpDate.ValueChanged += (s, e) => OnValueChanged();
            dtpTime.ValueChanged += (s, e) => OnValueChanged();
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("The combined date and time value.")]
        public DateTime Value
        {
            get
            {
                return dtpDate.Value.Date + dtpTime.Value.TimeOfDay;
            }
            set
            {
                dtpDate.Value = value.Date;
                dtpTime.Value = value.Date + value.TimeOfDay; // preserve date part but only time matters
                OnValueChanged();
            }
        }

        /// <summary>
        /// Gets the date part.
        /// </summary>
        /// <value>The date part.</value>
        [Browsable(false)]
        public DateTime DatePart => dtpDate.Value.Date;

        /// <summary>
        /// Gets the time part.
        /// </summary>
        /// <value>The time part.</value>
        [Browsable(false)]
        public TimeSpan TimePart => dtpTime.Value.TimeOfDay;

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        public event EventHandler ValueChanged;


        /// <summary>
        /// Called when [value changed].
        /// </summary>
        protected virtual void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
