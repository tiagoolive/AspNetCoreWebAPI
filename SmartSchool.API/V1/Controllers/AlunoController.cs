using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using SmartSchool.API.V1.Dtos;
using SmartSchool.WebAPI.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Versão 1 do controlador de alunos
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Método responsavel para retornar todos os alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams ,true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunosResult));
        }

        /// <summary>
        /// Método responsável por retornar apenas um único alunoDTO.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByDisciplina/{id}")]
        public async Task<IActionResult> GetDisciplinaId(int id)
        {
            var result = await _repo.GetAllAlunosByDisciplinaIdAsync(id, false);
            return Ok(result);
        }

        /// <summary>
        /// Método responsável por retornar apenas um único alunoDTO.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }

        /// <summary>
        /// Método responstável por retornar apenas um Aluno por meio do código Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("aluno não encontrado");

            var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);

            return Ok(alunoDto);
        }

        /// <summary>
        /// Método responsável por criar um novo aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoPatchDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoPatchDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}/trocarEstado")]
        public IActionResult trocarEstado(int id, TrocaEstadoDto trocaEstado)
        {
            var aluno = _repo.GetAlunoById(id);           
            if (aluno == null) return BadRequest("aluno não encontrado");

            aluno.Ativo = trocaEstado.Estado;

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                var msn = aluno.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Aluno {msn} com sucesso!" });
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("aluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest("Aluno não deletado");
        }
    }
}
