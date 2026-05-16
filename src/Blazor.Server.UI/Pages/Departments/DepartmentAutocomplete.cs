using CleanArchitecture.Blazor.Application.Features.Departments.DTOs;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Blazor.Server.UI.Pages.Departments;

public class DepartmentAutocomplete : MudAutocomplete<int?>
{

    [Inject]
    private IDepartmentService DepartmentService { get; set; } = default!;


    private List<DepartmentDto> _departments = new();

    // supply default parameters, but leave the possibility to override them
    public override Task SetParametersAsync(ParameterView parameters)
    {


        Dense = true;
        ResetValueOnEmptyText = true;
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
            _departments = (await DepartmentService.GetAllDepartmentsAsync()).ToList();
            ForceRender(true);
        }
    }

    private Task<IEnumerable<int?>> Search(string? value, CancellationToken token)
    {
        var list = new List<int?>();
        if (string.IsNullOrEmpty(value))
        {
            return Task.FromResult(_departments.Select(x =>new int?(x.Id)).AsEnumerable());
        }
        else
        {
            var result = _departments.Where(x => x.Name!.ToLower().Contains((value ?? string.Empty).ToLower())).Select(x =>new int?(x.Id)).AsEnumerable();
            return Task.FromResult(result);
        }
        
    }

    private string GetName(int? id) => _departments.Find(b => b.Id == id)?.Name ?? string.Empty;
}