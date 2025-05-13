using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QRCoder;
using static System.Net.Mime.MediaTypeNames;
namespace QrcodeGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateCodeQr(object sender, RoutedEventArgs e)
        {
            string data = Txt_data.Text;
            using QRCodeGenerator qrGenerator = new ();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            using QRCode qrCode = new (qrCodeData);
            using Bitmap qrBitmap = qrCode.GetGraphic(20);

            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //string filePath = System.IO.Path.Combine(desktopPath, "qrcode.png");
            qrBitmap.Save("qrcode.png", ImageFormat.Png);
            MessageBox.Show($"Código QR Generado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            System.Diagnostics.Process.Start(new ProcessStartInfo("qrcode.png") { UseShellExecute = true });

        }
    }
}