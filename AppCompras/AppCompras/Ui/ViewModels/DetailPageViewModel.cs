using System;
using System.Threading.Tasks;
using AppCompras.Data.Network.Callers;
using AppCompras.Models;
using AppCompras.Ui.ViewModel;
using Xamarin.Forms;

namespace AppCompras.Ui.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {

        public int Cant { get; set; } = 1;
        public Products Product { get; set; }
        public float Precio { get; set; }
        public float PrecioBase { get; set; }


        public DetailPageViewModel(INavigation navigation, Products product)
        {
            Product = product;
            PrecioBase = float.Parse(product.Precio);
            Precio = PrecioBase;
            Navigation = navigation;
        }

        public Command Minnus => new Command(() =>
        {
            if (Cant != 0)
                Cant--;
            Precio = Cant * PrecioBase;
        });

        public Command Pluss => new Command(() =>
        {
            Cant++;
            Precio = Cant * PrecioBase;

        });

        public Command BackCommand => new Command(() =>
        {
            Navigation.PopAsync();
        });

        public Command AggCompra => new Command(async () =>
        {

            crudDetalleCompra funcion = new crudDetalleCompra();
            if (Cant == 0) Cant = 1;

            var detalle = new Detallecompras()
            {
                Total = Precio.ToString(),
                Cantidad = Cant.ToString(),
                Idproducto = Product.Idproducto,
                Preciocompra = Product.Precio
            };
            await funcion.InsertDC(detalle);

            MessagingCenter.Send(this, "update",detalle);
            await Navigation.PopAsync();
        });


    }
}

