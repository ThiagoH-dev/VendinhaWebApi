// See https://aka.ms/new-console-template for more information

using NHibernate.Tool.hbm2ddl;
using System.ComponentModel;
using VendinhaConsole;
using VendinhaConsole.Entidades;
using VendinhaConsole.Services;

// Recriação/Adaptação do input() do python porque deu vontade.
static T Input<T>(string output) where T : IParsable<T>
{
    try
    {
        Console.WriteLine(output);
        string? input = Console.ReadLine();

        if (input == null)
            throw new Exception("Answer is null.");

        T data = T.Parse(input, null);
        return data;
    }
    catch(Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);

        if (Input<string>("Digite 1 para tentar novamente:") == "1")
            return Input<T>(output);

        return default(T);
    }

}

GenerateSchema_Fixture.Can_generate_schema();
SchemaExport 

Console.WriteLine("Vamos criar um Cliente.");

//var a1 = new Aluno
//{
//    Nome = "Rodrigo",
//    DataNascimento = new DateTime(2000, 1, 1),
//    Codigo = 1001
//};

ClienteService.CriarCliente(new Cliente()
{
    CPF = Input<string>("Digite o cpf:"),
    Nome = Input<string?>("Digite um nome:"),
    DataNascimento = Input<DateTime>("Digite uma data:"),
    Email = Input<string?>("Digite um email:")
}, out var erros);
if (erros != null)
    Console.WriteLine(erros.ToString());

Console.WriteLine(ClienteService.String());
