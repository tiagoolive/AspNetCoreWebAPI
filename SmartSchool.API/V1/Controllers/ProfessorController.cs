using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.V1.Dtos;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Versão 1 do controlador de professor
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _repo.GetProfessoreById(id);
            if (prof == null) return BadRequest("Professor não encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(prof);

            return Ok(_mapper.Map<ProfessorDto>(professorDto));
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(ProfessorRegistrarDto model, int id)
        {
            var prof = _repo.GetProfessoreById(id);
            if (prof == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);

            _repo.Update(prof);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }
            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(ProfessorRegistrarDto model, int id)
        {
            var prof = _repo.GetProfessoreById(id);
            if (prof == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);

            _repo.Update(prof);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<Professor>(prof));
            }

            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessoreById(id);
            if (prof == null) return BadRequest("Professor não existente");

            _repo.Delete(prof);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }
            return BadRequest("Professor não deletado");
        }
    }
}
