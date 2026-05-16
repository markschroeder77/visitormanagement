// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Visitors.DTOs;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Pagination;
using CleanArchitecture.Blazor.Application.Features.Visitors.Constant;
using CleanArchitecture.Blazor.Application.Features.Visitors.Queries.Reports;
using CleanArchitecture.Blazor.Application.Features.VisitorHistories.Commands.Create;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Services;

public interface IVisitorService
{
    Task<VisitorDto?> SearchVisitorAsync(string keyword, CancellationToken cancellationToken = default);
    Task<List<VisitorDto>> SearchVisitorFuzzyAsync(string keyword, CancellationToken cancellationToken = default);
    Task<List<VisitorDto>> SearchPendingApprovalVisitorsAsync(string? keyword, CancellationToken cancellationToken = default);
    Task<List<VisitorDto>> SearchPendingCheckingVisitorsAsync(string? keyword, CancellationToken cancellationToken = default);
    Task<List<VisitorDto>> SearchPendingConfirmVisitorsAsync(string? keyword, CancellationToken cancellationToken = default);
    Task<List<VisitorDto>> SearchPendingCheckinVisitorsAsync(string? keyword, CancellationToken cancellationToken = default);
    Task<PaginatedData<VisitorDto>> GetVisitorsWithPaginationAsync(VisitorsWithPaginationQuery query, CancellationToken cancellationToken = default);
    Task<VisitorDto?> GetVisitorByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<VisitorDto>> GetAllVisitorsAsync(CancellationToken cancellationToken = default);
    Task<List<VisitorDto>> GetRelatedVisitorsAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> DeleteVisitorAsync(int[] ids, CancellationToken cancellationToken = default);
    Task<Result<int>> CreateVisitorAsync(string passCode, int? id, string? name, string? phoneNumber, string? email, string? identificationNo, string? purpose, int? siteId, CancellationToken cancellationToken = default);
    Task<Result<int>> UpdateVisitorAsync(VisitorDto dto, CancellationToken cancellationToken = default);
    Task<Result<int>> ApproveVisitorAsync(string outcome, int[] visitorIds, string? comment = null, CancellationToken cancellationToken = default);
    Task<Result<int>> CheckVisitorAsync(string outcome, int[] visitorIds, string? comment = null, CancellationToken cancellationToken = default);
    Task<Result<int>> ConfirmVisitorAsync(int[] ids, CancellationToken cancellationToken = default);
    Task<Result> CompleteVisitorInfoAsync(VisitorDto dto, CancellationToken cancellationToken = default);
    Task<Result<int>> CreateVisitorHistoryAsync(CreateVisitorHistoryCommand command, CancellationToken cancellationToken = default);
    Task<Result> UpdateVisitorSurveyResponseAsync(int visitorId, int? responseValue, CancellationToken cancellationToken = default);
    Task<Result<int>> SubmitVisitorRequestAsync(string passCode, string? name, string? phoneNumber, string? email, string? identificationNo, string? purpose, string? companyName, string? gender, string? licensePlateNumber, int? siteId, CancellationToken cancellationToken = default);
    Task<List<VisitorStatusSumarryDto>> GetKanbanDataAsync(CancellationToken cancellationToken = default);
    Task<Tuple<int, int, int>> GetDashboardDataAsync(CancellationToken cancellationToken = default);
    Task<List<VisitorCountedMonth>> GetCountedMonthlyDataAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, int>> GetCountedPurposeDataAsync(CancellationToken cancellationToken = default);
}