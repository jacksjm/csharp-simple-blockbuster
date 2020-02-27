using System;
using Entities;
using System.Collections.Generic;

/*
    The namespace Principal
    Contains the functions of the system
*/
namespace Principal
{   
    /*
        The Executavel class
        Contains information about the execution
    */
    class Executavel
    {
        // Collections that serve as a database
        static List<Cliente> clientes = new List<Cliente>();
        static List<Filme> filmes = new List<Filme>();
        static List<Locacao> locacoes = new List<Locacao>();

         /*
            The Main class
            The entrance of the system
        */
        static void Main(string[] args)
        {           
            Console.WriteLine("============ Blockbuster! ============ ");
            int opt = 0;
            do {
                // Show menu options
                Console.WriteLine("+-------------------------+");
                Console.WriteLine("| Digite a opção desejada |");
                Console.WriteLine("| 1 - Cadastrar Cliente   |");
                Console.WriteLine("| 2 - Cadastrar Filme     |");
                Console.WriteLine("| 3 - Cadastrar Locação   |");
                Console.WriteLine("| 4 - Listar Clientes     |");
                Console.WriteLine("| 5 - Consultar Cliente   |");
                Console.WriteLine("| 6 - Listar Filmes       |");
                Console.WriteLine("| 7 - Consultar Filme     |");
                Console.WriteLine("| 8 - Consultar Locação   |");
                Console.WriteLine("| 9 - Importar Dados      |");
                Console.WriteLine("| 0 - Sair                |");
                Console.WriteLine("+-------------------------+");

                try{
                    opt = Convert.ToInt32(Console.ReadLine());
                } catch {
                    Console.WriteLine("Opção inválida");
                    opt = 99;
                }
                
                // Checks the selected option
                switch(opt){
                    case 1:
                        inserirCliente();
                    break;
                    case 2:
                        inserirFilme();
                    break;
                    case 3:
                        inserirLocacao();
                    break;
                    case 4:
                        listarClientes();
                    break;
                    case 5:
                        consultarCliente();
                    break;
                    case 6:
                        listarFilmes();
                    break;
                    case 7:
                        consultarFilme();
                    break;
                    case 8:
                        consultarLocacao();
                    break;
                    case 9:
                        importarDados();
                    break;
                }
            } while(opt != 0);
            
        }

        /// <summary>
        /// This method is responsible for creating customers
        /// </summary>
        private static void inserirCliente(){
            Console.WriteLine("Informações sobre o cliente: ");
            Console.WriteLine("Informe o nome: ");
            String nome = Console.ReadLine();
            Console.WriteLine("Informe a data de nascimento (dd/mm/yyyy): ");
            String sDtNasc = Console.ReadLine();
            DateTime dtNasc;
            try{
                dtNasc = Convert.ToDateTime(sDtNasc);
            } catch {
                Console.WriteLine("Formato inválido de data, será utilizada a data atual pra cadastro");
                dtNasc = DateTime.Now;
            }
            Console.WriteLine("Informe o C.P.F.: ");
            String cpf = Console.ReadLine();
            Console.WriteLine("Informe a quantidade de dias para devolução: ");
            int qtdDias = Convert.ToInt32(Console.ReadLine());

            Cliente cliente = new Cliente(
                clientes.Count, 
                nome, 
                dtNasc, 
                cpf, 
                qtdDias
            );

            // Insert the costumer on "db"
            clientes.Add(cliente);
        }

        /// <summary>
        /// This method is responsible for creating the movies
        /// </summary>
        private static void inserirFilme(){
            Console.WriteLine("Informações sobre o filme: ");
            Console.WriteLine("Informe o nome: ");
            String nome = Console.ReadLine();
            Console.WriteLine("Informe a data de lançamento (dd/mm/yyyy): ");
            String sDtLancamento = Console.ReadLine();
            DateTime dtLancamento;
            try{
                dtLancamento = Convert.ToDateTime(sDtLancamento);
            } catch {
                Console.WriteLine("Formato inválido de data, será utilizada a data atual pra cadastro");
                dtLancamento = DateTime.Now;
            }
            Console.WriteLine("Informe a Sinopse: ");
            String cpf = Console.ReadLine();
            Console.WriteLine("Informe o valor para locação: ");
            double valor = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Informe a quantidade em estoque: ");
            int estoque = Convert.ToInt32(Console.ReadLine());


            Filme filme = new Filme(
                filmes.Count, 
                nome, 
                dtLancamento, 
                cpf, 
                valor,
                estoque
            );

            // Insert the movie on "db"
            filmes.Add(filme);
        }

