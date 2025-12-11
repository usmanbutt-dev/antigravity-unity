# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
