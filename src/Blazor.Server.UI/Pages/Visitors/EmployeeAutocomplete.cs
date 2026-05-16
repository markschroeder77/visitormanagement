using CleanArchitecture.Blazor.Application.Features.Employees.DTOs;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Blazor.Server.UI.Pages.Visitors;

public class EmployeeAutocomplete : MudAutocomplete<int?>
{
 
    [Inject]
    private IEmployeeService EmployeeService { get; set; } = default!;


    private List<EmployeeDto> _employees = new();

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
            _employees = (await EmployeeService.GetAllEmployeesAsync()).ToList();
            ForceRender(true);
        }
    }

    private  Task<IEnumerable<int?>> Search(string? value, CancellationToken token)
    {
        var list = new List<int?>();
        if (string.IsNullOrEmpty(value))
        {
            var result = _employees.Select(x => x.Id);
            foreach (var i in result)
            {
                list.Add(i);
            }
        }
        else
        {
            var result = _employees.Where(x => x.Name.Contains(value ?? string.Empty)).Select(x => x.Id);
            foreach (var i in result)
            {
                list.Add(i);
            }
            
        }
        return Task.FromResult(list.AsEnumerable());
    }

    private string GetName(int? id) {
        var emp = _employees.Find(b => b.Id == id);
        if(emp is null)
        {
            return String.Empty;
        }
        else
        {
            return $"{emp.Name} ({emp.Designation})";
        }
    }
}