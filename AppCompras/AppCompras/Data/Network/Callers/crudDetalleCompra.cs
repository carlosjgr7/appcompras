using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCompras.Data.Network.Conexions;
using AppCompras.Models;
using Firebase.Database.Query;

namespace AppCompras.Data.Network.Callers
{
    public class crudDetalleCompra
    {
        public async Task InsertDC(Detallecompras detallecompras)
        {
            await Conexion.firebase
                .Child("detallecompra")
                .PostAsync(detallecompras);
        }

        public async Task<List<Detallecompras>> getPreviadetallecompra()
        {
            return (await Conexion.firebase
                .Child("detallecompra")
                .OnceAsync<Detallecompras>())
                .Where(item => item.Key != "Modelo")
                .Select(item => new Detallecompras()
                {
                    Cantidad = item.Object.Cantidad,
                    Iddetallecompra = item.Object.Iddetallecompra,
                    Idproducto = item.Object.Idproducto,
                    Preciocompra = item.Object.Preciocompra,
                    Total = item.Object.Total
                })
              .ToList();
        }
    }
}

