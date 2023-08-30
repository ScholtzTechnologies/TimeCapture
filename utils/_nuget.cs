using System;
using System.Diagnostics;
using System.IO;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace TimeCapture.utils
{
    public class _nuget
    {
        public bool isSuccess { get; set; }
        public void CheckChromeDriver(out bool isUpdated, out bool isSuccessful)
        {
            isSuccessful = true;
            isUpdated = false;
            // NuGet package information
            string packageName = "Selenium.WebDriver.ChromeDriver"; // NuGet package name
            string packageVersion = "LATEST";    // Use "LATEST" to always get the latest version

            // Path to the ChromeDriver NuGet package
            string packagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".nuget", "packages", packageName);

            // Check if the package needs an update
            bool updateNeeded = CheckForPackageUpdate(packageName, packageVersion);

            if (updateNeeded)
            {
                // Update the package
                UpdatePackage(packageName, packageVersion, packagePath);
                isUpdated = true;
            }
            isSuccessful = isSuccess;
        }

        public static bool CheckForPackageUpdate(string packageName, string packageVersion)
        {
            SourceCacheContext cache = new SourceCacheContext();
            PackageSource packageSource = new PackageSource(NuGetConstants.V3FeedUrl);

            SourceRepository repository = Repository.Factory.GetCoreV3(packageSource);
            FindPackageByIdResource resource = repository.GetResource<FindPackageByIdResource>();

            NuGetVersion latestVersion = resource.GetAllVersionsAsync(packageName, cache, NullLogger.Instance, CancellationToken.None).Result
                                             .OrderByDescending(v => v)
                                             .FirstOrDefault();

            if (latestVersion != null)
            {
                if (packageVersion == "LATEST" || NuGetVersion.Parse(packageVersion) < latestVersion)
                {
                    return true; // Update needed
                }
            }

            return false; // No update needed
        }

        public void UpdatePackage(string packageName, string packageVersion, string packagePath)
        {
            string nugetExePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "nuget.exe");
            try
            {
                // Use the NuGet CLI to update the package
                Process.Start(nugetExePath, $"update {packageName} -Version {packageVersion} -Source \"{packagePath}\" -NonInteractive");
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
        }
    }
}
