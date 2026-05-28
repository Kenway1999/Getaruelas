# Student Consultation Logs

A C# console application that manages student consultation records using **file handling only** (no database, no web, no GUI).

## Features

- Add, view, search, update, soft-delete, and hard-delete records
- Unique RecordId (GUID) and SHA256 checksum for integrity
- Input validation (StudentName, StudentId, AdvisorName, Notes)
- All data persisted to `DataStore/records.ndjson` (newline-delimited JSON)
- Audit logging to `DataStore/audit.log` (every action with timestamp)
- Report generation: Monthly Consultation Summary by Advisor
- Graceful error handling for invalid input, missing files, malformed records, IO exceptions

## Record Model

| Field   | Description |
|---------|-------------|
| RecordId | GUID (unique ID) |
| StudentName | 2–100 chars |
| StudentId | 3–20 alphanumeric |
| AdvisorName | 2–100 chars |
| ConsultationNotes | Optional, max 2000 chars |
| CreatedAt | ISO 8601 timestamp |
| UpdatedAt | ISO 8601 timestamp |
| IsActive | bool (soft delete) |
| Checksum | SHA256 of persistent fields |

## How to Build & Run

```bash
dotnet build
dotnet run
```

Requirements: .NET 7 SDK or later.

## File Structure
