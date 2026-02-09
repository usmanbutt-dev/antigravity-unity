using System;
using System.IO;
using UnityEngine;

namespace Community.Antigravity
{
    /// <summary>
    /// Helper class to discover the Antigravity IDE installation path on the system.
    /// </summary>
    public static class AntigravityDiscovery
    {
        private const string WindowsExecutableName = "Antigravity.exe";
        private const string MacExecutableName = "Antigravity";

        /// <summary>
        /// Attempts to find the Antigravity IDE executable on the system.
        /// </summary>
        /// <returns>The full path to the executable, or null if not found.</returns>
        public static string FindAntigravityPath()
        {
#if UNITY_EDITOR_WIN
            return FindOnWindows();
#elif UNITY_EDITOR_OSX
            return FindOnMac();
#elif UNITY_EDITOR_LINUX
            return FindOnLinux();
#else
            return null;
#endif
        }

        private static string FindOnWindows()
        {
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            // Check common installation paths on Windows
            string[] possiblePaths = new[]
            {
                // User-level install (typical for VS Code forks)
                Path.Combine(localAppData, "Programs", "Antigravity", WindowsExecutableName),
                Path.Combine(localAppData, "Programs", "antigravity", WindowsExecutableName),
                Path.Combine(localAppData, "Programs", "Google Antigravity", WindowsExecutableName),
                
                // Machine-level install
                Path.Combine(programFiles, "Antigravity", WindowsExecutableName),
                Path.Combine(programFiles, "Google Antigravity", WindowsExecutableName),
                Path.Combine(programFilesX86, "Antigravity", WindowsExecutableName),
                
                // Scoop
                Path.Combine(userProfile, "scoop", "apps", "antigravity", "current", WindowsExecutableName),
                Path.Combine(userProfile, "scoop", "shims", "antigravity.exe"),
                
                // Chocolatey
                @"C:\ProgramData\chocolatey\lib\antigravity\tools\Antigravity.exe",
                @"C:\ProgramData\chocolatey\bin\antigravity.exe",
                
                // Portable installs
                Path.Combine(userProfile, "Antigravity", WindowsExecutableName),
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    Debug.Log($"[Antigravity] Found installation at: {path}");
                    return path;
                }
            }

            Debug.LogWarning("[Antigravity] Could not auto-detect Antigravity IDE installation. Please set the path manually in Preferences > External Tools.");
            return null;
        }

        private static string FindOnMac()
        {
            // Check standard macOS application paths
            string[] possiblePaths = new[]
            {
                "/Applications/Antigravity.app/Contents/MacOS/Antigravity",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Applications", "Antigravity.app", "Contents", "MacOS", "Antigravity"),
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    Debug.Log($"[Antigravity] Found installation at: {path}");
                    return path;
                }
            }

            Debug.LogWarning("[Antigravity] Could not auto-detect Antigravity IDE installation. Please set the path manually in Preferences > External Tools.");
            return null;
        }

        private static string FindOnLinux()
        {
            var userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            
            // Check standard Linux paths
            string[] possiblePaths = new[]
            {
                // Standard paths
                "/usr/bin/antigravity",
                "/usr/local/bin/antigravity",
                Path.Combine(userHome, ".local", "bin", "antigravity"),
                "/opt/antigravity/antigravity",
                "/opt/Antigravity/antigravity",
                
                // Snap
                "/snap/bin/antigravity",
                "/snap/antigravity/current/antigravity",
                
                // Flatpak
                Path.Combine(userHome, ".local", "share", "flatpak", "exports", "bin", "com.google.Antigravity"),
                "/var/lib/flatpak/exports/bin/com.google.Antigravity",
                
                // Extracted tarball
                Path.Combine(userHome, "antigravity", "antigravity"),
                Path.Combine(userHome, "Antigravity", "antigravity"),
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    Debug.Log($"[Antigravity] Found installation at: {path}");
                    return path;
                }
            }

            Debug.LogWarning("[Antigravity] Could not auto-detect Antigravity IDE installation. Please set the path manually in Preferences > External Tools.");
            return null;
        }

        /// <summary>
        /// Validates whether the given path points to a valid Antigravity executable.
        /// </summary>
        public static bool IsValidAntigravityPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            return File.Exists(path);
        }
    }
}
