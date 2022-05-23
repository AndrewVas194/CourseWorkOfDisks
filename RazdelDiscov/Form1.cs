using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace RazdelDiscov
{
    public partial class Form1 : Form
    {

        DriveInfo[] allDrives = DriveInfo.GetDrives();
        Process proc = new Process();
        public Form1()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                        
                        listView1.Items.Add("Имя диска: " + drive.Name);
                        listView1.Items.Add("Файловая система: " + drive.DriveFormat);
                        listView1.Items.Add("Тип диска: " + drive.DriveType);
                        listView1.Items.Add("Объем доступного свободного места: " + ((drive.AvailableFreeSpace / 1024) / 1024) / 1024 + " гб");
                        listView1.Items.Add("Готов ли диск: " + drive.IsReady);
                        listView1.Items.Add("Корневой каталог диска: " + drive.RootDirectory);
                        listView1.Items.Add("Размер диска (в байтах): " + ((drive.TotalSize / 1024) / 1024) / 1024 + " гб");
                        listView1.Items.Add("Метка тома диска: " + drive.VolumeLabel);
                        listView1.Items.Add("________________________________________");
                    
                    
                }
                catch { }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    comboBox1.Items.Add("Метка диска: " + drive.Name);
                    comboBox4.Items.Add("Метка диска: " + drive.Name);
                    comboBox5.Items.Add("Метка диска: " + drive.Name);

                }
                catch { }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();


            listView1.Items.Add("Диск " + allDrives[comboBox1.SelectedIndex].Name);
            listView1.Items.Add("  Тип: " + allDrives[comboBox1.SelectedIndex].DriveType);
            if (allDrives[comboBox1.SelectedIndex].IsReady == true)
            {
                listView1.Items.Add("  Метка тома: " + allDrives[comboBox1.SelectedIndex].VolumeLabel);
                
                listView1.Items.Add("  Имя файловой системы: " + allDrives[comboBox1.SelectedIndex].DriveFormat);
                
                listView1.Items.Add(
                    "  Объем доступного свободного места на диске: " +
                    Math.Round((allDrives[comboBox1.SelectedIndex].AvailableFreeSpace) / Math.Pow(1024, 3)));

                listView1.Items.Add(
                    "  Общий объем доступного свободного места на диске: " +
                     Math.Round((allDrives[comboBox1.SelectedIndex].TotalFreeSpace) / Math.Pow(1024, 3)));

                listView1.Items.Add(
                    "  Общий размер места для хранения: " +
                    Math.Round((allDrives[comboBox1.SelectedIndex].TotalSize) / Math.Pow(1024, 3)));

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                int partitionSize = Convert.ToInt16(comboBox3.SelectedItem);
                CreatePart.CreatePartition(partitionSize);

                listView1.Items.Add("Formate completed");
                listView1.Items.Add("Пожалуйста, нажмите кнопку Рестарт");
            }
            catch (Exception ex)
            {
                listView1.Items.Add(ex.Message);
            }
        }


        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Items.Add("Диск " + allDrives[comboBox5.SelectedIndex].Name);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = @"C:\Windows\System32\diskpart.exe";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.StandardInput.WriteLine("select disk 0");
            proc.StandardInput.WriteLine("select partition " + (comboBox5.SelectedIndex + 1));
            proc.StandardInput.WriteLine("delete partition");
            proc.StandardInput.WriteLine("exit");
            proc.WaitForExit();
            listView1.Items.Add("Удаление успешно");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Items.Add("Диск " + allDrives[comboBox4.SelectedIndex].Name);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            proc.StartInfo.UseShellExecute = false;           
            proc.StartInfo.RedirectStandardOutput = true;            
            proc.StartInfo.FileName = @"C:\Windows\System32\diskpart.exe";           
            proc.StartInfo.RedirectStandardInput = true;           
            proc.StartInfo.CreateNoWindow = true;           
            proc.Start();
            proc.StandardInput.WriteLine("select disk 0");
            proc.StandardInput.WriteLine("select volume " + allDrives[comboBox4.SelectedIndex].Name);           
            proc.StandardInput.WriteLine("create partition primary");
            proc.StandardInput.WriteLine("format fs=ntfs quick");
            proc.StandardInput.WriteLine("exit");
            proc.WaitForExit();
            listView1.Items.Add("Форматирование успешно");
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        
    }
}
