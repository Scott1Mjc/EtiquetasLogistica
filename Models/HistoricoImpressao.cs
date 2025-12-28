namespace EtiquetasLogistica.Models
{
    public class HistoricoImpressao
    {
        public int Id { get; set; }
        public string Destinatario { get; set; }
        public string NotaFiscal { get; set; }
        public int TotalVolumes { get; set; }
        public string DataImpressao { get; set; }
    }
}
