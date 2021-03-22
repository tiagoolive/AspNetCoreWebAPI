using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Dtos
{
    public class AlunoPatchDto
    {
        /// <summary>
        /// Identificador e chave do banco
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Nome é o primeiro e segundo nome do Aluno
        /// </summary>
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}
