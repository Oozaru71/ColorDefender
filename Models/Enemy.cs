using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorDefender.Models
{
    public class Enemy:Image
    {
        private String path = "C:/Users/Micheal/source/repos/ColorDefender/Images/";
        public static String[] mTypes = { "blueEnemy.png", "greenEnemy.png", "purpleEnemy.png"};
        private int tHp;
        private int cHp;
        private int tCounter;
        private string mType;
   //     private ImageSource eSource;

        public Enemy() 
        {
            var rand = new Random();
            this.mType = mTypes[rand.Next(3)];
            if (this.mType == "blueEnemy.png") 
            {
                this.tHp = 20;
                this.cHp = 20;
                this.tCounter = 10;
            }
            if (this.mType == "greenEnemy.png")
            {
                this.tHp = 40;
                this.cHp = 40;
                this.tCounter = 7;
            }
            if (this.mType == "purpleEnemy.png")
            {
                this.tHp = 80;
                this.cHp = 80;
                this.tCounter = 5;
            }
            ImageSource testImg = new BitmapImage(new Uri(path + mType));

            this.Source = testImg;
        }
        public Enemy(string name)
        {
            if (name == "first") 
            {
                this.mType = "blueEnemy.png";
                this.tHp = 20;
                this.cHp = 20;
                this.tCounter = 10;
            }
            //code for last foe
            /*
            if(name=="last")
            {

            }
            */
            ImageSource testImg = new BitmapImage(new Uri(path + mType));

            this.Source = testImg;
        }
        public int getTHP ()
        {
            return this.tHp;
        }
        public int getCHP() { return this.cHp; }

        public void setCHP(int hp) { this.cHp = hp; }    

        public int getTCounter() { return this.tCounter; }

   //     public ImageSource getSource() { retur }

    }
}
