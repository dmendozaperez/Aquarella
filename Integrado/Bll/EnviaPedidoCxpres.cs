using System;
using Integrado.comercioxpress;

namespace Integrado.Bll
{
    internal class EnviaPedidoCxpres
    {
        public EnviaPedidoCxpres()
        {
        }

        public static implicit operator EnviaPedidoCxpres(EnviaPedidoCxpress v)
        {
            throw new NotImplementedException();
        }
    }
}