# Studying on claude.ai (phone or browser)

Use this when you're away from the PC and can't reach a Claude Code session.

Claude Code plugins don't run on claude.ai. Instead, the tutor's rules go into a claude.ai Project's
instructions: [PROJECT_INSTRUCTIONS.md](PROJECT_INSTRUCTIONS.md). They follow the same course, teaching
style and log format as the desktop tutor, without the build, the Angular MCP or file editing.

## 1. Create the project (once)

claude.ai → **Projects → New project**, named **"TS + Angular Course"**.

- **Instructions:** paste everything in [PROJECT_INSTRUCTIONS.md](PROJECT_INSTRUCTIONS.md) except its title line.
- **Knowledge files:** the repo is on Azure DevOps, so claude.ai's GitHub sync can't pull it. Upload:
  - `Course/SYLLABUS.md`, `Course/PROGRESS.md`, every note in `Course/lessons/`
  - `AngularLab/CLAUDE.md` (the Angular v22 conventions)
  - Optional: code files the current lessons use, such as `TypeScriptLab/src/app.ts`

## 2. Study

Open a chat in the project and say **"next lesson"**, **"quiz me"**, **"flashcards"** or **"status"**.

## 3. Back at the PC

1. In the claude.ai chat, say **"end session"** and copy the log block it gives you.
2. In Claude Code, say **"log my mobile session"** and paste it. The desktop tutor updates `PROGRESS.md` and `SYLLABUS.md`.
3. Re-upload `PROGRESS.md`, `SYLLABUS.md` and any new lesson notes to the project, so the next mobile chat isn't working from old copies.

## Keeping the two tutors in sync

`PROJECT_INSTRUCTIONS.md` is the master copy of the claude.ai rules. When you change it, paste it into
the project again. The desktop skill (`plugin/skills/course-tutor/SKILL.md`) shares the teaching style and
log format, so when you change one, check whether the other needs the same change.
