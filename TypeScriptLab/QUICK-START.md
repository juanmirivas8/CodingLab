# Quick Start - TypeScriptLab

## Available scripts

Run these from the `TypeScriptLab/` directory:

```bash
npm install
```

| Script | Command | What it does |
|--------|---------|--------------|
| **dev** (recommended) | `npm run dev` | Starts the Vite dev server on http://localhost:8080 and opens the browser. |
| **start** | `npm start` | Alias for `npm run dev`. |
| **build** | `npm run build` | Runs TypeScript compilation (`tsc`) and produces a production build in `dist/`. |
| **preview** | `npm run preview` | Serves the production build from `dist/` via Vite. |

## One-shot dev run

```bash
npm install
npm run dev
```

The dev server uses port `8080` by default (`vite.config.ts`) and opens the browser automatically.

## Project configuration

- **Entry point:** `src/app.ts`
- **TypeScript config:** `tsconfig.json`
  - Target: `ES2020`
  - Module: `ESNext`
  - Strict mode enabled
  - Includes files under `src/`
- **Vite config:** `vite.config.ts`
  - Dev server port: `8080`
  - `open: true` (opens browser automatically)
  - Build output: `dist/`
  - Source maps enabled

## JetBrains Rider run configuration

If the run configurations are present in the IDE:

1. Open the run configuration dropdown (top-right).
2. Select **"TypeScript Lab - Dev"**.
3. Click **Run** (▶️) or press **Shift + F10**.

This executes `npm run dev`, starts the server, and opens the browser at http://localhost:8080.
