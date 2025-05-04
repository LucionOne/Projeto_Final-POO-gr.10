using System;
using System.Collections.Generic;
using System.Linq;

namespace jogo;

public class JogoRepo
{
    private List<Jogo> jogos = new List<Jogo>();
    private int proximoId = 1;

    public void Adicionar(Jogo jogo)
    {
        jogo.Id = proximoId++;
        jogos.Add(jogo);
    }

    public List<Jogo> ListarTodos()
    {
        return jogos;
    }

    public Jogo? BuscarPorId(int id)
    {
        return jogos.FirstOrDefault(j => j.Id == id);
    }

    public bool Remover(int id)
    {
        var jogo = BuscarPorId(id);
        if (jogo != null)
        {
            jogos.Remove(jogo);
            return true;
        }
        return false;
    }

    public bool Atualizar(int id, Jogo novoJogo)
    {
        var jogo = BuscarPorId(id);
        if (jogo != null)
        {
            jogo.Data = novoJogo.Data;
            jogo.Local = novoJogo.Local;
            jogo.TipoCampo = novoJogo.TipoCampo;
            jogo.JogadoresPorTime = novoJogo.JogadoresPorTime;
            jogo.LimiteDeTimes = novoJogo.LimiteDeTimes;
            return true;
        }
        return false;
    }

    public bool AdicionarInteressado(int jogoId, int jogadorId)
    {
        var jogo = BuscarPorId(jogoId);
        if (jogo == null) return false;

        if (!jogo.InteressadosIds.Contains(jogadorId))
        {
            jogo.InteressadosIds.Add(jogadorId);
            return true;
        }
        return false;
    }

    public bool RemoverInteressado(int jogoId, int jogadorId)
    {
        var jogo = BuscarPorId(jogoId);
        if (jogo == null) return false;

        return jogo.InteressadosIds.Remove(jogadorId);
    }
}
