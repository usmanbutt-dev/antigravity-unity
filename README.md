<p align="center">
  <img src="images/hero_banner_2to1.png" alt="Antigravity for Unity Banner" width="100%" />
</p>

# Antigravity IDE Support for Unity

A premium, open-source Unity package that integrates the **Google Antigravity IDE** as an external code editor. Keep your agentic workflows fast, seamless, and fully integrated with Unity.

[![Unity 2021.3+](https://img.shields.io/badge/Unity-2021.3%2B-blue.svg)](https://unity.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![GitHub Stars](https://img.shields.io/github/stars/usmanbutt-dev/antigravity-unity?style=social)](https://github.com/usmanbutt-dev/antigravity-unity)


## ⚡ Quick Start

```
Window > Package Manager > + > Add package from git URL...
→ https://github.com/usmanbutt-dev/antigravity-unity.git
```

Then: `Edit > Preferences > External Tools` → Select **Antigravity**

## ✨ Features

- 🚀 **Seamless Integration** - Antigravity appears in Unity's External Tools dropdown
- 📍 **Smart Navigation** - Opens files at the correct line and column
- 🔍 **Auto-Detection** - Finds Antigravity on Windows, macOS, Linux (including Scoop, Chocolatey, Homebrew, Snap, Flatpak)
- 📁 **Context Menu** - Right-click any file → "Open in Antigravity"
- ⚙️ **Config Generators** - One-click setup for `.omnisharp.json`, `.editorconfig`, `.vscode/settings.json`
- 🔄 **Project Sync** - Generates proper `.csproj` and `.sln` files

## 📦 Installation

### Option 1: Git URL (Recommended)

1. Open Unity → `Window > Package Manager`
2. Click `+` → `Add package from git URL...`
3. Enter: `https://github.com/usmanbutt-dev/antigravity-unity.git`
4. Click `Add`

### Option 2: Manual Installation

1. Download from [Releases](https://github.com/usmanbutt-dev/antigravity-unity/releases)
2. Extract to `YourProject/Packages/com.community.antigravity-unity`

## 🛠️ Setup

1. Go to `Edit > Preferences > External Tools`
2. Select **Antigravity** from the dropdown
3. If not auto-detected, click **Browse...** to select the executable

**Default Paths (post-I/O 2026):**
| Platform | Path |
|----------|------|
| Windows | `%LOCALAPPDATA%\Programs\Antigravity IDE\Antigravity IDE.exe` |
| macOS | `/Applications/Antigravity IDE.app` |
| Linux | `/usr/bin/antigravity-ide` |

> **Note:** Legacy `Antigravity.exe` / `Antigravity.app` paths (pre-I/O 2026) are also supported for backward compatibility.

## 📋 Window Menu

Access all features from `Window > Antigravity`:

| Menu Item | Description |
|-----------|-------------|
| Open Project Folder | Opens project root in Antigravity |
| Open Assets Folder | Opens Assets folder in Antigravity |
| Regenerate Project Files | Forces .csproj/.sln regeneration |
| Open Preferences | Quick link to External Tools settings |
| **Generate All Config Files** | Creates all config files below |
| Generate .omnisharp.json | Better C# IntelliSense |
| Generate .editorconfig | Unity coding conventions |
| Generate .vscode/settings.json | Workspace settings & exclusions |

## ⚙️ Config File Generators

Run `Window > Antigravity > Generate All Config Files` to create:

### `.omnisharp.json`
Configures OmniSharp for Unity:
- Enables Roslyn analyzers
- Enables import completion
- Excludes Library/Temp/Logs from analysis

### `.editorconfig`
Sets up Unity coding conventions:
- 4-space indentation
- Allman brace style
- `_camelCase` for private fields
- Disables false-positive warnings (IDE0051, IDE0044)

### `.vscode/settings.json`
Optimizes workspace:
- File associations (`.shader` → HLSL, `.asmdef` → JSON)
- Excludes Library/Temp/Logs from explorer
- Enables format-on-save
- Configures OmniSharp settings

## 🎯 Context Menu

Right-click any file or folder in the Project window → **"Open in Antigravity"**

## 🔧 Compatibility

| Platform | Status |
|----------|--------|
| Windows | ✅ Supported (Scoop, Chocolatey, WinGet) |
| macOS | ✅ Supported (Homebrew) |
| Linux | ✅ Supported (Snap, Flatpak, AppImage) |

| Unity Version | Status |
|---------------|--------|
| 2021.3 LTS | ✅ |
| 2022.x | ✅ |
| 2023.x | ✅ |
| Unity 6 | ✅ |

## ❓ Troubleshooting

**Antigravity doesn't appear in dropdown**
- Check `Window > Package Manager > In Project` for the package
- Restart Unity

**"Editor path is not set" error**
- Go to `Preferences > External Tools` → Click **Browse...**

**IntelliSense not working**
- Run `Window > Antigravity > Generate All Config Files`
- Restart Antigravity

## 🤝 Contributing

Contributions are welcome! Feel free to open issues or submit Pull Requests to help improve the package.

## 📄 License

MIT License - see [LICENSE](LICENSE)

---

*Community-maintained. Not affiliated with Google.*

