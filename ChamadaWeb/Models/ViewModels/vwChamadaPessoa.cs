using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChamadaWeb.Models.ViewModels
{
    public class vwChamadaPessoa
    {
        public int Id { get; set; }
        public int IdChamada { get; set; }
        public int IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public bool Presenca { get; set; }
        public decimal Pontuacao { get; set; }
        public System.DateTime DataAlteracao { get; set; }

        public virtual Chamada Chamada { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}