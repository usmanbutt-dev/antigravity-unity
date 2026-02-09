using System.IO;
using UnityEditor;
using UnityEngine;

namespace Community.Antigravity
{
    /// <summary>
    /// Menu items for Antigravity IDE integration.
    /// </summary>
    public static class AntigravityMenuItems
    {
        private const int MenuPriority = 1000;

        #region Assets Context Menu

        [MenuItem("Assets/Open in Antigravity", priority = MenuPriority)]
        private static void OpenSelectedInAntigravity()
        {
            var selected = Selection.activeObject;
            if (selected == null) return;

            var path = AssetDatabase.GetAssetPath(selected);
            if (string.IsNullOrEmpty(path)) return;

            var fullPath = Path.GetFullPath(path);
            
            if (AssetDatabase.IsValidFolder(path))
            {
                OpenPath(fullPath, isFolder: true);
            }
            else
            {
                OpenPath(fullPath, isFolder: false);
            }
        }

        [MenuItem("Assets/Open in Antigravity", true)]
        private static bool OpenSelectedInAntigravityValidate()
        {
            return Selection.activeObject != null;
        }

        #endregion

        #region Window Menu

        [MenuItem("Window/Antigravity/Open Project Folder", priority = 100)]
        private static void OpenProjectFolder()
        {
            var projectPath = Directory.GetParent(Application.dataPath)?.FullName;
            if (!string.IsNullOrEmpty(projectPath))
            {
                OpenPath(projectPath, isFolder: true);
            }
        }

        [MenuItem("Window/Antigravity/Open Assets Folder", priority = 101)]
        private static void OpenAssetsFolder()
        {
            OpenPath(Application.dataPath, isFolder: true);
        }

        [MenuItem("Window/Antigravity/Regenerate Project Files", priority = 200)]
        private static void RegenerateProjectFiles()
        {
            var currentEditor = Unity.CodeEditor.CodeEditor.CurrentEditor;
            if (currentEditor != null)
            {
                currentEditor.SyncAll();
                Debug.Log("[Antigravity] Project files regenerated.");
            }
            else
            {
                Debug.LogWarning("[Antigravity] No code editor is currently set.");
            }
        }

        [MenuItem("Window/Antigravity/Open Preferences", priority = 300)]
        private static void OpenPreferences()
        {
            SettingsService.OpenUserPreferences("Preferences/External Tools");
        }

        #endregion

        #region Helper Methods

        private static void OpenPath(string path, bool isFolder)
        {
            var editorPath = EditorPrefs.GetString("Antigravity_EditorPath", null);
            if (string.IsNullOrEmpty(editorPath))
            {
                Debug.LogError("[Antigravity] Editor path is not configured. Go to Preferences > External Tools.");
                return;
            }

            if (!File.Exists(editorPath))
            {
                Debug.LogError($"[Antigravity] Editor not found at: {editorPath}");
                return;
            }

            var projectPath = Directory.GetParent(Application.dataPath)?.FullName ?? ".";
            
            string arguments;
            if (isFolder)
            {
                arguments = $"-r \"{path}\"";
            }
            else
            {
                arguments = $"-r \"{path}\" \"{projectPath}\"";
            }

            try
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = editorPath,
                    Arguments = arguments,
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                System.Diagnostics.Process.Start(startInfo);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[Antigravity] Failed to open: {ex.Message}");
            }
        }

        #endregion
    }
}
