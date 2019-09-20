using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

// ==================== //
//                      //
//      [EZCONFIG]      //
//                      //
// ==================== //

namespace EzConfig
{
    class EzConfig
    {
        #region Fields
        private static string ConfigPath = string.Empty;
        #endregion

        #region Read Value
        public static string ReadValue(string SectionName, string KeyName)
        {
            StringBuilder sb = new StringBuilder(5000);
            Imports.GetPrivateProfileString(SectionName, KeyName, "", sb, sb.Capacity, ConfigPath);
            string alinan = sb.ToString();
            sb.Clear();
            return alinan;
        }

        #endregion

        #region Write Value

        public static bool WriteValue(string SectionName, string KeyName, string Value)
        {
            if (Others.IsWritable())
            {
                bool Return;
                Return = Imports.WritePrivateProfileString(SectionName, KeyName, Value, ConfigPath);
                return Return;
            }
            else
            {
                return false;
                throw new Exception("The requested file does not exist in the specified location.");
            }
        }

        public static bool WriteValue(string SectionName, string KeyName, int Value)
        {
            if (Others.IsWritable())
            {
                bool Return;
                Return = Imports.WritePrivateProfileString(SectionName, KeyName, Value.ToString(), ConfigPath);
                return Return;
            }
            else
            {
                return false;
                throw new Exception("The requested file does not exist in the specified location.");
            }
        }

        public static bool WriteValue(string SectionName, string KeyName, bool Value)
        {
            if (Others.IsWritable())
            {
                bool Return;
                Return = Imports.WritePrivateProfileString(SectionName, KeyName, Value.ToString(), ConfigPath);
                return Return;
            }
            else
            {
                return false;
                throw new Exception("The requested file does not exist in the specified location.");
            }
        }

        #endregion

        public static class Others
        {
            public static void SelectConfig(string _ConfigPath, bool AutoConfigCreate = false)
            {
                ConfigPath = _ConfigPath + ".ini";

                if (AutoConfigCreate)
                {
                    if (!File.Exists(ConfigPath))
                    {
                        CreateConfig();
                    }
                }
            }

            public static void CreateConfig()
            {
                using (FileStream fs = new FileStream(ConfigPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    fs.Close();
                }
            }

            public static void ResetConfig()
            {
                try
                {
                    using (FileStream fs = new FileStream(ConfigPath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        File.WriteAllText(ConfigPath, string.Empty);
                        fs.Close();
                    }
                }
                catch { throw new Exception("The requested file does not exist in the specified location."); }
            }

            public static bool IsWritable()
            {
                return ConfigPath != string.Empty ? true : false;
            }
        }

        private static class Imports
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

            [DllImport("kernel32.dll")]
            public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        }
    }
}
