/* Student Name: Bhavanandhan L R
 * Student ID: 23103646
 * Date: 22/12/2023
 * Assignment 4*/
//This form is to display the inventory balances
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bhavan_BAP_Assignment4
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }
                
        public void LoadValues(string[] Items, int[] Original_Stock, int[] Current_Stock)
        {
            //Just to check if the array has values
            if (Original_Stock.Length > 0 && Current_Stock.Length > 0)
            {
                //Assigning Original value and current stock value to textbox
                Item1Ori.Text = Original_Stock[0].ToString();
                Item1Cur.Text = Current_Stock[0].ToString();
                Item2Ori.Text = Original_Stock[1].ToString();
                Item2Cur.Text = Current_Stock[1].ToString();
                Item3Ori.Text = Original_Stock[2].ToString();
                Item3Cur.Text = Current_Stock[2].ToString();
                Item4Ori.Text = Original_Stock[3].ToString();
                Item4Cur.Text = Current_Stock[3].ToString();
                Item5Ori.Text = Original_Stock[4].ToString();
                Item5Cur.Text = Current_Stock[4].ToString();
                Item6Ori.Text = Original_Stock[5].ToString();
                Item6Cur.Text = Current_Stock[5].ToString();
                Item7Ori.Text = Original_Stock[6].ToString();
                Item7Cur.Text = Current_Stock[6].ToString();
                Item8Ori.Text = Original_Stock[7].ToString();
                Item8Cur.Text = Current_Stock[7].ToString();
                Item9Ori.Text = Original_Stock[8].ToString();
                Item9Cur.Text = Current_Stock[8].ToString();
                Item10Ori.Text = Original_Stock[9].ToString();
                Item10Cur.Text = Current_Stock[9].ToString();
                Item11Ori.Text = Original_Stock[10].ToString();
                Item11Cur.Text = Current_Stock[10].ToString();
                Item12Ori.Text = Original_Stock[11].ToString();
                Item12Cur.Text = Current_Stock[11].ToString();
                Item13Ori.Text = Original_Stock[12].ToString();
                Item13Cur.Text = Current_Stock[12].ToString();
                Item14Ori.Text = Original_Stock[13].ToString();
                Item14Cur.Text = Current_Stock[13].ToString();
                Item15Ori.Text = Original_Stock[14].ToString();
                Item15Cur.Text = Current_Stock[14].ToString();
                Item16Ori.Text = Original_Stock[15].ToString();
                Item16Cur.Text = Current_Stock[15].ToString();
                Item17Ori.Text = Original_Stock[16].ToString();
                Item17Cur.Text = Current_Stock[16].ToString();
                Item18Ori.Text = Original_Stock[17].ToString();
                Item18Cur.Text = Current_Stock[17].ToString();
                Item19Ori.Text = Original_Stock[18].ToString();
                Item19Cur.Text = Current_Stock[18].ToString();
                Item20Ori.Text = Original_Stock[19].ToString();
                Item20Cur.Text = Current_Stock[19].ToString();
            }
            else
            {
                //Incase if array is empty N/A is assigned
                Item1Ori.Text = "N/A"; 
                Item1Cur.Text = "N/A";
                Item2Ori.Text = "N/A";
                Item2Cur.Text = "N/A";
                Item3Ori.Text = "N/A";
                Item3Cur.Text = "N/A";
                Item4Ori.Text = "N/A";
                Item4Cur.Text = "N/A";
                Item5Ori.Text = "N/A";
                Item5Cur.Text = "N/A";
                Item6Ori.Text = "N/A";
                Item6Cur.Text = "N/A";
                Item7Ori.Text = "N/A";
                Item7Cur.Text = "N/A";
                Item8Ori.Text = "N/A";
                Item8Cur.Text = "N/A";
                Item9Ori.Text = "N/A";
                Item9Cur.Text = "N/A";
                Item10Ori.Text = "N/A";
                Item10Cur.Text = "N/A";
                Item11Ori.Text = "N/A";
                Item11Cur.Text = "N/A";
                Item12Ori.Text = "N/A";
                Item12Cur.Text = "N/A";
                Item13Ori.Text = "N/A";
                Item13Cur.Text = "N/A";
                Item14Ori.Text = "N/A";
                Item14Cur.Text = "N/A";
                Item15Ori.Text = "N/A";
                Item15Cur.Text = "N/A";
                Item16Ori.Text = "N/A";
                Item16Cur.Text = "N/A";
                Item17Ori.Text = "N/A";
                Item17Cur.Text = "N/A";
                Item18Ori.Text = "N/A";
                Item18Cur.Text = "N/A";
                Item19Ori.Text = "N/A";
                Item19Cur.Text = "N/A";
                Item20Ori.Text = "N/A";
                Item20Cur.Text = "N/A";
            }
        }
        
        private void Close_Inv_Form_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
