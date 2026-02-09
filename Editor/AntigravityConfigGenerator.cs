using System.IO;
using UnityEditor;
using UnityEngine;

namespace Community.Antigravity
{
    /// <summary>
    /// Generates configuration files for optimal Antigravity IDE experience.
    /// </summary>
    public static class AntigravityConfigGenerator
    {
        private static string ProjectRoot => Directory.GetParent(Application.dataPath)?.FullName;

        #region Menu Items

        [MenuItem("Window/Antigravity/Generate All Config Files", priority = 150)]
        private static void GenerateAllConfigs()
        {
            GenerateOmniSharpConfig();
            GenerateEditorConfig();
            GenerateVSCodeSettings();
            Debug.Log("[Antigravity] All configuration files generated.");
            AssetDatabase.Refresh();
        }

        [MenuItem("Window/Antigravity/Generate .omnisharp.json", priority = 160)]
        private static void GenerateOmniSharpConfigMenu()
        {
            GenerateOmniSharpConfig();
            AssetDatabase.Refresh();
        }

        [MenuItem("Window/Antigravity/Generate .editorconfig", priority = 161)]
        private static void GenerateEditorConfigMenu()
        {
            GenerateEditorConfig();
            AssetDatabase.Refresh();
        }

        [MenuItem("Window/Antigravity/Generate .vscode/settings.json", priority = 162)]
        private static void GenerateVSCodeSettingsMenu()
        {
            GenerateVSCodeSettings();
            AssetDatabase.Refresh();
        }

        #endregion

        #region Config Generators

        /// <summary>
        /// Generates .omnisharp.json for better C# IntelliSense in Antigravity.
        /// </summary>
        public static void GenerateOmniSharpConfig()
        {
            var path = Path.Combine(ProjectRoot, ".omnisharp.json");
            
            var content = @"{
    ""RoslynExtensionsOptions"": {
        ""enableAnalyzersSupport"": true,
        ""enableImportCompletion"": true,
        ""enableDecompilationSupport"": true
    },
    ""FormattingOptions"": {
        ""enableEditorConfigSupport"": true,
        ""organizeImports"": true
    },
    ""RenameOptions"": {
        ""RenameInComments"": false,
        ""RenameInStrings"": false,
        ""RenameOverloads"": false
    },
    ""ImplementTypeOptions"": {
        ""PropertyGenerationBehavior"": ""PreferAutoProperties"",
        ""InsertionBehavior"": ""AtTheEnd""
    },
    ""FileOptions"": {
        ""excludeSearchPatterns"": [
            ""**/Library/**/*"",
            ""**/Temp/**/*"",
            ""**/Logs/**/*"",
            ""**/obj/**/*"",
            ""**/Packages/**/*""
        ]
    }
}";

            File.WriteAllText(path, content);
            Debug.Log($"[Antigravity] Generated: {path}");
        }

        /// <summary>
        /// Generates .editorconfig matching Unity coding conventions.
        /// </summary>
        public static void GenerateEditorConfig()
        {
            var path = Path.Combine(ProjectRoot, ".editorconfig");
            
            var content = @"# Unity C# EditorConfig
# https://editorconfig.org

root = true

[*]
charset = utf-8
end_of_line = lf
indent_style = space
indent_size = 4
insert_final_newline = true
trim_trailing_whitespace = true

[*.cs]
# Indentation
csharp_indent_case_contents = true
csharp_indent_switch_labels = true
csharp_indent_block_contents = true
csharp_indent_braces = false

# New lines
csharp_new_line_before_open_brace = all
csharp_new_line_before_else = true
csharp_new_line_before_catch = true
csharp_new_line_before_finally = true
csharp_new_line_before_members_in_object_initializers = true
csharp_new_line_before_members_in_anonymous_types = true

# Spacing
csharp_space_after_cast = false
csharp_space_after_keywords_in_control_flow_statements = true
csharp_space_between_method_declaration_parameter_list_parentheses = false
csharp_space_between_method_call_parameter_list_parentheses = false
csharp_space_between_parentheses = false
csharp_space_before_colon_in_inheritance_clause = true
csharp_space_after_colon_in_inheritance_clause = true
csharp_space_around_binary_operators = before_and_after

# Wrapping
csharp_preserve_single_line_statements = false
csharp_preserve_single_line_blocks = true

# Naming conventions
dotnet_naming_rule.private_fields_underscore.severity = suggestion
dotnet_naming_rule.private_fields_underscore.symbols = private_fields
dotnet_naming_rule.private_fields_underscore.style = underscore_prefix

dotnet_naming_symbols.private_fields.applicable_kinds = field
dotnet_naming_symbols.private_fields.applicable_accessibilities = private

dotnet_naming_style.underscore_prefix.capitalization = camel_case
dotnet_naming_style.underscore_prefix.required_prefix = _

# Unity-specific
dotnet_diagnostic.IDE0051.severity = none  # Unused private members (Unity uses reflection)
dotnet_diagnostic.IDE0044.severity = none  # Add readonly modifier (Unity serialization)

[*.{shader,compute,cginc,hlsl}]
indent_size = 4

[*.{json,asmdef,asmref}]
indent_size = 2

[*.{xml,csproj,props,targets}]
indent_size = 2
";

            File.WriteAllText(path, content);
            Debug.Log($"[Antigravity] Generated: {path}");
        }

        /// <summary>
        /// Generates .vscode/settings.json for optimal workspace configuration.
        /// </summary>
        public static void GenerateVSCodeSettings()
        {
            var vscodePath = Path.Combine(ProjectRoot, ".vscode");
            if (!Directory.Exists(vscodePath))
            {
                Directory.CreateDirectory(vscodePath);
            }

            var path = Path.Combine(vscodePath, "settings.json");
            
            var content = @"{
    // File Associations
    ""files.associations"": {
        ""*.shader"": ""hlsl"",
        ""*.compute"": ""hlsl"",
        ""*.cginc"": ""hlsl"",
        ""*.hlsl"": ""hlsl"",
        ""*.asmdef"": ""json"",
        ""*.asmref"": ""json"",
        ""*.inputactions"": ""json""
    },

    // Exclude from Explorer
    ""files.exclude"": {
        ""**/.git"": true,
        ""**/.DS_Store"": true,
        ""**/Library"": true,
        ""**/Temp"": true,
        ""**/Logs"": true,
        ""**/UserSettings"": true,
        ""**/obj"": true,
        ""**/*.meta"": false
    },

    // Search Exclusions
    ""search.exclude"": {
        ""**/Library"": true,
        ""**/Temp"": true,
        ""**/Logs"": true,
        ""**/Packages"": true,
        ""**/*.asset"": true,
        ""**/*.unity"": true,
        ""**/*.prefab"": true
    },

    // C# Settings
    ""csharp.referencesCodeLens.enabled"": true,
    ""csharp.maxProjectFileCountForDiagnosticAnalysis"": 10000,

    // OmniSharp
    ""omnisharp.enableRoslynAnalyzers"": true,
    ""omnisharp.enableImportCompletion"": true,
    ""omnisharp.enableDecompilationSupport"": true,

    // Editor
    ""editor.formatOnSave"": true,
    ""editor.formatOnType"": true,
    ""editor.tabSize"": 4,
    ""editor.insertSpaces"": true,
    ""editor.rulers"": [120],

    // Unity Meta Files
    ""[meta]"": {
        ""editor.formatOnSave"": false
    }
}";

            File.WriteAllText(path, content);
            Debug.Log($"[Antigravity] Generated: {path}");
        }

        #endregion
    }
}
