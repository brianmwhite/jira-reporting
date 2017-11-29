using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace jr
{
    public class LocalProfileInfo
    {
        public static string ConfigPath
        {
            get
            {
                string profileDir = Environment.GetEnvironmentVariable(
                    RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "USERPROFILE" : "HOME");

                return Path.Combine(profileDir, ".jrcli");
            }
        }

        public static string JiraCredentialPath
        {
            get { return Path.Combine(ConfigPath, "jira.json"); }
        }
        
        public static void CreateConfigDirectoryIfNotExists()
        {
            if (!Directory.Exists(ConfigPath))
            {
                Directory.CreateDirectory(ConfigPath);
            }
        }

        public static void CreateJiraCredentialsFile(string json)
        {
            CreateConfigDirectoryIfNotExists();
            File.WriteAllText(JiraCredentialPath, json);
        }

        public static JiraCredentials LoadJiraCredentials()
        {
            string json = File.ReadAllText(JiraCredentialPath);
            JiraCredentials jc = JsonConvert.DeserializeObject<JiraCredentials>(json);
            return jc;
        }
    }
}