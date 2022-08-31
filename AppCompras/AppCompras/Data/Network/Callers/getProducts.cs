using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppCompras.Data.Network.Conexions;
using AppCompras.Models;

namespace AppCompras.Data.Network.Callers
{
    public class getProducts
    {
        public async Task<List<Products>> getListProducts()
        {
            return (await Conexion.firebase
                .Child("Productos")
                .OnceAsync<Products>()).Select(item => new Products
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Peso = item.Object.Peso,
                    Precio = item.Object.Precio,
                    Idproducto = item.Key
                }).ToList();
        }

        public async Task<Products> getProductsById(string id)
        {
            return (await Conexion.firebase
                .Child("Productos")
                .OnceAsync<Products>()).Select(item => new Products
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Peso = item.Object.Peso,
                    Precio = item.Object.Precio,
                    Idproducto = item.Key
                }).Where(item => item.Idproducto == id).Single();

        }

        public async Task<string> getProductsImageById(string id)
        {
            return (await Conexion.firebase
                .Child("Productos")
                .OnceAsync<Products>()).Select(item => new Products
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Peso = item.Object.Peso,
                    Precio = item.Object.Precio,
                    Idproducto = item.Key
                }).Where(item => item.Idproducto == id).Single().Icono;

        }

        public async Task<string> getProductsDescriptionById(string id)
        {
            return (await Conexion.firebase
                .Child("Productos")
                .OnceAsync<Products>()).Select(item => new Products
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Peso = item.Object.Peso,
                    Precio = item.Object.Precio,
                    Idproducto = item.Key
                }).Where(item => item.Idproducto == id).Single().Descripcion;

        }
    }
}

