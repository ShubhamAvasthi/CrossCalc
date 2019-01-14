using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossCalc
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        double num1 = 0, num2 = 0;
        char op = ' ';
        bool isnum1 = false, isnum2 = false;
        bool sign1 = false, sign2 = false, result = false;
        private void Button_Clicked(Button sender, EventArgs e)
        {
            bool divideByZeroError = false;
            if (sender.Text == "C")
            {
                num1 = num2 = 0;
                op = ' ';
                result = sign1 = sign2 = isnum1 = isnum2 = false;
            }
            else if (Char.IsDigit(sender.Text[0]))
            {
                if (op == ' ')
                {
                    if(result)
                    {
                        result = false;
                        sign1 = false;
                        num1 = Convert.ToInt64(sender.Text);
                    }
                    else
                        num1 = 10 * num1 + Convert.ToInt64(sender.Text);
                    if(num1 != 0 || sign1 == true)
                        isnum1 = true;
                }
                else
                {
                    num2 = 10 * num2 + Convert.ToInt64(sender.Text);
                    isnum2 = true;
                }
            }
            else
            {
                if (!isnum1 && sender.Text[0] == '-')
                    sign1 = true;
                else if (!isnum2 && op != ' ' && sender.Text[0] == '-')
                    sign2 = true;
                else if (isnum1)
                {
                    if (isnum2)
                    {
                        num1 *= (sign1 ? -1 : 1);
                        num2 *= (sign2 ? -1 : 1);
                        switch (op)
                        {
                            case '+': num1 += num2; break;
                            case '-': num1 -= num2; break;
                            case '×': num1 *= num2; break;
                            case '÷':
                                if (op == '÷' && num2 == 0)
                                {
                                    num1 = 0;
                                    result = false;
                                    divideByZeroError = true;
                                }
                                else
                                    num1 /= num2; break;
                        }
                        op = ' ';
                        num2 = 0;
                        isnum2 = sign2 = false;
                        isnum1 = !divideByZeroError;
                        result = !divideByZeroError;
                        if (num1 < 0)
                        {
                            sign1 = true;
                            num1 *= -1;
                        }
                        else
                            sign1 = false;
                    }
                    if (sender.Text[0] != '=' && sign2 != true)
                        op = sender.Text[0];
                }
                
            }
            if (divideByZeroError)
                screen.Text = "Divide by Zero Error";
            else
            {
                screen.Text = (sign1 ? "-" : "") + (isnum1 ? num1.ToString((result ? "N" : "")) : "");
                if (op != ' ')
                    screen.Text += " " + op;
                if (sign2 || isnum2)
                    screen.Text += " " + (sign2 ? "-" : "") + (isnum2 ? num2.ToString() : "");
            }
        }
    }
}
