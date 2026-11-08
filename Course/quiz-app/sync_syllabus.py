"""Build the quiz app's `meta/syllabus` document from SYLLABUS.md and PROGRESS.md.

Usage (from the repo root):
    python3 Course/quiz-app/sync_syllabus.py > /tmp/syllabus.json
Then write it to the artifact's `meta` collection, doc `syllabus` (the course-tutor skill does this).
"""
import json
import re
import sys
from datetime import datetime, timezone
from pathlib import Path

course = Path(__file__).resolve().parent.parent
row = re.compile(r"^\|\s*((?:TS|NG)-\d{2})\s*\|\s*(.+?)\s*\|\s*(\S+)\s*\|\s*$")

lessons = []
for line in (course / "SYLLABUS.md").read_text(encoding="utf-8").splitlines():
    m = row.match(line)
    if m:
        lesson_id, title, status = m.groups()
        lessons.append({"id": lesson_id, "title": title.replace("`", ""), "status": status})

current = re.search(r"\*\*Current lesson:\*\*\s*((?:TS|NG)-\d{2})", (course / "PROGRESS.md").read_text(encoding="utf-8"))

json.dump(
    {
        "lessons": lessons,
        "current": current.group(1) if current else None,
        "syncedAt": datetime.now(timezone.utc).isoformat(timespec="seconds"),
    },
    sys.stdout,
    ensure_ascii=False,
    indent=1,
)
