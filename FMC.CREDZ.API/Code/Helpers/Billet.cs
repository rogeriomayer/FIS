namespace FMC.Billet
{
    public class Billet
    {
        public string LinhaDigitavel { get; set; }

        public string CodigoBarras { get; set; }

        public string NossoNumero { get; set; }

        public byte[] PDF { get; set; }

        public long IdBoleto { get; set; }

        public bool Registrado { get; set; }
    }
}
