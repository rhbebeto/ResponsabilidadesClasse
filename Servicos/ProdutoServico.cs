using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponsabilidadesClasse.Models;

namespace ResponsabilidadesClasse.Servicos
{
    internal class ProdutoServico
    {
        private readonly Repositorios.ProdutoRepositorio _repositorio;
        public ProdutoServico()
        {
            _repositorio = new Repositorios.ProdutoRepositorio();
        }

        public void Cadastrar()
        {

            var produto = ColetarProduto();
            _repositorio.Inserir(new Models.Produto());

            Console.WriteLine($"Produto {produto.NomeProduto} cadastrado com sucesso!");
            Console.ReadKey();
        }
        public void Listar()
        {
            var produtos = _repositorio.Listar();
            Console.WriteLine("Deseja também listar algum produto inativo? \n1 - Sim\n2 - Não");
            if (Int32.Parse(Console.ReadLine()) == 2)
            {
                produtos = produtos.Where(x => x.Situacao == true).ToList();
            }

            Console.Clear();
            foreach (var p in produtos)
            {
                Console.WriteLine($"Identificador: {p.IdProduto};Nome: {p.NomeProduto}; Valor: {p.Valor}; Situação: {(p.Situacao ? "ativo" : "inativo")}");
            }

        }

        public void Atualizar()
        {
            Console.WriteLine("Qual id do produto que deja atuializar");
            int idInformado = Convert.ToInt32(Console.ReadLine());
            if (!_repositorio.SeExiste(idInformado))
            {
                Console.WriteLine("Esse produto não existe ");
                return ;
            }
            var produto = ColetarProduto();
            produto.IdProduto = idInformado;
            _repositorio.Atualizar(produto);
        }

        private Produto ColetarProduto()
        {

            Console.WriteLine("Qual nome do produto?");
            var produto = Console.ReadLine();
            Console.WriteLine($"Qual o valor do produto {produto}?");
            var valor = Convert.ToDecimal(Console.ReadLine());


            return new Produto()
            {
                NomeProduto = produto,
                Valor = valor,
                Situacao = true
            };


        }

    }
}