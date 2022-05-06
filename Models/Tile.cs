using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace ColorDefender
{

    public class Tile:Button
    {
        private String path = "C:/Users/Micheal/source/repos/ColorDefender/Images/";
        public static String[] shapes = { "Circle.png", "Diamond.png", "Square.png","Triangle.png"};
        private int col;
        private int row;

        private int shape;
        private string shapeName;

        private Image img; 

       
        public Tile(int row, int col)
        {
            this.col = col;
            this.row = row;

            var rand = new Random();
            this.shape = rand.Next(4);
            this.shapeName = shapes[shape];
            this.img = new Image();

            this.img.Width = 40;
            this.img.Height = 40;

            ImageSource testImg = new BitmapImage(new Uri(path+shapeName));

            img.Source = testImg;

            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;
           
            stackPnl.Children.Add(img);

            
            this.Content = stackPnl;

            this.Background=Brushes.Transparent;

        }
        public int getRow()
        {
            return row;
        }
        public int getColumn()
        {
            return col;
        }
        public string getShapeName()
        {
            return shapeName;
        }
        public int getShape()
        {
            return shape;
        }
        public void setRow(int x)
        {
            this.row = x;
        }

        public void setColumn(int x)
        {
            this.col = x;
        }
        public void setShapeName(string x)
        {
            this.shapeName=x;
        }
        public void setShape(int x)
        {
            this.shape = x;        
        }
        public void turnBad() 
        {
            this.shape = -1;
            this.setShapeName("null.png");
            this.img = new Image();

   

            ImageSource testImg = new BitmapImage(new Uri(path + shapeName));

            img.Source = testImg;

            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;

            stackPnl.Children.Add(img);


            this.Content = stackPnl;
        }
        public void turnRainbow()
        {
            this.shape = 5;
            this.setShapeName("Rainbow.png");
            this.img = new Image();



            ImageSource testImg = new BitmapImage(new Uri(path + shapeName));

            img.Source = testImg;

            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;

            stackPnl.Children.Add(img);


            this.Content = stackPnl;
        }
    }
}
