using System;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace TimeCapture.utils
{
    public class _nuget
    {
        private const string ChromeDriverExe = "chromedriver.exe";
        private const string ChromeDriverUrl = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE";
        private const string DownloadUrlTemplate = "https://chromedriver.storage.googleapis.com/{0}/chromedriver_win32.zip";

        public bool UpdateChromeDriver(out string error)
        {
            DateTime currentLastModified = GetFileLastModified(ChromeDriverExe);
            DateTime latestLastModified = GetLatestChromeDriverLastModified();
            error = null;
            if (latestLastModified > currentLastModified)
            {
                try
                {
                    DownloadAndInstallChromeDriver(latestLastModified.ToString("dd-MM-yyyy"));
                    return true;
                }
                catch (Exception exception)
                {
                    error = "Failed to update drivers. Reason: " + exception.Message.ToString();
                    return false;
                }
            }
            return false;
        }

        private DateTime GetFileLastModified(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(filePath))
            {
                return File.GetLastWriteTime(filePath);
            }
            return DateTime.MinValue; // Return a minimum date if the file doesn't exist.
        }

        private string GetCurrentChromeDriverVersion()
        {
            string currentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ChromeDriverExe);
            if (File.Exists(currentPath))
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(currentPath);
                return fileVersionInfo.FileVersion;
            }
            return null;
        }

        private DateTime GetLatestChromeDriverLastModified()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    string latestVersion = client.DownloadString(ChromeDriverUrl).Trim();
                    string downloadUrl = string.Format(DownloadUrlTemplate, latestVersion);
                    WebRequest webRequest = WebRequest.Create(downloadUrl);
                    webRequest.Method = "HEAD"; // Send a HEAD request to retrieve the last modified date without downloading the file.

                    using (WebResponse response = webRequest.GetResponse())
                    {
                        if (response is HttpWebResponse httpWebResponse)
                        {
                            return httpWebResponse.LastModified;
                        }
                    }
                }
            }
            catch
            {
                new _logger().Log(LogType.Error, "Failed to update selenium chrome driver", "Chrome Driver Update");
            }
            return DateTime.MinValue;
        }

        private string GetLatestChromeDriverVersion()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    string latestVersion = client.DownloadString(ChromeDriverUrl).Trim();
                    return latestVersion;
                }
            }
            catch
            {
                return null;
            }
        }

        private void DownloadAndInstallChromeDriver(string version)
        {
            string downloadUrl = string.Format(DownloadUrlTemplate, version);
            string tempZipFile = Path.Combine(Path.GetTempPath(), "chromedriver.zip");
            string extractPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(downloadUrl, tempZipFile);
                }

                System.IO.Compression.ZipFile.ExtractToDirectory(tempZipFile, extractPath);
            }
            catch
            {
                // Handle download and extraction errors
            }
            finally
            {
                File.Delete(tempZipFile);
            }
        }
    }
}
