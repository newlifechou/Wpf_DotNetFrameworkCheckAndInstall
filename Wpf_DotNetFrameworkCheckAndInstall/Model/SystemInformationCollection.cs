using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace Wpf_DotNetFrameworkCheckAndInstall.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemInformationCollection
    {
        public string OSVersion
        {
            get
            {
                return Environment.OSVersion.ToString();
            }
        }

        public string NetFrameworkVersion
        {
            get
            {
                return Environment.Version.ToString();
            }
        }

        public string ProcessorCount
        {
            get
            {
                return Environment.ProcessorCount.ToString();
            }
        }

        public string Is64Bit
        {
            get
            {
                return Environment.Is64BitOperatingSystem.ToString();
            }
        }

        public string AllDotNetFrameworkVersion
        {
            get
            {
                return GetVersionFromRegistry() + "\r\n" + Get45or451FromRegistry();
            }
        }

        public List<string> DNFrameworkNameList
        {
            get
            {
                string folderPath = Path.Combine(Environment.CurrentDirectory,Properties.Settings.Default.InstallFolder);
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                if (!dir.Exists)
                {
                    return null;
                }
                List<string> result = new List<string>();
                foreach (var file in dir.GetFiles("*.exe"))
                {
                    result.Add(file.Name);
                }
                return result;
            }
        }

        public void OpenIt(string file)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory,Properties.Settings.Default.InstallFolder, file);
            if (File.Exists(filePath))
            {
                try
                {
                    ProcessStartInfo p = new ProcessStartInfo(filePath);
                    Process process = new Process();
                    process.StartInfo = p;
                    if (!process.Start())
                    {
                        //启动失败，写入日志
                    }
                }
                catch (Exception ex)
                {
                    //操作被用户取消错误
                }

            }
        }


        /// <summary>
        /// 获取NF1-4.0版本
        ///代码来源MSDN
        ///https://msdn.microsoft.com/zh-cn/library/hh925568(v=vs.110).aspx#net_b
        /// </summary>
        private string  GetVersionFromRegistry()
        {
            StringBuilder sb = new StringBuilder();
            // Opens the registry key for the .NET Framework entry.
            using (RegistryKey ndpKey =
                RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                // As an alternative, if you know the computers you will query are running .NET Framework 4.5 
                // or later, you can use:
                // using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, 
                // RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (install == "") //no install info, must be later.
                            //Console.WriteLine(versionKeyName + "  " + name);
                            sb.AppendLine("versionKeyName" + name);
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                                //Console.WriteLine(versionKeyName + "  " + name + "  SP" + sp);
                                sb.AppendLine(versionKeyName + "  " + name + "  SP" + sp);
                            }

                        }
                        if (name != "")
                        {
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (name != "")
                                sp = subKey.GetValue("SP", "").ToString();
                            install = subKey.GetValue("Install", "").ToString();
                            if (install == "") //no install info, must be later.
                                //Console.WriteLine(versionKeyName + "  " + name);
                                sb.AppendLine(versionKeyName + "  " + name);
                            else
                            {
                                if (sp != "" && install == "1")
                                {
                                    //Console.WriteLine("  " + subKeyName + "  " + name + "  SP" + sp);
                                    sb.AppendLine("  " + subKeyName + "  " + name + "  SP" + sp);
                                }
                                else if (install == "1")
                                {
                                    //Console.WriteLine("  " + subKeyName + "  " + name);
                                    sb.AppendLine("  " + subKeyName + "  " + name);

                                }
                            }
                        }
                    }
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// 从注册表中获取NF4.5以上的版本信息
        /// </summary>
        private string Get45or451FromRegistry()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (true)
                {
                    //Console.WriteLine("Version: " + CheckFor45DotVersion(releaseKey));
                    return "Version: " + CheckFor45DotVersion(releaseKey);
                }
            }
        }
        /// <summary>
        /// 确认NF4.5的具体版本
        /// </summary>
        /// <param name="releaseKey"></param>
        /// <returns></returns>
        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393295)
            {
                return "4.6 or later";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }
            // This line should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }

    }


}
