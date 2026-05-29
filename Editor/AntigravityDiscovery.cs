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
        // Post-I/O 2026: Antigravity was split into "Antigravity" (agent) and "Antigravity IDE" (editor)
        private const string WindowsIdeExecutableName = "Antigravity IDE.exe";
        private const string WindowsLegacyExecutableName = "Antigravity.exe";
        private const string MacIdeExecutableName = "Antigravity IDE";
        private const string MacLegacyExecutableName = "Antigravity";

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
            // Post-I/O 2026: "Antigravity IDE" paths are prioritized over legacy "Antigravity" paths
            string[] possiblePaths = new[]
            {
                // === Antigravity IDE (post-I/O 2026 split) ===
                // User-level install
                Path.Combine(localAppData, "Programs", "Antigravity IDE", WindowsIdeExecutableName),
                Path.Combine(localAppData, "Programs", "antigravity-ide", WindowsIdeExecutableName),
                Path.Combine(localAppData, "Programs", "Google Antigravity IDE", WindowsIdeExecutableName),
                
                // Machine-level install
                Path.Combine(programFiles, "Antigravity IDE", WindowsIdeExecutableName),
                Path.Combine(programFiles, "Google Antigravity IDE", WindowsIdeExecutableName),
                Path.Combine(programFilesX86, "Antigravity IDE", WindowsIdeExecutableName),
                
                // Scoop
                Path.Combine(userProfile, "scoop", "apps", "antigravity-ide", "current", WindowsIdeExecutableName),
                Path.Combine(userProfile, "scoop", "shims", "antigravity-ide.exe"),
                
                // Chocolatey
                @"C:\ProgramData\chocolatey\lib\antigravity-ide\tools\Antigravity IDE.exe",
                @"C:\ProgramData\chocolatey\bin\antigravity-ide.exe",

                // === Legacy "Antigravity" paths (pre-split, backward compat) ===
                // User-level install
                Path.Combine(localAppData, "Programs", "Antigravity", WindowsLegacyExecutableName),
                Path.Combine(localAppData, "Programs", "antigravity", WindowsLegacyExecutableName),
                Path.Combine(localAppData, "Programs", "Google Antigravity", WindowsLegacyExecutableName),
                
                // Machine-level install
                Path.Combine(programFiles, "Antigravity", WindowsLegacyExecutableName),
                Path.Combine(programFiles, "Google Antigravity", WindowsLegacyExecutableName),
                Path.Combine(programFilesX86, "Antigravity", WindowsLegacyExecutableName),
                
                // Scoop
                Path.Combine(userProfile, "scoop", "apps", "antigravity", "current", WindowsLegacyExecutableName),
                Path.Combine(userProfile, "scoop", "shims", "antigravity.exe"),
                
                // Chocolatey
                @"C:\ProgramData\chocolatey\lib\antigravity\tools\Antigravity.exe",
                @"C:\ProgramData\chocolatey\bin\antigravity.exe",
                
                // Portable installs
                Path.Combine(userProfile, "Antigravity IDE", WindowsIdeExecutableName),
                Path.Combine(userProfile, "Antigravity", WindowsLegacyExecutableName),
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
            var userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            
            // Check standard macOS application paths
            // Post-I/O 2026: "Antigravity IDE" paths are prioritized over legacy "Antigravity" paths
            string[] possiblePaths = new[]
            {
                // Antigravity IDE (post-split)
                $"/Applications/Antigravity IDE.app/Contents/MacOS/{MacIdeExecutableName}",
                Path.Combine(userHome, "Applications", "Antigravity IDE.app", "Contents", "MacOS", MacIdeExecutableName),
                
                // Homebrew cask
                $"/opt/homebrew/bin/antigravity-ide",
                
                // Legacy (pre-split)
                $"/Applications/Antigravity.app/Contents/MacOS/{MacLegacyExecutableName}",
                Path.Combine(userHome, "Applications", "Antigravity.app", "Contents", "MacOS", MacLegacyExecutableName),
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
            // Post-I/O 2026: "antigravity-ide" paths are prioritized over legacy "antigravity" paths
            string[] possiblePaths = new[]
            {
                // === Antigravity IDE (post-split) ===
                "/usr/bin/antigravity-ide",
                "/usr/local/bin/antigravity-ide",
                Path.Combine(userHome, ".local", "bin", "antigravity-ide"),
                "/opt/antigravity-ide/antigravity-ide",
                
                // Snap
                "/snap/bin/antigravity-ide",
                "/snap/antigravity-ide/current/antigravity-ide",
                
                // Flatpak
                Path.Combine(userHome, ".local", "share", "flatpak", "exports", "bin", "com.google.AntigravityIDE"),
                "/var/lib/flatpak/exports/bin/com.google.AntigravityIDE",
                
                // === Legacy "antigravity" paths (pre-split, backward compat) ===
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
                Path.Combine(userHome, "antigravity-ide", "antigravity-ide"),
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
