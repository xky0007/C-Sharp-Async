using System;
using System.Threading.Tasks;
namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrintDoubleString("Hello");

            // Task.Factory.StartNew(() =>
            // {
            //     Console.WriteLine(GetFullStringAsync("Hello-Async").Result);

            // });
            // Task.Factory.StartNew(() =>
            // {
            //     Console.WriteLine(GetFullStringAsync("Hello-Async").Result);

            // });

            
            // Task.WaitAll( PrintDoubleString("Hello"), PrintDoubleString("Hello"));

            Action<string> a1=new Action<string>((x)=>{
                Console.WriteLine(x+" "+x); 
            });
            Task.WaitAll(Task.Factory.StartNew(()=>a1("Hello-Action<>")));

            Func<string,Task<string>> f1=new Func<string, Task<string>>((x)=>{
                return Task.Factory.StartNew(() => GetFullString(x));
            });
            
            Console.WriteLine(Task.Factory.StartNew(()=>f1("Hello-Func<>")).Result.Result);
            //System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Main function finish");

        }

        public static string GetFullString(string s)
        {
            System.Threading.Thread.Sleep(2000);
            return s + " " + s;
        }

        public static Task<string> GetFullStringAsync(string s)
        {
            return Task.Factory.StartNew(() => GetFullString(s));
        }

        public static async Task PrintDoubleString(string s)
        {
            Console.WriteLine(await GetFullStringAsync(s));
        }
    }
}
