using CleanArchitecture.Blazor.Application.Features.CheckinPoints.DTOs;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Blazor.Server.UI.Pages.CheckinPoints;

public class CheckinPointAutocomplete : MudAutocomplete<int?>
{

    [Inject]
    private ICheckinPointService CheckinPointService { get; set; } = default!;


    private List<CheckinPointDto> _checkinpoints = new();

    // supply default parameters, but leave the possibility to override them
    public override Task SetParametersAsync(ParameterView parameters)
    {


        Dense = true;
        //ResetValueOnEmptyText = true;
        SearchFunc = Search;
        ToStringFunc = GetName;
        Clearable = true;
        return base.SetParametersAsync(parameters);
    }

    // when the value parameter is set, we have to load that one brand to be able to show the name
    // we can't do that in OnInitialized because of a strange bug (https://github.com/MudBlazor/MudBlazor/issues/3818)
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _checkinpoints = (await CheckinPointService.GetAllCheckinPointsAsync()).ToList();
            ForceRender(true);
        }
    }

    private Task<IEnumerable<int?>> Search(string? value, CancellationToken token)
    {
        var list = new List<int?>();
        if (string.IsNullOrEmpty(value))
        {
            var result = _checkinpoints.Select(x => new int?(x.Id)).AsEnumerable();
            return Task.FromResult(result);
        }
        else
        {
            var result = _checkinpoints.Where(x => x.Name.Contains(value ?? string.Empty)).Select(x => new int?(x.Id)).AsEnumerable();
            return Task.FromResult(result);
        }
       
    }

    private string GetName(int? id) {
        var chpoint = _checkinpoints.Find(b => b.Id == id);
        if (chpoint is null)
        {
            return String.Empty;
        }
        else
        {
            return $"{chpoint.Site} - {chpoint.Name}";
        }
    }
}