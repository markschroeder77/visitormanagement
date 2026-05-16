# Upgrade Options — CleanArchitecture.Blazor

Assessment: 7 code projects + docker-compose, all on net7.0 (modern .NET), 63 issues (24 mandatory), 6 security vulnerabilities, IdentityModel APIs flagged

## Strategy

### Upgrade Strategy
All projects are on net7.0 (modern .NET), solution has 7 code projects (≤15), 4-tier dependency depth with no CI-green constraint. All-at-Once is the best fit: a single atomic TFM bump with package updates and code fixes.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects simultaneously in a single atomic pass. Fastest approach, no multi-targeting overhead. |
| Top-Down | Upgrade entry-point apps first, temporarily multi-target shared libraries. Adds overhead not needed here. |

## Project Structure

### Package Management
Solution has 7 projects, all SDK-style, all on modern .NET, no Directory.Packages.props exists. Modern-to-modern upgrade with no Framework coexistence — CPM adds consistency.

| Value | Description |
|-------|-------------|
| **Central Package Management (CPM)** (selected) | Creates Directory.Packages.props, moves package versions out of project files. Better consistency across the 7 projects. |
| Per-Project (defer CPM to post-migration) | Each project retains its own versions. Recommended during Framework migrations; not needed here. |

## Compatibility

### Unsupported API Handling
Assessment flagged 17 binary-incompatible API occurrences and 12 source-incompatible occurrences, primarily from IdentityModel & Claims-based Security APIs (11 issues).

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve every API change in the same task, including complex ones. Leaves no deferred work or stubs to clean up. |
| Defer Complex Changes | Apply simple replacements inline; stub complex changes and create resolution subtasks. |

## Modernization

### Nullable Reference Types
Target is net10.0 (supports NRTs), no projects have Nullable enabled. However, solution has 7 projects and assessment shows 63 issues — migration is already demanding.

| Value | Description |
|-------|-------------|
| **Leave Disabled** (selected) | Does not enable nullable. Maintains existing null handling. Enable separately after migration as a distinct effort. |
| Enable Nullable Reference Types | Adds `<Nullable>enable</Nullable>` to all project files. May require significant code updates given codebase size. |
