using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace boxalino_client_SDK_CSharp
{
   public class Common
    {

        public static HttpCookie cookie;
        static SessionIDManager manager = new SessionIDManager();
      

        public static void session_start()
        {
            string newID = manager.CreateSessionID(System.Web.HttpContext.Current);
            bool redirected = false;
            bool isAdded = false;
            manager.SaveSessionID(System.Web.HttpContext.Current, newID, out redirected, out isAdded);
        }
        public static string session_id()
        {
            return System.Web.HttpContext.Current.Session.SessionID;
        }

        public static void setcookie(HttpCookie cookie)
        {
            HttpContext.Current.Request.Cookies.Add(cookie);
        }

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            
            for (int i = 1; i <= n; i++)
            {
               
                for (int j = 1; j <= m; j++)
                {
                  
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
           
            return d[n, m];
        }

        public static string[] array_keys(Dictionary<string, List<string>> dict)
        {
            return dict.Keys.ToArray();
        }
        public static void unlink(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
        }

        public static void AddKeyValuePair<K, V>(IDictionary<K, V> me, KeyValuePair<K, V> other)
        {
            me.Add(other.Key, other.Value);
        }

        public static string file_get_contents(string fileName)
        {

            string sContents = string.Empty;
            if (fileName.ToLower().IndexOf("http:") > -1)
            {
              
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] response = wc.DownloadData(fileName);
                sContents = System.Text.Encoding.ASCII.GetString(response);
            }
            else
            {
               
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                sContents = sr.ReadToEnd();
                sr.Close();
            }
            return sContents;
        }

        public static string PHPMd5Hash(string pass)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.UTF8.GetBytes(pass);
                byte[] hash = md5.ComputeHash(input);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static int strpos(string haystack, string needle, int offset = 0)
        {
            return haystack.IndexOf(needle, offset);
        }

        public static List<T> array_shift<T>(List<T> items)
        {
            List<T> list = new List<T>();
            list.Add(items.FirstOrDefault());
            items.RemoveAt(0);
            return list;
        }

        public static bool method_exists(object obj, string method_name)
        {
            MethodInfo methodInfo = obj.GetType().GetMethod(method_name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            return (methodInfo != null);
        }


       
    }

    public class NestedDictionary<K, V> : Dictionary<K, NestedDictionary<K, V>>
    {
        public V Value { set; get; }

        public new NestedDictionary<K, V> this[K key]
        {
            set { base[key] = value; }

            get
            {
                if (!base.Keys.Contains<K>(key))
                {
                    base[key] = new NestedDictionary<K, V>();
                }
                return base[key];
            }
        }
    }
    public class SemiNumericComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (IsNumeric(s1) && IsNumeric(s2))
            {
                if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
                if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
                if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
            }

            if (IsNumeric(s1) && !IsNumeric(s2))
                return -1;

            if (!IsNumeric(s1) && IsNumeric(s2))
                return 1;

            return string.Compare(s1, s2, true);
        }

        public static bool IsNumeric(object value)
        {
            try
            {
                int i = Convert.ToInt32(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
