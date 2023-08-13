using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HomeInventory
{
    public partial class TemplatePage : ContentPage
    {
        Button reg; //Register new item into the array
        Button SortArray;//Button to sort the data up or down

        public int sortCount = 2;//Count to check if sorting down or sorting up

        //Initialize array for current stock details
        private static Algo[] current = new Algo[30];

        //Declare layout outside of main function to allow use in method
        StackLayout stackLay = new StackLayout();

        //String variable for file location to reduce code length
        public String filename = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        int deleteCount = 0;//To test button count.

        Grid[] grid = new Grid[50];//Testing array grid to ease process

        public TemplatePage(String userFile, String pageName)
        {
            this.BackgroundImageSource = "background3.png"; //Image used for the background
            filename = filename + userFile;
            //Initialize array for current stock details
            for (int i = 0; i < current.Length; i++)
            {
                current[i] = new Algo();
            }

            //Open filestream to deserialize array from binary data
            current = Algo.DeserializeData(current, filename);

            //Base details of page
            this.Title = pageName;
            BackgroundColor = Color.White;
            Padding = new Thickness(20, 20, 20, 20);

            //Initialize register button
            stackLay.Children.Add(reg = new Button
            {
                Text = "Register New Item!",
                FontSize = 30,
                BackgroundColor = Color.WhiteSmoke,
                BorderWidth = 60,
                CornerRadius=10
            });
            reg.Clicked += OnRegister; //Event handler if button is clicked

            //Initialize Sort button
            stackLay.Children.Add(new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition(),
                    new ColumnDefinition{Width = new GridLength(120)},
                    new ColumnDefinition(),
                },
                Children = {
                    {SortArray = new Button
                    {
                        Text = "Sort",
                        FontSize = 20,
                        BackgroundColor = Color.WhiteSmoke,
                        CornerRadius=10
                    },1,0 }
                }
            });
            SortArray.Clicked += Sort;

            //Openfilestream to serialize data
            Algo.SerializeData(current, filename);

            //Initialize contents of stacks
            for (int i = 0; i < current.Length; i++)
            {
                current[i].LabelSet(Convert.ToString(i)); //Initialize labels since they cant be serialized
                //If is required in case the array is empty.
                if (String.IsNullOrEmpty(current[i].Name) != true)
                {
                    stackLay.Children.Add(current[i].algoGrid);
                    current[i].add.Clicked += OnAdd;
                    current[i].minus.Clicked += OnMinus;
                    current[i].delete.Clicked += OnClickDelete;
                }
            }
            //Arrangement of contents of page. Grid inside stack, stack inside scrollview, SV is the content
            ScrollView sv = new ScrollView { };
            sv.Content = stackLay; //stack can be scrolled
            this.Content = sv;
        }

        //Method for when register button is pressed
        async public void OnRegister(Object a, EventArgs s)
        {
            string itemQuantity=""; //To receive string input from Display Prompt
            string itemName=""; //To receive item name input from user
            int itemQty=0; //To convert itemQuantity into int from string

            itemName = await DisplayPromptAsync("Detail 1", "Item Name?"); //To prompt user to key-in item name
            if (String.IsNullOrEmpty(itemName) == true) return; //If user didn't input name, the event stops by return

            itemQuantity = await DisplayPromptAsync("Detail 2", "Quantity of item?","OK","Cancel","",maxLength: -1,keyboard: Keyboard.Numeric,""); //To prompt user to key-in quantity
            if (int.TryParse(itemQuantity, out itemQty) == false)itemQty = 0; //If user didn't input number or pressed cancel, default is 0

            for (int i = 0; i < current.Length; i++)
            {
                if (current[i] is null) { current[i] = new Algo(); }
                if (current[i].Name == "")
                {
                    current[i].Name = itemName.ToUpper();
                    current[i].Qty = itemQty;
                    current[i].Code = 100 + i;
                    current[i].LabelSet(Convert.ToString(i)); //Initialize labels since they cant be serialized

                    //Add new grid
                    stackLay.Children.Add(current[i].algoGrid);
                    //Initialize button click event handler
                    current[i].add.Clicked += OnAdd;
                    current[i].minus.Clicked += OnMinus;
                    current[i].delete.Clicked += OnClickDelete;

                    //To serialize array content. aka save content.
                    //Details will be the same even if app restart.
                    Algo.SerializeData(current, filename);
                    break; //To stop the for loop (faster processing)
                }
            }
        }
        public void OnAdd(Object a, EventArgs s)
        {
            var but = (Button)a;
            deleteCount = Convert.ToInt32(but.StyleId);
            current[deleteCount].OnClickAdd();
            Algo.SerializeData(current, filename);
        }
        public void OnMinus(Object a, EventArgs s)
        {
            var but = (Button)a;
            deleteCount = Convert.ToInt32(but.StyleId);
            current[deleteCount].OnClickMinus();
            Algo.SerializeData(current, filename);
        }
        async public void OnClickDelete(Object a, EventArgs s)
        {
            var but = (Button)a;
            deleteCount = Convert.ToInt32(but.StyleId);
            if (await DisplayAlert("WARNING!", "Are you sure you want to delete " + current[deleteCount].Name + "?", "YES", "NO") == true)
            {
                //Rearranging the data after deletion
                for (int i = deleteCount; i < current.Length - 1; i++)
                {
                    if (current[i] is null) current[i] = new Algo();
                    if (current[i + 1] is null) current[i + 1] = new Algo();
                    if (current[i + 1].Name == "") 
                    {
                        current[i].OnDelete(); //To make sure all the grids, labels, buttons are gone
                        break;
                    }
                    Algo.SwapAlgo(ref current[i], ref current[i + 1]);//Swapping positions with the deleted file.
                    
                }
                if (current[current.Length - 1] is null) current[current.Length - 1] = new Algo();//To avoid null exception
                current[current.Length - 1].OnDelete();//To make sure even the last object is removed.
                Algo.SerializeData(current, filename);
            }
        }
        //General Sort function
        public void Sort(Object a, EventArgs s)
        {
            for (int i = 0; i < current.Length - 1; i++)
            {
                if (current[i] is null ^ current[i + 1] is null) break;
                if (current[i].Name == "" ^ current[i + 1].Name == "") break;//Break the loop if the array is empty

                for (int j = 0; j < current.Length - 1; j++)
                {
                    if (current[j] is null ^ current[j + 1] is null) break;
                    if (current[j].Name == "" ^ current[j + 1].Name == "") break;//Break the loop if the array is empty

                    if (sortCount % 2 == 0)//Sort Descending
                    {
                        if (current[j].Qty < current[j + 1].Qty)
                        {
                            Algo.SwapAlgo(ref current[j], ref current[j + 1]);//Swapping positions
                            
;                        }
                    }
                    else //Sort ascending
                    {
                        if (current[j].Qty > current[j + 1].Qty)
                        {
                            Algo.SwapAlgo(ref current[j], ref current[j + 1]);//Swapping positions
                            
                        }
                    }
                }

            }
            if (sortCount % 2 == 0) ((Button)a).Text = "Sort U"; //Updating the button label. Nex is sort up
            else ((Button)a).Text = "Sort D";//Updating the button label. Next is sort down
            Algo.SerializeData(current, filename);
            sortCount = sortCount + 1;
        }
    }
}
