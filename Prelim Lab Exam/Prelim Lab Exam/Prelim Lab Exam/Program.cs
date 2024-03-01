using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prelim_Lab_Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            logIn(); 
        }
        static void logIn()
        {
            int[] Stock = { 30, 50, 50, 50, 80, 25, 100, 65, 50 };
            int[,] temp = new int[10,1];
            int[] Cost = {20, 25, 15, 8, 5, 75, 25, 40, 25};
            string[] StockName = {"Shampoo", "Bar of Soap", "Instant Noodles", "Chips", "Cracker", "Soda",
                "Bottled Water", "Sandwich Spread", "Soup Sachet"};
            string name = "Silayan", pass = "12345", nameInp, passInp, guestLogin;
            Console.WriteLine("Welcome to Saribert Store!: Please Log In!");
            Console.Write("Would You like to log in as a guest(customer)? Y/N: ");
            guestLogin = Console.ReadLine();
            guestLogin = guestLogin.ToUpper();
            if (guestLogin == "Y" || guestLogin == "YES")
            {
                Console.Clear();
                Guest(Stock, StockName, temp, Cost);
            }
            else if (guestLogin == "N" || guestLogin == "NO")
            {
                Console.Write("Enter Admin Name: ");
                nameInp = Console.ReadLine();
                Console.Write("Enter Password: ");
                passInp = Console.ReadLine();
                if (name == nameInp)
                {
                    if (pass == passInp)
                    {

                        Admin(Stock, StockName);
                    }
                }
            }
            else
            {
                Console.Write("Invalid Input, Please Try Again\n");
                logIn();
            }

        }
        static void Guest(int[] Items, string[] ItemName, int[,]  temp, int[] Cost)
        {
            int[,] Order = new int[10, 1];
            int[] OrdIndex = new int[9];
            Order = temp;
            int OrdTempHold, itemAmount, j = 0;
            string userInp;
            bool strngCheck;
            while (j < 10)
            {
                Console.WriteLine("Greetings Valuable Customer! What will you purchase today?");
                Console.WriteLine("Our Current Items in stock are: ");
                for (int i = 0; i < Items.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + Items[i] + " " + ItemName[i] + " " + Cost[i] + " pesos");
                }
                Console.Write("Input Item Number to Add to Cart. To Check out or Cancel the order, type CHECKOUT or CANCEL: ");
                userInp = Console.ReadLine();
                strngCheck = int.TryParse(userInp, out OrdTempHold);
                if (!strngCheck)
                {
                    userInp = userInp.ToUpper();
                    if (userInp == "CANCEL")
                    {
                        logIn();
                    }
                    else if (userInp == "CHECKOUT")
                    {
                        Console.Clear();
                        for (int i = j; i < OrdIndex.Length; i++)
                        {
                            OrdIndex[i] = 20;
                        }
                        ChkOut(Order, Items, ItemName, OrdIndex, Cost);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid User Input!");
                        Console.ReadLine();
                        Console.Clear();
                        Guest(Items, ItemName, Order, Cost);
                    }
                }
                else
                {
                    if (OrdTempHold < 9)
                    {
                        Console.Write("How many would you like to purchase?: ");
                        itemAmount = Convert.ToInt16(Console.ReadLine());
                        --OrdTempHold;
                        Order[OrdTempHold,0] = itemAmount;
                        OrdIndex[j] = OrdTempHold;
                        int Tpor = 0;
                        for (int i = 0; i < 9; i++)
                        {
                            Tpor = Tpor + Order[i,0];
                        }
                        if (Tpor == 10)
                        {
                            Console.WriteLine("10 Items are in cart, redirecting to checkout panel. Press ENTER");
                            Console.ReadLine();
                            Console.Clear();
                            for (int i = ++j; i < OrdIndex.Length; i++)
                            {
                                OrdIndex[i] = 20;
                            }
                            ChkOut(Order, Items, ItemName, OrdIndex, Cost);
                        }
                        else if (Tpor > 10)
                        {
                            Console.Clear();

                            Console.WriteLine("Too Many items in cart, order will be cancelled. Redirecting to LogIn page");
                            Console.Clear();
                            logIn();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Item Number Invalid. Press ENTER to go back to item ordering");
                        Console.ReadLine();
                        Console.Clear();
                        Guest(Items, ItemName, Order, Cost);
                    }
                }
                j++;
                Console.Clear();
            }
            Console.WriteLine("10 Items are in cart, redirecting to checkout panel. Press ENTER");
            Console.ReadLine();
            ChkOut(Order, Items, ItemName, OrdIndex, Cost);
        }
        static void ChkOut(int[,] Order, int[] Items, string[] ItemName, int[] OrdIndex, int[] Cost)
        {
            double tot = 0;
            Console.WriteLine("This is your current shopping cart, would you like to check out now?");
            for (int i = 0; i < OrdIndex.Length; i++)
            {
                if (OrdIndex[i] < 9)
                {
                    Console.Write(Order[OrdIndex[i], 0] + " " + ItemName[OrdIndex[i]] + " Which Will Be " + (Cost[OrdIndex[i]]*Order[OrdIndex[i], 0]) + " Pesos\n");
                    tot = tot + (Cost[OrdIndex[i]] * Order[OrdIndex[i], 0]);
                }
                else
                {
                    break;
                }
            }
            tot = tot + (tot * 0.20);
            Console.Write("Total will be " + tot + " Pesos (20% VAT tax was applied), will you continue with the purchase? (Y/N): ");
            string userInp = Console.ReadLine();
            if (userInp == "Y" || userInp == "YES")
            {
                Console.Clear();
                Console.WriteLine(tot + " Pesos was deducted in your account, Thank you for purchasing, would you like to purchase more items?");
                for (int i = 0; i < OrdIndex.Length; i++)
                {
                    if (OrdIndex[i] < 9)
                    {
                        Items[OrdIndex[i]] = Items[OrdIndex[i]] - Order[OrdIndex[i], 0];
                    }
                    else
                    {
                        break;
                    }
                }
                Console.Write("Would you like to purchase more?(Y/N): ");
                userInp = Console.ReadLine();
                if (userInp == "YES" || userInp == "Y")
                {
                    Guest(Items, ItemName, Order, Cost);
                }
                else
                {
                    logIn();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Order Cancelled, Redirecting to logIn page!");
                Console.ReadLine();
                logIn();
            }
        }

        static void Admin(int[] Stock, string[] StockName)
        {
            Console.WriteLine("Welcome to the admin console, what would you like to do?");
        }
    }
}
