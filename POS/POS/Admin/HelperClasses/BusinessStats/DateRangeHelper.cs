using System;
using System.Windows.Forms;

namespace POS.Admin
{
    public static class DateRangeHelper
    {
        public static void SetupDateDefaults(DateTimePicker dtpFrom, DateTimePicker dtpTo, ComboBox cmbQuickFilter)
        {
            dtpFrom.MaxDate = dtpTo.MaxDate = DateTime.Today;
            dtpFrom.MinDate = dtpTo.MinDate = new DateTime(2000, 1, 1);
            cmbQuickFilter.Items.AddRange(new[] { "Today", "This Week", "This Month", "This Year", "All Time", "Custom" });
            dtpFrom.Enabled = dtpTo.Enabled = false;
            dtpFrom.Value = new DateTime(2000, 1, 1);
            dtpTo.Value = DateTime.Today;
            cmbQuickFilter.SelectedIndex = 4;
        }

        public static (DateTime from, DateTime to) GetFilterDates(string filter, DateTime now)
        {
            return filter switch
            {
                "Today" => (now, now),
                "This Week" => (now.AddDays(-(int)now.DayOfWeek), now),
                "This Month" => (new DateTime(now.Year, now.Month, 1), now),
                "This Year" => (new DateTime(now.Year, 1, 1), now),
                "All Time" => (new DateTime(2000, 1, 1), now),
                _ => (now, now)
            };
        }
    }
}