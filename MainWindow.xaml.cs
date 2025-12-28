using EtiquetasLogistica.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EtiquetasLogistica
{
    public partial class MainWindow : Window
    {
        private int totalVolumes = 1;
        private int volumeAtual = 1;
        private DatabaseService _databaseService;

        public MainWindow()
        {
            InitializeComponent();

            _databaseService = new DatabaseService();

            TxtDestinatario.TextChanged += AtualizarPreview_TextChanged;
            TxtNotaFiscal.TextChanged += AtualizarPreview_TextChanged;

            AtualizarPreview();
        }

        private void TxtVolumes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TxtVolumes.Text, out int total) && total > 0)
            {
                totalVolumes = total;
                volumeAtual = 1;

                LblVolumeAtualPreview.Text = volumeAtual.ToString();
                LblVolumePreview.Text = totalVolumes.ToString();

                AtualizarPreview();
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

        private void Historico_Click(object sender, RoutedEventArgs e)
        {
            var janela = new HistoricoWindow
            {
                Owner = this
            };
            janela.ShowDialog();
        }

        private async void Imprimir_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TxtDestinatario.Text) || string.IsNullOrWhiteSpace(TxtNotaFiscal.Text))
            {
                MessageBox.Show(
                    "Preencha o destinatário e a nota fiscal.",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            if (!int.TryParse(TxtVolumes.Text, out totalVolumes))
            {
                MessageBox.Show("Número de volumes inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); //Validar se a messageboxbutton é realmente necessária
                return;

            }

            for (int i = 1; i <= totalVolumes; i++)
            {
                volumeAtual = i;

                AtualizarPreview();
                await Task.Delay(500); // Lembrar de remover esse delay depois

                /* AQUI entra a impressão real depois
                 ImprimirEtiqueta(volumeAtual, totalVolumes); */
            }

            _databaseService.SalvarImpressao(
                TxtDestinatario.Text,
                TxtNotaFiscal.Text,
                totalVolumes
            );

            volumeAtual = 1;
            AtualizarPreview();

            MessageBox.Show("Impressão registrada no histórico");
        }

        private void ImprimirEtiqueta(int volume, int total)
        {
            /* Aqui futuramente: - gerar ZPL / EPL - ou enviar para GC420t */
        }
    }
}