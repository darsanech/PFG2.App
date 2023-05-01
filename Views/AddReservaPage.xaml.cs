namespace PFG2.Views;

using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using PFG2.Models;
using PFG2.ViewModel;
using System;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

public partial class AddReservaPage : ContentPage
{
    public AddReservaPage(AddReservaViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as AddReservaViewModel).OnLoad();
    }
    int nProducto = 0;
    /*
    private void ModProd(object sender, EventArgs e, int type)
    {
        Button button = (Button)sender;
        StackLayout par = (StackLayout)button.Parent;
        Label labelnum=(Label)par.Children[2];
        int nv = Int32.Parse(labelnum.Text)+type;
        StackLayout gpar = (StackLayout)par.Parent;
        (BindingContext as AddReservaViewModel).ModProductList(par.StyleId, type > 0);
        if (nv > 0) {
            labelnum.Text = nv.ToString();
        }
        else
        {
            gpar.Children.Remove(par);
            nProducto--;
            if (nProducto < 5)
            {
                Productto.IsVisible = true;
                AddProductto.IsVisible = true;
            }
        }
    }
    
    private void AddProd_Button(object sender, EventArgs e)
    {
        
        if(Productto.SelectedIndex != -1)
        {
            Label label = new Label()
            {
                StyleId = "ProductoLabel",
                Text = (BindingContext as AddReservaViewModel).ProducteName(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,

            };
            Button res = new Button()
            {
                Text = "-",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            res.Clicked += new EventHandler((sender, e) => ModProd(sender, e, -1));
            Label labelnum = new Label()
            {
                Text = "1",
                FontSize=20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            Button sum = new Button()
            {
                Text = "+",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            sum.Clicked += new EventHandler((sender, e) => ModProd(sender, e, 1));
            StackLayout HSL = new StackLayout();
            HSL.StyleId = (BindingContext as AddReservaViewModel).ProducteName();
            HSL.Orientation = StackOrientation.Horizontal;
            HSL.VerticalOptions = LayoutOptions.Start;
            HSL.HorizontalOptions = LayoutOptions.Fill;
            HSL.HeightRequest = 60;
            HSL.Spacing = 10;
            HSL.Children.Add(label);
            HSL.Children.Add(res);
            HSL.Children.Add(labelnum);
            HSL.Children.Add(sum);
            (BindingContext as AddReservaViewModel).ModProductList(label.Text,true);
            ProductosStack.Children.Add(HSL);
            Productto.SelectedIndex = -1;
            nProducto++;
            if (nProducto > 4)
            {
                Productto.IsVisible = false;
                AddProductto.IsVisible= false;
            }
        }
    
    }
    */
}
