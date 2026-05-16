# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade all projects from net7.0 to net10.0 (LTS)
**Scope**: 7 code projects across 4 dependency tiers — Domain, Application, Infrastructure, Blazor.Server.UI, plus 3 test projects

### Selected Strategy
**All-at-Once** — All projects upgraded simultaneously in a single atomic operation.
**Rationale**: 7 projects, all on net7.0 (modern .NET), dependency graph is 4 tiers deep but manageable. Single atomic TFM bump with package updates and code fixes is the fastest path forward.

## Tasks

### 01-prerequisites: Verify SDK and toolchain readiness

Confirm that the .NET 10 SDK is installed and that any global.json files in the repository are compatible with net10.0. The solution currently targets net7.0, and the .NET 10 SDK must be present before any project files are modified. If global.json pins an older SDK version, it must be updated to allow .NET 10 builds. This task produces no code changes — it is a gate that ensures the environment is ready.

**Done when**: `dotnet --version` reports a .NET 10 SDK, any global.json files are updated to allow net10.0, and a baseline build of the current solution confirms no pre-existing errors.

---

### 02-upgrade-projects: Upgrade all projects to net10.0

Update all 7 code projects simultaneously to target net10.0. This is the core task of the upgrade and covers:

- **Target framework**: Change `<TargetFramework>net7.0</TargetFramework>` to `net10.0` in all project files: `Domain`, `Application`, `Infrastructure`, `Blazor.Server.UI`, `Application.IntegrationTests`, `Application.UnitTests`, `Domain.UnitTests`.
- **Package updates**: Bump all Microsoft.* packages from 7.x to 10.x (`Microsoft.EntityFrameworkCore.*` → 10.0.x, `Microsoft.AspNetCore.*` → 10.0.x, `Microsoft.Extensions.*` → 10.0.x). Fix security vulnerabilities in `AutoMapper` (→ 16.x), `MailKit`/`MimeKit` (→ 4.16.x), `SixLabors.ImageSharp` (→ 4.x), `Serilog.Enrichers.ClientInfo` (→ 2.9.x), `System.Linq.Dynamic.Core` (→ 1.7.x). Replace the deprecated `AutoMapper.Extensions.Microsoft.DependencyInjection` (functionality merged into AutoMapper 16.x).
- **Central Package Management**: Create `Directory.Packages.props` at the solution root, moving all package versions out of individual project files. All `<PackageReference>` entries in project files become version-less; versions are centralized in the props file.
- **API breaking changes**: Fix the 17 binary-incompatible and 12 source-incompatible API usages flagged in the assessment, primarily in IdentityModel & Claims-based Security (11 issues concentrated in `Infrastructure` and `Blazor.Server.UI`). All fixes applied inline — no stubs or deferred subtasks.

The highest-risk area is `Infrastructure.csproj` (30 issues, 16 mandatory) due to IdentityModel API surface changes and EF Core 10 behavioral changes. `Application.csproj` (15 issues) and `Blazor.Server.UI.csproj` (12 issues) are the next highest.

**Done when**: All 7 project files target net10.0, `Directory.Packages.props` exists with all package versions centralized, the full solution builds with 0 errors and 0 warnings, and all security vulnerabilities are resolved.

---

### 03-tests-and-validation: Run tests and finalize

Run the full test suite across all three test projects (`Domain.UnitTests`, `Application.UnitTests`, `Application.IntegrationTests`) and address any test failures introduced by framework or package behavioral changes. Document any deferred recommendations (e.g., enabling Nullable Reference Types, evaluating AutoMapper 16 configuration API changes, Duende IdentityServer 7+ migration if needed).

**Done when**: All tests pass, solution builds cleanly on net10.0, and a brief summary of any post-upgrade recommendations is recorded.
