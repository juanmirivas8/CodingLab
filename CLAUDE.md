# CodingLab — repository guidance

## Purpose

This repository is a playground for learning, experimenting with, and comparing coding ideas. Prefer small, focused, readable examples that make the concept being explored clear. Keep experiments easy to run and change; do not add production-scale abstractions, cross-lab dependencies, or broad refactors unless a task calls for them. Different labs may intentionally demonstrate different approaches.

## Repository layout

- `AngularLab/` — Angular application with server-side rendering. Its source is under `src/`; use its local npm scripts.
- `TypeScriptLab/` — browser-based TypeScript/Vite playground. The app entry point is `src/app.ts`, with implementation examples under `src/Implementations/`.
- `DotNetLab/` — .NET solution `DotNetLab.sln`, containing `DotNetLab.Model`, `DotNetLab.Database` (SQL project), `DotNetLab.MinimalAPI.Host`, `DotNetLab.Services`, `DotNetLab.Data`, and `DotNetLab.NotificationFunction`.
- `Miscellaneous/` — space for standalone experiments that do not belong to one of the focused labs.
- `Course/` — personal TypeScript + Angular course: syllabus, progress log and lesson notes. `Course/plugin/` is the `ts-angular-course` Claude Code plugin (tutor skill, `ng-curriculum` and `exercise-reviewer` subagents, Angular CLI MCP), installed from the local `codinglab` marketplace. Keep `Course/SYLLABUS.md` and `Course/PROGRESS.md` in sync when lessons change.

Each lab has its own `CLAUDE.md` with lab-specific commands and conventions, which Claude Code loads when working in that folder.

Treat each lab as independent. Run commands from that lab's directory and follow its manifests rather than assuming a repository-wide runner. There is no shared root-level build or test command.

## Local commands

Use the scripts currently declared by each `package.json`; install dependencies in the lab you are working on, not at the repository root.

From `AngularLab/`:

```sh
npm install
npm start
npm run build
npm test
```

From `TypeScriptLab/`:

```sh
npm install
npm run dev
npm run build
npm run preview
```

`TypeScriptLab` also supports `npm start` for Vite. Its dev server uses port `8080`, which is also used by the DotNetLab API; choose another port or stop the conflicting service if both need to run locally.

From `DotNetLab/`:

```sh
dotnet restore DotNetLab.sln
dotnet build DotNetLab.sln
```

The C# project files target .NET 10. Treat project and package manifests as authoritative when a README or quick-start document disagrees; update documentation separately when requested.

## Change and validation practices

- Follow the nested `CLAUDE.md` of the lab being changed. `AngularLab/CLAUDE.md` supplies Angular-specific TypeScript, Angular, and accessibility guidance for that lab; do not apply it to the other labs.
- Keep a learning example focused and explain its intent through clear names and concise structure. Match the local language, formatting, and established patterns rather than imposing conventions from another lab.
- Validate changes in the affected lab with its available build or test command. Angular provides `npm test`; the TypeScript and .NET areas do not currently define a dedicated automated test suite, so use their available build checks and add focused tests when a task introduces non-trivial behavior.
- Avoid editing generated output such as `dist/`, `node_modules/`, or build artifacts; change their source and regenerate them only when needed.

## Asking the user questions

Ask every question (clarifications, confirmations, choices, quizzes) through `AskUserQuestion`, using multiple-choice options. Never put code in question or option text, because it renders as plain text: show snippets as fenced code blocks in the message before the call, or in an option's `preview` field. Subagents can't ask the user, so they return open questions to the main conversation.

## Secrets and local services

- Never commit credentials, tokens, connection strings, or populated local environment files. Keep local values in the appropriate ignored environment file or .NET User Secrets, and use example files for shareable configuration.
- Do not print, copy, or expose values from `DotNetLab/DeploymentFiles/.env` or other local secret files. `.claude/settings.json` denies reading `.env`, `local.settings.json` and similar files, plus generated folders (`node_modules/`, `dist/`, `bin/`, `obj/`); don't work around these rules. Do not replace real local values with placeholders in a way that overwrites the user's configuration.
- Docker Compose services may use persistent databases, queues, or volumes. Inspect the relevant compose configuration before starting services, and do not remove volumes or destroy local data unless explicitly requested.