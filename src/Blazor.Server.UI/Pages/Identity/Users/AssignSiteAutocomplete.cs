using CleanArchitecture.Blazor.Application.Features.Sites.DTOs;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blazor.Server.UI.Pages.Identity.Users;

public class AssignSiteAutocomplete : MudAutocomplete<string?>
{

    [Inject]
    private ISiteService SiteService { get; set; } = default!;


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

    private Task<IEnumerable<string?>> Search(string? value, CancellationToken token)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Task.FromResult(_sites.Select(x => x.Name).ToList().AsEnumerable());
        }
        else
        {
            var result = _sites.Where(x => x.Name!.StartsWith(value ?? string.Empty)).Select(x => x.Name).ToList();
            return Task.FromResult(result.AsEnumerable());
        }
    }

    private string GetName(string? txt)
    {
        if (string.IsNullOrEmpty(txt))
        {
            return String.Empty;
        }
        else
        {
            return _sites.Find(b => b.Name == txt)?.Name ?? string.Empty;
        }
    }
}