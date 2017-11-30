using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VendingMachine
{
    public partial class VendingChoice : Form
    {
        int stock;
        double quarters;
        double dimes;
        double nickels;
        double cost1;
        double cost2;
        double cost3;
        double cost4;
        string item1;
        string item2;
        string item3;
        string item4;
        int numItem1;
        int numItem2;
        int numItem3;
        int numItem4;

        double deposit = 0.00;
        int countDollar = 0;
        int countQuarter = 0;
        int countDime = 0;
        int countNickel = 0;

        public VendingChoice()
        {
            InitialLoad();
            InitializeComponent();
        }

        private void VendingGroup_Enter(object sender, EventArgs e)
        {
            bool soldOut = false;
            double costItem1 = cost1;
            double costItem2 = cost2;
            double costItem3 = cost3;
            double costItem4 = cost4;

            if (Item1Button.Checked)
            {
                if (numItem1 == 0)
                    soldOut = true;
            }
            else if (Item2Button.Checked)
            {
                if (numItem2 == 0)
                    soldOut = true;
            }
            else if (Item3Button.Checked)
            {
                if (numItem3 == 0)
                    soldOut = true;
            }
            else if (Item4Button.Checked)
            {
                if (numItem4 == 0)
                    soldOut = true;
            }

            if (soldOut == false)
            {
                if (Item1Button.Checked)
                {
                    costItem1 -= deposit;
                    if (costItem1 < 0)
                        Display.Text = "$0.00";
                    else
                        Display.Text = cost1.ToString("$0.00");
                    if (costItem1 <= 0)
                        Dispence(costItem1, item1);
                }
                else if (Item2Button.Checked)
                {
                    costItem2 -= deposit;
                    if (costItem2 < 0)
                        Display.Text = "$0.00";
                    else
                        Display.Text = cost2.ToString("$0.00");
                    if (costItem2 <= 0)
                        Dispence(costItem2, item2);
                }
                else if (Item3Button.Checked)
                {
                    costItem3 -= deposit;
                    if (costItem3 < 0)
                        Display.Text = "$0.00";
                    else
                        Display.Text = cost3.ToString("$0.00");
                    if (costItem3 <= 0)
                        Dispence(costItem3, item3);
                }
                else if (Item4Button.Checked)
                {
                    costItem4 -= deposit;
                    if (costItem4 < 0)
                        Display.Text = "$0.00";
                    else
                        Display.Text = cost4.ToString("$0.00");
                    if (costItem4 <= 0)
                        Dispence(costItem4, item4);
                }
            }
            else
            {
                Display.Text = "Sold Out";
            }
        }

        private void PurchaseBox_Enter(object sender, EventArgs e)
        {
            Display.Text = deposit.ToString("$0.00");
            Item1Button.Checked = false;
            Item2Button.Checked = false;
            Item3Button.Checked = false;
            Item4Button.Checked = false;
        }

        private void DollarBox_Click(object sender, EventArgs e)
        {
            countDollar++;

            if (countDollar == 1)
            {
                deposit += 1;
                PurchaseBox_Enter(sender, e);
            }
            else
            {
                Display.Text = "Only 1 dollar bill per purchase.";
                countDollar = 1;
            }
        }

        private void QuarterBox_Click(object sender, EventArgs e)
        {
            deposit += 0.25;
            countQuarter++;
            PurchaseBox_Enter(sender, e);
        }

        private void DimeBox_Click(object sender, EventArgs e)
        {
            deposit += 0.10;
            countDime++;
            PurchaseBox_Enter(sender, e);
        }

        private void NickelBox_Click(object sender, EventArgs e)
        {
            deposit += 0.05;
            countNickel++;
            PurchaseBox_Enter(sender, e);
        }

        private void Item1Button_CheckedChanged(object sender, EventArgs e)
        {
            VendingGroup_Enter(sender, e);
        }

        private void Item2Button_CheckedChanged(object sender, EventArgs e)
        {
            VendingGroup_Enter(sender, e);
        }

        private void Item3Button_CheckedChanged(object sender, EventArgs e)
        {
            VendingGroup_Enter(sender, e);
        }

        private void Item4Button_CheckedChanged(object sender, EventArgs e)
        {
            VendingGroup_Enter(sender, e);
        }

        private void ReturnCash_Click(object sender, EventArgs e)
        {
            string phrase = "Returningin:\n";

            if (countDollar == 1)
                phrase += countDollar + " Dollar Bill.\n";
            if (countQuarter == 1)
                phrase += countQuarter + " Quarter.\n";
            else if (countQuarter > 1)
                phrase += countQuarter + " Quarters.\n";
            if (countDime == 1)
                phrase += countDime + " Dime.\n";
            else if (countDime > 1)
                phrase += countDime + " Dimes.\n";
            if (countNickel == 1)
                phrase += countNickel + " Nickel.\n";
            else if (countNickel > 1)
                phrase += countNickel + " Nickels.\n";


            if (deposit > 0)
            {
                Display.Text = "Returning " + deposit.ToString("$0.00");
                MessageBox.Show(phrase);
                deposit = 0;
                countDollar = 0;
                countQuarter = 0;
                countDime = 0;
                countNickel = 0;
                PurchaseBox_Enter(sender, e);
            }
            else
                MessageBox.Show("No Money to return.");
        }

        private void Dispence(double cash, string item)
        {
            bool isChange = true;
            double change = 0;
            int countQ = 0;
            int countD = 0;
            int countN = 0;

            if (Math.Round(cash, 2) == 0.00)
            {
                Display.Text = "Enjoy your " + item;
                if (item == item1)
                    numItem1--;
                else if (item == item2)
                    numItem2--;
                else if (item == item3)
                    numItem3--;
                else if (item == item4)
                    numItem4--;

                countDollar = 0;
                countQuarter = 0;
                countDime = 0;
                countNickel = 0;
                deposit = 0;
            }
            else
            {
                while (Math.Round(cash, 2) < 0 && isChange == true)
                {
                    while (Math.Round(cash, 2) <= -0.25 && Math.Round(quarters, 2) != 0.00)
                    {
                        cash += 0.25;
                        change += 0.25;
                        quarters -= 0.25;
                        countQ++;
                    }
                    while (Math.Round(cash, 2) <= -0.10 && Math.Round(dimes, 2) != 0.00)
                    {
                        cash += 0.10;
                        change += 0.10;
                        dimes -= 0.10;
                        countD++;
                    }
                    while (Math.Round(cash, 2) <= -0.05 && Math.Round(nickels, 2) != 0.00)
                    {
                        cash += 0.05;
                        change += 0.05;
                        nickels -= 0.05;
                        countN++;
                    }
                    if (Math.Round(cash, 2) != 0.00)
                        isChange = false;
                }

                if (isChange == true)
                {
                    string phrase = "Change Due: " + change.ToString("$0.00") + ".\n";

                    if (countQ == 1)
                        phrase += countQ + " Quarter.\n";
                    else if (countQ > 1)
                        phrase += countQ + " Quarters.\n";
                    if (countD == 1)
                        phrase += countD + " Dime.\n";
                    else if (countD > 1)
                        phrase += countD + " Dimes.\n";
                    if (countN == 1)
                        phrase += countN + " Nickel.\n";
                    else if (countN > 1)
                        phrase += countN + " Nickels.\n";

                    Display.Text = "Enjoy your " + item;
                    MessageBox.Show(phrase);
                    if (item == item1)
                        numItem1--;
                    else if (item == item2)
                        numItem2--;
                    else if (item == item3)
                        numItem3--;
                    else if (item == item4)
                        numItem4--;

                    deposit = 0;
                    Display.Text = deposit.ToString("$0.00");
                    countDollar = 0;
                    countQuarter = 0;
                    countDime = 0;
                    countNickel = 0;
                }
                else {
                    Display.Text = "Use exact change";
                }
            }
            Item1Button.Checked = false;
            Item2Button.Checked = false;
            Item3Button.Checked = false;
            Item4Button.Checked = false;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Item1Button.Checked = false;
            Item2Button.Checked = false;
            Item3Button.Checked = false;
            Item4Button.Checked = false;
            bool file = false;

            Display.Text = deposit.ToString("$0.00");

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Vending Machine Restock.";
                ofd.Filter = "Text Files (*.txt) | *.txt | All Files(*.*) | *.*";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string name = ofd.FileName;
                    StreamReader sr = new StreamReader(@name);
                    if (new FileInfo(@name).Length == 0)
                    {
                        sr.Close();
                    }
                    else
                    {
                        file = true;
                        FileLoad(name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (file == false)
            {
                InitialLoad();
                Item1Button.Text = item1;
                Item2Button.Text = item2;
                Item3Button.Text = item3;
                Item4Button.Text = item4;
            }
        }

        private void InitialLoad()
        {
            stock = 5;
            numItem1 = stock;
            numItem2 = stock;
            numItem3 = stock;
            numItem4 = stock;
            quarters = 0.25 * stock;
            nickels = 0.05 * stock;
            dimes = 0.10 * stock;
            cost1 = 0.80;
            cost2 = 0.95;
            cost3 = 0.60;
            cost4 = 1.15;
            item1 = "Peanuts";
            item2 = "Crackers";
            item3 = "Candy Bar";
            item4 = "Cookies";
        }

        private void FileLoad(string name)
        {
            int counter = 1;
            string line;
            stock = 5;

            quarters = 0.25 * stock;
            nickels = 0.05 * stock;
            dimes = 0.10 * stock;
            numItem1 = stock;
            numItem2 = stock;
            numItem3 = stock;
            numItem4 = stock;
            StreamReader fileName = new StreamReader(@name);

            while ((line = fileName.ReadLine()) != null)
            {
                if (counter == 1)
                {
                    item1 = line;
                    counter++;
                }
                else if (counter == 2)
                {
                    cost1 = double.Parse(line);
                    counter++;
                }
                else if (counter == 3)
                {
                    item2 = line;
                    counter++;
                }
                else if (counter == 4)
                {
                    cost2 = double.Parse(line);
                    counter++;
                }
                else if (counter == 5)
                {
                    item3 = line;
                    counter++;
                }
                else if (counter == 6)
                {
                    cost3 = double.Parse(line);
                    counter++;
                }
                else if (counter == 7)
                {
                    item4 = line;
                    counter++;
                }
                else if (counter == 8)
                {
                    cost4 = double.Parse(line);
                    counter++;
                }
            }
            fileName.Close();
            Item1Button.Text = item1;
            Item2Button.Text = item2;
            Item3Button.Text = item3;
            Item4Button.Text = item4;
        }
    }
}