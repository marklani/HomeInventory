using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace HomeInventory
{
    [Serializable()]
    public class Algo//Constructor
    {
        //Basic characteristic of class
        private string name; //name of item i.e kentang, gula
        private int code; //Item code of item. Frozen food 200~, stock 100~ defined in each page
        //Code will not be shown. it is for barcode later on.
        private int qty; //Quantity of item. Default is 0.
        [NonSerialized()] public Button add; //Button object to add to quantity
        [NonSerialized()] public Button minus; //Button object to subtract from quantity
        [NonSerialized()] public Button delete; //Button object to subtract from quantity
        [NonSerialized()] public Label labelName; //label object containing name
        [NonSerialized()] public Label labelQty; //label object containing quantity of item
        [NonSerialized()] public Frame frameName; //frame of label Name
        [NonSerialized()] public Frame frameQty; // frame of label quantity
        [NonSerialized()] public Grid algoGrid; //Grid of the class to clean up the template page
        public Algo()
        {
            name = "";
            code = 0;
            qty = 0;
            labelName = new Label
            {
                Text = name,
                FontSize = 15,
                TextColor = Color.Blue,
                IsVisible = false,

            };
            labelQty = new Label
            {
                Text = Convert.ToString(Qty),
                FontSize = 15,
                TextColor = Color.Blue,
                IsVisible = false
            };
            add = new Button
            {
                Text = "+",
                FontSize = 15,
                //BorderColor = Color.Blue,
                BackgroundColor = Color.White,
                BorderColor = Color.WhiteSmoke,
                TextColor = Color.Blue,
                CornerRadius = 10,
                IsVisible = false
            };
            minus = new Button
            {
                Text = "-",
                FontSize = 15,
                //BorderColor = Color.Blue,
                BackgroundColor = Color.White,
                BorderColor = Color.WhiteSmoke,
                TextColor = Color.Blue,
                CornerRadius = 10,
                IsVisible = false
            };
            delete = new Button
            {
                Text = "X",
                FontSize = 15,
                //BorderColor = Color.Blue,
                BackgroundColor = Color.White,
                BorderColor = Color.WhiteSmoke,
                TextColor = Color.Blue,
                CornerRadius = 10,
                IsVisible = false
            };
            frameName = new Frame
            {
                Content = labelName,
                BorderColor = Color.WhiteSmoke,
                CornerRadius = 10,
                HasShadow = false,
                IsVisible = false
            };
            frameQty = new Frame
            {
                Content = labelQty,
                BorderColor = Color.WhiteSmoke,
                CornerRadius = 10,
                HasShadow = false,
                IsVisible = false
            };
            algoGrid = new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(150) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
                Children = {
                    { frameName, 0, 0 },
                    { frameQty, 1, 0 },
                    { add, 2, 0 },
                    { minus, 3, 0 },
                    { delete, 4, 0 }
                },
                IsVisible = false
            };
        }
        public string Name 
        {
            get { return name; }
            set 
            { 
                name = value;
                labelName.Text = value;
                labelName.IsVisible = true;
                frameName.Content = labelName;
                frameName.IsVisible = true;
            }
        }
        public int Code 
        {  
            get { return code; }
            set { code = value; }
        }
        public int Qty
        {
            get { return qty; }
            set 
            { 
                qty = value;
                labelQty.Text = Convert.ToString(qty);
                labelQty.IsVisible = true;
                frameQty.Content = labelQty;
                frameQty.IsVisible = true;
            }
        }
        //This method initializes the labels and buttons after deserializing data.
        //Since labels and buttons can't be serialized.
        public void LabelSet(String id)
        {
            if (String.IsNullOrEmpty(name))
            {
                labelName = new Label
                {
                    Text = "",
                    FontSize = 15,
                    TextColor = Color.Blue,
                    IsVisible = false,
                    StyleId = id
                };
                labelQty = new Label
                {
                    Text = Convert.ToString(Qty),
                    FontSize = 15,
                    TextColor = Color.Blue,
                    IsVisible = false,
                    StyleId = id
                };
                add = new Button
                {
                    Text = "+",
                    FontSize = 15,
                    //BorderColor = Color.Blue,
                    BackgroundColor = Color.White,
                    BorderColor = Color.WhiteSmoke,
                    TextColor = Color.Blue,
                    CornerRadius = 10,
                    IsVisible = false,
                    StyleId = id
                };
                minus = new Button
                {
                    Text = "-",
                    FontSize = 15,
                    //BorderColor = Color.Blue,
                    BackgroundColor = Color.White,
                    BorderColor = Color.WhiteSmoke,
                    TextColor = Color.Blue,
                    CornerRadius = 10,
                    IsVisible = false,
                    StyleId = id
                };
                delete = new Button
                {
                    Text = "X",
                    FontSize = 15,
                    //BorderColor = Color.Blue,
                    BackgroundColor = Color.White,
                    BorderColor = Color.WhiteSmoke,
                    TextColor = Color.Blue,
                    CornerRadius = 10,
                    IsVisible = false,
                    StyleId = id
                };
                frameName = new Frame
                {
                    Content = labelName,
                    BorderColor = Color.WhiteSmoke,
                    CornerRadius = 10,
                    IsVisible = false,
                    HasShadow = false,
                    StyleId = id
                };
                frameQty = new Frame
                {
                    Content = labelQty,
                    BorderColor = Color.WhiteSmoke,
                    CornerRadius = 10,
                    IsVisible = false,
                    HasShadow = false,
                    StyleId = id
                };
                algoGrid = new Grid
                {
                    ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(150) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
                    Children = {
                    { frameName, 0, 0 },
                    { frameQty, 1, 0 },
                    { add, 2, 0 },
                    { minus, 3, 0 },
                    { delete, 4, 0 }
                },
                    IsVisible = false,
                    StyleId=id
                };
            }
            else
            {
                labelName = new Label
                {
                    Text = name,
                    FontSize = 15,
                    TextColor = Color.Blue,
                    VerticalOptions=LayoutOptions.Center,
                    IsVisible = true,
                    StyleId = id
                };
                labelQty = new Label
                {
                    Text = Convert.ToString(Qty),
                    FontSize = 15,
                    TextColor = Color.Blue,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    IsVisible = true,
                    StyleId = id
                };
                add = new Button
                {
                    Text = "+",
                    FontSize = 15,
                    //BorderColor = Color.Blue,
                    BackgroundColor = Color.White,
                    BorderColor = Color.WhiteSmoke,
                    TextColor = Color.Blue,
                    CornerRadius = 10,
                    IsVisible = true,
                    StyleId = id
                };
                minus = new Button
                {
                    Text = "-",
                    FontSize = 15,
                    //BorderColor = Color.Blue,
                    BackgroundColor = Color.White,
                    BorderColor = Color.WhiteSmoke,
                    TextColor = Color.Blue,
                    CornerRadius = 10,
                    IsVisible = true,
                    StyleId = id
                };
                delete = new Button
                {
                    Text = "X",
                    FontSize = 15,
                    //BorderColor = Color.Blue,
                    BackgroundColor = Color.White,
                    BorderColor = Color.WhiteSmoke,
                    TextColor = Color.Blue,
                    CornerRadius = 10,
                    IsVisible = true,
                    StyleId = id
                };
                frameName = new Frame
                {
                    Content = labelName,
                    BorderColor = Color.WhiteSmoke,
                    CornerRadius = 10,
                    HasShadow = false,
                    IsVisible = true
                };
                frameQty = new Frame
                {
                    Content = labelQty,
                    BorderColor = Color.WhiteSmoke,
                    CornerRadius = 10,
                    HasShadow = false,
                    IsVisible = true
                };
                algoGrid = new Grid
                {
                    ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(150) },
                    new ColumnDefinition { Width = new GridLength(80) },
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
                    Children = {
                    { frameName, 0, 0 },
                    { frameQty, 1, 0 },
                    { add, 2, 0 },
                    { minus, 3, 0 },
                    { delete, 4, 0 }
                },
                    IsVisible = true,
                    StyleId=id
                };
            }
        }
        //Method to serialize data
        public static void SerializeData(Algo [] array, String filePath)
        {
            //Open filestream to serialize array to binary data
            FileStream fsout = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fsout, array);
            fsout.Close();//Close file after extracting data
        }
        //Method to deserialize data
        public static Algo[] DeserializeData(Algo []array, String filePath)
        {
            //Open filestream to deserialize array from binary data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsin = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            if (fsin.Length != 0)
            {
                array = (Algo[])bf.Deserialize(fsin);
                fsin.Close(); //Close file after extracting data
            }
            return array;
        }
        //Method to add the quantity of the item
        public void OnClickAdd() { Qty = Qty + 1;}
        //Method to subtract the quantity of the item
        public void OnClickMinus() { if (Qty != 0) { Qty = Qty - 1; } }
        //Method used to delete/remove objects from view
        public void OnDelete()
        {
            name = "";
            code = 0;
            qty = 0;
            delete.StyleId = "";
            add.StyleId = "";
            minus.StyleId = "";
            algoGrid.StyleId = "";
            algoGrid.IsVisible = false;
            frameName.IsVisible = false;
            frameQty.IsVisible = false;
            labelName.IsVisible = false;
            labelQty.IsVisible = false;
            add.IsVisible = false;
            minus.IsVisible = false;
            delete.IsVisible = false;
        }
        //Swapping between two algo objects
        public static void SwapAlgo(ref Algo a, ref Algo b) 
        {
            int swapQty = 0;
            String swapName = "";
            int swapCode = 0;

            //Swap intermediate with a
            swapQty = a.Qty;
            swapName = a.Name;
            swapCode = a.Code;
            //Swap a with b
            a.Qty = b.Qty;
            a.Name = b.Name;
            a.Code = b.Code;
            //Swap b with c
            b.Qty = swapQty;
            b.Name = swapName;
            b.Code = swapCode;
        }
    }
}
