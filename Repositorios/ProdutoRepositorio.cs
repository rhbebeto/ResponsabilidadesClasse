using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ResponsabilidadesClasse.Models;

namespace ResponsabilidadesClasse.Repositorios
{
    internal class ProdutoRepositorio
    {
        private readonly string _caminhoBase = "C:\\Projetos\\Databases\\produto.csv";
        private List<Produto> ListagemProdutos = new List<Produto>();
        public ProdutoRepositorio()
        {
            if (!File.Exists(_caminhoBase))
            {
                var file = File.Create(_caminhoBase);
                file.Close();
            }
        }

        public List<Produto> Listar()
        {
            CarregarProdutos();
            return ListagemProdutos;
        }

        public int ProximoID()
        {
            CarregarProdutos();

            if (ListagemProdutos.Count == 0)
                return 1;

            return ListagemProdutos.Max(x => x.IdProduto + 1);
        }
        public void Inserir(Produto produto)
        {
            var identificador = ProximoID();
            var sw = new StreamWriter(_caminhoBase);
            sw.WriteLine(GerarLinhaProduto(identificador, produto));
            sw.Close();
        }

        public bool SeExiste(int id)
        {
            CarregarProdutos();
            return ListagemProdutos.Any(x => x.IdProduto == id);
        }

        public void Atualizar(Produto produto)
        {
            CarregarProdutos();
            var posicao = ListagemProdutos.FindIndex(x => x.IdProduto == produto.IdProduto);
            ListagemProdutos[posicao] = produto;
            RegravarProdutos(ListagemProdutos);
        }

        public void Remover(int IdProduto)
        {
            var posicao = ListagemProdutos.FindIndex(x => x.IdProduto == IdProduto);


        }
        
        #region Métodos privados 
        private Produto LinhaTextoParaProduto(string linha)
        {
            var colunas = linha.Split(';');
            var produto = new Produto();
            produto.IdProduto = int.Parse(colunas[0]);
            produto.NomeProduto = colunas[1];
            produto.Valor = decimal.Parse(colunas[2]);
            produto.Situacao = true;
            return produto; 
        }

        private void CarregarProdutos()
        {
            var sr = new StreamReader(_caminhoBase);
            ListagemProdutos.Clear();
            while (true)
            {
                var linha = sr.ReadLine();
                if (linha == null)
                    break;
                
                ListagemProdutos.Add(LinhaTextoParaProduto(linha));
            }

            sr.Close();
        }

        private void RegravarProdutos(List<Produto> produtos)
        {
            var sw = new StreamWriter(_caminhoBase);
            foreach(var produto  in produtos.OrderBy(x => x.IdProduto))
            {
                sw.WriteLine(GerarLinhaProduto(produto.IdProduto, produto));
            }
            sw.Close();
            
        }
        private string GerarLinhaProduto(int identificador, Produto produto)
        {
            return $"{identificador};{produto.NomeProduto};{produto.Valor};{produto.Situacao}";
        }
        #endregion

    }
}
