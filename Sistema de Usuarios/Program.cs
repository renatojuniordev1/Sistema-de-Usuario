using Sistema_de_Usuario.Models;
using Sistema_de_Usuario.Services;
using System.Runtime.CompilerServices;



class Program
{


    static UsuarioService service = new UsuarioService();
    static void Main()
    {
        service.Carregar();
        Program.Menu();
    }

    public static void Menu()
    {
        int op;
        do
        {


            Console.WriteLine("=====MENU=====");
            Console.WriteLine("1 - Cadastro");
            Console.WriteLine("2 - Buscar");
            Console.WriteLine("3 - Lista");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolher: ");
            if (!int.TryParse(Console.ReadLine(), out op))
            {
                Console.WriteLine("Digite um numero indicados");
                continue;
            }



            switch (op)
            {
                case 1:
                    Cadastro();
                    break;
                case 2:
                    BuscaporId();
                    break;
                case 3:
                    Lista();
                    break;
                case 4: break;
            }
        }
        while (op != 4);
    }

    public static void Cadastro()
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Digite numeros");
            return;
        }





        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Ñão pode deixar em branco");
            return;
        }


        Console.Write("Email: ");
        string email = Console.ReadLine();


        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Ñão pode deixar em branco");
            return;
        }

        Usuario usuario = new Usuario()
        {
            Id = id,
            Nome = nome,
            Email = email
        };
        try
        {
            service.Adicionar(usuario);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Cadastro concluido com sucesso!");
    }

    public static void BuscaporId()
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Digite numeros");
            return;
        }
        var usuario = service.BuscarPorId(id);
        if (usuario == null)
        {
            Console.WriteLine("Usuario não encontrado");

        }
        else
        {

            Console.WriteLine($"ID: {usuario.Id} | Nome: {usuario.Nome} | Email: {usuario.Email}");
        }
    }

    public static void Lista()
    {
        var lista = service.List();
        if (lista.Count == 0)
        {
            Console.WriteLine("Lista vazia");
        }
        foreach (var item in lista)
        {
            Console.WriteLine($"ID: {item.Id} | Nome: {item.Nome} | Email: {item.Email}");
        }
    }
}
