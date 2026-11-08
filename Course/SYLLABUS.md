# TypeScript + Angular Course — Syllabus

The **course structure and lesson status live here**. `PROGRESS.md` holds the
session-by-session log; `lessons/` holds one note per lesson.

Status legend: ⬜ not started · 🟡 in progress · ✅ done · 🔁 needs review · ⏭️ skipped

## Tracks

| Track | Lab | Where lesson code goes |
|-------|-----|------------------------|
| **TS** — TypeScript language | `TypeScriptLab/` | `TypeScriptLab/src/Lessons/<ID>/`, wired into `selectableFunctions` in `src/app.ts` |
| **NG** — Angular (v22) | `AngularLab/` | NG-01…NG-21: the *Recipe Box* app (see NG-00). NG-22+: `AngularLab/src/app/` or the Recipe Box, decided per lesson |

NG-01…NG-21 follow the Angular CLI MCP **`ai_tutor`** curriculum ("Smart Recipe
Box", modules 1–21) one-to-one, so the tutor's module numbers equal the lesson
numbers. NG-22+ go beyond the tutor and cover the rest of modern Angular.

## Learning path (recommended order)

Each TypeScript block unlocks the concepts the next Angular block depends on.

1. TS-00 → TS-01…TS-08 (foundations, composing types)
2. NG-00 → NG-01…NG-06 (fundamentals, signals)
3. TS-09…TS-11 (classes, modules)
4. NG-07…NG-12 (component architecture)
5. TS-12…TS-15 (generics, type-level programming)
6. NG-13…NG-17 (forms, services, routing, Material)
7. TS-16…TS-19 (async, DOM, decorators, runtime validation)
8. NG-18…NG-21 (Signal Forms)
9. NG-22…NG-31 (beyond the tutor, capstone)

---

## Track TS — TypeScript (`TypeScriptLab/`)

### Unit 0 — Placement
| ID | Lesson | Status |
|----|--------|--------|
| TS-00 | Placement check: what I already know (diagnostic quiz, sets experience level) | ✅ |

### Unit 1 — Foundations
| ID | Lesson | Status |
|----|--------|--------|
| TS-01 | Tooling: `tsc`, Vite, `tsconfig.json`, what `strict` turns on | ⬜ |
| TS-02 | Primitive types, inference, `any` vs `unknown` vs `never` | ⬜ |
| TS-03 | Functions: parameter/return types, optional & default params, overloads, `void` | ⬜ |
| TS-04 | Object types: `type` vs `interface`, optional & `readonly` properties | ⬜ |

### Unit 2 — Composing types
| ID | Lesson | Status |
|----|--------|--------|
| TS-05 | Union, intersection and literal types | ⬜ |
| TS-06 | Narrowing: `typeof`, `in`, `instanceof`, type predicates, discriminated unions, exhaustive `never` checks | ⬜ |
| TS-07 | Template literal types | ⬜ |
| TS-08 | Enums vs `as const` objects, `satisfies` | ⬜ |

### Unit 3 — Classes and modules
| ID | Lesson | Status |
|----|--------|--------|
| TS-09 | Classes: fields, constructors, access modifiers, `implements` (the existing `Logger`) | ⬜ |
| TS-10 | Abstract classes, inheritance vs composition | ⬜ |
| TS-11 | ES modules: named/default exports, `import type`, barrels | ⬜ |

### Unit 4 — Generics and type-level programming
| ID | Lesson | Status |
|----|--------|--------|
| TS-12 | Generic functions, classes and constraints | ⬜ |
| TS-13 | Built-in utility types (`Partial`, `Pick`, `Omit`, `Record`, `ReturnType`…) | ⬜ |
| TS-14 | `keyof`, `typeof`, indexed access types | ⬜ |
| TS-15 | Mapped and conditional types, `infer` | ⬜ |

### Unit 5 — Async and runtime
| ID | Lesson | Status |
|----|--------|--------|
| TS-16 | Promises, `async`/`await`, typed error handling | ⬜ |
| TS-17 | Typing the DOM: elements, events, null checks | ⬜ |
| TS-18 | Decorators (standard TC39) — and how Angular's differ | ⬜ |
| TS-19 | `fetch` + validating `unknown` JSON at runtime | ⬜ |

---

## Track NG — Angular (`AngularLab/`)

### Setup
| ID | Lesson | Status |
|----|--------|--------|
| NG-00 | Create the Recipe Box app for the tutor (keeps the existing playground untouched) | ⬜ |

### Phase 1 — Angular fundamentals (`ai_tutor` modules 1–3)
| ID | Lesson | Status |
|----|--------|--------|
| NG-01 | Getting started: project structure | ⬜ |
| NG-02 | Dynamic text with interpolation | ⬜ |
| NG-03 | Event listeners `(click)` | ⬜ |

### Phase 2 — State and signals (modules 4–6)
| ID | Lesson | Status |
|----|--------|--------|
| NG-04 | Writable signals, part 1: `set` | ⬜ |
| NG-05 | Writable signals, part 2: `update` | ⬜ |
| NG-06 | Computed signals | ⬜ |

### Phase 3 — Component architecture (modules 7–12)
| ID | Lesson | Status |
|----|--------|--------|
| NG-07 | Property and attribute binding | ⬜ |
| NG-08 | Creating and nesting components | ⬜ |
| NG-09 | Component inputs with signals: `input()` | ⬜ |
| NG-10 | Styling components | ⬜ |
| NG-11 | List rendering with `@for` | ⬜ |
| NG-12 | Conditional rendering with `@if` | ⬜ |

### Phase 4 — Advanced features and architecture (modules 13–17)
| ID | Lesson | Status |
|----|--------|--------|
| NG-13 | Two-way binding | ⬜ |
| NG-14 | Services and dependency injection | ⬜ |
| NG-15 | Basic routing | ⬜ |
| NG-16 | Introduction to forms (reactive forms) | ⬜ |
| NG-17 | Introduction to Angular Material | ⬜ |

### Phase 5 — Signal Forms (modules 18–21; stable in v22)
| ID | Lesson | Status |
|----|--------|--------|
| NG-18 | Introduction to Signal Forms | ⬜ |
| NG-19 | Submitting and resetting | ⬜ |
| NG-20 | Validation | ⬜ |
| NG-21 | Field state and error messages | ⬜ |

### Phase 6 — Beyond the tutor
| ID | Lesson | Status |
|----|--------|--------|
| NG-22 | HTTP: `HttpClient`, `resource()` / `httpResource()` | ⬜ |
| NG-23 | Routing deep dive: lazy loading, guards, resolvers, route input binding | ⬜ |
| NG-24 | RxJS essentials and signal interop (`toSignal`, `toObservable`) | ⬜ |
| NG-25 | Testing components and services with Vitest | ⬜ |
| NG-26 | Directives and pipes (with the `host` object) | ⬜ |
| NG-27 | `effect()`, `linkedSignal()` and state patterns | ⬜ |
| NG-28 | Server-side rendering and hydration (AngularLab is SSR) | ⬜ |
| NG-29 | Performance: `@defer`, OnPush/zoneless, `NgOptimizedImage` | ⬜ |
| NG-30 | Accessibility: WCAG AA, focus management, AXE audit | ⬜ |
| NG-31 | Capstone: plan and build a small app end to end | ⬜ |
