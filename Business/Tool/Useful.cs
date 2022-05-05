using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Tool
{
    public static class Useful
    {
        #region Gets

        public static string GetApplicationDirectory()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory.Replace("WebAPIUnitTest\\bin\\Debug", "WebAPI\\");
        }
        
        public static string GetMessagesDirectory()
        {
            return GetApplicationDirectory() + @"Contents\Useful\Messages.html";
        }
        
        public static string GetAppSettings(string keyWebConfig)
        {
            return ConfigurationManager.AppSettings[keyWebConfig];
        }
        
        public static int GetPageSizeMaximun()
        {
            return Convert.ToInt32(GetAppSettings("PageSizeMaximun"));
        }

        public static string GetRutCheckDigit(int rut)
        {
            int count = 2;
            int accumulator = 0;

            while (rut != 0)
            {
                int multiple = (rut % 10) * count;
                accumulator = accumulator + multiple;
                rut = rut / 10;
                count = count + 1;
                if (count == 8)
                {
                    count = 2;
                }
            }

            int digit = 11 - (accumulator % 11);
            string rutDigit = digit.ToString().Trim();
            if (digit == 10)
            {
                rutDigit = "K";
            }
            if (digit == 11)
            {
                rutDigit = "0";
            }

            return rutDigit;
        }

        public static string GetApplicationNameText()
        {
            return "OpenAPI";
        }        
        #endregion

        #region Validate

        public static bool ValidateDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset == DateTimeOffset.MinValue)
                return false;
            else
                return true;
        }

        public static bool ValidateRut(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            Regex expression = new Regex(GetAppSettings("IsRut"));
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expression.IsMatch(rut))
            {
                return false;
            }
            char[] charCut = { '-' };
            string[] arrayRut = rut.Split(charCut);
            if (dv != GetRutCheckDigit(Convert.ToInt32(arrayRut[0])))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Convert

        public static string ConvertSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(enconding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }

        #endregion
    }
}
