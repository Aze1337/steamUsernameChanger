using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Security.AccessControl;

namespace ConsoleApp2
{
    class HttpTasks
    {
        public static CookieContainer cookies = new CookieContainer();
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string alphabetUrl = "0123456789";
        private static Random randomNum = new Random();
        public static string theFamousSessionId = "";



        public static void DoRun()
        {
            while (true)
            {
                GetId();
                System.Threading.Thread.Sleep(5000); // THIS IS WHERE YOU CHANGE DELAY. IT'S IN ms. 
            }
        }


        // get an current account user-id
        private static void GetId()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://steamcommunity.com/my/goto");
            req.Method = "GET";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
            req.CookieContainer = cookies;



            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();
                    int position = response.IndexOf("https://steamcommunity.com/id/");
                    position = response.IndexOf("id", position);
                    position = response.IndexOf("/", position) + 1;
                    int secondPos = response.IndexOf("/", position);
                    string userId = response.Substring(position, (secondPos - position));
                    GetSessionID(userId);
                }
            }
            catch (Exception)
            {
            }
        }


        //// scrape g_sessionID with the userId from GetId()
        private static void GetSessionID(string userId)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://steamcommunity.com/id/" + userId);
            req.Method = "GET";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
            req.CookieContainer = cookies;
            req.Referer = "https://steamcommunity.com/login/home/?goto=";

            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();
                    int position = response.IndexOf("g_sessionID");
                    position = response.IndexOf('"', position) + 1;
                    int secondPos = response.IndexOf('"', position);
                    string gSessionId = response.Substring(position, (secondPos - position));
                    EditProfileInfo(userId, gSessionId);
                }
            }
            catch (Exception)
            {
            }
        }



        public static string GenerateWebKitFormId()
        {
            string generatedString = "";
            for (int i = 0; i < 34; i++)
            {
                int randomNumber = randomNum.Next(0, alphabet.Length);
                generatedString += alphabet[randomNumber];
            }
            return generatedString;
        }

        public static string GenerateUrlId()
        {
            string generatedString = "";
            for (int i = 0; i < 17; i++)
            {
                int randomNumber = randomNum.Next(0, alphabetUrl.Length);
                generatedString += alphabetUrl[randomNumber];
            }
            return generatedString;
        }


        // use previously scraped userid, sessionid and random generating string
        private static void EditProfileInfo(string userId, string gSessionId)
        {
            string webKitId = GenerateWebKitFormId();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://steamcommunity.com/id/" + userId + "/edit/");
            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
            req.CookieContainer = cookies;
            req.ContentType = "multipart/form-data; boundary=----WebKitFormBoundary" + webKitId;

            string postData = "------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"sessionID\"\r\n\r\n" + gSessionId + "\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"type\"\r\n\r\nprofileSave\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"weblink_1_title\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"weblink_1_url\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"weblink_2_title\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"weblink_2_url\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"weblink_3_title\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"weblink_3_url\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"personaName\"\r\n\r\n" + GenerateUrlId() + "\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"real_name\"\r\n\r\n" + GenerateUrlId() + "\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"customURL\"\r\n\r\n" + GenerateUrlId() + "\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"country\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"state\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"city\"\r\n\r\n\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"summary\"\r\n\r\nNo information given.\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"type\"\r\n\r\nprofileSave\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"sessionID\"\r\n\r\n" + gSessionId + "\r\n------WebKitFormBoundary" + webKitId + "\r\nContent-Disposition: form-data; name=\"json\"\r\n\r\n1\r\n------WebKitFormBoundary" + webKitId + "--\r\n";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            try
            {
                using (Stream writer = req.GetRequestStream())
                {
                    writer.Write(data, 0, data.Length);
                }
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Console.WriteLine("life is good just remember that");
            }
            catch (Exception)
            {
            }
        }

    }
}
