﻿using Microsoft.AspNetCore.Mvc;
using MimicAPI.Database;
using MimicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly MimicContext _banco;

        public PalavrasController(MimicContext banco)
        {
            _banco = banco;
        }

        //APP
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas()
        {
            return Ok(_banco.Palavras);
        }

        //Web
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            return Ok(_banco.Palavras.Find(id));
        }

        [Route("")]

        [HttpPost]
        public ActionResult Cadastrar(Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, Palavra palavra)
        {
            palavra.Id = id;
            _banco.Palavras.Update(palavra);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            _banco.Palavras.Remove(_banco.Palavras.Find(id));
            return Ok();
        }


    }
}
