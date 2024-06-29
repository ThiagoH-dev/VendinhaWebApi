using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendinhaConsole.Entidades
{
    public class Divida
    {
        public Divida() 
        {
            Valor = 0.0;
            DataCriacao = DateTime.Now;
            Pendente = true;
        }
        public int Id { get; set; }
        public double Valor { get; set; }
        public string? Descricao { get; set; }
        public bool Pendente { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
