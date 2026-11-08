# ts-angular-course

A Claude Code plugin for a personal TypeScript + Angular course. It reads and updates the course
files in `Course/` (`SYLLABUS.md`, `PROGRESS.md`, `lessons/`).

| Component | What it does |
|-----------|--------------|
| `skills/course-tutor` | The interactive tutor. Say "next lesson", "quiz me", "status" or "end session". |
| `agents/ng-curriculum` | Turns one Angular CLI MCP `ai_tutor` module (or an angular.dev topic) into a short lesson brief. |
| `agents/exercise-reviewer` | Checks a finished exercise, runs the build, and returns hints instead of solutions. |
| `.mcp.json` | Starts the Angular CLI MCP server (`npx -y @angular/cli mcp`). |

It's installed at project scope from the local `codinglab` marketplace (`Course/.claude-plugin/marketplace.json`)
and enabled in `.claude/settings.json`. After editing plugin files, run `/reload-plugins` or start a new session.
Check changes with:

```sh
claude plugin validate Course/plugin
```
