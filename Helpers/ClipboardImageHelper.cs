using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Maywork.WPF.Helpers;

public static class ClipboardImageHelper
{
    public static void SetImage(BitmapSource bmp)
    {
        if (bmp == null) return;

        var ms = new MemoryStream();
        var enc = new PngBitmapEncoder();
        enc.Frames.Add(BitmapFrame.Create(bmp));
        enc.Save(ms);
        ms.Position = 0;

        var data = new DataObject();
        data.SetData("PNG", ms);
        data.SetData(DataFormats.Bitmap, bmp);

        Clipboard.SetDataObject(data, true);
    }

    public static BitmapSource? GetImage()
    {
        if (Clipboard.ContainsData("PNG"))
        {
            if (Clipboard.GetData("PNG") is MemoryStream stream)
            {
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = stream;
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();
                bmp.Freeze();
                return bmp;
            }
        }

        if (Clipboard.ContainsImage())
        {
            var bmp = Clipboard.GetImage();
            bmp?.Freeze();
            return bmp;
        }

        return null;
    }
}
/*
// 使い方

// Clip->BitmapSource
var bmp = ClipboardImageHelper.GetImage();

// BitmapSource->Clip
ClipboardImageHelper.SetImage(bmp);

*/