using MudBlazor;

namespace Blazor.Server.UI;

public class Theme
{
    public static MudTheme ApplicationTheme()
    {
       var theme = new MudTheme()
        {

            PaletteLight = new PaletteLight
            {
                Primary = "#283593",
                Black = "#0A0E19",
                Success = "#64A70B",
                Secondary = "#ff4081ff",
                AppbarBackground = "rgba(255,255,255,0.8)",
                AppbarText = "#424242",
                TextSecondary = "#425466",
                Dark = "#110E2D",
                DarkLighten = "#1A1643",
                GrayDefault = "#4B5563",
                GrayLight = "#9CA3AF",
                GrayLighter = "#adbdccff"
            },
           PaletteDark = new PaletteDark
           {
               Primary = "#7e6fff",
               Dark = "#343a40",
               Secondary = "#adb5bd",
               SecondaryDarken = "rgba(245,110,80,1.0)",
               PrimaryContrastText = "#c3cbe4",
               Info = "#47bce8",
               Error = "#f56e50",
               Success = "#2cb57e",
               Warning = "#f5bd58",
               InfoContrastText = "#f6f6f6",
               Black = "#27272f",
               Background = "#0e1824",

               Surface = "#121e2d",
               DrawerBackground = "#121e2d",
               DrawerText = "#8fa6bf",
               DrawerIcon = "rgba(255,255,255, 0.50)",
               AppbarBackground = "rgba(14,24,36, 0.80)",
               AppbarText = "rgba(255,255,255, 0.70)",
               TextPrimary = "#a6b0cf",
               TextSecondary = "#9599ad",
               ActionDefault = "rgba(195,203,228,.80)",
               ActionDisabled = "rgba(255,255,255, 0.26)",
               ActionDisabledBackground = "rgba(255,255,255, 0.12)",
               DarkDarken = "rgba(21,27,34,0.7)",
               Divider = "#192a3f",
               DividerLight = "rgba(255,255,255, 0.06)",
               TableLines = "#192a3f",
               LinesDefault = "rgba(255,255,255, 0.12)",
               LinesInputs = "rgba(255,255,255, 0.3)",
               TextDisabled = "rgba(255,255,255, 0.2)",

           },
           LayoutProperties = new LayoutProperties
            {
                AppbarHeight = "80px",
                DefaultBorderRadius = "6px",
            }
        };
        return theme;
    }
}
