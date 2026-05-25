using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Maywork.WPF.Helpers;

public static class SaveImageShortcut
{
    // Imageターゲット
    public static readonly DependencyProperty TargetProperty =
        DependencyProperty.RegisterAttached(
            "Target",
            typeof(Image),
            typeof(SaveImageShortcut),
            new PropertyMetadata(null, OnChanged));

    public static void SetTarget(DependencyObject obj, Image value)
        => obj.SetValue(TargetProperty, value);

    public static Image GetTarget(DependencyObject obj)
        => (Image)obj.GetValue(TargetProperty);

    // 保存パス保持（内部状態）
    static readonly DependencyProperty LastFilePathProperty =
        DependencyProperty.RegisterAttached(
            "LastFilePath",
            typeof(string),
            typeof(SaveImageShortcut),
            new PropertyMetadata(null));

    static void SetLastFilePath(DependencyObject obj, string value)
        => obj.SetValue(LastFilePathProperty, value);

    static string GetLastFilePath(DependencyObject obj)
        => (string)obj.GetValue(LastFilePathProperty);

    static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window window) return;
        if (e.NewValue is not Image image) return;

        // Ctrl+S（上書き or SaveAs）
        window.InputBindings.Add(
            new KeyBinding(
                new RelayCommand(() => Save(window, image, false)),
                new KeyGesture(Key.S, ModifierKeys.Control)
            )
        );

        // Ctrl+Shift+S（常にSaveAs）
        window.InputBindings.Add(
            new KeyBinding(
                new RelayCommand(() => Save(window, image, true)),
                new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
            )
        );
    }

    static void Save(Window window, Image image, bool forceSaveAs)
    {
        if (image.Source is not BitmapSource bmp) return;

        var path = GetLastFilePath(window);

        // SaveAsが必要な条件
        if (forceSaveAs || string.IsNullOrEmpty(path))
        {
            var dialog = new SaveFileDialog
            {
                Title = "画像を保存",
                Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp"
            };

            if (dialog.ShowDialog() != true) return;

            path = dialog.FileName;
            SetLastFilePath(window, path);
        }

        ImageHelper.SavePng(bmp, path);
    }
}

/*
// 使い方
要:
RelayCommand.cs
ImageHelper.cs

XAML
<Window 省略
        xmlns:h="clr-namespace:Maywork.WPF.Helpers"
        h:OpenImageShortcut.Target="{Binding ElementName=MyImage}"
        h:SaveImageShortcut.Target="{Binding ElementName=MyImage}" />
    <Grid>
        <ScrollViewer>
            <Canvas>
                <Image x:Name="MyImage" />
*/