using System;
using System.Collections.Generic;
using System.Linq;
 
namespace SistemaQuadras
{
    class Program
    {
        static List<Quadra> quadras = new List<Quadra>();
        static List<Aluguel> alugueis = new List<Aluguel>();
        static int proximoId = 1;
        static int HORAS_MAXIMAS = 4;
 
        static void Main(string[] args)
        {
            int opcao = -1;
 
            while (opcao != 0)
            {
                Console.WriteLine();
                Console.WriteLine("--------------------------------");
                Console.WriteLine(" Sistema de Aluguel de Quadras");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("1 - Cadastrar quadra");
                Console.WriteLine("2 - Listar quadras");
                Console.WriteLine("3 - Buscar quadra");
                Console.WriteLine("4 - Alugar quadra");
                Console.WriteLine("5 - Listar alugueis");
                Console.WriteLine("6 - Encerrar aluguel (liberar quadra)");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opcao: ");
 
                string entrada = Console.ReadLine();
 
                if (!int.TryParse(entrada, out opcao))
                {
                    Console.WriteLine("Opcao invalida.");
                    continue;
                }
 
                switch (opcao)
                {
                    case 1:
                        CadastrarQuadra();
                        break;
                    case 2:
                        ListarQuadras();
                        break;
                    case 3:
                        BuscarQuadra();
                        break;
                    case 4:
                        AlugarQuadra();
                        break;
                    case 5:
                        ListarAlugueis();
                        break;
                    case 6:
                        EncerrarAluguel();
                        break;
                    case 0:
                        Console.WriteLine("Encerrando o sistema...");
                        break;
                    default:
                        Console.WriteLine("Opcao invalida.");
                        break;
                }
            }
        }
 
        static void CadastrarQuadra()
        {
            Console.WriteLine("--- Cadastro de quadra ---");
 
            Console.Write("Nome da quadra: ");
            string nome = Console.ReadLine();
 
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome nao pode ser vazio. Cadastro cancelado.");
                return;
            }
 
            Console.Write("Esporte praticado: ");
            string esporte = Console.ReadLine();
 
            Console.Write("Valor por hora: ");
            double valorHora;
            if (!double.TryParse(Console.ReadLine(), out valorHora) || valorHora < 0)
            {
                Console.WriteLine("Valor invalido. Cadastro cancelado.");
                return;
            }
 
            Quadra quadra = new Quadra(proximoId, nome, esporte, valorHora);
            proximoId++;
            quadras.Add(quadra);
 
            Console.WriteLine("Quadra cadastrada com sucesso! (Id: " + quadra.Id + ")");
        }
 
        static void ListarQuadras()
        {
            Console.WriteLine("--- Lista de quadras ---");
 
            if (quadras.Count == 0)
            {
                Console.WriteLine("Nenhuma quadra cadastrada.");
                return;
            }
 
            foreach (Quadra q in quadras)
            {
                string situacao = q.Disponivel ? "Disponivel" : "Alugada";
                Console.WriteLine("Id: " + q.Id + " | " + q.Nome + " | " + q.Esporte +
                    " | R$ " + q.ValorHora + "/h | " + situacao);
            }
        }
 
        static void BuscarQuadra()
        {
            Console.Write("Digite o nome (ou parte do nome) da quadra: ");
            string busca = Console.ReadLine();
 
            var encontradas = quadras.Where(q => q.Nome.ToLower().Contains(busca.ToLower())).ToList();
 
            if (encontradas.Count == 0)
            {
                Console.WriteLine("Nenhuma quadra encontrada com esse nome.");
                return;
            }
 
            foreach (Quadra q in encontradas)
            {
                string situacao = q.Disponivel ? "Disponivel" : "Alugada";
                Console.WriteLine("Id: " + q.Id + " | " + q.Nome + " | " + q.Esporte +
                    " | R$ " + q.ValorHora + "/h | " + situacao);
            }
        }
 
        static void AlugarQuadra()
        {
            Console.WriteLine("--- Alugar quadra ---");
            ListarQuadras();
 
            Console.Write("Digite o Id da quadra desejada: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id invalido.");
                return;
            }
 
            Quadra quadra = quadras.FirstOrDefault(q => q.Id == id);
 
            if (quadra == null)
            {
                Console.WriteLine("Quadra nao encontrada.");
                return;
            }
 
            if (!quadra.Disponivel)
            {
                Console.WriteLine("Essa quadra ja esta alugada no momento.");
                return;
            }
 
            Console.Write("Nome do cliente: ");
            string nomeCliente = Console.ReadLine();
 
            if (string.IsNullOrWhiteSpace(nomeCliente))
            {
                Console.WriteLine("Nome do cliente nao pode ser vazio. Aluguel cancelado.");
                return;
            }
 
            Console.Write("Telefone do cliente: ");
            string telefone = Console.ReadLine();
 
            Console.Write("Quantidade de horas (1 a " + HORAS_MAXIMAS + "): ");
            int horas;
            if (!int.TryParse(Console.ReadLine(), out horas) || horas < 1 || horas > HORAS_MAXIMAS)
            {
                Console.WriteLine("Quantidade de horas invalida. O maximo permitido e " +
                    HORAS_MAXIMAS + " horas.");
                return;
            }
 
            Cliente cliente = new Cliente(nomeCliente, telefone);
            Aluguel aluguel = new Aluguel(quadra, cliente, horas);
 
            quadra.Disponivel = false;
            alugueis.Add(aluguel);
 
            Console.WriteLine("Aluguel realizado com sucesso!");
            Console.WriteLine("Quadra: " + quadra.Nome + " | Cliente: " + cliente.Nome +
                " | Horas: " + horas + " | Valor total: R$ " + aluguel.ValorTotal);
        }
 
        static void ListarAlugueis()
        {
        }
 
        static void EncerrarAluguel()
        {
        }
    }
}