using System;
using System.Diagnostics;
using System.IO;
using Unity.CodeEditor;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Community.Antigravity
{
    /// <summary>
    /// Unity Code Editor integration for Google Antigravity IDE.
    /// Implements IExternalCodeEditor to register Antigravity as an available external editor.
    /// </summary>
    [InitializeOnLoad]
    public class AntigravityEditor : IExternalCodeEditor
    {
        private const string EditorPathKey = "Antigravity_EditorPath";
        private static readonly GUIContent ResetPathButtonContent = new GUIContent("Reset Path", "Attempt to auto-detect the Antigravity installation path.");
        private static readonly GUIContent BrowseButtonContent = new GUIContent("Browse...", "Manually select the Antigravity executable.");

        private string _cachedPath;

        static AntigravityEditor()
        {
            // Register this editor with Unity's Code Editor system
            CodeEditor.Register(new AntigravityEditor());
        }

        /// <summary>
        /// The installation info for this editor.
        /// </summary>
        public CodeEditor.Installation[] Installations => GetInstallations();

        /// <summary>
        /// Determines if this editor should be used for the given path.
        /// </summary>
        public bool TryGetInstallationForPath(string editorPath, out CodeEditor.Installation installation)
        {
            // Check if the path matches our known Antigravity paths
            if (IsAntigravityPath(editorPath))
            {
                installation = new CodeEditor.Installation
                {
                    Name = "Antigravity",
                    Path = editorPath
                };
                return true;
            }

            installation = default;
            return false;
        }

        /// <summary>
        /// Called when the user selects this editor in Preferences.
        /// </summary>
        public void Initialize(string editorInstallationPath)
        {
            _cachedPath = editorInstallationPath;
        }

        /// <summary>
        /// Opens a file at a specific line and column.
        /// </summary>
        public bool OpenProject(string filePath, int line, int column)
        {
            var editorPath = GetEditorPath();
            if (string.IsNullOrEmpty(editorPath))
            {
                Debug.LogError("[Antigravity] Editor path is not set. Please configure it in Preferences > External Tools.");
                return false;
            }

            // Build the arguments - Antigravity uses VS Code-style arguments
            // -g file:line:column opens the file at the specified location
            // -r reuses the existing window
            var projectPath = Directory.GetParent(Application.dataPath)?.FullName ?? ".";
            
            string arguments;
            if (!string.IsNullOrEmpty(filePath))
            {
                // Open specific file at line
                if (line > 0)
                {
                    arguments = $"-r -g \"{filePath}\":{line}:{column} \"{projectPath}\"";
                }
                else
                {
                    arguments = $"-r \"{filePath}\" \"{projectPath}\"";
                }
            }
            else
            {
                // Just open the project folder
                arguments = $"-r \"{projectPath}\"";
            }

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = editorPath,
                    Arguments = arguments,
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                Process.Start(startInfo);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[Antigravity] Failed to open editor: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Synchronizes the project files (generates .csproj and .sln).
        /// </summary>
        public void SyncAll()
        {
            // Use Unity's built-in project generation
            // This creates .csproj files compatible with VS Code-based editors
            AssetDatabase.Refresh();
            Debug.Log("[Antigravity] Project files synchronized.");
        }

        /// <summary>
        /// Synchronizes only if needed.
        /// </summary>
        public void SyncIfNeeded(string[] addedFiles, string[] deletedFiles, string[] movedFiles, string[] movedFromFiles, string[] importedFiles)
        {
            // For now, just do a full sync
            // A more sophisticated implementation could do incremental updates
            SyncAll();
        }

        /// <summary>
        /// Renders the GUI for the External Tools preferences.
        /// </summary>
        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            
            var currentPath = GetEditorPath();
            EditorGUILayout.LabelField("Antigravity Path:", currentPath ?? "(Not Set)");

            if (GUILayout.Button(BrowseButtonContent, GUILayout.Width(80)))
            {
                var newPath = EditorUtility.OpenFilePanel(
                    "Select Antigravity Executable",
                    string.IsNullOrEmpty(currentPath) ? "" : Path.GetDirectoryName(currentPath),
                    Application.platform == RuntimePlatform.WindowsEditor ? "exe" : ""
                );

                if (!string.IsNullOrEmpty(newPath))
                {
                    SetEditorPath(newPath);
                }
            }

            if (GUILayout.Button(ResetPathButtonContent, GUILayout.Width(80)))
            {
                var detectedPath = AntigravityDiscovery.FindAntigravityPath();
                if (!string.IsNullOrEmpty(detectedPath))
                {
                    SetEditorPath(detectedPath);
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.HelpBox(
                "Antigravity IDE uses VS Code-style arguments. Files will be opened with -g flag for line navigation.",
                MessageType.Info
            );
        }

        private CodeEditor.Installation[] GetInstallations()
        {
            var path = GetEditorPath();
            
            // If we don't have a cached path, try to discover it
            if (string.IsNullOrEmpty(path))
            {
                path = AntigravityDiscovery.FindAntigravityPath();
                if (!string.IsNullOrEmpty(path))
                {
                    SetEditorPath(path);
                }
            }

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                return new[]
                {
                    new CodeEditor.Installation
                    {
                        Name = "Antigravity",
                        Path = path
                    }
                };
            }

            // Return empty array if not found - editor won't appear in dropdown
            // But we still want it to appear so users can configure it
            return new[]
            {
                new CodeEditor.Installation
                {
                    Name = "Antigravity (Not Configured)",
                    Path = ""
                }
            };
        }

        private string GetEditorPath()
        {
            if (string.IsNullOrEmpty(_cachedPath))
            {
                _cachedPath = EditorPrefs.GetString(EditorPathKey, null);
            }
            return _cachedPath;
        }

        private void SetEditorPath(string path)
        {
            _cachedPath = path;
            EditorPrefs.SetString(EditorPathKey, path);
            Debug.Log($"[Antigravity] Editor path set to: {path}");
        }

        private bool IsAntigravityPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var fileName = Path.GetFileName(path).ToLowerInvariant();
            return fileName.Contains("antigravity");
        }
    }
}
