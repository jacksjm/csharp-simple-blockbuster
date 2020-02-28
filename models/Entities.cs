using System;
using System.Collections.Generic;

/*
    The namespace Entitites
    Contains all entities for basic use
*/
namespace Entities {
    /*
        The Cliente class
        Contains information about the clientes
    */
    class Cliente {
        /* 
            Getters and Setters 
        */
        /// <value>Get and Set the value of idCliente</value>
        public int idCliente { get; set; }
        /// <value>Get and Set the value of nome</value>
        public string nome { get; set; }
        /// <value>Get and Set the value of dtNasc</value>
        public DateTime dtNasc { get; set; }
        /// <value>Get and Set the value of cpf</value>
        public string cpf { get; set; }
        /// <value>Get and Set the value of dias</value>
        public int dias { get; set; }
        /// <value>Get and Set the value of locacoes</value>
        public List<Locacao> locacoes { get; set; }

        /// <summary>Constructor to Cliente object.</summary>
        public Cliente (int idCliente, string nome, DateTime dtNasc, string cpf, int dias) {
            this.idCliente = idCliente;
            this.nome = nome;
            this.dtNasc = dtNasc;
            this.cpf = cpf;
            this.dias = dias;
            locacoes = new List<Locacao> ();
        }

        /// <summary>This method insert a new movie rental for the customer.</summary>
        /// <param name="locacao">The rental object.</param>
        public void inserirLocacao (Locacao locacao) {
            locacoes.Add (locacao);
        }

        /// <summary>This method get the movies quantitie.</summary>
        /// <returns>Number of films rented by the customer.</returns>
        public int getQtdFilmes () {
            int qtdFilmes = 0;

            locacoes.ForEach (
                locacao => qtdFilmes += locacao.filmes.Count
            );

            return qtdFilmes;
        }

        /// <sumary>This method determines the string convertion.</sumary>
        public string ToString (bool simple = false) {
            if (simple) {
                string retorno = $"Id: {idCliente} - Nome: {nome}\n" +
                    "   Locações: \n";
                if (locacoes.Count > 0) {
                    locacoes.ForEach (
                        locacao => retorno += $"    Id: {locacao.idLocacao} - " +
                        $"Data: {locacao.dtLocacao} - " +
                        $"Data de Devolução: {locacao.getDataDevolucao()}\n"
                    );
                } else {
                    retorno += "    Não há locações";
                }

                return retorno;
            }

            string dtNasc = this.dtNasc.ToString("dd/MM/yyyy");

            return $"Nome: {nome}\n" +
                $"Data de Nasciment: {dtNasc}\n" +
                $"Qtd de Filmes: {getQtdFilmes()}";
        }
    }
    /*
        The Filme class
        Contains information about the movies
    */
    class Filme {
        /* 
            Getters and Setters 
        */
        /// <value>Get and Set the value of idFilme</value>
        public int idFilme { get; set; }
        /// <value>Get and Set the value of nomeFilme</value>
        public string nomeFilme { get; set; }
        /// <value>Get and Set the value of dtLancamento</value>
        public DateTime dtLancamento { get; set; }
        /// <value>Get and Set the value of sinopse</value>
        public string sinopse { get; set; }
        /// <value>Get and Set the value of valor</value>
        public double valor { get; set; }
        /// <value>Get and Set the value of qtdEstoque</value>
        public int qtdEstoque { get; set; }
        /// <value>Get and Set the value of locacoes</value>
        public List<Locacao> locacoes { get; set; }

        /// <summary>Constructor to Filme object.</summary>
        public Filme (int idFilme, string nomeFilme, DateTime dtLancamento, string sinopse, double valor, int qtdEstoque) {
            this.idFilme = idFilme;
            this.nomeFilme = nomeFilme;
            this.dtLancamento = dtLancamento;
            this.sinopse = sinopse;
            this.valor = valor;
            this.qtdEstoque = qtdEstoque;
            locacoes = new List<Locacao> ();
        }

        /// <summary>This method insert a movie into a customer rental.</summary>
        /// <param name="filme">The rental object.</param>
        public void setarLocacao (Locacao locacao) {
            locacoes.Add (locacao);
        }

        /// <summary>This method get the movie rental quantity.</summary>
        public int getQtdLocacoes () {
            return locacoes.Count;
        }

        /// <sumary>This method determines the string convertion.</sumary>
        public string ToString (bool simple = false) {
            if (simple) {
                return $"Id: {idFilme} - Nome: {nomeFilme}";
            }

            string valor = this.valor.ToString("C2");

            return $"Nome: {nomeFilme}\n" +
                $"Valor: {valor}\n" +
                $"Qtd de Locacoes: {getQtdLocacoes()}";
        }
    }

    /*
        The Locacao class
        Contains information about the movie locations
    */
    class Locacao {
        /* 
            Getters and Setters 
        */
        /// <value>Get and Set the value of idLocacao</value>
        public int idLocacao { get; set; }
        /// <value>Get and Set the value of cliente</value>
        public Cliente cliente { get; set; }
        /// <value>Get and Set the value of dtLocacao</value>
        public DateTime dtLocacao { get; set; }
        /// <value>Get and Set the value of idCliente</value>
        public List<Filme> filmes { get; set; }

        /// <summary>
        /// Constructor to Locacao object.
        /// </summary>
        /// <param name="idLocacao">Unique rental identification</param>
        /// <param name="cliente">Customer object</param>
        /// <param name="dtLocacao">Rental date</param>
        public Locacao (int idLocacao, Cliente cliente, DateTime dtLocacao) {
            this.idLocacao = idLocacao;
            this.cliente = cliente;
            this.dtLocacao = dtLocacao;
            filmes = new List<Filme> ();
            cliente.locacoes.Add (this);
        }

        /// <summary>
        /// This method insert a movie into a customer rental.
        /// </summary>
        /// <param name="filme">The movie object.</param>
        public void inserirFilme (Filme filme) {
            filmes.Add (filme);
            filme.setarLocacao (this);
        }

        /// <summary>
        /// This method get the total value of the rental
        /// </summary>
        /// <returns>The value of the rental.</returns>
        public double getValorTotal () {
            double valorTotal = 0;

            filmes.ForEach (
                filme => valorTotal += filme.valor
            );
            return valorTotal;
        }

        /// <summary>
        /// This method get the number of films
        /// </summary>
        /// <returns>The number of films</returns>
        public double getQtdFilmes () {
            return filmes.Count;
        }

        /// <summary>
        /// This method calculates the return date
        /// </summary>
        /// <returns>The customer's return date</returns>
        public DateTime getDataDevolucao () {
            return dtLocacao.AddDays (cliente.dias);
        }

        /// <sumary>This method determines the string convertion.</sumary>
        public override string ToString () {
            string valor = getValorTotal().ToString("C2");
            string retorno = $"Cliente: {cliente.nome}\n" +
                $"Data da Locacao: {dtLocacao}\n" +
                $"Valor: {valor}\n" +
                $"Data de Devolucao: {getDataDevolucao()}\n" +
                "   Filmes:\n";

            if (filmes.Count > 0) {
                filmes.ForEach (
                    filme => retorno += $"    Id: {filme.idFilme} - " +
                    $"Nome: {filme.nomeFilme}\n"
                );
            } else {
                retorno += "    Não há filmes";
            }

            return retorno;
        }
    }
}
