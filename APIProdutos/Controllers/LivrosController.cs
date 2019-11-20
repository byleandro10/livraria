using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APILivraria.Business;
using APILivraria.Models;
using Microsoft.AspNetCore.Authorization;

namespace APILivraria.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class LivrosController : Controller
    {
        private LivroService _service;

        public LivrosController(LivroService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Livro> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("{codigoBarras}")]
        public IActionResult Get(string codigoBarras)
        {
            var livro = _service.Obter(codigoBarras);
            if (livro != null)
                return new ObjectResult(livro);
            else
                return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody]Livro livro)
        {
            return _service.Incluir(livro);
        }

        [HttpPut]
        public Resultado Put([FromBody]Livro livro)
        {
            return _service.Atualizar(livro);
        }

        [HttpDelete("{codigoBarras}")]
        public Resultado Delete(string codigoBarras)
        {
            return _service.Excluir(codigoBarras);
        }
    }
}