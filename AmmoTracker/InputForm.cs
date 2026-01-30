using AmmoTracker.Databases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmmoTracker
{
    public partial class InputForm : Form
    {
        private readonly DatabaseHelper m_DatabaseHelper;
        private readonly string m_TableName;     // "Manufacturers", "Calibers", or "Grains"
        private readonly string m_ColumnName;    // "ManufacturerName", "CaliberName", "GrainValue"
        private readonly string m_DisplayLabel;

        public long NewId { get; private set; } = -1;

        [Obsolete("This is only used by the designer", true)]
        public InputForm()
        { }

        public InputForm(DatabaseHelper dbHelper, string tableName, string columnName, string displayLabel)
        {
            m_DatabaseHelper = dbHelper;
            m_TableName = tableName;
            m_ColumnName = columnName;
            m_DisplayLabel = displayLabel;

            InitializeComponent();

            Text = $@"Add New {m_DisplayLabel}";
            lblPrompt.Text = $@"Enter new {m_DisplayLabel}:";
            btnAdd.Text =@"Add";
            btnCancel.Text = @"Cancel";
        }

        [AllowNull] 
        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var value = txtInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show($@"{m_DisplayLabel} cannot be empty.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInput.Focus();
                return;
            }

            if (m_DatabaseHelper.CRUD.ValueAlreadyExists(m_TableName, m_ColumnName, value))
            {
                MessageBox.Show($@"{m_DisplayLabel} '{value}' already exists.", @"Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInput.Focus();
                txtInput.SelectAll();
                return;
            }

            NewId = m_DatabaseHelper.CRUD.InsertNewValue(m_TableName, m_ColumnName, value);

            if (NewId > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
