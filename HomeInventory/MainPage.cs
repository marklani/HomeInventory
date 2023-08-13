using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeInventory
{
    public partial class MainPage : ContentPage
    {
        //Initialize buttons to navigate between pages
        Button pageOne; //Kitchen Stock
        Button pageTwo; //Frozen food Stock
        Button pageThree; //Wardrobe
        Button pageFour; //Others

        public MainPage()
        {
            //String of page names and save file names
            String pageOneName = "Kitchen Stock";
            String pageTwoName = "Frozen Food";
            String pageThreeName = "Wardrobe";
            String pageFourName =  "Others";

            this.Title = "Home Inventory";
            BackgroundColor = Color.White;
            Padding = new Thickness(20,20,20,20);

            this.BackgroundImageSource ="background2.png"; //Image used for the background

            StackLayout s = new StackLayout
            {
                Spacing = 30,
            };

            s.Children.Add(pageOne = new Button
            {
                Text = pageOneName,
                FontSize = 30,
                BackgroundColor = Color.WhiteSmoke,
                BorderWidth = 60,
                MinimumHeightRequest = 30,
                CornerRadius=10
            });
            s.Children.Add(pageTwo = new Button
            {
                Text = pageTwoName,
                FontSize = 30,
                BackgroundColor = Color.WhiteSmoke,
                BorderWidth = 60,
                MinimumHeightRequest = 30,
                CornerRadius=10
            });
            s.Children.Add(pageThree = new Button
            {
                Text = pageThreeName,
                FontSize = 30,
                BackgroundColor = Color.WhiteSmoke,
                BorderWidth = 60,
                MinimumHeightRequest = 30,
                CornerRadius = 10
            });
            s.Children.Add(pageFour = new Button
            {
                Text = pageFourName,
                FontSize = 30,
                BackgroundColor = Color.WhiteSmoke,
                BorderWidth = 60,
                MinimumHeightRequest = 30,
                CornerRadius = 10
            });

            pageOne.Clicked+= (pageOne, e) =>Navigation.PushAsync(new TemplatePage("/"+pageOneName+".binary",pageOneName));
            pageTwo.Clicked += (pageTwo, e) => Navigation.PushAsync(new TemplatePage("/" + pageTwoName + ".binary", pageTwoName));
            pageThree.Clicked += (pageThree, e) => Navigation.PushAsync(new TemplatePage("/" + pageThreeName + ".binary", pageThreeName));
            pageFour.Clicked += (pageFour, e) => Navigation.PushAsync(new TemplatePage("/" + pageFourName + ".binary", pageFourName));

            this.Content = s;
        }
    }
}
