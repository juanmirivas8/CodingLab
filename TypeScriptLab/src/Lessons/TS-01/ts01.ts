// TS-01 — Tooling: tsc, Vite, tsconfig.json, strict
// Exercise (see Course/lessons/TS-01-tooling.md):
//   1. Write three functions below, each breaking ONE rule on purpose.
//   2. Run `npx tsc --noEmit` (3 errors), then `npm run dev` (page still loads).
//   3. Fix the errors, wire ts01Playground into `selectableFunctions` in src/app.ts,
//      and check that `npm run build` passes.
//   4. Bonus: set "strict": false in tsconfig.json, rerun tsc, then restore it.

export function ts01Playground(): void {
    // TODO 1 — noImplicitAny: a function with an untyped parameter.

    // TODO 2 — strictNullChecks: read `.length` of a `string | null`.

    // TODO 3 — noUnusedLocals: declare a local variable and never use it.

    // TODO 4 — call the three functions here so the playground runs them.
}
