using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PROJETO.Models;

namespace PROJETO.Interfaces
{
    public interface IEventoRepositorio
    {
        Task<EventoTbl> BuscarPorId(int id);
        Task<List<EventoTbl>> ListarEventos();
        Task<List<EventoTbl>> BuscarPalavraChave(string nome);
        Task<EventoTbl> BuscarPorNome(string nomeEvento);
        Task<List<EventoTbl>> BuscarPorCategoria(string categoria);
        Task<List<EventoTbl>> BuscarPorStatus(string status);
        Task<EventoTbl> Post(EventoTbl evento);
        Task<EventoTbl> Put(int id, EventoTbl evento);
        Task<EventoTbl> DeletarEvento(int id);
        Task<EventoTbl> UploadImagem(string imagem);
    }
}