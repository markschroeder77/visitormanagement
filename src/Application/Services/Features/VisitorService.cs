// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Interfaces;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Visitors.DTOs;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Search;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.GetById;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.GetAll;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Reports;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Kanban;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Related;
using CleanArchitecture.Blazor.Application.Features.Visitors.Commands.Create;
using CleanArchitecture.Blazor.Application.Features.Visitors.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Visitors.Commands.Delete;
using CleanArchitecture.Blazor.Application.Features.Visitors.Commands.Approve;
using CleanArchitecture.Blazor.Application.Features.Visitors.Commands.Checking;
using CleanArchitecture.Blazor.Application.Features.Visitors.Commands.CompleteVisitorInfo;
using CleanArchitecture.Blazor.Application.Features.Visitors.Commands.Update;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.Commands.Create;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Services.Features;

public class VisitorService : IVisitorService
{
    private readonly ISender _mediator;

    public VisitorService(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<VisitorDto?> SearchVisitorAsync(string keyword, CancellationToken ct = default)
        => await _mediator.Send(new SearchVisitorQuery(keyword), ct);

    public async Task<List<VisitorDto>> SearchVisitorFuzzyAsync(string keyword, CancellationToken ct = default)
        => await _mediator.Send(new SearchVisitorFuzzyQuery(keyword), ct);

    public async Task<List<VisitorDto>> SearchPendingApprovalVisitorsAsync(string? keyword, CancellationToken ct = default)
        => await _mediator.Send(new SearchPendingApprovalVisitorsQuery(keyword), ct);

    public async Task<List<VisitorDto>> SearchPendingCheckingVisitorsAsync(string? keyword, CancellationToken ct = default)
        => await _mediator.Send(new SearchPendingCheckingVisitorsQuery(keyword), ct);

    public async Task<List<VisitorDto>> SearchPendingConfirmVisitorsAsync(string? keyword, CancellationToken ct = default)
        => await _mediator.Send(new SearchPendingConfirmVisitorsQuery(keyword), ct);

    public async Task<List<VisitorDto>> SearchPendingCheckinVisitorsAsync(string? keyword, CancellationToken ct = default)
        => await _mediator.Send(new SearchPendingCheckinVisitorsQuery(keyword), ct);

    public async Task<PaginatedData<VisitorDto>> GetVisitorsWithPaginationAsync(VisitorsWithPaginationQuery query, CancellationToken ct = default)
        => await _mediator.Send(query, ct);

    public async Task<VisitorDto?> GetVisitorByIdAsync(int id, CancellationToken ct = default)
        => await _mediator.Send(new GetByIdVisitorQuery(id), ct);

    public async Task<List<VisitorDto>> GetAllVisitorsAsync(CancellationToken ct = default)
        => (await _mediator.Send(new GetAllVisitorsQuery(), ct)).ToList();

    public async Task<List<VisitorDto>> GetRelatedVisitorsAsync(int id, CancellationToken ct = default)
        => await _mediator.Send(new GetRelatedVisitorQuery(id), ct) ?? new();

    public async Task<Result<int>> CreateVisitorAsync(string passCode, int? visitorId, string? name, string? phoneNumber, string? email, string? identificationNo, string? purpose, int? siteId, CancellationToken ct = default)
    {
        var command = new CreateVisitorCommand
        {
            PassCode = passCode,
            Name = name,
            PhoneNumber = phoneNumber,
            Email = email,
            IdentificationNo = identificationNo,
            Purpose = purpose,
            SiteId = siteId
        };
        return await _mediator.Send(command, ct);
    }

    public async Task<Result<int>> UpdateVisitorAsync(VisitorDto dto, CancellationToken ct = default)
    {
        var command = new AddEditVisitorCommand();
        // Map properties from dto
        command.Id = dto.Id;
        command.Name = dto.Name;
        command.PassCode = dto.PassCode;
        command.QrCode = dto.QrCode;
        command.Email = dto.Email;
        command.PhoneNumber = dto.PhoneNumber;
        command.IdentificationNo = dto.IdentificationNo;
        command.LicensePlateNumber = dto.LicensePlateNumber;
        command.Address = dto.Address;
        command.Gender = dto.Gender;
        command.CompanyName = dto.CompanyName;
        command.Purpose = dto.Purpose;
        command.Comment = dto.Comment;
        command.DesignationId = dto.DesignationId;
        command.EmployeeId = dto.EmployeeId;
        command.CheckinDate = dto.CheckinDate;
        command.CheckoutDate = dto.CheckoutDate;
        command.ExpectedDate = dto.ExpectedDate;
        command.ExpectedTime = dto.ExpectedTime;
        command.Avatar = dto.Avatar;
        command.TripCode = dto.TripCode;
        command.HealthCode = dto.HealthCode;
        command.NucleicAcidTestReport = dto.NucleicAcidTestReport;
        command.Status = dto.Status;
        command.Apppoved = dto.Apppoved;
        command.ApprovalOutcome = dto.ApprovalOutcome;
        command.PrivacyPolicy = dto.PrivacyPolicy;
        command.Promise = dto.Promise;
        command.SiteId = dto.SiteId;
        command.Companions = dto.Companions;
        command.Employee = dto.Employee;
        command.Designation = dto.Designation;
        command.ApprovalHistories = dto.ApprovalHistories;
        command.ApprovalComment = dto.ApprovalComment;
        return await _mediator.Send(command, ct);
    }

    public async Task<Result> DeleteVisitorAsync(int[] ids, CancellationToken ct = default)
        => await _mediator.Send(new DeleteVisitorCommand(ids), ct);

    public async Task<Result<int>> ApproveVisitorAsync(string outcome, int[] visitorIds, string? comment = null, CancellationToken ct = default)
        => await _mediator.Send(new ApprovalVisitorsCommand(outcome, visitorIds, comment), ct);

    public async Task<Result<int>> CheckVisitorAsync(string outcome, int[] visitorIds, string? comment = null, CancellationToken ct = default)
        => await _mediator.Send(new CheckingVisitorsCommand(outcome, visitorIds, comment), ct);

    public async Task<Result<int>> ConfirmVisitorAsync(int[] ids, CancellationToken ct = default)
        => await _mediator.Send(new ConfirmVisitorCommand(ids), ct);

    public async Task<Result> CompleteVisitorInfoAsync(VisitorDto dto, CancellationToken ct = default)
    {
        var command = new CompleteVisitorInfoCommand();
        // Map from dto
        command.Id = dto.Id;
        command.PassCode = dto.PassCode;
        command.Name = dto.Name;
        command.IdentificationNo = dto.IdentificationNo;
        command.CompanyName = dto.CompanyName;
        command.Address = dto.Address;
        command.Avatar = dto.Avatar;
        command.Email = dto.Email;
        command.DesignationId = dto.DesignationId;
        command.EmployeeId = dto.EmployeeId;
        command.Gender = dto.Gender;
        command.LicensePlateNumber = dto.LicensePlateNumber;
        command.PhoneNumber = dto.PhoneNumber;
        command.Purpose = dto.Purpose;
        command.Apppoved = dto.Apppoved;
        command.ApprovalOutcome = dto.ApprovalOutcome;
        command.CheckinDate = dto.CheckinDate;
        command.CheckoutDate = dto.CheckoutDate;
        command.Comment = dto.Comment;
        command.Designation = dto.Designation;
        command.Companions = dto.Companions;
        command.Employee = dto.Employee;
        command.EmployeeDesignation = dto.EmployeeDesignation;
        command.ExpectedDate = dto.ExpectedDate;
        command.ExpectedTime = dto.ExpectedTime;
        command.HealthCode = dto.HealthCode;
        command.QrCode = dto.QrCode;
        command.NucleicAcidTestReport = dto.NucleicAcidTestReport;
        command.SiteId = dto.SiteId;
        command.Status = dto.Status;
        command.TripCode = dto.TripCode;
        return await _mediator.Send(command, ct);
    }

    public async Task<Result<int>> CreateVisitorHistoryAsync(CreateVisitorHistoryCommand command, CancellationToken ct = default)
        => await _mediator.Send(command, ct);

    public async Task<Result> UpdateVisitorSurveyResponseAsync(int visitorId, int? responseValue, CancellationToken ct = default)
        => await _mediator.Send(new UpdateVisitorSurveyResponseCommand(visitorId, responseValue), ct);

    public async Task<Result<int>> SubmitVisitorRequestAsync(string passCode, string? name, string? phoneNumber, string? email, string? identificationNo, string? purpose, string? companyName, string? gender, string? licensePlateNumber, int? siteId, CancellationToken ct = default)
    {
        var command = new VisitorRequestCommand
        {
            PassCode = passCode,
            Name = name,
            PhoneNumber = phoneNumber,
            Email = email,
            IdentificationNo = identificationNo,
            Purpose = purpose,
            CompanyName = companyName,
            Gender = gender,
            LicensePlateNumber = licensePlateNumber,
            SiteId = siteId,
            PrivacyPolicy = true,
            Promise = true
        };
        return await _mediator.Send(command, ct);
    }

    public async Task<List<VisitorStatusSumarryDto>> GetKanbanDataAsync(CancellationToken ct = default)
        => await _mediator.Send(new GetKanbanDataQuery(), ct);

    public async Task<Tuple<int, int, int>> GetDashboardDataAsync(CancellationToken ct = default)
        => await _mediator.Send(new GetDashboardDataQuery(), ct);

    public async Task<List<VisitorCountedMonth>> GetCountedMonthlyDataAsync(CancellationToken ct = default)
        => await _mediator.Send(new GetVisitorCountedMonthlyDataQuery(), ct) ?? new();

    public async Task<Dictionary<string, int>> GetCountedPurposeDataAsync(CancellationToken ct = default)
        => await _mediator.Send(new GetVisitorCountedPurposeDataQuery(), ct) ?? new();
}
