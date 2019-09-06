using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using IronPython.Hosting;
using System.Diagnostics;

namespace BaseStationManager
{
    public partial class Form1 : Form
    {
        private bool data = false;
        private string datasrc;
        private string local;
        private string destyear;
        private string mlcl;
        private bool convert = false;
        private bool dwnld = true;
        private List<DateTime> choosen = new List<DateTime>();
        public Form1()
        {
            InitializeComponent();
            Control();
        }

        private void Control()
        {
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            convert = false;
            if (data == false)
            {
                DateTime past = DateTime.Now;
                // int y = past.Year;
                // int m = past.Month;
                // int d = past.Day -2;
                // if(d == 0)
                // {
                //     m = past.Month - 1;
                //     d = past.
                // }
                // int h = past.Hour;
                // int min = past.Minute;
                // int s = past.Second;
                // int mill = past.Millisecond;

                TimeSpan a = new TimeSpan(2, 0, 0, 0);

                dateTimePicker1.Value = past.Subtract(a);//new System.DateTime(y, m, d, h, min, s, mill);
                dateTimePicker1.Update();
                checkedListBox1.Enabled = false;
                checkedListBox1.BackColor = Color.DarkGray;
                checkedListBox1.Update();
                label4.Text = "Enter SRC Folder";
                label4.Update();
                button1.Enabled = false;
                button1.Update();
                button5.Enabled = false;
                button5.Update();
                string history = @"\\video-01\Operations\SBETs\Apps\BaseStationManager\dbpath.txt";
                string init;
                if (File.Exists(history))
                {
                    using (StreamReader sr = new StreamReader(history))
                    {
                        // Read the stream to a string, and write the string to the console.
                        init = sr.ReadToEnd();

                    }
                    textBox2.Text = init;
                    textBox2.Update();
                }
                all.Enabled = false;
                none.Enabled = false;
               
            }
            else if (data == true)
            {
                checkedListBox1.Enabled = true;
                checkedListBox1.BackColor = Color.White;
                all.Enabled = true;
                none.Enabled = true;

                Chilkat.Csv csv = new Chilkat.Csv();
                csv.HasColumnNames = true;
                csv.LoadFile(datasrc);
                int row;
                int n = csv.NumRows;
                string[] list = new string[n];
                string[] defa = new string[n];
                for (row = 0; row<=n-1; row++)
                {
                    list[row] = csv.GetCell(row, 1);
                    defa[row] = csv.GetCell(row, 2);
                }
                checkedListBox1.Items.AddRange(list);
                checkedListBox1.CheckOnClick = true;
                

                for (int x = 0; x<list.Length; x++)
                {
                    if(defa[x] == "Y")
                    {
                        checkedListBox1.SetItemChecked(x,true);
                    }
                }

              
            }
        }

