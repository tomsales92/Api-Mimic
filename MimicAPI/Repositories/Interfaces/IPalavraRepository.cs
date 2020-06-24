using MimicAPI.Helpers;
using MimicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Repositories.Interfaces
{
    public interface IPalavraRepository
    {
        PaginationList<Palavra> ObterPalavras(PalavraUrlQuery query);
        Palavra obter(int id);
        void Cadastrar(Palavra palavra);
        void Atualizar(Palavra palavra);
        void Deletar(int id);
    }
}
