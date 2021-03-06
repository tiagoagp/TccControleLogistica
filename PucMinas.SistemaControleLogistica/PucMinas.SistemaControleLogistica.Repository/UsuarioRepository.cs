﻿using Dapper;
using Npgsql;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario RetornarUsuario(string email, string senha)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                Usuario usuario = pgsqlConnection.Query<Usuario>("select * from usuario where email = @email AND senha = @senha", new { senha = senha, email = email }).FirstOrDefault();
                return usuario;   
            }
        }

        public Usuario RetornarUsuario(Guid id)
        {

            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                Usuario usuario = pgsqlConnection.Query<Usuario>("select * from usuario where id = @id", new { id = id }).FirstOrDefault();
                return usuario;
            }
        }
    }
}