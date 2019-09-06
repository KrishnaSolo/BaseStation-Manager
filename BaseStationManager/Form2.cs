using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseStationManager
{
    public partial class Multiple : Form
    {
        public List<DateTime> r = new List<DateTime>();
        public DateTime start = new DateTime(2,2,2);
        public DateTime end = new DateTime(2,2,2);

        public Multiple()
        {
            InitializeComponent();
            DateTime past = DateTime.Now;
            TimeSpan a = new TimeSpan(2, 0, 0, 0);

            monthCalendar1.SetDate(past.Subtract(a));
            //monthCalendar1.SetDate(new System.DateTime(y, m, d));
            r.Clear();
            label1.Text = "Items Selected: " + r.Count();
            label1.Update();
        }

        private void Ranger()
        {
            monthCalendar1.MaxSelectionCount = 365;
            monthCalendar1.SetSelectionRange(start, end);
            SelectionRange range = monthCalendar1.SelectionRange;

            var temp = range.Start;
            while (temp != range.End)
            {
                r.Add(temp);
                temp = temp.AddDays(1);
            }
            r.Add(temp);
            label1.Text = "Items Selected: " + r.Count();
            label1.Update();
        }

        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void MonthCalendar1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
            if(start == new DateTime(2, 2, 2))
            {
                start = monthCalendar1.SelectionStart;
            }
            else if(end == new DateTime(2, 2, 2))
            {
                end = monthCalendar1.SelectionEnd;
                Ranger();
            }

        }
    }
}
