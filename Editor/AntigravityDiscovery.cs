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
            // Check common installation paths on Windows
            string[] possiblePaths = new[]
            {
                // User-level install (typical for VS Code forks)
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Antigravity", WindowsExecutableName),
                // Machine-level install
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Antigravity", WindowsExecutableName),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Antigravity", WindowsExecutableName),
                // Alternative naming conventions
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "antigravity", WindowsExecutableName),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Google Antigravity", WindowsExecutableName),
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
            // Check standard Linux paths
            string[] possiblePaths = new[]
            {
                "/usr/bin/antigravity",
                "/usr/local/bin/antigravity",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local", "bin", "antigravity"),
                "/opt/antigravity/antigravity",
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
