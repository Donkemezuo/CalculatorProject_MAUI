using System;
using System.Collections.Generic;
using Calculator.Resources.Themes;
using Calculator;

namespace Calculator;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private void ChangeTheme(ResourceDictionary theme)
    {
        ICollection<ResourceDictionary> resourcesDictionary = Application.Current.Resources.MergedDictionaries;
        if (resourcesDictionary != null)
        {
            // Clear existing resources
            resourcesDictionary.Clear();

            // Set the specified theme
            resourcesDictionary.Add(theme);

            // Dismiss the flyout menu
            Shell.Current.FlyoutIsPresented = false;
        }
    }

    void DarkModeSelected(object sender, EventArgs e)
    {
        ChangeTheme(new DarkBGColorTheme());
    }

    void LightModeSelected(object sender, EventArgs e)
    {
        ChangeTheme(new LightBGColorTheme());
    }

    void PinkModeSelected(object sender, EventArgs e)
    {
        ChangeTheme(new PinkBGColorTheme());
    }

    void RedModeSelected(object sender, EventArgs e)
    {
        ChangeTheme(new RedBGColorTheme());
    }
}

