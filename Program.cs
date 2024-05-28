using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace Test
{
    class Program
    {
        public static ContaBancaria? ContaLogada = null;
        static void Main(string[] args)
        {
            Console.Clear();
            Menu.Principal();
        }
    }

    class Menu
    {
        public static void Principal()
        {
            char opc;
            bool wrongDigit = false;

            while (true)
            {
                Console.WriteLine("<--- Banco Maluco Nacional --->");
                Console.WriteLine("<--- Menu Incial --->");
                Console.WriteLine("1 - Cadastrar Conta");
                Console.WriteLine("2 - Entrar em uma Conta");
                Console.WriteLine("3 - Sair da Aplicação");

                if(wrongDigit)
                    Console.WriteLine("\nEntre com uma opção válida!");

                Console.Write("\nSua opção: ");
                opc = Convert.ToChar(Console.ReadLine());


                switch (opc)
                {
                    case '1':
                        Console.Clear();
                        Cadastrar();
                        break;
                    case '2':
                        Console.Clear();
                        Login();
                        if(Program.ContaLogada != null)
                            Logado();
                        break;
                    case '3':
                        Console.WriteLine("Encerrando Aplicação!");
                        Environment.Exit(0);
                        break;
                    default:
                        wrongDigit = true;
                        Console.Clear();
                        break;
                }
            }
        }

        private static void Logado()
        {
            char opc;
            string opc2;
            double amount;
            bool keep = true, wrongDigit = false;

            while (keep)
            {
                Console.Clear();
                Console.WriteLine($"Bem-Vindo(a) {Program.ContaLogada.GetTitular()}!\n");
                Console.WriteLine("<--- Banco Maluco Nacional --->");
                Console.WriteLine("<--- Opções da Conta --->");
                Console.WriteLine("1 - Checar Informações");
                Console.WriteLine("2 - Realizar um Depósito");
                Console.WriteLine("3 - Realizar um Saque");
                Console.WriteLine("4 - Realizar uma Transferência");
                Console.WriteLine("5 - Sair da Conta");
                Console.WriteLine("6 - Sair da Aplicação");

                if(wrongDigit)
                    Console.WriteLine("\nEntre com uma opção válida!");

                Console.Write("\nSua opção: ");
                opc = Convert.ToChar(Console.ReadLine());


                switch (opc)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Informações do Titular:\n");
                        Console.WriteLine(Program.ContaLogada.ToString());
                        Console.WriteLine("\nPressione 'Enter' para continuar");
                        Console.ReadKey();
                        break;
                    case '2':
                        Console.Clear();
                        Console.Write("Entre com o valor do Depósito: R$");
                        amount = Convert.ToDouble(Console.ReadLine());
                        Program.ContaLogada.Depositar(amount);
                        Console.WriteLine("Depósito Bem-Sucedido!");
                        Console.WriteLine(Program.ContaLogada.GetSaldo());
                        Console.ReadKey();
                        break;
                    case '3':
                        bool be = true;
                        while(be)
                        {
                            Console.Clear();
                            Console.Write("Entre com o valor do Saque: R$");
                            amount = Convert.ToDouble(Console.ReadLine());

                            if(!Program.ContaLogada.Sacar(amount))
                            {
                                Console.WriteLine("Valor do saque acima do saldo na conta.");
                                Console.WriteLine("Tentar Novamente? S/N: ");

                                bool stay = true;
                                while(stay)
                                {
                                    opc2 = Console.ReadLine();

                                    switch (opc2.ToUpper())
                                    {
                                        case "S":
                                            Console.Clear();
                                            stay = false;
                                            break;
                                        case "N":
                                            Console.Clear();
                                            stay = false;
                                            be = false;
                                            break;
                                        default:
                                            Console.WriteLine("Opção Inválida! Tente novamente.");
                                            break;
                                    }
                                }
                            }else
                                break;
                        }

                        if(!be)
                            break;

                        Console.WriteLine("Saque Bem-Sucedido!");
                        Console.WriteLine(Program.ContaLogada.GetSaldo());
                        Console.ReadKey();
                        break;
                    case '4':
                        Console.Clear();
                        break;
                    case '5':
                        Console.Clear();
                        Console.WriteLine("Fazendo Logout!");
                        keep = false;
                        break;
                    case '6':
                        Console.WriteLine("Encerrando Aplicação!");
                        Environment.Exit(0);
                        break;
                    default:
                        wrongDigit = true;
                        Console.Clear();
                        break;
                }
            }
        }

        private static void Cadastrar()
        {
            string nomeTitular, senhaTitular, opc;
            double quantia = 0;

            Console.WriteLine("<--- Banco Maluco Nacional --->");
            Console.WriteLine("<--- Cadastro --->");
            
            Console.Write("1 - Entre com o nome do Titular: ");
            nomeTitular = Console.ReadLine();

            Console.Write("2 - Entre com uma senha para a conta\nAtenção! Sua senha estará visível: ");
            senhaTitular = Console.ReadLine();

            bool keep = true;
            while(keep)
            {
                Console.Write("3 - Deseja adicionar uma quantia inicial? S/N: ");
                opc = Console.ReadLine();

                switch (opc.ToUpper())
                {
                    case "S":
                        Console.Write("4 - Entre com uma quantidade em R$: ");
                        quantia = Convert.ToDouble(Console.ReadLine());
                        keep = false;
                        break;
                    case "N":
                        keep = false;
                        break;
                    default:
                        Console.WriteLine("Opção Inválida! Tente novamente.\n");
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine("Conta Criada com Sucesso!\n");
            ContaBancaria.CriaConta(nomeTitular, senhaTitular, quantia);

            Console.WriteLine("\nVocê será redirecionado para a tela inicial!");
            Console.ReadKey();
            Console.Clear();
        }

        private static void Login()
        {
            string? senha, nConta, opc;
            bool keep = true;

            while (keep)
            {
                Console.WriteLine("<--- Banco Maluco Nacional --->");
                Console.WriteLine("<--- Login --->");

                Console.Write("Entre com o número da sua Conta: ");
                nConta = Console.ReadLine();

                Console.Write("Entre com a senha da sua Conta: ");
                senha = Console.ReadLine();

                if(!ContaBancaria.LoginConta(nConta, senha))
                {
                    Console.WriteLine("Credênciais Inválidas!");
                    Console.Write("Deseja tentar novamente? S/N: ");

                    bool stay = true;
                    while(stay)
                    {
                        opc = Console.ReadLine();

                        switch (opc.ToUpper())
                        {
                            case "S":
                                Console.Clear();
                                stay = false;
                                break;
                            case "N":
                                Console.Clear();
                                stay = false;
                                keep = false;
                                break;
                            default:
                                Console.WriteLine("Opção Inválida! Tente novamente.");
                                break;
                        }
                    }
                } else {
                    Console.Clear();
                    break;
                }
            }
        }
    }

    class ContaBancaria
    {
        private static readonly List<ContaBancaria> Contas = new List<ContaBancaria>();
        private static int NumConta = 01;
        private readonly string NomeTitular, SenhaTitular, NumeroConta;
        private double QuantiaConta;

        private ContaBancaria(string nome, string senha, double amount, string nConta)
        {
            this.NomeTitular = nome;
            this.SenhaTitular = senha;
            this.QuantiaConta = amount;
            this.NumeroConta = nConta;
        }

        public string GetTitular()
        {
            return this.NomeTitular;
        }

        public string GetSaldo()
        {
            return $"Saldo atual: R${this.QuantiaConta:f2}";
        }

        public void Depositar(double amount)
        {
            this.QuantiaConta += amount;
        }

        public bool Sacar(double amount)
        {
            if(this.QuantiaConta < amount)
                return false;

            this.QuantiaConta -= amount;
            return true;
        }

        public void Transferir(double amount, string nConta)
        {
            ContaBancaria paraConta = GetConta(nConta);

            if(paraConta == null)
            {
                Console.WriteLine("Conta para transferência não existe!");
                return;
            }

            if(!this.Sacar(amount))
            {
                Console.WriteLine("Valor do saque acima do saldo na conta.");
                return;
            }

            paraConta.Depositar(amount);
            Console.WriteLine("Transferência Concluída!");
        }

        public override string ToString()
        {
            string str = "";
            str += $"Titular da Conta: {this.NomeTitular}\n";
            str += $"Número da Conta: {this.NumeroConta}\n";
            str += $"Saldo da Conta: R${this.QuantiaConta}\n";
            return str;
        }

        public static void CriaConta(string nome, string senha, double amount)
        {
            string nCont = "ABC-" + NumConta.ToString();
            NumConta++;

            ContaBancaria cont = new ContaBancaria(nome, senha, amount, nCont);
            Contas.Add(cont);

            Console.WriteLine(cont.ToString());
        }

        public static bool LoginConta(string nConta, string senha)
        {
            foreach (ContaBancaria conta in Contas)
            {
                if (conta.NumeroConta == nConta && conta.SenhaTitular == senha){
                    Program.ContaLogada = conta;
                    return true;
                }
            }

            return false;
        }

        private static ContaBancaria GetConta(string nConta)
        {
            foreach (ContaBancaria conta in Contas)
            {
                if (conta.NumeroConta == nConta)
                    return conta;
            }

            return null;
        }
    }
}