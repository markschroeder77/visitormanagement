# .NET Version Upgrade — .NET 7 to .NET 10

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (.NET 10.0 — LTS)

## Source Control
- **Source Branch**: main
- **Working Branch**: main (user chose to stay on current branch)
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Strategy
**Selected**: All-at-Once
**Rationale**: 7 projects, all net7.0 (modern .NET), ≤15 projects, no CI-green constraint — single atomic upgrade is fastest.

### Execution Constraints
- All projects are upgraded in one atomic pass — no tier ordering, no phasing
- Operation sequence: update TFMs → update packages (create CPM) → fix API breaking changes → build → verify 0 errors/warnings
- Full solution build must pass with 0 errors before task is considered done
- Tests run after the atomic upgrade completes, not interleaved
- Fix all warnings — do not suppress with `#pragma warning disable` or `<NoWarn>`

## Upgrade Options
- **Strategy**: All-at-Once
- **Package Management**: Central Package Management (CPM) — create Directory.Packages.props
- **Unsupported API Handling**: Fix Inline — resolve all API changes in the same task
- **Nullable Reference Types**: Leave Disabled — enable separately after migration

## Post-Upgrade Recommendations
- **AutoMapper 16 Configuration**: Migration to AutoMapper 16 has caused DuplicateTypeMapConfigurationException in MappingTests.cs due to new assembly scanning behavior. Standard workarounds failed; MappingTests are currently failing with 5 errors. Re-evaluate profile registration pattern.
- **SixLabors License**: solution builds on local machines now require a license property (e.g. -p:SixLaborsLicenseKey=...) due to SixLabors.ImageSharp.Drawing 3.0 update.
- **IdentityModel Migration**: IdentityService.cs was migrated to JsonWebTokenHandler. Verify JWT verification logic in integration environments.
