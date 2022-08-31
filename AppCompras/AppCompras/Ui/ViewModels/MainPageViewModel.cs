using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppCompras.Data.Network.Callers;
using AppCompras.Models;
using AppCompras.Ui.ViewModel;
using AppCompras.Ui.Views;
using Xamarin.Forms;

namespace AppCompras.Ui.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        int index = 0;
        public List<Detallecompras> Detallecompras { get; set; }
        public ObservableCollection<ViewProductPreSealed> PreSealeds { get; set; } = new ObservableCollection<ViewProductPreSealed>();
        public int Cant { get; set; } = 0;
        public bool ViewDetail { get; set; } = false;
        public bool ViewPrincipal { get; set; } = true;
        public float Total { get; set; }
        private bool delivery { get; set; } = false;



        public MainPageViewModel(INavigation navigation, StackLayout leftsite, StackLayout rightsite)
        {
            Navigation = navigation;
            getAndPaint(leftsite, rightsite);
            callShow();
            MessagingCenter.Subscribe<DetailPageViewModel>(this, "update", (sender) =>
            {
                callShow();
            });

        }


        public bool Delivery
        {
            get { return delivery; }
            set
            {
                delivery = value;
                OnpropertyChanged();
                if (delivery == true)
                {
                    Total += 10;
                }
                else
                {
                    Total -= 10;
                }
            }
        }

        public async void getAndPaint(StackLayout leftsite, StackLayout rightsite)
        {
            await ShowProducts(leftsite, rightsite);

        }

        public async Task ShowProducts(StackLayout leftsite, StackLayout rightsite)
        {

            var productslist = await new getProducts().getListProducts();
            var box = new BoxView
            {
                HeightRequest = 60,

            };
            rightsite.Children.Add(box);
            foreach (var item in productslist)
            {
                PrintProducts(item, index, leftsite, rightsite);
                index++;
            }

        }

        public void PrintProducts(Products item, int index, StackLayout leftSite, StackLayout rightSite)
        {
            var ubication = Convert.ToBoolean(index % 2);
            Console.WriteLine(ubication);
            var column = ubication ? rightSite : leftSite;

            var frame = new Frame
            {
                HeightRequest = 300,
                CornerRadius = 10,
                Margin = 8,
                HasShadow = false,
                BackgroundColor = Color.White,
                Padding = 20
            };

            var stack = new StackLayout
            {

            };

            var image = new Image
            {
                Source = item.Icono,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 10)

            };

            var price = new Label
            {
                Text = "$" + item.Precio,
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 32,
                Margin = new Thickness(0, 10)
            };

            var description = new Label
            {
                Text = item.Descripcion,
                TextColor = Color.Black,
                FontSize = 18,
                CharacterSpacing = 1
            };

            var wheigth = new Label
            {
                Text = item.Peso,
                TextColor = Color.FromHex("#cdcdcd"),
                FontSize = 16,
                CharacterSpacing = 1
            };

            stack.Children.Add(image);
            stack.Children.Add(price);
            stack.Children.Add(description);
            stack.Children.Add(wheigth);
            frame.Content = stack;
            var tap = new TapGestureRecognizer();

            tap.Tapped += async (object sender, EventArgs e) =>
             {
                 await Navigation.PushAsync(new DetailPage(item));
             };

            column.Children.Add(frame);
            stack.GestureRecognizers.Add(tap);

        }

        public async void callShow()
        {
            await ShowPreviusView();
        }


        public async Task ShowPreviusView()
        {
            PreSealeds.Clear();
            Detallecompras = await new crudDetalleCompra().getPreviadetallecompra();
            foreach (var item in Detallecompras)
            {
                PreSealeds.Add(new ViewProductPreSealed()
                {
                    Url = await new getProducts().getProductsImageById(item.Idproducto),
                    Idproduct = item.Idproducto,
                    Cantidad = item.Cantidad,
                    Total = item.Total,
                    Description = await new getProducts().getProductsDescriptionById(item.Idproducto)
                });
                Total += float.Parse(item.Total);
            }
            Cant = PreSealeds.Count;
        }

        public Command ChangeViewUp => new Command(async () =>
        {
            ViewDetail = true;
            ViewPrincipal = false;

        });

        public Command ChangeViewDown => new Command(async () =>
        {
            ViewDetail = false;
            ViewPrincipal = true;

        });

    }
}