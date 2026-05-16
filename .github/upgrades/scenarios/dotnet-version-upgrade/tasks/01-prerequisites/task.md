# 01-prerequisites: Verify SDK and toolchain readiness

## Objective
Confirm the environment is ready for the net7.0 → net10.0 upgrade.

## Findings

- **.NET 10 SDK**: ✅ Installed and compatible
- **global.json**: ✅ Not present — no SDK pinning to update
- **Baseline build**: ✅ 0 errors, 619 pre-existing warnings (unused fields in Razor pages, etc.)
  - All 619 warnings are pre-existing and must be resolved by end of upgrade (no-warning rule)
  - No errors — clean baseline confirmed

## Done When
- .NET 10 SDK confirmed installed ✅
- global.json checked/updated if needed ✅
- Baseline build verified 0 errors ✅
