using Microsoft.Win32;
using QRCoder;
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
            Lbl_version.Text = "Versión" + Properties.Settings.Default.version;
        }

        private void GenerateCodeQr(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = Cursors.Wait;
                string data = Txt_data.Text;
                string pathsave = "qrcode.jpg";
                using QRCodeGenerator qrGenerator = new();
                using QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                using QRCode qrCode = new(qrCodeData);
                using Bitmap qrBitmap = qrCode.GetGraphic(20);
                
                if(Check_saveqr.IsChecked == true)
                {
                    SaveFileDialog saveFileDialog1 = new()
                    {
                        Filter = "JPeg Image|*.jpg",
                        Title = "Guardar Qrcode"
                    };
                    saveFileDialog1.ShowDialog();
                    pathsave = saveFileDialog1.FileName;
                    qrBitmap.Save(pathsave, ImageFormat.Png);

                }
                else
                {
                    qrBitmap.Save(pathsave, ImageFormat.Png);

                }
                MessageBox.Show($"Código QR Generado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Process.Start(new ProcessStartInfo(pathsave) { UseShellExecute = true });
                Txt_data.Clear();
                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al generar el código QR {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Check_saveqr.IsChecked = false;
        }
    }
}