        private List<string> getselected()
        {
            List<string> count = new List<string>();
            int cnt = 0;
            foreach (object itemchecked in checkedListBox1.CheckedItems)
            {
                count.Add(itemchecked.ToString());
                cnt += 1; 
            }

            //label4.Text = string.Format("{0} Stations Selected",cnt.ToString());
            //label4.Update();
            return count;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (choosen.Count() == 0)
            {
                if (dwnld)
                {
                    List<string> id = getselected();
                    string bse = "ftp://cors.lsu.edu/FTP_push/A/t02/";
                    DateTime end = dateTimePicker1.Value;
                    string year = dateTimePicker1.Value.ToString();
                    char[] check = year.ToCharArray();
                    //MessageBox.Show(year);
                    string y = check[0].ToString() + check[1].ToString() + check[2].ToString() + check[3].ToString();
                    string mth = check[5].ToString() + check[6].ToString();
                    string day = check[8].ToString() + check[9].ToString();
                    string file = y + mth + day + "0000A";
                    destyear = y + mth + day;
                    DateTime initial = new DateTime(Convert.ToInt32(y), 1, 1);
                    string days = ((end.Date - initial.Date).Days + 1).ToString();
                    if (days.Count() < 3)
                    {
                        days = "0" + days;
                    }
                    if (id.Count() == 0)
                    {
                        MessageBox.Show("You did not choose anything");
                    }
                    else
                    {
                        for (int x = 0; x < id.Count(); x++)
                        {
                            try
                            {
                                string site = bse + y + "/" + days + "/" + id[x] + "/" + id[x] + file + ".T02";
                                //site = "ftp://cors.lsu.edu//FTP_push/A/t02/README.txt";
                                //MessageBox.Show(site);
                                using (WebClient request = new WebClient())
                                {
                                    //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(site);
                                    //request.Method = WebRequestMethods.Ftp.DownloadFile;
                                    request.Credentials = new NetworkCredential("Roadware", "fugroFTP.18");
                                    string lcl = local + "\\" + id[x] + file + ".T02";
                                    //MessageBox.Show(lcl);
                                    string download = string.Format("Downloading: {0}", id[x]);
                                    label4.Text = download;
                                    label4.Update();
                                    if (!Directory.Exists(local))
                                    {
                                        Directory.CreateDirectory(local);
                                    }
                                    request.DownloadFile(site, lcl);
                                    //using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                    //using (Stream fileStream = File.Create(lcl))
                                    //{
                                    //  ftpStream.CopyTo(fileStream);
                                    //}
                                }
                                //MessageBox.Show("Done 2");
                                string progress = string.Format("{0} Downloaded", (id[x]));
                                label4.Text = progress;
                                label4.Update();
                            }
                            catch (WebException ex)
                            {
                                //MessageBox.Show(((FtpWebResponse)ex.Response).StatusDescription);
                                string inval = string.Format("Base Station ID {0} not found", (id[x]));
                                MessageBox.Show(inval);
                            }
                        }
                        //MessageBox.Show(response.StatusDescription);
                        label4.Text = "Finished donwloading";
                        label4.Update();
                    }
                }
                else if (convert)
                {
                    string transfer = @"\\video-01\Operations\SBETs\Apps\BaseStationManager\base.txt";
                    string recieve = @"\\video-01\Operations\SBETs\Apps\BaseStationManager\station.txt";
                    try
                    {
                        if (File.Exists(transfer))
                        {
                            File.Delete(transfer);
                        }
                        using (StreamWriter writer = new StreamWriter(transfer, false))
                        {
                            writer.WriteLine(destyear);
                            writer.WriteLine(local);
                        }
                        if (File.Exists(recieve))
                        {
                            File.Delete(recieve);
                        }
                    }
                    catch (Exception ex)
                    {
                        label4.Text = ex.ToString();
                        label4.Update();
                    }

                    string destpy = "\\\\video-01\\Operations\\SBETs\\LA19_LSU_Rinex_Files\\" + destyear;
                    if (Directory.Exists(destpy))
                    {
                        label4.Text = destyear + " exists";
                        label4.Update();
                    }
                    else
                    {
                        Directory.CreateDirectory(destpy);
                        label4.Text = "Creating " + destyear + " directory";
                        label4.Update();
                    }

                    string filename = @"\\video-01\Operations\SBETs\Apps\BaseStationManager\module1.py";
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(@"\\video-01\Operations\SBETs\Apps\BaseStationManager\dist\module1.exe")
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    label4.Text = "Converting..";
                    label4.Update();
                    try
                    {
                        p.Start();
                    }
                    catch (Exception ew)
                    {
                        label4.Text = ew.Message;
                        label4.Update();
                    }
                    p.WaitForExit();
                    p.Close();
                    p.Dispose();
                    label4.Text = "Done Converting";
                    label4.Update();
                    try
                    {
                        if (p.HasExited == false)
                        {
                            p.Kill();
                        }
                    }
                    catch
                    {

                    }
                    while (!File.Exists(recieve))
                    {
                        label4.Text = "Running Script...";
                        label4.Update();
                    }
                    string doneG;
                    using (StreamReader sr = new StreamReader(recieve))
                    {
                        // Read the stream to a string, and write the string to the console.
                        doneG = sr.ReadToEnd();

                    }
                    if (doneG == "true")
                    {
                        label4.Text = "Completed Program";
                        label4.Update();
                    }
                    else if (doneG == "false")
                    {
                        label4.Text = "Convert Rinex was not succesful ";
                        label4.Update();

                    }
                    string history = @"\\video-01\Operations\SBETs\Apps\BaseStationManager\dbpath.txt";
                    if (File.Exists(history))
                    {
                        File.Delete(history);
                    }
                    using (FileStream fs = File.Create(history))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(local);
                        fs.Write(info, 0, info.Length);
                    }
                }
            }
            else if (choosen.Count() > 0)
            { string glob  = mlcl;
                if (dwnld)
                {
                    foreach (var time in choosen)
                    {
                        string loca;
                        List<string> id = getselected();
                        string bse = "ftp://cors.lsu.edu/FTP_push/A/t02/";
                        DateTime end = time;
                        string year = time.ToString();
                        char[] check = year.ToCharArray();
                        //MessageBox.Show(year);
                        string y = check[0].ToString() + check[1].ToString() + check[2].ToString() + check[3].ToString();
                        string mth = check[5].ToString() + check[6].ToString();
                        string day = check[8].ToString() + check[9].ToString();
                        string file = y + mth + day + "0000A";
                        destyear = y + mth + day;
                        if (mlcl.Contains("LSU_RINEX_T02"))
                        {
                            loca = "\\\\video-01\\Operations\\SBETs\\LSU_RINEX_T02\\" + destyear;
                        }
                        else
                        {
                            loca = mlcl + "\\" + destyear;
                        }
                        glob = loca;
                        DateTime initial = new DateTime(Convert.ToInt32(y), 1, 1);
                        string days = ((end.Date - initial.Date).Days + 1).ToString();
                        Console.WriteLine("day: " + days);
                        if (days.Count() < 3 && days.Count() > 2)
                        {
                            days = "0" + days;
                        }
                        else if (days.Count() < 2)
                        {
                            days = "00" + days;
                        }
                        if (id.Count() == 0)
                        {
                            MessageBox.Show("You did not choose anything");
                        }
                        else
                        {
                            for (int x = 0; x < id.Count(); x++)
                            {
                                try
                                {
                                    string site = bse + y + "/" + days + "/" + id[x] + "/" + id[x] + file + ".T02";
                                    //site = "ftp://cors.lsu.edu//FTP_push/A/t02/README.txt";
                                    //MessageBox.Show(site);
                                    using (WebClient request = new WebClient())
                                    {
                                        //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(site);
                                        //request.Method = WebRequestMethods.Ftp.DownloadFile;
                                        request.Credentials = new NetworkCredential("Roadware", "fugroFTP.18");
                                        string lcl = loca + "\\" + id[x] + file + ".T02";
                                        //MessageBox.Show(lcl);
                                        string download = string.Format("Downloading: {0}", id[x]);
                                        label4.Text = download;
                                        label4.Update();
                                        if (!Directory.Exists(mlcl))
                                        {
                                            Directory.CreateDirectory(mlcl);
                                        }
                                        if (!Directory.Exists(loca))
                                        {
                                            Directory.CreateDirectory(loca);
                                        }
                                        request.DownloadFile(site, lcl);
                                        //using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                        //using (Stream fileStream = File.Create(lcl))
                                        //{
                                        //  ftpStream.CopyTo(fileStream);
                                        //}
                                    }
                                    //MessageBox.Show("Done 2");
                                    string progress = string.Format("{0} Downloaded", (id[x]));
                                    label4.Text = progress;
                                    label4.Update();
                                }
                                catch (WebException ex)
                                {
                                    //MessageBox.Show(((FtpWebResponse)ex.Response).StatusDescription);
                                    string inval = string.Format("Base Station ID {0} not found", (id[x]));
                                    MessageBox.Show(inval);
                                }
                            }

                            //MessageBox.Show(response.StatusDescription);
                            label4.Text = "Finished donwloading";
                            label4.Update();
                        }
                    }
                }

                else if (convert)
                {
                    foreach (var time in choosen)
                    {
                        string loca;
                        DateTime end = time;
                        string year = time.ToString();
                        char[] check = year.ToCharArray();
                        //MessageBox.Show(year);
                        string y = check[0].ToString() + check[1].ToString() + check[2].ToString() + check[3].ToString();
                        string mth = check[5].ToString() + check[6].ToString();
                        string day = check[8].ToString() + check[9].ToString();
                        destyear = y + mth + day;
                        if (mlcl.Contains("LSU_RINEX_T02"))
                        {
                            loca = "\\\\video-01\\Operations\\SBETs\\LSU_RINEX_T02\\" + destyear;
                        }
                        else
                        {
                            loca = mlcl + "\\" + destyear;
                        }
                        glob = loca;
                        string transfer = @"\\video-01\Operations\SBETs\Apps\BaseStationManager-r2.0\base.txt";
                        string recieve = @"\\video-01\Operations\SBETs\Apps\BaseStationManager-r2.0\station.txt";
                        try
                        {
                            if (File.Exists(transfer))
                            {
                                File.Delete(transfer);
                            }
                            using (StreamWriter writer = new StreamWriter(transfer, false))
                            {
                                writer.WriteLine(destyear);
                                writer.WriteLine(glob);
                            }
                            if (File.Exists(recieve))
                            {
                                File.Delete(recieve);
                            }
                        }
                        catch (Exception ex)
                        {
                            label4.Text = ex.ToString();
                            label4.Update();
                        }

                        string destpy = "\\\\video-01\\Operations\\SBETs\\LA19_LSU_Rinex_Files\\" + destyear;
                        if (Directory.Exists(destpy))
                        {
                            label4.Text = destyear + " exists";
                            label4.Update();
                        }
                        else
                        {
                            Directory.CreateDirectory(destpy);
                            label4.Text = "Creating " + destyear + " directory";
                            label4.Update();
                        }

                        string filename = @"\\video-01\Operations\SBETs\Apps\BaseStationManager-r2.0\module1.py";
                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(@"\\video-01\Operations\SBETs\Apps\BaseStationManager-r2.0\dist\module1.exe")
                        {
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        label4.Text = "Converting..";
                        label4.Update();
                        try
                        {
                            p.Start();
                        }
                        catch (Exception ew)
                        {
                            label4.Text = ew.Message;
                            label4.Update();
                        }
                        p.WaitForExit();
                        p.Close();
                        p.Dispose();
                        label4.Text = "Done Converting";
                        label4.Update();
                        try
                        {
                            if (p.HasExited == false)
                            {
                                p.Kill();
                            }
                        }
                        catch
                        {

                        }
                        while (!File.Exists(recieve))
                        {
                            label4.Text = "Running Script...";
                            label4.Update();
                        }
                        string doneG;
                        using (StreamReader sr = new StreamReader(recieve))
                        {
                            // Read the stream to a string, and write the string to the console.
                            doneG = sr.ReadToEnd();

                        }
                        if (doneG == "true")
                        {
                            label4.Text = "Completed Program";
                            label4.Update();
                        }
                        else if (doneG == "false")
                        {
                            label4.Text = "Convert Rinex was not succesful ";
                            label4.Update();

                        }
                        string history = @"\\video-01\Operations\SBETs\Apps\BaseStationManager-r2.0\dbpath.txt";
                        if (File.Exists(history))
                        {
                            File.Delete(history);
                        }
                        using (FileStream fs = File.Create(history))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(local);
                            fs.Write(info, 0, info.Length);
                        }
                    }
                }
                }
            }
        

