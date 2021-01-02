using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp2
{
    class BrowserPart
    {


        // selenium browser to log into steam
        public static void Login()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://steamcommunity.com/login/home/?goto=");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press any key once logged!");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var elements in driver.Manage().Cookies.AllCookies)
            {
                try
                {
                    System.Net.Cookie cookie = new System.Net.Cookie(elements.Name, elements.Value);
                    cookie.Domain = "steamcommunity.com";
                    HttpTasks.cookies.Add(cookie);
                }
                catch (Exception) { }
            }
            driver.Quit();
        }
    }
}
