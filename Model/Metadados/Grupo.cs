using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Metadados
{
    public class Grupo : Entity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal DepreciacaoMensal { get; set; }
        public int VidaUtil { get; set; }
    }
}
