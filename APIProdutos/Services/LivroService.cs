using System;
using System.Collections.Generic;
using System.Linq;
using APILivraria.Data;
using APILivraria.Models;

namespace APILivraria.Business
{
    public class LivroService
    {
        private ApplicationDbContext _context;

        public LivroService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Livro Obter(string codigoBarras)
        {
            codigoBarras = codigoBarras?.Trim().ToUpper();
            if (!String.IsNullOrWhiteSpace(codigoBarras))
            {
                return _context.Livros.Where(
                    p => p.CodigoBarras == codigoBarras).FirstOrDefault();
            }
            else
                return null;
        }

        public IEnumerable<Livro> ListarTodos()
        {
            return _context.Livros
                .OrderBy(p => p.Titulo).ToList();
        }

        public Resultado Incluir(Livro dadoslivro)
        {
            Resultado resultado = DadosValidos(dadoslivro);
            resultado.Acao = "Inclusão de Livro";

            if (resultado.Inconsistencias.Count == 0 &&
                _context.Livros.Where(
                p => p.CodigoBarras == dadoslivro.CodigoBarras).Count() > 0)
            {
                resultado.Inconsistencias.Add(
                    "Código de Barras já cadastrado");
            }

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.Livros.Add(dadoslivro);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(Livro dadosLivro)
        {
            Resultado resultado = DadosValidos(dadosLivro);
            resultado.Acao = "Atualização de Livro";

            if (resultado.Inconsistencias.Count == 0)
            {
                Livro livro = _context.Livros.Where(
                    p => p.CodigoBarras == dadosLivro.CodigoBarras).FirstOrDefault();

                if (livro == null)
                {
                    resultado.Inconsistencias.Add(
                        "Livro não encontrado");
                }
                else
                {
                    livro.Titulo = dadosLivro.Titulo;
                    livro.Preco = dadosLivro.Preco;
                    livro.Editora = dadosLivro.Editora;
                    livro.Autor = dadosLivro.Autor;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(string codigoBarras)
        {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de Livro";

            Livro livro = Obter(codigoBarras);
            if (livro == null)
            {
                resultado.Inconsistencias.Add(
                    "Livro não encontrado");
            }
            else
            {
                _context.Livros.Remove(livro);
                _context.SaveChanges();
            }
                
            return resultado;
        }

        private Resultado DadosValidos(Livro livro)
        {
            var resultado = new Resultado();
            if (livro == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do Livro");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(livro.CodigoBarras))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o Código de Barras");
                }
                if (String.IsNullOrWhiteSpace(livro.Titulo))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o Titulo do Livro");
                }
                if (livro.Preco <= 0)
                {
                    resultado.Inconsistencias.Add(
                        "O Preço do Livro deve ser maior do que zero");
                }
            }

            return resultado;
        }
    }
}