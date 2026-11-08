# Course Progress

## Now

- **Current lesson:** TS-01 — Tooling: `tsc`, Vite, `tsconfig.json`, `strict`
- **Experience level:** 2 / 10 (beginner in TS, strong C# background, so classes and generics can go faster)
- **Next up:** TS-01 → TS-02 → TS-03
- **Open questions / things to revisit:** none yet

## Review queue

Concepts marked 🔁 in the syllabus, oldest first. A study session can start by
quizzing these.

- Microtasks vs timers (missed twice in the quiz app; got it right in the 2026-11-08 warm-up). Revisit in TS-16.
- `unknown` vs `any` (missed in the quiz app). Covered in TS-02.

## Session log

Newest first. One entry per study session, desktop or remote.

<!--
### YYYY-MM-DD · desktop|remote · <lesson IDs>
- **Did:** what was covered / built
- **Code:** files or commit
- **Struggled with:** …
- **Next:** …
-->

### 2026-11-08 · desktop · quiz app fix
- **Did:** Reviewed quiz results (TS-00 4/8, retry 3/4). Warm-up on event loop order: correct. Found that a quiz left early never saved an `attempts` doc (only progress); the app now saves the attempt after every answer.
- **Code:** `Course/quiz-app/course-drills.html` (republished, uncommitted)
- **Struggled with:** microtasks vs timers (quiz), `unknown` vs `any`.
- **Next:** TS-01.

### 2026-11-08 · desktop · TS-00
- **Did:** Placement check: 4 background questions and an 8-question quiz, scored 3/8. Set experience level to 2.
- **Code:** none (notes in `lessons/TS-00-placement.md`)
- **Struggled with:** `any`/`unknown`, literal inference, `type` vs `interface`, narrowing, `keyof` generics, microtask order.
- **Next:** TS-01 tooling and `tsconfig.json`.

### 2026-11-08 · desktop · setup
- **Did:** Created the course structure (`SYLLABUS.md`, `PROGRESS.md`, lesson template, `course-tutor` skill, Claude Project instructions).
- **Next:** TS-00 placement check, then NG-00 to create the Recipe Box app.
