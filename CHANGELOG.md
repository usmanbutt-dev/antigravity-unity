# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.4.0] - 2026-05-29

### Fixed
- **Antigravity IDE detection** - Updated auto-discovery to prioritize the new **Antigravity IDE** executable paths on Windows, macOS, and Linux following the Google I/O 2026 product split. Legacy `Antigravity.exe` / `Antigravity.app` paths are still supported for backward compatibility. ([#2](https://github.com/usmanbutt-dev/antigravity-unity/issues/2))
- **LICENSE.meta restored** - Re-added the `LICENSE.meta` file for Unity package compliance. The `.gitattributes` file ensures GitHub license detection remains unaffected. ([#1](https://github.com/usmanbutt-dev/antigravity-unity/issues/1))

### Changed
- Updated README default paths to reflect the post-I/O 2026 Antigravity IDE executable names

---

## [1.3.0] - 2026-02-09

### Added
- **Config File Generators** - New menu items under `Window > Antigravity`:
  - Generate All Config Files
  - Generate `.omnisharp.json` - Better C# IntelliSense for Unity
  - Generate `.editorconfig` - Unity coding style conventions
  - Generate `.vscode/settings.json` - Workspace settings, file associations, exclusions

---

## [1.2.0] - 2026-02-09

### Added
- **Context Menu**: Right-click "Open in Antigravity" option in Project window
- **Window Menu**: New menu items under `Window > Antigravity`:
  - Open Project Folder
  - Open Assets Folder
  - Regenerate Project Files
  - Open Preferences
- **Extended Discovery Paths**:
  - Windows: Scoop, Chocolatey support
  - Linux: Snap, Flatpak support

---

## [1.1.0] - 2024-12-28

### Added
- Unity-internal file type handling: Files like `.unity`, `.prefab`, `.asset`, `.mat`, `.controller`, `.anim`, and other Unity-specific formats are now handled by Unity internally instead of being opened in Antigravity

### Fixed
- Improved compatibility with Unity's asset opening workflow

---

## [1.0.0] - 2024-12-11

### Added
- Initial release
- `IExternalCodeEditor` implementation for Unity integration
- Auto-detection of Antigravity installation on Windows, macOS, and Linux
- Manual path configuration via Preferences GUI
- VS Code-style file opening with `-g file:line:column` arguments
- Project synchronization support
- Preferences UI with Browse and Reset buttons

### Supported Platforms
- Windows (tested on Windows 10/11)
- macOS (tested on macOS 12+)
- Linux (tested on Ubuntu 22.04)

### Supported Unity Versions
- Unity 2021.3 LTS and later
- Unity 2022.x
- Unity 2023.x
- Unity 6 (6000.x)
