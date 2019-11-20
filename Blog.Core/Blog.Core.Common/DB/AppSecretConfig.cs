using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blog.Core.Common.DB
{
    public class AppSecretConfig
    {
        private static string Audience_Secret = AppSettings.App(new string[] { "Audience", "Secret" });
        private static string Audience_Secret_File = AppSettings.App(new string[] { "Audience", "SecretFile" });

        public static string Audience_Secret_String => InitAudience_Secret();

        private static string InitAudience_Secret()
        {
            var securityString = DifDbConnOfSecurity(Audience_Secret_File);
            if (!string.IsNullOrEmpty(Audience_Secret_File) && !string.IsNullOrEmpty(securityString))
            {
                return securityString;
            }
            else
            {
                return Audience_Secret;
            }
        }

        private static string DifDbConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (Exception) { }
            }

            return conn[conn.Length - 1];
        }
    }
}
