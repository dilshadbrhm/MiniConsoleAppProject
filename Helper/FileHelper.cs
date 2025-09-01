using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniConsoleAppProject.Helper
{
    internal class FileHelper
    {
        public static void Serialize<T>(string path,List<T> value)
        {
            string json = JsonConvert.SerializeObject(value);
            using(StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(json); 
            }

        }
        public static  List<T> Deserialize<T>(string path)
        {
            if (!File.Exists(path))
            {
                 return new List<T>();  
            }
            using(StreamReader sr = new StreamReader(path))
            {
                string json =sr.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }
    }
}
