# TypeScript + Angular Course

A personal, self-paced course that uses this repo's labs as its workspace:
`TypeScriptLab/` for the language and `AngularLab/` (Angular 22) for the framework.

## What's here

| File | Purpose |
|------|---------|
| [SYLLABUS.md](SYLLABUS.md) | Course structure: tracks, units, lesson IDs, status. |
| [PROGRESS.md](PROGRESS.md) | Where I am now, the review queue, and the session log (past sessions). |
| [lessons/](lessons/) | One note per lesson (goals, key ideas, my solution, flashcards). Start from `_TEMPLATE.md`. |
| [quiz-app/](quiz-app/) | **TypeScript & Angular Drills**, a phone-friendly spaced-repetition quiz published at https://claude.ai/artifact/Rdr2xYVVuQgX9P3dNbRXej. Includes the page source, the seed questions and `sync_syllabus.py`. The desktop tutor reads your scores from it and adds each lesson's flashcards to it. |
| `../.claude/skills/course-tutor`, `../.claude/agents/` | The `course-tutor` skill (run it with `/course-tutor`) and the `ng-curriculum` and `exercise-reviewer` subagents. `../.mcp.json` declares the Angular CLI MCP server. |

Past, present and future are all in the files: ✅ lessons and the session log
are the past, the "Now" block is the present, and ⬜ lessons in the learning path are the future.

## Ways to study

### 1. At the PC — Claude Code (main mode)

Open a Claude Code session in the repo root and say **"next lesson"** (or `/course-tutor`).
The tutor reads the course files and asks every question as a clickable quiz. For Angular lessons
it sends the `ng-curriculum` subagent to digest the Angular CLI MCP `ai_tutor` module, and when you
finish an exercise the `exercise-reviewer` subagent checks it with a real build. Say **"end session"**
to update the log.

The project's `.mcp.json` starts the Angular CLI MCP server (`npx -y @angular/cli mcp`).

### 2. Away from the PC with the PC on — Remote Control

Turn on Remote Control for a Claude Code session on this machine, then continue it
from the Claude app on your phone. It's the same session with the same files and tools,
so the tutor can still read the repo and update the log. Say **"mobile mode"** if you
can't write code comfortably.

### 3. Phone drills — the Drills app

Open the published **TypeScript & Angular Drills** artifact on your phone for spaced-repetition quizzes.
It shares its database with the desktop tutor, so there is nothing to copy back. Free-form front-end
questions can go in a plain claude.ai chat or Project; copy anything worth keeping into `lessons/`.

## Conventions

- Lesson IDs: `TS-xx` (TypeScript), `NG-xx` (Angular). NG-01…NG-21 match the `ai_tutor` modules 1–21.
- Lesson code: `TypeScriptLab/src/Lessons/<ID>/`; Angular tutor work goes in the Recipe Box app (NG-00).
- Commit course files together with lesson code: `Course: <ID> <title>`.
