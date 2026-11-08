# TS-01 — Tooling: tsc, Vite, tsconfig.json, strict

- **Track / unit:** TS Unit 1
- **Status:** 🟡 in progress
- **Dates:** started 2026-11-08 · finished —
- **Sources:** https://www.typescriptlang.org/tsconfig

## Goals

- I can explain what `tsc` and Vite each do, and why `tsc` runs with `noEmit` here.
- I can name what `strict` turns on (`strictNullChecks`, `noImplicitAny`, …).
- I know types are erased at runtime and `as` is not a check.

## Key ideas

Short notes in my own words. Include the smallest code snippet that shows the idea.

## Exercise

Write three functions that each break one strict rule (`noImplicitAny`, `strictNullChecks`, `noUnusedLocals`), see `tsc --noEmit` fail while `npm run dev` still loads, then fix them, wire into `selectableFunctions` and pass `npm run build`. Bonus: toggle `strict`.

**My solution:** `TypeScriptLab/src/Lessons/TS-01/ts01.ts` (skeleton only, not started)

## Mistakes and gotchas

- …

## Flashcards

Question/answer pairs for spaced review (also loaded into the Drills app).

- **Q:** … — **A:** …

## Open questions

- …
