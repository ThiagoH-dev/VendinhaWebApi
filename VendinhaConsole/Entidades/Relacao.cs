namespace VendinhaConsole.Entidades
{
    public class Relacao
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public Divida Divida { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
    }
}
