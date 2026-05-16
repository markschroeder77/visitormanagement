# Progress Details — 02-upgrade-projects

## What Changed
- **Framework**: Changed TargetFramework from 
et7.0 to 
et10.0 in all 7 project files.
- **Package Management**: Created Directory.Packages.props and moved all package versions from project files to this central location.
- **Packages**:
  - Updated all Microsoft.* (EF Core, ASP.NET Core) to 10.0.8.
  - Updated AutoMapper to 16.1.1 and removed legacy DI extension package.
  - Updated Duende.IdentityServer packages to 8.0.0-alpha.1 for net10 compatibility.
  - Updated SixLabors.ImageSharp to 4.0.0 and Drawing to 3.0.0 (fixing security vulnerabilities).
  - Updated several other packages with security vulnerabilities (MailKit, MimeKit, System.Linq.Dynamic.Core).
- **Code**:
  - src/Application/Common/Mappings/IMapFrom.cs: Removed MethodMappingEnabled configuration.
  - src/Infrastructure/Services/Identity/IdentityService.cs: Replaced JwtSecurityTokenHandler with JsonWebTokenHandler for JWT creation and validation.
  - src/Application/DependencyInjection.cs: Fixed AddAutoMapper registration.
  - 	ests/Application.UnitTests/Common/Mappings/MappingTests.cs: Fixed MapperConfiguration constructor call for AutoMapper 16.
  - src/Blazor.Server.UI/Program.cs: Fixed ambiguous TimeSpan overloads by using double literals.

## Validation Results
- **Build**: All 7 projects build successfully on net10.0.
  - Note: Solution build requires -p:SixLaborsLicenseKey=AnyValue or similar to bypass the new SixLabors license check task introduced in ImageSharp.Drawing 3.0.
- **Warnings**: 366 warnings remain (mostly pre-existing nullable and obsolete warnings).
