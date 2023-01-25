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
using System.Drawing.Imaging;

namespace Business.Tool
{
    public static class Useful
    {
        #region Gets

        public static string GetApplicationDirectory()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory.Replace("WebAPIUnitTests\\bin\\Debug", "WebAPI\\");
        }
                
        public static string GetAppSettings(string keyWebConfig)
        {
            return ConfigurationManager.AppSettings[keyWebConfig];
        }

        public static string GetPngToBase64String(string path)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);
            string base64String = Convert.ToBase64String(imageBytes);
            return String.Format("data:image/png;base64,{0}", base64String);
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

        public static string GetAllTextFile(string path)
        {
            string text = File.ReadAllText(path);
            return text;
        }

        //public static ImageFormat GetImageFormat(string conventionFromBase64String)
        //{
        //    if (conventionFromBase64String.ToLower().Contains("data:image/bmp;base64"))
        //        return ImageFormat.Bmp;
        //    if (conventionFromBase64String.ToLower().Contains("data:image/emf;base64"))
        //        return ImageFormat.Emf;
        //    if (conventionFromBase64String.ToLower().Contains("data:image/exif;base64"))
        //        return ImageFormat.Exif;
        //    if (conventionFromBase64String.ToLower().Contains("data:image/gif;base64"))
        //        return ImageFormat.Gif;
        //    if (conventionFromBase64String.ToLower().Contains("data:image/icon;base64"))
        //        return ImageFormat.Icon;
        //    if (conventionFromBase64String.ToLower().Contains("data:image/jpeg;base64") || conventionFromBase64String.ToLower().Contains("data:image/jpg;base64"))
        //        return ImageFormat.Jpeg;
        //    if (conventionFromBase64String.ToLower().Contains("data:image/tiff;base64"))
        //        return ImageFormat.Tiff;
        //    if (conventionFromBase64String.ToLower().Contains("data:image/wmf;base64"))
        //        return ImageFormat.Wmf;
        //    else
        //        return ImageFormat.MemoryBmp;
        //}

        public static string GetApplicationNameText()
        {
            return "OpenAPI";
        }        

        public static string GetImagePath()
        {
            return $"{GetApplicationDirectory()}Contents\\api-200.png";
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

        public static bool ValidateBase64String(string base64String)
        {
            return (base64String.Trim().Length % 4 == 0) && Regex.IsMatch(base64String.Trim(), @GetAppSettings("IsBase64String"), RegexOptions.None);
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

        #region Replace
        public static string ReplaceConventionImageFromBase64String(string base64String)
        {
            base64String = base64String.Replace("data:image/bmp;base64,", "");
            base64String = base64String.Replace("data:image/emf;base64,", "");
            base64String = base64String.Replace("data:image/exif;base64,", "");
            base64String = base64String.Replace("data:image/gif;base64,", "");
            base64String = base64String.Replace("data:image/icon;base64,", "");
            base64String = base64String.Replace("data:image/jpeg;base64,", "");
            base64String = base64String.Replace("data:image/jpg;base64,", "");
            base64String = base64String.Replace("data:image/png;base64,", "");
            base64String = base64String.Replace("data:image/tiff;base64,", "");
            base64String = base64String.Replace("data:image/wmf;base64,", "");
            return base64String;
        }
        #endregion
    }
}
