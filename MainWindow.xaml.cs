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
        private int totalVolumes = 1;
        private int volumeAtual = 1;

        public MainWindow()
        {
            InitializeComponent();
            TxtDestinatario.TextChanged += AtualizarPreview_TextChanged;
            TxtNotaFiscal.TextChanged += AtualizarPreview_TextChanged;
        }

        private void TxtVolumes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TxtVolumes.Text, out int total) && total > 0)
            {
                totalVolumes = total;
                volumeAtual = 1;

                LblVolumeAtualPreview.Text = volumeAtual.ToString();
                LblVolumePreview.Text = totalVolumes.ToString();
            }
            else
            {
                //Limpa o preview se estiver vazio 
                totalVolumes = 0;
                volumeAtual = 0;

                LblVolumeAtualPreview.Text = "";
                LblVolumePreview.Text = "";
            }
        }

        private void AtualizarPreview()
        {
            // Atualizar textos
            LblDestinatarioPreview.Text = TxtDestinatario.Text;
            LblNotaPreview.Text = TxtNotaFiscal.Text;
            LblVolumeAtualPreview.Text = volumeAtual.ToString();
            LblVolumePreview.Text = totalVolumes.ToString();

            // Ajustar fonte do destinatário automaticamente
            AjustarFonteDestinatario();
        }

        private void AtualizarPreview_TextChanged(object sender, TextChangedEventArgs e)
        {
            AtualizarPreview();
        }

        private void AjustarFonteDestinatario()
        {
            if (string.IsNullOrWhiteSpace(LblDestinatarioPreview.Text))
            {
                LblDestinatarioPreview.FontSize = 40;
                return;
            }

            double maxFontSize = 40;
            double minFontSize = 10;

            LblDestinatarioPreview.FontSize = maxFontSize;
            LblDestinatarioPreview.UpdateLayout();

            double maxWidth = LblDestinatarioPreview.ActualWidth;
            double maxHeight = 110;

            while ((LblDestinatarioPreview.ActualHeight > maxHeight || LblDestinatarioPreview.ActualWidth > maxWidth) && LblDestinatarioPreview.FontSize > minFontSize)
            {
                LblDestinatarioPreview.FontSize -= 0.5;
                LblDestinatarioPreview.UpdateLayout();
            }
        }

        private async void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= totalVolumes; i++)
            {
                volumeAtual = i;

                AtualizarPreview();
                await Task.Delay(500);

                /* AQUI entra a impressão real depois
                 ImprimirEtiqueta(volumeAtual, totalVolumes); */
            }

            AtualizarPreview();
        }

        private void ImprimirEtiqueta(int volume, int total)
        {
            /* Aqui futuramente: - gerar ZPL / EPL - ou enviar para GC420t */            
        }
    }
}