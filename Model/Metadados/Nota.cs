using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Metadados
{
    public class Nota : Entity
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataEntrada { get; set; }
        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }
    }
}
