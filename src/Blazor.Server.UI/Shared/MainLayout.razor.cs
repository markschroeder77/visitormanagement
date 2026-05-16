using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazor.Server.UI.Components.Shared;
using Blazor.Server.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;
using CleanArchitecture.Blazor.Application.Common.Models;
using CleanArchitecture.Blazor.Infrastructure.Services.Authentication;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Blazor.Infrastructure.Hubs;
using CleanArchitecture.Blazor.Infrastructure.Extensions;
using Blazor.Server.UI.Services;
using Microsoft.JSInterop;
using Toolbelt.Blazor.HotKeys2;

namespace Blazor.Server.UI.Shared;

public partial class MainLayout: IAsyncDisposable
{
    private bool _commandPaletteOpen;
    private HotKeysContext? _hotKeysContext;
    private bool _sideMenuDrawerOpen = true;
    private UserPreferences UserPreferences = new();
    [Inject] private LayoutService LayoutService { get; set; } = default!;
    private MudThemeProvider _mudThemeProvider=default!;
    private bool _themingDrawerOpen;
    [Inject] private IDialogService _dialogService { get; set; } = default!;
    [Inject] private HotKeys _hotKeys { get; set; } = default!;
    [CascadingParameter]
    protected Task<AuthenticationState> _authState { get; set; } = default!;
    [Inject]
    private ProfileService _profileService { get; set; } = default!;
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;
    public async ValueTask DisposeAsync()
    {
        _profileService.OnChange -= _profileService_OnChange;
        LayoutService.MajorUpdateOccured -= LayoutServiceOnMajorUpdateOccured;
        _authenticationStateProvider.AuthenticationStateChanged -= _authenticationStateProvider_AuthenticationStateChanged;
        if (_hotKeysContext is not null)
        {
            try
            {
                await _hotKeysContext.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // The circuit is already disconnected, JS interop is unavailable.
                // This is expected during disposal and can be safely ignored.
            }
        }
        GC.SuppressFinalize(this);
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await ApplyUserPreferences();
            StateHasChanged();
        }
       
    }
    private async Task ApplyUserPreferences()
    {
        var defaultDarkMode = LayoutService.IsDarkMode;
        UserPreferences= await LayoutService.ApplyUserPreferences(defaultDarkMode);
    }
    protected override async Task OnInitializedAsync()
    {
        LayoutService.SetBaseTheme(Theme.ApplicationTheme());
        LayoutService.MajorUpdateOccured += LayoutServiceOnMajorUpdateOccured;
        _profileService.OnChange += _profileService_OnChange;
        _hotKeysContext = _hotKeys.CreateContext()
            .Add(ModCode.Ctrl, Code.K, OpenCommandPalette, "Open command palette.");
        _authenticationStateProvider.AuthenticationStateChanged += _authenticationStateProvider_AuthenticationStateChanged;
        var state = await _authState;
        if (state.User.Identity != null && state.User.Identity.IsAuthenticated)
        {
              await _profileService.Set(state.User);
        }
       await base.OnInitializedAsync();

    }

    private void _profileService_OnChange()
    {
        InvokeAsync(() => StateHasChanged());
    }

    private void LayoutServiceOnMajorUpdateOccured(object? sender, EventArgs e) => StateHasChanged();
    private void _authenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> authenticationState)
    {
        InvokeAsync(async () =>
        {
            var state = await authenticationState;
            if (state.User.Identity != null && state.User.Identity.IsAuthenticated)
            {
               await _profileService.Set(state.User);
            }
        });
    }


    protected void SideMenuDrawerOpenChangedHandler(bool state)
    {
        _sideMenuDrawerOpen = state;
    }
    protected void ThemingDrawerOpenChangedHandler(bool state)
    {
        _themingDrawerOpen = state;
    }
    protected void ToggleSideMenuDrawer()
    {
        _sideMenuDrawerOpen = !_sideMenuDrawerOpen;
    }
    private async Task OpenCommandPalette()
    {
        if (!_commandPaletteOpen)
        {
            var options = new DialogOptions
            {
                NoHeader = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            var commandPalette = await _dialogService.ShowAsync<CommandPalette>("", options);
            _commandPaletteOpen = true;

            await commandPalette.Result;
            _commandPaletteOpen = false;
        }
    }

    
}