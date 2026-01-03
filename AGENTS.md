# Agent Guide

- This repository has a .NET 6 API in `WavoProjects.Api` and an Angular 18 UI in `WavoProjects.UI/WavOps`.
- Favor environment-variable driven configuration with sensible fallbacks to the checked-in config files (appsettings, environment.ts, etc.).
- When updating Angular environment values, use the `NG_APP_...` variables to override defaults.
- Keep container and Docker-related assets developer-friendly (clear ports, reusable environment variables).
- Use clear, descriptive commit messages summarizing the changes.
