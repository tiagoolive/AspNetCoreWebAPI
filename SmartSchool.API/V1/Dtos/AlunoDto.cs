using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Dtos
{
    public class AlunoDto
    {
        /// <summary>
        /// Identificador e chave do banco
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Chave do aluno, para outros negócios na Instituição
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        ///  Nome é o primeiro e segundo nome do Aluno
        /// </summary>
        public string Nome { get; set; }
        public string Telefone { get; set; }
        /// <summary>
        /// Esta idade é um calculo relacionado a data de nascimento do Aluno
        /// </summary>
        public int Idade { get; set; }
        public DateTime DataIni { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
