using System.IO;
using System.Windows;

using Maywork.WPF.Helpers;

namespace MWImageViewer;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ExceptionHandlerHelper.LogAction = (category, ex) =>
        {
            // 好きなログ処理へ差し替え可能
            File.AppendAllText(
                "error.log",
                $"[{DateTime.Now}] [{category}] {ex}\n");
        };

        ExceptionHandlerHelper.HandleAndContinue = false;

        ExceptionHandlerHelper.RegisterGlobalHandlers(this);
    }    
}