        private void Button2_Click(object sender, EventArgs e)
        {
            string src = textBox1.Text;
            FolderBrowserDialog fldr = new FolderBrowserDialog();
            OpenFileDialog ofDlg = new OpenFileDialog();
            if (!string.IsNullOrEmpty(src))
            {
                ofDlg.InitialDirectory = Path.GetDirectoryName(src);
            }
            ofDlg.Filter = "csv Files (*.csv|*.CSV";
            ofDlg.FilterIndex = 1;
            ofDlg.Multiselect = false;
            ofDlg.Title = "Select Station ID csv file";
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofDlg.FileNames[0];
                datasrc = ofDlg.FileNames[0];
                data = true;
                Control();
            }
            else
            {
                MessageBox.Show("No input given");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string dest = dateTimePicker1.Value.ToString();
            //FolderBrowserDialog fldr = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(dest))
            {
                //textBox2.Text = fldr.SelectedPath;
                char[] check = dest.ToCharArray();
                //MessageBox.Show(year);
                string y = check[0].ToString() + check[1].ToString() + check[2].ToString() + check[3].ToString();
                string mth = check[5].ToString() + check[6].ToString();
                string day = check[8].ToString() + check[9].ToString();
                dest = y + mth + day;
                destyear = dest;
                textBox2.Text = "\\\\video-01\\Operations\\SBETs\\LSU_RINEX_T02\\" + dest;
                local = textBox2.Text;
                mlcl = textBox2.Text;
            }
            //if (fldr.ShowDialog() == DialogResult.OK)
            //{
              //  textBox2.Text = fldr.SelectedPath;
                //local = fldr.SelectedPath;
            //}
            else
            {
                MessageBox.Show("No Date given");
            }
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            int cnt = 0;
            foreach (object itemchecked in checkedListBox1.CheckedItems)
            {
                //count.Add(itemchecked.ToString());
                cnt += 1;
            }
            
            if(cnt == 0)
            {
                button1.Enabled = false;
                button1.Update();
                button5.Enabled = false;
                button5.Update();
            }
            else
            {
                button1.Enabled = true;
                button1.Update();
                button5.Enabled = true;
                button5.Update();
            }

            label4.Text = string.Format("{0} Stations Selected", cnt.ToString());
            label4.Update();
        }


        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            local = textBox2.Text;
            mlcl = local;
        }

        private void All_CheckedChanged(object sender, EventArgs e)
        {
            if (all.Checked)
            {
                CheckedListBox.ObjectCollection all = checkedListBox1.Items;
                int cnt = all.Count;
                for (int i = 0; i<cnt; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void None_CheckedChanged(object sender, EventArgs e)
        {
            if (none.Checked)
            {
                CheckedListBox.ObjectCollection all = checkedListBox1.Items;
                int cnt = all.Count;
                for (int i = 0; i < cnt; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            choosen.Add(dateTimePicker1.Value);
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            Multiple a = new Multiple();
            a.ShowDialog();
            choosen = a.r;
            textBox3.Text = a.start.ToString();
            textBox4.Text = a.end.ToString();
            textBox3.Update();
            textBox4.Update();

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            convert = true;
            dwnld = false;
            button1.PerformClick();
        }
    }
}