        /// <summary>
        /// This method is responsible for creating the rents
        /// </summary>
        private static void inserirLocacao(){
            Console.WriteLine("Informações sobre a locação: ");
            Cliente cliente;
            Filme filme;

            // Search the costumer with id
            do{
                Console.WriteLine("Informe o id do cliente: ");
                int idCliente = Convert.ToInt32(Console.ReadLine());
                cliente = null; // Reset the value to avoid garbage

                // Try to locate the information in the collection
                try{
                    cliente = clientes.Find(cliente => cliente.idCliente == idCliente);
                    if(cliente == null){ // If the information is not present, a message is returned
                        Console.WriteLine("Cliente não localizado, favor digitar outro id.");
                    }
                } catch {
                    Console.WriteLine("Cliente não localizado, favor digitar outro id.");
                }
                
            }while(cliente == null);

            // Insert the rent to the costumer
            Locacao locacao = new Locacao(locacoes.Count, cliente, DateTime.Now);
            cliente.inserirLocacao(locacao);
            // Insert the rent on "db"
            locacoes.Add(locacao);

            // Search the movie with id
            int filmOpt = 0;
            do{
                Console.WriteLine("Informe o id do filme alugado: ");
                int idFilme = Convert.ToInt32(Console.ReadLine());
                filme = null; // Reset the value to avoid garbage

                // Try to locate the information in the collection
                try{
                    filme = filmes.Find(filme => filme.idFilme == idFilme);
                    if(filme == null){ // If the information is not present, a message is returned
                        Console.WriteLine("Filme não localizado, favor digitar outro id.");
                    }
                } catch {
                    Console.WriteLine("Filme não localizado, favor digitar outro id.");
                }

                if(filme != null){
                    // Insert the movie on the rent
                    locacao.inserirFilme(filme);
                    Console.WriteLine("Deseja informar outro filme? " + 
                        "Informar 1 para Não ou qualquer outro valor para Sim.");
                    filmOpt = Convert.ToInt32(Console.ReadLine());
                }
            }while(filmOpt != 1);
        }

        /// <summary>
        /// This method is responsible for listing customers
        /// </summary>
        private static void listarClientes(){
            Console.WriteLine("Lista de Clientes: ");
            clientes.ForEach(cliente => Console.WriteLine(cliente.ToString(true)));
        }

        /// <summary>
        /// This method is responsible for consulting a customer
        /// </summary>
        private static void consultarCliente(){
            Cliente cliente;

            // Search the costumer with id
            do{
                Console.WriteLine("Informe o cliente que deseja consultar: ");
                int idCliente = Convert.ToInt32(Console.ReadLine());
                cliente = null; // Reset the value to avoid garbage

                // Try to locate the information in the collection
                try{
                    cliente = clientes.Find(cliente => cliente.idCliente == idCliente);
                    if(cliente == null){ // If the information is not present, a message is returned
                        Console.WriteLine("Cliente não localizado, favor digitar outro id.");
                    }
                } catch {
                    Console.WriteLine("Cliente não localizado, favor digitar outro id.");
                }
            }while(cliente == null);
            Console.WriteLine(cliente.ToString());
        }

        /// <summary>
        /// This method is responsible for listing the movies
        /// </summary>
        private static void listarFilmes(){
            Console.WriteLine("Lista de Filmes: ");
            filmes.ForEach(filme => Console.WriteLine(filme.ToString(true)));
        }

        /// <summary>
        /// This method is responsible for consulting a movie
        /// </summary>
        private static void consultarFilme(){
            Filme filme;

            // Search the movie with id
            do{
                Console.WriteLine("Informe o filme que deseja consultar: ");
                int idFilme = Convert.ToInt32(Console.ReadLine());
                filme = null; // Reset the value to avoid garbage

                // Try to locate the information in the collection
                try{
                    filme = filmes.Find(filme => filme.idFilme == idFilme);
                    if(filme == null){ // If the information is not present, a message is returned
                        Console.WriteLine("Filme não localizado, favor digitar outro id.");
                    }
                } catch {
                    Console.WriteLine("Filme não localizado, favor digitar outro id.");
                }
            }while(filme == null);
            Console.WriteLine(filme.ToString());
        }

