# TypeScript + Angular Course

A personal, self-paced course that uses this repo's labs as its workspace:
`TypeScriptLab/` for the language and `AngularLab/` (Angular 22) for the framework.

## What's here

| File | Purpose |
|------|---------|
| [SYLLABUS.md](SYLLABUS.md) | Course structure: tracks, units, lesson IDs, status. |
| [PROGRESS.md](PROGRESS.md) | Where I am now, the review queue, and the session log (past sessions). |
| [lessons/](lessons/) | One note per lesson (goals, key ideas, my solution, flashcards). Start from `_TEMPLATE.md`. |
| [CLAUDE_PROJECT.md](CLAUDE_PROJECT.md) | How to set up a claude.ai Project for studying on mobile. |
| [plugin/](plugin/) | The `ts-angular-course` Claude Code plugin: `course-tutor` skill, `ng-curriculum` and `exercise-reviewer` subagents, Angular CLI MCP. |
| `.claude-plugin/marketplace.json` | Local `codinglab` marketplace that the repo's `.claude/settings.json` installs the plugin from. |

Past, present and future are all in the files: ✅ lessons and the session log
are the past, the "Now" block is the present, and ⬜ lessons in the learning path are the future.

## Three ways to study

### 1. At the PC — Claude Code (main mode)

Open a Claude Code session in the repo root and say **"next lesson"** (or `/ts-angular-course:course-tutor`).
The tutor reads the course files and asks every question as a clickable quiz. For Angular lessons
it sends the `ng-curriculum` subagent to digest the Angular CLI MCP `ai_tutor` module, and when you
finish an exercise the `exercise-reviewer` subagent checks it with a real build. Say **"end session"**
to update the log.

The plugin brings its own Angular CLI MCP server (`npx -y @angular/cli mcp`).

### 2. Away from the PC with the PC on — Remote Control

Turn on Remote Control for a Claude Code session on this machine, then continue it
from the Claude app on your phone. It's the same session with the same files and tools,
so the tutor can still read the repo and update the log. Say **"mobile mode"** if you
can't write code comfortably.

### 3. Fully offline from the PC — Claude Project

Follow [CLAUDE_PROJECT.md](CLAUDE_PROJECT.md). The project tutors from uploaded
copies of the course files, and at the end gives you a log block to paste back here.

## Conventions

- Lesson IDs: `TS-xx` (TypeScript), `NG-xx` (Angular). NG-01…NG-21 match the `ai_tutor` modules 1–21.
- Lesson code: `TypeScriptLab/src/Lessons/<ID>/`; Angular tutor work goes in the Recipe Box app (NG-00).
- Commit course files together with lesson code: `Course: <ID> <title>`.
