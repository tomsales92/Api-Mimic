using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Helpers;
using MimicAPI.Models;
using MimicAPI.Models.DTO;
using MimicAPI.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly IPalavraRepository _repository;
        private readonly IMapper _mapper;

        public PalavrasController(IPalavraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //APP
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas([FromQuery]PalavraUrlQuery query)
        {
            var item = _repository.ObterPalavras(query);

            if (query.PagNumero > item.Paginacao.TotalPaginas)
            {
                return NotFound();
            }
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Paginacao));

            return Ok(item.ToList());
        }

        //Web
   
        [HttpGet("{id}", Name = "Obterpalavra")]
        public ActionResult Obter(int id)
        {
           var obj = _repository.obter(id);

            if (obj == null)
                return NotFound();

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(obj);

            palavraDTO.Links = new List<LinkDTO>();
      

            palavraDTO.Links.Add(
                new LinkDTO("self", Url.Link("ObterPalavra", new {id = palavraDTO.Id }), "GET")
                );

            palavraDTO.Links.Add(
                new LinkDTO("update", Url.Link("Atualizar", new { id = palavraDTO.Id }), "PUT")
                );

            palavraDTO.Links.Add(
                new LinkDTO("delete", Url.Link("Deletar", new { id = palavraDTO.Id }), "DELETE")
                );

            return Ok(palavraDTO);
        }

        [Route("")]

        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _repository.Cadastrar(palavra);
            return Created($"/api/palavras/{palavra.Id}", palavra);
        }

       
        [HttpPut("{id}", Name ="Atualizar")]
        public ActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {

            var obj = _repository.obter(id);
            if (obj == null)
            {
                return NotFound();
            }

            palavra.Id = id;
            _repository.Atualizar(palavra);
            return Ok();
        }

        [HttpGet("{id}", Name = "Deletar")]
        public ActionResult Deletar(int id)
        {

            var palavra = _repository.obter(id);

            if (palavra == null)
                return NotFound();
            _repository.Deletar(id);

            return NoContent();
        }


    }
}
