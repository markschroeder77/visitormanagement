using CleanArchitecture.Blazor.Application.Features.Sites.DTOs;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blazor.Server.UI.Pages.Identity.Users;

public class SelectSiteAutocomplete : MudAutocomplete<SiteDto?>
{

    [Inject]
    private ISiteService SiteService { get; set; } = default!;
    [Parameter]
    public EventCallback<string> SiteChanged { get; set; }

    private List<SiteDto> _sites = new();

    // supply default parameters, but leave the possibility to override them
    public override Task SetParametersAsync(ParameterView parameters)
    {
        Dense = true;
        SearchFunc = Search;
        ToStringFunc = GetName;
        return base.SetParametersAsync(parameters);
    }

    // when the value parameter is set, we have to load that one brand to be able to show the name
    // we can't do that in OnInitialized because of a strange bug (https://github.com/MudBlazor/MudBlazor/issues/3818)
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _sites = (await SiteService.GetAllSitesAsync()).ToList();
            ForceRender(true);
        }
    }
    
    private Task<IEnumerable<SiteDto?>> Search(string? value, CancellationToken token)
    {
        List<SiteDto?> list = new();
        foreach(var item in _sites)
        {
            list.Add(item);
        }
        
        return Task.FromResult(list.AsEnumerable());
    }

    private string GetName(SiteDto? item)
    {
        return item?.Name ?? string.Empty;
    }
}