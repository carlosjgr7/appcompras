using System;
using System.Collections.Generic;
using AppCompras.Models;
using AppCompras.Ui.ViewModels;
using Xamarin.Forms;

namespace AppCompras.Ui.Views
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(Products product)
        {
            InitializeComponent();
            BindingContext = new DetailPageViewModel(Navigation,product);
        }
    }
}

