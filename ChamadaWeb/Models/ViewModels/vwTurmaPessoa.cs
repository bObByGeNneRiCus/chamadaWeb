using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChamadaWeb.Models.ViewModels
{
    public class vwTurmaPessoa
    {
        public int Id { get; set; }
        public int IdTurma { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DataInicio { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DataFim { get; set; }
        public string NomeTurma { get; set; }
        public int IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string RG { get; set; }
        public Nullable<decimal> Pontuacao { get; set; }
        public bool IsProfessor { get; set; }
        public bool IsSuperintendente { get; set; }
    }
}