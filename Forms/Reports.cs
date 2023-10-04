using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeCapture.Forms
{
    public partial class Reports : Form
    {
        public DataSet dsTime = new DataSet();
        public Dictionary<string, double> aggregatedTotals = new Dictionary<string, double>();
        public Dictionary<int, double> aggregatedTotalsTickets = new Dictionary<int, double>();
        public TimeCapture timeCapture { get; set; }
        public Reports(TimeCapture time)
        {
            timeCapture = time;
            InitializeComponent();
            this.lblTicket.Visible = false;
            this.cbTicket.Visible = false;
            descTicket.Visible = false;
            descTicket2.Visible = false;
            GetTickets();
            SetReports();
            cbReportType.SelectedValueChanged += CbReportType_SelectedValueChanged;
            cbTicket.SelectedValueChanged += CbTicket_SelectedValueChanged;
        }

        private void CbReportType_SelectedValueChanged(object? sender, EventArgs e)
        {
            if (cbReportType.SelectedValue.ToString() != "-1")
                this.formsPlot1.Plot.Clear();
            this.formsPlot1.Refresh();

            this.lblTicket.Visible = false;
            this.cbTicket.Visible = false;
            descTicket.Visible = false;
            descTicket2.Visible = false;

            ReportType Type = ReportUtil.GetReportType(this.cbReportType.SelectedValue.ToString());
            bool OK;
            string sDate;
            string sEnd;

            if (Type == ReportType.ByDay)
            {
                timeCapture.ShowDateInputDialog("Please provide a date", out OK, out sDate);
                if (OK)
                    dsTime = new Access().getTimeByDay(sDate);
            }
            else if (Type == ReportType.ByDateRange)
            {
                timeCapture.ShowDateRangeInputDialog("Please provide a date range", out OK, out sDate, out sEnd);
                if (OK)
                    dsTime = new Access().GetTimeByDateRange(sDate, sEnd);
            }
            else if (Type == ReportType.ByTicket)
            {
                timeCapture.PushNofication("Please select a ticket number", NotificationType.Info);
                this.lblTicket.Visible = true;
                this.cbTicket.Visible = true;
                descTicket.Visible = true;
                descTicket2.Visible = true;
            }

            if (dsTime.HasRows())
            {
                /*
                 *  Goal:
                 * -----------------------------
                 * 
                 * 1. Compare values. Total time being the important one.
                 * 
                 *  Handle:
                 *  ----------------------------
                 *  
                 *      By Day:
                 *          Compare tasks by day. Foreach ticket number get the total time, then add to list
                 *         
                 *      By Date range:
                 *          Compare days, get the total time by day, add to list
                 *          
                 *      By Ticket No:
                 *          Compare by tickets, get total by ticket, add to list
                 * 
                 */

                List<Time> lTime = new List<Time>();
                List<double> lValues = new List<double>();
                double[] aValues;
                string[] aLables;
                List<string> lLables = new List<string>();
                if (Type == ReportType.ByDay)
                {
                    dsTime.Tables[0].DefaultView.Sort = "Date Asc";
                    foreach (DataRow row in dsTime.Tables[0].Rows)
                    {
                        lValues.Add((double)Convert.ToInt32(Convert.ToDateTime(row.GetDataRowStringValue("Total")).ToString("mm")));
                        lLables.Add(row.GetDataRowStringValue("Item"));
                    }
                    aValues = lValues.ToArray();
                    aLables = lLables.ToArray();

                    this.formsPlot1.Plot.AddBar(aValues);
                    this.formsPlot1.Plot.XTicks(aLables);
                    this.formsPlot1.Plot.SetAxisLimits(yMin: 0);
                    this.formsPlot1.Plot.SaveFig("ByDay.png");
                }
                else if (Type == ReportType.ByDateRange)
                {
                    foreach (DataRow row in dsTime.Tables[0].Rows)
                    {
                        Time time = new Time();
                        time.TimeID = row.GetDataRowIntValue("TimeID");
                        time.Item = row.GetDataRowStringValue("Item");
                        time.TicketNo = row.GetDataRowIntValue("TicketNo");
                        time.Start = row.GetDataRowStringValue("Start");
                        time.End = row.GetDataRowStringValue("End");
                        time.Total = row.GetDataRowStringValue("Total");
                        time.TimeType = row.GetDataRowStringValue("TimeType");
                        time.Description = row.GetDataRowStringValue("Description");
                        time.Type = row.GetDataRowStringValue("TicketType");
                        time.Date = row.GetDataRowStringValue("Date");
                        lTime.Add(time);
                    }

                    foreach (var item in lTime)
                    {
                        if (aggregatedTotals.ContainsKey(item.Date))
                        {
                            aggregatedTotals[item.Date] += (double)Convert.ToInt32(Convert.ToDateTime(item.Total).ToString("mm"));
                        }
                        else
                        {
                            aggregatedTotals[item.Date] = (double)Convert.ToInt32(Convert.ToDateTime(item.Total).ToString("mm"));
                        }
                    }

                    List<Time> aggregatedList = aggregatedTotals.Select(kv => new Time { Date = kv.Key, Total = kv.Value.ToString() }).ToList();

                    foreach (var item in aggregatedList)
                    {
                        lValues.Add((double)Convert.ToInt32(item.Total));
                        lLables.Add(item.Date);
                    }

                    aValues = lValues.ToArray();
                    aLables = lLables.ToArray();

                    this.formsPlot1.Plot.AddBar(aValues);
                    this.formsPlot1.Plot.XTicks(aLables);
                    this.formsPlot1.Plot.SetAxisLimits(yMin: 0);
                    this.formsPlot1.Plot.SaveFig("ByDay.png");
                }
                this.formsPlot1.Refresh();
            }
        }

        private void CbTicket_SelectedValueChanged(object? sender, EventArgs e)
        {
            this.formsPlot1.Plot.Clear();
            dsTime = new Access().GetTimeByTicketNo(cbTicket.SelectedValue.ToString());
            List<Time> lTime = new List<Time>();
            List<double> lValues = new List<double>();
            double[] aValues;
            string[] aLables;
            List<string> lLables = new List<string>();
            if (dsTime.HasRows())
            {
                foreach (DataRow row in dsTime.Tables[0].Rows)
                {
                    Time time = new Time();
                    time.TimeID = row.GetDataRowIntValue("TimeID");
                    time.Item = row.GetDataRowStringValue("Item");
                    time.TicketNo = row.GetDataRowIntValue("TicketNo");
                    time.Start = row.GetDataRowStringValue("Start");
                    time.End = row.GetDataRowStringValue("End");
                    time.Total = row.GetDataRowStringValue("Total");
                    time.TimeType = row.GetDataRowStringValue("TimeType");
                    time.Description = row.GetDataRowStringValue("Description");
                    time.Type = row.GetDataRowStringValue("TicketType");
                    time.Date = row.GetDataRowStringValue("Date");
                    lTime.Add(time);
                }

                foreach (var item in lTime)
                {
                    if (aggregatedTotalsTickets.ContainsKey(item.TicketNo))
                    {
                        aggregatedTotalsTickets[item.TicketNo] += (double)Convert.ToInt32(Convert.ToDateTime(item.Total).ToString("mm"));
                    }
                    else
                    {
                        aggregatedTotalsTickets[item.TicketNo] = (double)Convert.ToInt32(Convert.ToDateTime(item.Total).ToString("mm"));
                    }
                }

                List<Time> aggregatedList = aggregatedTotalsTickets.Select(kv => new Time { TicketNo = kv.Key, Total = kv.Value.ToString() }).ToList();

                foreach (var item in aggregatedList)
                {
                    lValues.Add((double)Convert.ToInt32(item.Total));
                    lLables.Add(item.TicketNo.ToString());
                }

                aValues = lValues.ToArray();
                aLables = lLables.ToArray();

                this.formsPlot1.Plot.AddBar(aValues);
                this.formsPlot1.Plot.XTicks(aLables);
                this.formsPlot1.Plot.SetAxisLimits(yMin: 0);
                this.formsPlot1.Plot.SaveFig("ByDay.png");
            }
            this.formsPlot1.Refresh();

        }

        public void GetTickets()
        {
            List<CSVImport.Tickets> lTickets = new();
            DataSet dsTickets = new Access().GetTickets();

            if (dsTickets.HasRows())
            {
                foreach (DataRow row in dsTickets.Tables[0].Rows)
                {
                    CSVImport.Tickets csvTicket = new CSVImport.Tickets();
                    csvTicket.ID = row.GetDataRowIntValue("ID");
                    csvTicket.Name = row.GetDataRowStringValue("Name");
                    lTickets.Add(csvTicket);
                }
            }

            cbTicket.DataSource = lTickets;
            cbTicket.DisplayMember = "Name";
            cbTicket.ValueMember = "ID";
        }

        public void SetReports()
        {
            List<Report> reports = new List<Report>();
            Report none = new Report()
            {
                ID = -1, Name = "Please select..."
            };
            reports.Add(none);
            foreach (Report report in Report.Reports)
            {
                reports.Add(report);
            }

            cbReportType.DataSource = reports;
            cbReportType.DisplayMember = "Name";
            cbReportType.ValueMember = "ID";
        }

        public class Report
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public static List<Report> Reports = new List<Report>
            {
                new Report
                {
                    ID = 0,
                    Name = "By Day"
                },
                new Report
                {
                    ID = 1,
                    Name = "By Date Range"
                },
                new Report
                {
                    ID = 2,
                    Name = "By Ticket"
                }
            };
        }

        public enum ReportType
        {
            ByDay,
            ByDateRange,
            ByTicket
        };
    }

    public static class ReportUtil
    {
        public static Reports.ReportType GetReportType(string Type)
        {
            Reports.ReportType reportType = (Reports.ReportType)Enum.Parse(typeof(Reports.ReportType), Type.Replace(" ", "").ToString());
            return reportType;
        }
    }

}
