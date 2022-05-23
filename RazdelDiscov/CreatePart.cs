using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
namespace RazdelDiscov
{
    class CreatePart
    { 
        public static int CreatePartition(int partitionSizeInGB)
        {
            
            
            string sd = string.Empty;
            int result = 0;

            List<DriveInfo> allDrivesInfo = DriveInfo.GetDrives().Where(x => x.DriveType != DriveType.Network).OrderBy(c => c.Name).ToList();

            char newDriveName = allDrivesInfo.LastOrDefault().Name.FirstOrDefault();
            newDriveName = (Char)(Convert.ToUInt16(newDriveName) + 1);

            List<DriveInfo> allFixDrives = DriveInfo.GetDrives().Where(c => c.DriveType == DriveType.Fixed).ToList();

            try
            {
                string scriptFilePath = System.IO.Path.GetTempPath() + @"\dpScript.txt";
                string driveName = allFixDrives.FirstOrDefault().Name;

                if (File.Exists(scriptFilePath))
                {
                    File.Delete(scriptFilePath);
                }

                File.AppendAllText(scriptFilePath,
                string.Format(
                "SELECT DISK=0\n" +
                "SELECT VOLUME=2\n" +
                "SHRINK DESIRED={1} MINIMUM={1}\n" +
                "CREATE PARTITION PRIMARY\n" +
                "ASSIGN \n" +
                "FORMAT FS=FAT32 QUICK\n" +
                "EXIT", driveName, partitionSizeInGB * 1000, newDriveName));
                int exitcode = 0;
                string resultSen = ExecuteCmd.ExecuteCmdCommand("DiskPart.exe" + " /s " + scriptFilePath, ref exitcode);
                File.Delete(scriptFilePath);
                if (exitcode > 0)
                {
                    result = exitcode;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}