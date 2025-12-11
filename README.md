# Antigravity IDE Support for Unity

[![Unity 2021.3+](https://img.shields.io/badge/Unity-2021.3%2B-blue.svg)](https://unity.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A Unity package that integrates [Google Antigravity IDE](https://antigravity.google) as an external code editor.

## Features

- 🚀 **Seamless Integration** - Antigravity appears in Unity's External Tools dropdown
- 📍 **Smart Navigation** - Opens files at the correct line and column
- 🔍 **Auto-Detection** - Automatically finds Antigravity installation on Windows, macOS, and Linux
- ⚙️ **Manual Configuration** - Browse to select custom installation paths
- 🔄 **Project Sync** - Generates proper `.csproj` and `.sln` files

## Installation

### Option 1: Git URL (Recommended)

1. Open Unity and go to `Window > Package Manager`
2. Click the `+` button in the top-left corner
3. Select `Add package from git URL...`
4. Enter the following URL:
   ```
   https://github.com/community/antigravity-unity.git
   ```
5. Click `Add`

### Option 2: OpenUPM

```bash
openupm add com.community.antigravity-unity
```

### Option 3: Manual Installation

1. Download this repository as a ZIP
2. Extract to your project's `Packages/` folder
3. Rename the folder to `com.community.antigravity-unity`

## Setup

1. Go to `Edit > Preferences > External Tools` (Windows/Linux) or `Unity > Preferences > External Tools` (macOS)
2. In the **External Script Editor** dropdown, select **Antigravity**
3. If Antigravity is not auto-detected, click **Browse...** to manually select the executable:
   - **Windows**: Usually `%LOCALAPPDATA%\Programs\Antigravity\Antigravity.exe`
   - **macOS**: Usually `/Applications/Antigravity.app`
   - **Linux**: Usually `/usr/bin/antigravity` or `/opt/antigravity/antigravity`

## Usage

Once configured, simply double-click any C# script in Unity's Project window. Antigravity will open with:
- The correct file focused
- The cursor at the line where the symbol is defined (when clicking from error messages)

## Requirements

- Unity 2021.3 or later
- [Google Antigravity IDE](https://antigravity.google) installed on your system

## Compatibility

| Platform | Status |
|----------|--------|
| Windows  | ✅ Fully Supported |
| macOS    | ✅ Fully Supported |
| Linux    | ✅ Fully Supported |

## Troubleshooting

### Antigravity doesn't appear in the dropdown
- Ensure the package is properly installed (check `Window > Package Manager > In Project`)
- Try closing and reopening Unity

### "Editor path is not set" error
- Go to `Preferences > External Tools` and use the **Browse...** button to manually select Antigravity

### Files open but cursor is at wrong position
- Antigravity uses VS Code-style arguments (`-g file:line:column`). Ensure you have the latest version of Antigravity installed.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Disclaimer

This is a community-maintained package and is not officially affiliated with Google or the Antigravity team.
