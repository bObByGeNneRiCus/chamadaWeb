//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChamadaWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChamadaPessoa
    {
        public int Id { get; set; }
        public int IdChamada { get; set; }
        public int IdPessoa { get; set; }
        public bool Presenca { get; set; }
        public decimal Pontuacao { get; set; }
        public System.DateTime DataAlteracao { get; set; }
    
        public virtual Chamada Chamada { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
