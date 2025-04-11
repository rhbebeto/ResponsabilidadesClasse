using System;
using System.Collections.Generic;
using System.Linq;
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
            sw.WriteLine($"{identificador};{produto.NomeProduto};{produto.Valor};{produto.Situacao}");
            sw.Close();
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
        
    }
}
#endregion 