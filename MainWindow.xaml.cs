using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EtiquetasLogistica
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TxtDestinatario.TextChanged += AtualizarPreview;
            TxtNotaFiscal.TextChanged += AtualizarPreview;
            TxtVolumes.TextChanged += AtualizarPreview;
            LblNotaPreview.Text = $"NF: {TxtNotaFiscal.Text}";
            LblVolumePreview.Text = $"VOLUME: {TxtVolumes.Text}";

        }

        private void AtualizarPreview(object sender, TextChangedEventArgs e)
        {
            // Correção: Usando .Text para TextBlock
            LblDestinatarioPreview.Text = TxtDestinatario.Text;
            LblNotaPreview.Text = $"NF: {TxtNotaFiscal.Text}";
            LblVolumePreview.Text = $"VOLUME: {TxtVolumes.Text}";
        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Impressão iniciada");
        }
    }
}
