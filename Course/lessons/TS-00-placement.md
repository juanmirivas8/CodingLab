# TS-00 — Placement check

- **Track / unit:** TS Unit 0
- **Status:** ✅ done
- **Dates:** started 2026-11-08 · finished 2026-11-08

## Background

- JS/TS: little, still learning
- Other languages: C# / .NET
- Angular: basics only (the AngularLab playground components)
- Self-rated TypeScript: 1–3 → **experience level 2**

## Quiz results — 3 / 8

| # | Topic | Result | Lesson |
|---|-------|--------|--------|
| Q1 | `any` vs `unknown` | ❌ not sure | TS-02 |
| Q2 | `const` vs `let` inference | ❌ answered `string` for both | TS-02 |
| Q3 | Constructor parameter properties | ✅ | TS-09 |
| Q4 | `type` vs `interface` | ❌ not sure | TS-04 |
| Q5 | Narrowing a discriminated union | ❌ thought a cast was needed | TS-06 |
| Q6 | Generics with `keyof` / `T[K]` | ❌ not sure | TS-12, TS-14 |
| Q7 | Event loop: microtasks vs timers | ❌ answered `1 4 2 3` | TS-16 |
| Q8 | `Partial<Omit<…>>` | ✅ | TS-13 |

## Takeaways for the tutor

- Start at TS-01 and skip nothing. The C# background means TS-09/TS-10 (classes) and TS-12 (generics) can go faster.
- Point out where TypeScript differs from C#: structural typing, literal types,
  narrowing instead of casts, and types being erased at runtime.

## Flashcards

- **Q:** Which can you call methods on without a check: `any` or `unknown`? — **A:** `any`. `unknown` must be narrowed first, which makes it the safe choice.
- **Q:** Type of `const x = "draft"` vs `let y = "draft"`? — **A:** `"draft"` (a literal type) vs `string`.
- **Q:** Inside `if (s.kind === "circle")` on a discriminated union, do you need a cast? — **A:** No. TypeScript narrows `s` to the circle type automatically.
- **Q:** Order of `log(1); setTimeout(log 2); Promise.then(log 3); log(4)`? — **A:** `1 4 3 2`. Promise callbacks (microtasks) run before timers.
