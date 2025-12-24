using Sistema_de_Usuario.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Usuario.Services
{
    public class UsuarioService
    {
        private List<Usuario> usuarios;
        private readonly string caminho = "Data/usuarios.json";

        public void Carregar()
        {
            try
            {

                string pasta = Path.GetDirectoryName(caminho);
                if (!Directory.Exists(pasta))
                {
                    Directory.CreateDirectory(pasta);
                }

                if (!File.Exists(caminho))
                {
                    File.WriteAllText(caminho, "[]");
                    usuarios = new List<Usuario>();
                    return;
                }

                string json = File.ReadAllText(caminho);
                usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
                return;
            }
            catch (Exception ex)
            {

                usuarios = new List<Usuario>();
            }

        }

        public void Salvar()
        {
            try
            {
                string json = JsonSerializer.Serialize<List<Usuario>>(usuarios);
                File.WriteAllText(caminho, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar");
            }
        }

        public void Adicionar(Usuario usuario)
        {
            if (usuarios.Any(a => a.Id == usuario.Id))
            {
                throw new Exception("ID já existe");
            }
            usuarios.Add(usuario);
            Salvar();
        }

        public Usuario BuscarPorId(int id)
        {
            var usuario = usuarios.Find(a => a.Id == id);
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }

        public List<Usuario> List()
        {
            var list = usuarios.OrderBy(a => a.Id).ToList();
            return list;
        }
    }



}
