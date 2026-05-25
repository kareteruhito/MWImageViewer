using System.Windows;
using System.Windows.Input;

namespace Maywork.WPF.Helpers;

public static class FullScreenHelper
{
    public static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
            "Enable",
            typeof(bool),
            typeof(FullScreenHelper),
            new PropertyMetadata(false, OnChanged));

    public static void SetEnable(Window element, bool value)
        => element.SetValue(EnableProperty, value);

    public static bool GetEnable(Window element)
        => (bool)element.GetValue(EnableProperty);

    private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window window) return;

        if ((bool)e.NewValue)
        {
            var command = new RelayCommand(() =>
            {
                Toggle(window);
            });

            window.InputBindings.Add(
                new KeyBinding(command, Key.F11, ModifierKeys.None)
            );
        }
    }

    private static void Toggle(Window window)
    {
        if (window.WindowStyle == WindowStyle.SingleBorderWindow)
        {
            window.WindowStyle = WindowStyle.None;
            window.WindowState = WindowState.Maximized;
        }
        else
        {
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.WindowState = WindowState.Normal;
        }
    }
}
/*
// 使い方
<Window
    ...
    xmlns:h="clr-namespace:Maywork.WPF.Helpers"
    h:FullScreenHelper.Enable="True">
</Window>
*/