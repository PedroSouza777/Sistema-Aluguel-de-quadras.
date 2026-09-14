using System;

namespace SistemaQuadras
{    class Aluguel
    {
        public Quadra Quadra;
        public Cliente Cliente;
        public int Horas;
        public double ValorTotal;

        public Aluguel(Quadra quadra, Cliente cliente, int horas)
        {
            Quadra = quadra;
            Cliente = cliente;
            Horas = horas;
            ValorTotal = quadra.ValorHora * horas;
        }
    }
}