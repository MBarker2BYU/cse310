using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeTimeTracker.Controls
{
    public partial class AdvDateTimePicker : UserControl
    {
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

        [Browsable(false)]
        public DateTime DatePart => dtpDate.Value.Date;

        [Browsable(false)]
        public TimeSpan TimePart => dtpTime.Value.TimeOfDay;

        public event EventHandler ValueChanged;

        
        protected virtual void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
