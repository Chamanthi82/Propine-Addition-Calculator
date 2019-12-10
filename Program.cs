using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;





namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DriverInstance instance = new DriverInstance(WebDriver.Firefox);
            DataSource source = new DataSource(DataSource.DataType.Excel);

            //-------------Configuration Section------------
            int rowCount = 12;
            string sum = string.Empty;
            string val1 = string.Empty;
            string val2 = string.Empty;
            string excelSum = string.Empty;
            //----------------------------------------------

            Console.WriteLine("--------------------------QA Summary-----------------------------------");

            try
            {
                for (int i = 1; i <= rowCount; i++)
                {
                    val1 = source.Range.Cells[i, 1].Value2.ToString();
                    val2 = source.Range.Cells[i, 2].Value2.ToString();
                    excelSum = source.Range.Cells[i, 3].Value2.ToString();


                    instance.SetElementValue("firstNumber", val1);
                    instance.SetElementValue("secondNumber", val2);
                    instance.SubmitForm(string.Empty, "btn-default");

                    sum = instance.ReadValue(TagType.ByXPath, ".//div[2]/div/div/div", ElementType.Div);

                    instance.ClearTextBox(TagType.ByName, "firstNumber");
                    instance.ClearTextBox(TagType.ByName, "secondNumber");

                    if (sum.Equals(excelSum))
                    {
                        Console.WriteLine("Line {0} validated and Status is {1}", i, "Pass");
                    }
                    else
                    {
                        Console.WriteLine("Line {0} validated and Status is {1}", i, "Fail");
                    }
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No such Element found. Plese check Element name provided");
            }
            catch (NotFoundException)
            {
                Console.WriteLine("No such Element found. Plese check Element name provided");
            }

            Console.WriteLine("--------------------------QA Summary End -------------------------------");

        }
    }
}
