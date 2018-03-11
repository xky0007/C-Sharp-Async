using System;
using System.Threading.Tasks;
namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintDoubleString("Hello");

            Action<string> a1=new Action<string>((x)=>{
                Console.WriteLine(x+" "+x); 
            });
            Task.Run(()=>a1("Hello1"));

            Func<string,Task<string>> f1=new Func<string,Task<string>>(async (x)=>{
                return await GetFullStringAsync(x);
            });

            var res=Task.Factory.StartNew(()=>f1("hello2"));
            Console.WriteLine(res.Result.Result);
            System.Threading.Thread.Sleep(1000);
        }

        public static string GetFullString(string s)
        {
            return s+" "+s;
        }

        public static Task<string> GetFullStringAsync(string s)
        {
            return Task.Run(()=>GetFullString(s));
        }

        public static async void PrintDoubleString(string s)
        {
            Console.WriteLine(await GetFullStringAsync(s));
        }
    }
}
