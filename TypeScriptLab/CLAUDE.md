# TypeScriptLab — guidance

Browser-based TypeScript playground built with Vite. It is also the workspace for the
TypeScript track (TS-xx) of the course in `../Course/`.

## Commands (run from `TypeScriptLab/`)

```sh
npm install
npm run dev      # Vite dev server on http://localhost:8080 (opens the browser)
npm run build    # tsc type-check, then vite build into dist/
npm run preview  # serve dist/
```

Port 8080 is also used by the DotNetLab API container. Stop one of them, or run `npx vite --port 5173`.
There is no test suite: `npm run build` (or `npx tsc --noEmit`) is the validation step.

## Structure

- `src/app.ts` — entry point. Example functions are registered in the `selectableFunctions` array.
- `src/Interfaces/` — contracts such as `Logger`.
- `src/Implementations/` — classes implementing them (`ConsoleLogger`, `TextTransformer`).
- `src/Styles/` — plain CSS split by concern (variables, reset, layout, typography, components).
- `src/Lessons/<ID>/` — course exercise code (for example `src/Lessons/TS-03/`), wired into `selectableFunctions`.

## Conventions

- `strict` is on (target ES2020, ESNext modules). Don't use `any`; prefer `unknown` plus narrowing.
- Plain TypeScript and DOM APIs only, no frameworks. Keep examples small and self-explanatory.
- Match the existing file naming: PascalCase files for classes and interfaces, one per file.
- This lab pins TypeScript `^5.3`, while AngularLab uses `~6.0`. Mention it if a feature behaves differently.
