using EtiquetasLogistica.Data;
using System.Windows;

namespace EtiquetasLogistica
{
    public partial class HistoricoWindow : Window
    {
        public HistoricoWindow()
        {
            InitializeComponent();
            CarregarHistorico();
        }

        private void CarregarHistorico()
        {
            var db = new DatabaseService();
            GridHistorico.ItemsSource = db.ObterHistorico();
        }
    }
}

