using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VendinhaConsole.Entidades
{
    public class Cliente
    {
        public Cliente()
        {
            Nome = "Cliente Sem Nome";
            CPF = "000.000.000-00";
        }
        public int Id { get; set; }
        [StringLength(40, MinimumLength = 5)]
        public string? Nome { get; set; }
        private string? cpf;
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string? CPF 
        {
            get => cpf;
            // Replace em tudo except 0 a 9.
            set => cpf = Regex.Replace(string.IsNullOrEmpty(value) ? "" : value, "[^0-9]", "");
        }
        public DateTime? DataNascimento { get; set; }

        private string? email;
        public string? Email
        {
            get => email;
            set => email = value?.ToLower();
        }
        public virtual string ToString()
        {
            return ($"{CPF}, {Nome}, {Email}, {DataNascimento}");
        }
    }
}
