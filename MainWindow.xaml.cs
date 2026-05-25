using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Maywork.WPF.Helpers;
using Microsoft.Win32;

namespace MWImageViewer;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Ctrl+Oのショートカットを登録
        this.InputBindings.Add(
            new KeyBinding(
                new RelayCommand(() => OpenImage()),   // 実行処理
                new KeyGesture(Key.O, ModifierKeys.Control) // Ctrl+O
            )
        );
        // Ctrl+Cのショートカット登録
        this.InputBindings.Add(
            new KeyBinding(
                new RelayCommand(() => CopyToClip()),   // 実行処理
                new KeyGesture(Key.C, ModifierKeys.Control) // Ctrl+C
            )
        );

        // Ctrl+Vのショートカット登録
        this.InputBindings.Add(
            new KeyBinding(
                new RelayCommand(() => PasteFromClip()),   // 実行処理
                new KeyGesture(Key.V, ModifierKeys.Control) // Ctrl+V
            )
        );

        // ファイルドロップ
        FileDropHelper.SetFileDrop(DropBase, files =>
        {
            foreach (var file in files)
            {
                if (!ImageHelper.IsSupportedImage(file)) continue;


                LoadImage(file);
            }
        });
    }
    // ファイルダイアログを開いて画像を読み込み、Imageに表示する
    void OpenImage()
    {
        // ファイル選択ダイアログ
        var dialog = new OpenFileDialog
        {
            Title = "画像を選択",
            Filter = "画像ファイル|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.webp|すべてのファイル|*.*"
        };

        // キャンセル時は何もしない
        if (dialog.ShowDialog() == false) return;

        // 選択されたファイルパス
        var file = dialog.FileName;

        LoadImage(file);

    }
    // 画像ファイルをロードし表示
    void LoadImage(string file)
    {
        // -----------------------------
        // 画像読み込み
        // -----------------------------
        var bmp = ImageHelper.Load(file);
        if (bmp is null) return;

        // -----------------------------
        // DPI変換
        // -----------------------------
        var converted = ImageHelper.To96Dpi(bmp);

        Image1.Source = converted;
    }

    // クリップボードへコピー
    void CopyToClip()
    {
        var bmp = Image1.Source as BitmapSource;
        if (bmp is null) return;

        ClipboardImageHelper.SetImage(bmp);
    }
    // クリップボードから貼り付け
    void PasteFromClip()
    {
        var bmp = ClipboardImageHelper.GetImage();
        if (bmp is null) return;

        Image1.Source = bmp;
    }
}