        /// <summary>
        /// This method is responsible for consulting a rent
        /// </summary>
        private static void consultarLocacao(){
            Locacao locacao;

            // Search the rent with id
            do{
                Console.WriteLine("Informe a locacao que deseja consultar: ");
                int idLocacao = Convert.ToInt32(Console.ReadLine());
                locacao = null; // Reset the value to avoid garbage

                // Try to locate the information in the collection
                try{
                    locacao = locacoes.Find(locacao => locacao.idLocacao == idLocacao);
                    if(locacao == null){ // If the information is not present, a message is returned
                        Console.WriteLine("Locação não localizada, favor digitar outro id.");
                    }
                } catch {
                    Console.WriteLine("Locação não localizada, favor digitar outro id.");
                }
            }while(locacao == null);
            Console.WriteLine(locacao.ToString());
        }

        /// <summary>
        /// This method is responsible for importing fictitious data
        /// </summary>
        private static void importarDados(){
            Cliente cliente;
            Filme filme;
            Locacao locacao;

            /* Generate movies*/
            filme = new Filme(
                filmes.Count,
                "Titanic",
                new DateTime(1997,1,1),
                "A bordo do luxuoso transatlântico, Rose, uma jovem da alta sociedade, se sente pressionada com a vida que leva. Ao conhecer Jack, um artista pobre e aventureiro, os dois se apaixonam. Mas eles terão que enfrentar um desafio ainda maior que o preconceito social com o destino trágico do navio.",
                10,
                2
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Pulp Fiction - Tempo De Violência",
                new DateTime(1994,1,1),
                "Os assassinos Vincent e Jules passam por imprevistos ao recuperar uma mala para um mafioso. O boxeador Butch é pago pelo mesmo mafioso para perder uma luta, e a esposa do criminoso fica sob responsabilidade de Vincent por uma noite. Essas histórias se relacionam numa teia repleta de violência.",
                15,
                1
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Rocky - Um Lutador",
                new DateTime(1976,1,1),
                "Rocky (Sylvester Stallone) é um lutador de boxe desconhecido que é desafiado pelo campeão dos pesos pesados, Apollo Creed (Carl Weathers). Rocky vê a luta como uma oportunidade e começa a treinar intensivamente para ser o vencedor. Vencedor do Oscar de Melhor Filme, Melhor Diretor e Melhor Edição.",
                25,
                1
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Vingadores: Guerra Infinita",
                new DateTime(2018,1,1),
                "O cruel Thanos pretende reunir todas as Jóias do Infinito em sua manopla para tornar-se o mais poderoso da galáxia e ser capaz de decidir o futuro da humanidade. Os Vingadores então se unem aos Guardiões da Galáxia e ao Pantera Negra na maior guerra de todos os tempos para impedir os planos do vilão.",
                30,
                3
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Bohemian Rhapsody",
                new DateTime(2018,1,1),
                "Juntos, Freddie Mercury (Rami Malek), Brian May (Gwilym Lee), Roger Taylor (Ben Hardy) e John Deacon (Joe Mazzello) começam a banda Queen, que revoluciona o cenário da música nos anos 70. Mercury é um cantor talentoso e de personalidade singular, mas os excessos começam a representar um problema para o futuro da banda. Baseado em fatos reais, o filme foi vencedor de quatro Oscars.",
                Convert.ToDouble("25,6"),
                2
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Azul É A Cor Mais Quente",
                new DateTime(2013,1,1),
                "A estudante Adèle (Adèle Exarchopoulos) vive em uma fase de autoconhecimento. Quando conhece Emma (Léa Seydoux), uma garota lésbica, ela se sente atraída e as duas começam a passar muito tempo juntas. Com isso, as colegas de Adèle a pressionam sobre sua sexualidade ao passo que o laço com Emma fica cada vez mais forte. Vencedor de 3 Palmas de Ouro no Festival de Cannes.",
                10,
                2
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "La La Land - Cantando Estações",
                new DateTime(2016,1,1),
                "Em Los Angeles, a aspirante a atriz Mia (Emma Stone) e o pianista de jazz Sebastian (Ryan Gosling) se apaixonam e juntos passam a perseguir seus sonhos e vontades. Conforme buscam o que desejam, os dois tentam fazer seu relacionamento dar certo. Vencedor de 6 Oscars.",
                Convert.ToDouble("15,5"),
                1
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Central Do Brasil",
                new DateTime(1998,1,1),
                "Dora (Fernanda Montenegro) escreve cartas para analfabetos na Central do Brasil. Uma de suas clientes tenta reaproximar o filho Josué (Vinícius de Oliveira) do pai, mas morre ao sair da estação. Dora então ajuda a criança encontrar o pai desaparecido.",
                20,
                1
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Clube Dos Cinco",
                new DateTime(1985,1,1),
                "Um cdf, um atleta, um caso perdido, uma princesa e um criminoso. Por pequenas infrações, os cinco adolescentes são obrigados a passar o sábado no colégio e escrever uma redação sobre o que pensam sobre si mesmos. Com o tempo eles colocam os rótulos de lado e começam a se abrir uns com os outros.",
                10,
                2
            );
            filmes.Add(filme);
            filme = new Filme(
                filmes.Count,
                "Kill Bill - Volume 1",
                new DateTime(2003,1,1),
                "A espadachim conhecida como \"A Noiva\" é uma das assassinas lideradas pelo misterioso Bill. Grávida, ela decide deixar o grupo, mas seus antigos companheiros se viram contra ela e a atacam durante o seu casamento. Após cinco anos em coma, ela parte em busca de vingança.",
                15,
                1
            );
            filmes.Add(filme);

            /* Generate costumers*/
            cliente = new Cliente(
                clientes.Count,
                "Gabriel João Caio dos Santos",
                new DateTime(1953,12,17),
                "800.404.403-46",
                10
            );
            locacao = new Locacao(
                locacoes.Count,
                cliente,
                DateTime.Now.AddDays(-5)
            );
            locacao.inserirFilme(filmes[1]);
            locacao.inserirFilme(filmes[3]);
            locacoes.Add(locacao);


            clientes.Add(cliente);
            cliente = new Cliente(
                clientes.Count,
                "Eduarda Isabela Raimunda Ramos",
                new DateTime(1978,11,17),
                "296.918.247-52",
                15
            );
            locacao = new Locacao(
                locacoes.Count,
                cliente,
                DateTime.Now.AddDays(-8)
            );
            locacao.inserirFilme(filmes[5]);
            locacao.inserirFilme(filmes[8]);
            locacoes.Add(locacao);


            clientes.Add(cliente);
            cliente = new Cliente(
                clientes.Count,
                "Stefany Joana Pereira",
                new DateTime(1995,12,8),
                "564.059.971-54",
                20
            );
            locacao = new Locacao(
                locacoes.Count,
                cliente,
                DateTime.Now.AddDays(-2)
            );
            locacao.inserirFilme(filmes[2]);
            locacoes.Add(locacao);

            
            clientes.Add(cliente);
            cliente = new Cliente(
                clientes.Count,
                "Amanda Carolina Giovana Araújo",
                new DateTime(1999,08,19),
                "628.602.153-10",
                5
            );
            locacao = new Locacao(
                locacoes.Count,
                cliente,
                DateTime.Now.AddDays(-10)
            );
            locacao.inserirFilme(filmes[4]);
            locacao.inserirFilme(filmes[9]);
            locacoes.Add(locacao);
            locacao = new Locacao(
                locacoes.Count,
                cliente,
                DateTime.Now
            );
            locacao.inserirFilme(filmes[1]);
            locacoes.Add(locacao);

            clientes.Add(cliente);
            cliente = new Cliente(
                clientes.Count,
                "Gabriel Juan Farias",
                new DateTime(1958,05,3),
                "647.340.889-42",
                10
            );
            clientes.Add(cliente);
            locacao = new Locacao(
                locacoes.Count,
                cliente,
                DateTime.Now
            );
            locacao.inserirFilme(filmes[6]);
            locacao.inserirFilme(filmes[7]);
            locacao.inserirFilme(filmes[8]);
            locacoes.Add(locacao);

            Console.WriteLine("======= IMPORTAÇÃO CONCLUÍDA COM SUCESSO =======");
        }
    }
}
