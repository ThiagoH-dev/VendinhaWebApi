using System.ComponentModel.DataAnnotations;
using VendinhaConsole.Entidades;
using NHibernate;

namespace VendinhaConsole.Services
{
    public class ClienteService
    {
        private readonly ISessionFactory session;

        public ClienteService(ISessionFactory session)
        {
            this.session = session;
        }
        public static string? String()
        {
            string data = "";
            foreach (Cliente cliente in Clientes)
            {
                data += cliente.ToString() + "\n";
            }
            return data;
        }

        public bool Criar(Cliente cliente, out List<ValidationResult> erros)
        {
            if (Validacao(cliente, out erros))
            {
                using var sessao = session.OpenSession();
                using var transaction = sessao.BeginTransaction();
                sessao.Save(cliente);
                transaction.Commit();
                return true;
            }
            return false;
        }

        public bool Editar(Cliente cliente, out List<ValidationResult> erros)
        {
            if (Validacao(cliente, out erros))
            {
                using var sessao = session.OpenSession();
                using var transaction = sessao.BeginTransaction();

                sessao.Merge(cliente);
                transaction.Commit();
                return true;
            }
            return false;
        }

        public Cliente Excluir(int id, out List<ValidationResult> erros)
        {
            erros = new List<ValidationResult>();
            using var sessao = session.OpenSession();
            using var transaction = sessao.BeginTransaction();
            var Cliente = sessao.Query<Cliente>()
                .Where(c => c.Id == id)
                .FirstOrDefault();
            if (Cliente == null)
            {
                erros.Add(new ValidationResult("Registro não encontrado",
                    new[] { "id" }));
                return null;
            }

            sessao.Delete(Cliente);
            transaction.Commit();
            return Cliente;
        }
        public virtual Cliente Retorna(int codigo)
        {
            using var sessao = session.OpenSession();
            var cliente = sessao.Get<Cliente>(codigo);
            return cliente;
        }

        public virtual List<Cliente> Listar(int page, int pageSize)
        {
            using var sessao = session.OpenSession();
            var clientes = page == 0 ? sessao.Query<Cliente>()
                .OrderByDescending(c => c.Id)
                .ToList() :
                sessao.Query<Cliente>()
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return clientes;
        }
        public virtual List<Cliente> Listar(int page, int pageSize, string busca)
        {
            using var sessao = session.OpenSession();
            var clientes = page == 0 ? sessao.Query<Cliente>()
                .Where(c => c.Nome.Contains(busca) ||
                            c.Email.Contains(busca) ||
                            c.Id.ToString().Contains(busca))
                .OrderByDescending(c => c.Id)
                .ToList() :
                sessao.Query<Cliente>()
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return clientes;
        }
        // Só para testes no console ------------------------------------------------
        private static int Contador = 1000;
        private static List<Cliente> Clientes = new List<Cliente>()
        {
            new Cliente { Nome = "Rodrigo teste", CPF = "469.366.770-61", Id = 1, 
                DataNascimento = new DateTime(2000,1,1), Email = "teste@gmail.com" },
            new Cliente { Nome = "Fulano de tal", CPF = "94809996093",Id = 2, 
                DataNascimento = new DateTime(2000,2,1), Email = "fulano@gmail.com" },
            new Cliente { Nome = "Jobiscleyson Souza", CPF = "860.522.350-20",Id = 3, 
                DataNascimento = new DateTime(2000,1,5), Email = "job@hotmail.com" },
            new Cliente { Nome = "Maria José", CPF = "520.651.370-96",Id = 4, 
                DataNascimento = new DateTime(1998,1,1), Email = "maria@outlook.com" },
        };

        public static bool Validacao(Cliente cliente,
            out List<ValidationResult> erros)
        {
            erros = new List<ValidationResult>();
            var valido = Validator.TryValidateObject(cliente,
                new ValidationContext(cliente),
                erros,
                true
            );

            var diaMinimo = DateTime.Today.AddYears(-16);
            if (cliente.DataNascimento > diaMinimo)
            {
                erros.Add(new ValidationResult(
                        "O cliente deve ser maior de 16 anos",
                        new[] { "DataNascimento" })
                    );
                valido = false;
            }
            //if (!ValidarCPF(cliente.CPF))

            return valido;
        }

        public static bool CriarCliente(Cliente cliente,
            out List<ValidationResult> erros)
        {
            cliente.Id = Contador++;
            var valido = Validacao(cliente, out erros);
            if (valido)
            {
                Clientes.Add(cliente);
            }
            else
            {
                foreach (var erro in erros)
                {
                    Console.WriteLine("{0}: {1}",
                        erro.MemberNames.First(),
                        erro.ErrorMessage
                    );
                }
            }
            return valido;
        }

        public static bool EditarCliente(Cliente novoCliente,
            out List<ValidationResult> erros)
        {
            var clienteExistente = Clientes.
                FirstOrDefault(x => x.Id == novoCliente.Id);

            erros = new List<ValidationResult>();

            if (clienteExistente == null)
            {
                return false;
            }

            var valido = Validacao(novoCliente, out erros);
            if (valido)
            {
                clienteExistente.Nome = novoCliente.Nome;
                clienteExistente.Email = novoCliente.Email;
                clienteExistente.DataNascimento = novoCliente.DataNascimento;
            }
            return valido;
        }
        
        public static List<Cliente> ListarTodos()
        {
            return Clientes;
        }
        public static List<Cliente> ListarPage(string buscaCliente, int skip = 0, int pageSize = 0)
        {
            var consulta = Clientes.Where(a =>
                    a.Id.ToString() == buscaCliente ||
                    a.Nome.Contains(buscaCliente,
                            StringComparison.OrdinalIgnoreCase) ||
                    a.Email.Contains(buscaCliente)
                )
                .OrderBy(x => x.DataNascimento)
                .AsEnumerable();
            if (skip > 0)
            {
                consulta = consulta.Skip(skip);
            }
            if (pageSize > 0)
            {
                consulta = consulta.Take(pageSize);
            }

            return consulta.ToList();
        }

        public static Cliente Remover(int Id)
        {
            var cliente = Clientes
                        .Where(x => x.Id == Id)
                        .FirstOrDefault();
            Clientes.Remove(cliente);
            return cliente;
        }

    }
}
