using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmmoTracker.Databases;

namespace AmmoTracker
{
    public partial class MinimumThreshold : Form
    {
        public MinimumThreshold()
        {
            InitializeComponent();
        }

        public MinimumThreshold(DatabaseHelper databaseHelper, long typeId, long minimumThreshold) : this()
        {

            m_DatabaseHelper = databaseHelper;
            m_TypeId = typeId;
            m_MinimumThreshold = minimumThreshold;

        }

        private readonly DatabaseHelper m_DatabaseHelper;
        private readonly long m_TypeId;
        private readonly long m_MinimumThreshold;

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if(m_MinimumThreshold == nudMinimumThreshold.Value)
                return;

            if (MessageBox.Show(@"Do you want to continue updating the Minimum Threshold?", @"Update Minimum Threshold", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                nudMinimumThreshold.Focus();
                return;
            }

            m_DatabaseHelper.CRUD.UpdateMinimumThreshold(m_TypeId, Convert.ToInt64(nudMinimumThreshold.Value));

            DialogResult = DialogResult.OK;
        }
    }
}
