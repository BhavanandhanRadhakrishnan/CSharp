//This is an EPOS application for B's Cafe that manages orders,stock, sales and reports appropriately
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bhavan_BAP_Assignment4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            //On Form load enables and disables accordingly and assignes stork to temporary variable
            Login_Panel.Visible = true;
            Orders_Panel.Visible = false;
            Cart_ListBox.Visible = true;
            Search_GroupBox.Visible = false;
            Summary_GroupBox.Visible = false;
            Array.Copy(Stock, Temp, Stock.Length);

        }

        //Global Variables Diclared
        const string Pwd = "000000";
        int Counter = 3;
        int LoginCounter = 0;
        int[,] rateArray = {{12,14,16,18,20},{15,17,19,13,11},{18,17,16,15,14},{16,18,13,12,14},
                            {1,2,3,4,5},{20,21,22,23,24},{25,26,27,24,23},{11,12,13,14,15},{16,15,14,13,12},
                            {7,8,9,10,11},{12,14,16,18,20},{15,17,19,13,11},{18,17,16,15,14},{16,18,13,12,14},
                            {1,2,3,4,5},{20,21,22,23,24},{25,26,27,24,23},{11,12,13,14,15},{16,15,14,13,12},
                            {7,8,9,10,11}};
        string[] Items = { "Beaf Burger", "Ham Brger", "Spicy chicken Burger", "Chicken Overloaded Burger", "Spicy Beaf Burger", "Chicken Tikka Burger",
            "Tofu Tikka Burger", "Veg Cheessy Burger", "Sweet Corn Burger", "Becon Burger", "Ham Special Burger",
            "Bs Spicy Burger", "B's Chicken Supreme", "B's Special Beaf Burger", "Sother Fried Checken Burger",
            "Egg Loaded Checken Burger","Double Checken Treat Burger", "B's Supreme Burger", "All tile Favorite Burger","Chef's Special Burger" };
        String[] Extras = {"Cheese", "Double Cheese","Egg Bulseye","Double Patty","No Extras" };
       
        int[] Stock = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 150, 250, 320, 450, 550, 650, 750, 850, 950, 1050 };
        //int[] Temp_Stock = { };
        int[] Temp = new int[20];
        int[] Sale_Temp = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };              
        string Total_Revenue;
        string Average_Revenue;
        string Total_Transactions;

       
        private void button1_Click(object sender, EventArgs e)
        {
            string Password = this.PasswordTextBox.Text;
            //If entered Password matches
            if (Password == Pwd) 
            {
                Orders_Panel.Visible = true;
                Login_Panel.Visible = false;

            }
            //3  attempts for wrong password entry
            else
            {
                Counter--;
                LoginCounter++;
                //If 3 wrong entries are done closes the application
                if (LoginCounter >= 3) 
                {
                    MessageBox.Show("Password Attempt Exceded, Please try again later");
                    this.Close();
                }
                //Displays number of entries left
                else
                {
                    Pass_Message.Text = "Invalid Password Attempt Left: " + Counter;

                }
            }
        }

        //To get current date in "dd-MM-yyyy" format
        private string GetDate()
        {
            
            return DateTime.Now.ToString("dd-MM-yyyy");
        }
        //To generate transaction ID
        private string GenerateUniqueID()
        {
            int i = 0;
            int tempstorage;
            Random a = new Random();
            string UniqueID = "";
            //6 digit number will be generated
            while (i < 6)
            {
                tempstorage = a.Next(0, 6);
                UniqueID += tempstorage;
                i++;
            }
            return UniqueID;
        }

        //Used to clear selections and text boxes
        private void ClearSelectionCart()
        {

            Quantity_TextBox.Clear();
            Calc_Price_textBox.Clear();
            ItemsListBox.SelectedIndex = -1;
            Extras_ListBox.SelectedIndex = -1;
        }

        //Based on the selections Value is displayed in the text box only if clicked it is not mandatory to add to cart
        private void Calculate_Button_Click(object sender, EventArgs e)
        {
            int Item_Index;
            int Extras_Index;
            int Rate, Qty = 0;
           

            //Atlease one item should be selected
            if (ItemsListBox.SelectedIndex != -1)
            {
                //Atleast any one extra should be selected
                if (Extras_ListBox.SelectedIndex != -1)
                {
                    Extras_Index = Extras_ListBox.SelectedIndex;
                    Item_Index = ItemsListBox.SelectedIndex;
                    Rate = rateArray[Item_Index, Extras_Index];
                    //Quantity is a mandatory fiels
                    try
                    {
                        Qty = int.Parse(Quantity_TextBox.Text);
                    }
                    catch //Quantity not entered
                    {
                        MessageBox.Show("Enter valid number in quantity text box", "Specify Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Quantity_TextBox.Focus();
                        return;
                    }

                    if (!int.TryParse(Quantity_TextBox.Text, out Qty) || Qty <= 0)
                    {
                        MessageBox.Show("Enter a valid positive number in the quantity text box", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Quantity_TextBox.Focus();
                        return; 
                    }

                    //Total rate Calculates
                    decimal Total_Rate = Rate * Qty;

                    Calc_Price_textBox.Text = Total_Rate.ToString("0.00") + " €";
                }
                else //if extra not selected
                {
                    MessageBox.Show("Please, select one option from the extras list!", "Select Extras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else //if item not selected
            {
                MessageBox.Show("Please, select one item from the list!", "Select an Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Clears all the selections and textbox
        private void Clear_Selection_Button_Click(object sender, EventArgs e)
        {
            ClearSelectionCart();
        }
        //Add's selected items to the list box
        private void AddToCart_Button_Click(object sender, EventArgs e)
        {
            Cart_ListBox.Visible = true;
            int Item_Index;
            int Rate, Qty = 0; 
            int Extras_Index;

            if (ItemsListBox.SelectedIndex != -1) //Item Selected
            {
                if (Extras_ListBox.SelectedIndex != -1) //Extra selected
                {
                    Extras_Index = Extras_ListBox.SelectedIndex;
                    Item_Index = ItemsListBox.SelectedIndex;
                    String Selction = Items[Item_Index];
                    String Selection2 = Extras[Extras_Index];
                    Rate = rateArray[Item_Index, Extras_Index];
                    int quantityleft = Temp[Item_Index];
                    try
                    {
                        Qty = int.Parse(Quantity_TextBox.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Enter valid number in quantity text box");
                        Quantity_TextBox.Focus();
                        return;
                    }
                    if (!int.TryParse(Quantity_TextBox.Text, out Qty) || Qty <= 0) //Negative number or zero
                    {
                        MessageBox.Show("Enter a valid positive number in the quantity text box", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Quantity_TextBox.Focus();
                        return;
                    }
                    //Check If we have required quantity and shows how much is left if there is less 
                    if (Qty <= Temp[Item_Index])
                    {
                        decimal Total_Rate = Rate * Qty;
                        ClearSelectionCart();
                        string Item2Cart = $"{Qty} X {Selction + "(" + Selection2 + ")"}: {Total_Rate}";
                        Cart_ListBox.Items.Add(Item2Cart);                        
                        Temp[Item_Index] -= Qty;
                        Sale_Temp[Item_Index] += Qty;
                    }
                    else //Quantity not entered
                    {
                        MessageBox.Show($"Quantity Exceeds for {Selction} \nQuanty Left is: {quantityleft}", "Quantity Exceeds", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else //extra not selected
                {
                    MessageBox.Show("Please, select one option from the extras list!", "Select Extras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else //item not selected
            {
                MessageBox.Show("Please, select one item from the list!", "Item Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //clears the cart
        private void ClearCart_Button_Click(object sender, EventArgs e)
        {
            Cart_ListBox.Items.Clear();
            ClearSelectionCart();
        }
        //confirms the items in the list to place order
        private void CheckOut_Button_Click(object sender, EventArgs e)
        {
            if (Cart_ListBox.Items.Count > 0)
            {
                decimal CheckOut_Total = 0;

                
                foreach (string Cart_Item in Cart_ListBox.Items)
                {
                    // Extract the total amount from the cart item string
                    string[] parts = Cart_Item.Split(':');
                    if (parts.Length == 2 && decimal.TryParse(parts[1].Trim(), out decimal itemTotal))
                    {
                        CheckOut_Total += itemTotal;
                    }
                }

                // Assign transaction ID and get current date
                string transactionId = GenerateUniqueID();
                string transactionDate = GetDate();

                // Ask for confirmation with transaction ID
                DialogResult transactionresult = MessageBox.Show($"Transaction Number:  {transactionId}\n\n Total Amount : {CheckOut_Total.ToString("0.00") } € \n\nDo you want to confirm the Order?\n\n If so kindly click Yes", "Checkout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (transactionresult == DialogResult.Yes)
                {
                    // Write transaction information in "transactions.txt" file
                    using (StreamWriter writer = new StreamWriter("transactions.txt", true))
                    {
                        writer.WriteLine(transactionId);
                        writer.WriteLine(transactionDate);
                        writer.WriteLine(string.Join("|", Cart_ListBox.Items.Cast<string>()));
                        writer.WriteLine(CheckOut_Total);

                    }
                    
                    MessageBox.Show("Thank you, Checkout successful!", "Checkout Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Clear list after confirmed
                    Cart_ListBox.Items.Clear();

                }
                else //If clicked on No
                {
                    MessageBox.Show("Checkout Canceled.", "Checkout Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else //if no item added to card
            {
                MessageBox.Show("Cart is empty.", "Invalid CheckOut", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //On clicked Displays the pannel with the options
        private void Search_Button_Click(object sender, EventArgs e)
        {
            ClearSelectionCart();
            Search_GroupBox.Visible = true;
            S_Transaction_ID_RadioButton.Checked = false;
            S_Date_RadioButton.Checked = false;
            Search_Result.Visible = false;
            Summary_GroupBox.Visible = false;
        }

        private void Find_Button_Click(object sender, EventArgs e)
        {
            
            string searchInput = Search_TextBox.Text;
            //Text should be entered to search
            if (!string.IsNullOrWhiteSpace(searchInput))
            {
                string[] lines = File.ReadAllLines("transactions.txt");
                //Search with Transaction ID
                if (S_Transaction_ID_RadioButton.Checked)
                {
                    //List Collection used
                    List<string> matchingTransactions = new List<string>();
                    for (int i = 0; i < lines.Length; i += 4)
                    {
                        if (lines[i].Equals(searchInput))
                        {
                            // Add the entire transaction details to the matching list
                            matchingTransactions.AddRange(lines.Skip(i).Take(4));
                        }
                    }

                    SearchResultsDisplay(matchingTransactions);
                }
                //Search with Date
                else if (S_Date_RadioButton.Checked)
                {                    
                    List<string> matchingTransactions = new List<string>();
                    for (int i = 1; i < lines.Length; i += 4)
                    {
                        if (lines[i].Equals(searchInput))
                        {
                            //add to list
                            matchingTransactions.AddRange(lines.Skip(i - 1).Take(4));
                        }
                    }

                    SearchResultsDisplay(matchingTransactions);
                }
                else
                {
                    MessageBox.Show("Please select a search criterion.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a search term.");
            }
        }
        //Function to Display the mathingTransactions list in SearchResult list box
        private void SearchResultsDisplay(List<string> matchingTransactions)
        {
            if (matchingTransactions.Count > 0)
            {
                Search_Result.Items.Clear();
                Search_Result.Visible = true;
                //each line added
                foreach (var transaction in matchingTransactions)
                {
                    Search_Result.Items.Add(transaction);
                }
            }
            else //If not match
            {
                Search_Result.Visible = false;
                MessageBox.Show("No matching transactions found.");
            }
        }
        //Close the search window(Panel)
        private void Close_Search_Button_Click(object sender, EventArgs e)
        {
            Search_GroupBox.Visible = false;
            S_Transaction_ID_RadioButton.Checked = false;
            S_Date_RadioButton.Checked = false;
            Search_Result.Visible = false;
            Summary_GroupBox.Visible = false;
        }

        //Shows sales report options and dashboard
        private void Summary_Button_Click(object sender, EventArgs e)
        {
            Summary_GroupBox.Visible = true;
            Summary_Label.Visible = true;
            Search_GroupBox.Visible = false;
            S_Transaction_ID_RadioButton.Checked = false;
            S_Date_RadioButton.Checked = false;
            Search_Result.Visible = false;
            ClearSelectionCart();
            // Read all the lines from transactions.txt file
            string[] lines = File.ReadAllLines("transactions.txt");

            // Calculate total number of transactions 
            //As each transaction occupies 4 lines written in the file
            int totalTransactions = lines.Length / 4; 

            // Calculate total revenue and average revenue
            decimal totalRevenue = 0;
            //gets the line based on the remained the line leaveas when index of the line being devided by 4
            // Selecting lines containing total revenue of each transaction
            foreach (string line in lines.Where((x, index) => index % 4 == 3)) 
            {
                if (decimal.TryParse(line, out decimal transactionTotal))
                {
                    totalRevenue += transactionTotal;
                }
            }

            //Average Revenue is assigned when the number of transaction is greater than zero
            //Condition eveluated based on ternary orerator
            decimal averageRevenue = totalTransactions > 0 ? totalRevenue / totalTransactions : 0;

            Total_Transaction_TextBox.Text = totalTransactions.ToString();
            Avg_Revenue_TextBox.Text = averageRevenue.ToString("0.00") + " € ";
            Total_Revenue_TextBox.Text = totalRevenue.ToString("0.00") + " € ";
            //Assigning vales so that it could be print in Summary file
            Total_Revenue = totalRevenue.ToString("0.00") + " € "; 
            Average_Revenue = averageRevenue.ToString("0.00") + " € ";
            Total_Transactions = totalTransactions.ToString();
        }
        //Generates the sales report file
        private void Sales_Report_Button_Click(object sender, EventArgs e)
        {
            string CurrentDate = GetDate();
            try
            {
                using (StreamWriter writer = new StreamWriter("Sales_Report.txt", false))
                {
                    //Sales_Report file will be written in the below format
                    writer.WriteLine(new string('_', 100));
                    writer.WriteLine("Total Company Sales Report");
                    writer.WriteLine("Date: " + CurrentDate);
                    writer.WriteLine(new string('_', 100));
                    writer.WriteLine(("Total Transactions: " + Total_Transactions));
                    writer.WriteLine("Total Revenue: " + Total_Revenue);
                    writer.WriteLine(("Average Revenue: " + Average_Revenue));
                    writer.WriteLine(new string('_', 100));
                    writer.WriteLine("Last Session Sales Report");
                    writer.WriteLine(new string('_', 100));
                    writer.WriteLine("Item Name : Quantity Sold");    
                    //Writes each item and their quantity
                    for (int i = 0; i < Items.Length; i++)
                    {
                        writer.WriteLine($"{Items[i]}: {Sale_Temp[i]}");
                    }
                    writer.WriteLine(new string('_', 100));
                    MessageBox.Show("Sales report generated successfully!", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Sales report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Brings back the stock to default
        private void Reset_Stock_Button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                Temp[i] = Stock[i];
            }
            MessageBox.Show("Stock has been reset.");
        }

        //Writes the inventory report with item name, initial stock and current stock
        private void Print_Inventory_Report_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("Inventory_Report.txt", false))
                {
                    writer.WriteLine("Inventory Report");
                    writer.WriteLine(new string('_', 100));
                    writer.WriteLine($"Item: Initial Stock | Current Available Stock");
                    writer.WriteLine(new string('_', 100));

                    //Iterate through Items array and fetch current quantity from Temp array and initial quantity from stock
                    for (int i = 0; i < Items.Length; i++)
                    {                        
                        writer.WriteLine($"{Items[i]}: {Stock[i]} | {Temp[i]}");
                    }

                    MessageBox.Show("Inventory report generated successfully!", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating inventory report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                

        private void Close_SalesReport_Button_Click(object sender, EventArgs e)
        {
            Summary_GroupBox.Visible = false;
        }
                
        //Button to open the inventory form which has the stock details
        private void View_Stock_Button_Click(object sender, EventArgs e)
        {
            Inventory InvReportForm = new Inventory();            
            InvReportForm.LoadValues(Items, Stock, Temp);
            InvReportForm.ShowDialog();

        }

        //Close the Application
        private void Close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
