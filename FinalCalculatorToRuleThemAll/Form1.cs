using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalCalculatorToRuleThemAll
{
    public partial class Form1 : Form
    {
        private Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Kollar om en knapp blev tryckt. genom att göra Button btn = (Button)sender;
        /// Kollar sedan om knappen innehåller något i arrayen Operator vilket sådana fall lägger till den
        /// Annars lägg bara till
        /// Updatera textboxen med nya informationen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string output;
            if (this.is_operator(btn.Text))
            {
                output = " " + btn.Text + " ";
            }
            else
            {
                output = btn.Text;
            }

            updateDisplay(output);
        }
        /// <summary>
        /// Om textboxen är endast 0 så ersätts den med information
        /// Annars lägger den till siffrorna till befintliga siffror
        /// </summary>
        /// <param name="update"></param>
        /// <param name="replace"></param>
        private void updateDisplay(string update)
        {
            if (display.Text == "0")
            {
                display.Text = update;
            }
            else
            {
                display.Text += update;
            }
        }
        /// <summary>
        /// String array som innehåller alla räknesätten
        /// skickar tillbaka korresponderande "operand" vid knapp tryck
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public bool is_operator(string op)
        {
            string[] operands = new string[] { "+", "-", "^", "*", "/" };
            return operands.Contains(op);
        }
        /// <summary>
        /// Calculerar först Power of funktionen genom att konvertera num1^num2 till double. Sedan koverteras Power of till display
        /// Annars 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateBtn(object sender, EventArgs e)
        {
            // Blir lite messy vid calc om det skulle blandas power of med andra operands.
            string input = display.Text;
            if (input.Contains('^'))
            {
                input=input.Replace(".", ",");
                int index = input.IndexOf('^');
                try
                {
                    // Konverterar numret innan ^ till double samt den efter till num2
                    // Använder funktionen math.pow
                    // Susbtring 0 för att den ska använda talet före ^ och sedan talet efter ^
                    double num1 = Convert.ToDouble(input.Substring(0,index));
                    double num2 = Convert.ToDouble(input.Substring(index + 1));
                    display.Text = Convert.ToString(Math.Pow(num1, num2)).Replace(",", ".");
                }
                catch (Exception error)
                {
                    MessageBox.Show("error, ogiltigt input?(för stort elelr blandning av operands?)");
                }
            }
            else
            {
                // normal calc
                // Sätter in numrerna till en  DataTable där varje nummer individuelt "storas" i en
                // row
                try
                {
                    var calc = new DataTable().Compute(input, null);
                    display.Text = Convert.ToString(calc).Replace(",", ".");
                }
                catch (Exception error)
                {
                    MessageBox.Show("error, ogiltigt input? div/0?");
                }
            }
        }
        // För att ta bort senaste
        public void delClicked(object sender, EventArgs e)
        {
            //Substring används för att dela upp display till index där den börjar på 0 (behåller hela) och minskar med
            //Högsta indexen (senaste) 1 gång per tryck
            display.Text = display.Text.Substring(0, display.Text.Length - 1);
            if (display.Text.Length == 0)
            {
                display.Text = "0";
            }
        }
        //clear all
        public void clearClicked(object sender, EventArgs e)
        {
            display.Text = "0";
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
