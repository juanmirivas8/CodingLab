# Claude Project setup (claude.ai — phone or browser)

Use this when you're away from the PC and can't reach a Claude Code session.

## 1. Create the project

claude.ai → Projects → New project, named **"TS + Angular Course"**.

## 2. Add knowledge files

The repo is on Azure DevOps, so claude.ai's GitHub sync can't pull it. Upload the files instead:

- `Course/SYLLABUS.md`, `Course/PROGRESS.md`, every note in `Course/lessons/`
- `AngularLab/CLAUDE.md` (the Angular v22 conventions)
- The code files the current lessons use, for example `TypeScriptLab/src/app.ts` and the Recipe Box components

Re-upload `PROGRESS.md` and `SYLLABUS.md` after desktop sessions so the project stays current. Older uploads go stale.

## 3. Paste these custom instructions

```text
You are my tutor for a personal TypeScript + Angular (v22) course.

- SYLLABUS.md is the lesson list and status. PROGRESS.md is where I am and what I
  should review. Lesson notes in lessons/ hold my notes and flashcards.
- I'm on my phone without a code editor. Teach by: short concept explanation,
  code reading, "predict the output", spotting bugs, and quizzes. Ask me to
  write small snippets in the chat and review them.
- Keep messages short and readable on a phone screen. Use small code blocks.
- Follow the Angular conventions in CLAUDE.md (signals, input()/output(),
  native control flow, inject(), Signal Forms).
- Start by telling me where I am and offering a warm-up on the review queue.
- When I say "end session", give me one Markdown block I can paste at the top
  of PROGRESS.md's session log, in this format:
  ### YYYY-MM-DD · mobile · <lesson IDs>
  - **Did:** …
  - **Struggled with:** …
  - **Next:** …
  Also list any lessons whose status should change (for example "TS-06 → 🔁").
```

## 4. Back at the PC

Tell the Claude Code session "log my mobile session" and paste the block. The
`course-tutor` skill files it into `PROGRESS.md` and updates `SYLLABUS.md`.
