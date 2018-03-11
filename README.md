# C-Sharp-Async

Async programming is very easy to easy in C# and it can be done by several ways. Here are some basic examples which are very easy to help you understand async programming.

1. Using exsting synchronous method:
  Suppose we have a normal method
  
        public static string GetFullString(string s)
        {
            return s+" "+s;
        }
  
  To make this asynchronous, we can simply add one method called GetFullStringAsync (typically an asynchronous method ends with Async)
  
        public static Task<string> GetFullStringAsync(string s)
        {
            return Task.FactoryStartNew(()=>GetFullString(s));
        }
  
  Here, we use Task.Run to start the synchronous method.
  
  If you want to call synchronous method, you may write some codes in the Main function like these:
  
      string res = GetFullString("Hello");
      Console.WriteLine(res);
      
  or
    
      Console.WriteLine(GetFullString("Hello"));
      
  Now, if you run the code, you should see **Hello Hello**
  
  Or, if you write `Console.WriteLine(GetFullStringAsync("Hello-Async").Result); //call with async method`
  You should see the output **Hello-Async Hello-Async".** Where is the difference?
\
\
\
\
  Add `System.Threading.Thread.Sleep(2000); // suppose this method takes 2s to finish` in GetFullString method and
  modify the Main function as 
  
    Console.WriteLine(GetFullStringAsync("Hello-Async"));
    Console.WriteLine(GetFullStringAsync("Hello-Async"));
  
  You may think these codes have different result as `Console.WriteLine(GetFullString("Hello"));` as we used Async methods. However, if you run the program. You will see the 1st output comes out after 2s and second is 2s later. 
  The reason is in the Main function, we write two statements so it will only start statement 2 whenever the 1st statement finish. Change the codes as:
  ```
  Task.Factory.StartNew(() => Console.WriteLine(GetFullStringAsync("Hello-Async"))});
  Task.Factory.StartNew(() => Console.WriteLine(GetFullStringAsync("Hello-Async"))});
  ```
  When you run the code, you respect expect to see two lines outputs but there is nothing in fact. What's the problem now?
  
  The reason is we start to call the asynchronous method from a new task without wait it (same concept when using Thread and Join, Wait). To test this, add `Console.WriteLine("Main function finish");` before the end of Main function. Run again and you should see ` Main function finsh`. Now we know the program exit after this statement. 
\
\
\
\
\
**How to fix the problem?**
  
  As we know the waitting time is 2 seconds, we can add `System.Threading.Thread.Sleep(3000);` in the Main function. Now, if you run the codes, you can see the two lines outputs print at the same time after the first 2s. Then, `Main function finish` after 1s.
  
  However, what if we don't know the execution time? This is very simple to play with Task class.
  \
  \
  Add one more function
  ```
        public static async Task PrintDoubleString(string s)
        {
            Console.WriteLine(await GetFullStringAsync(s));
        }
  ```
  In the main function, call as `Task.WaitAll( PrintDoubleString("Hello"), PrintDoubleString("Hello"));` Now the program is working!
  \
  \
  \
  \
  2. Async with Lamda expressions
  
  Asynchronous functions can be easily written by Func<> and Action<> with Lamda expressions and here are two examples. Anything with asynchornous is same as before.
```
Action<string> a1=new Action<string>((x)=>{
    Console.WriteLine(x+" "+x); 
});
Task.WaitAll(Task.Factory.StartNew(()=>a1("Hello-Action<>")));

Func<string,Task<string>> f1=new Func<string, Task<string>>((x)=>{
    return Task.Factory.StartNew(() => GetFullString(x));
});

Console.WriteLine(Task.Factory.StartNew(()=>f1("Hello-Func<>")).Result.Result);
```
 
 If you run the code, should see
```
Hello-Action<> Hello-Action<>
Hello-Func<> Hello-Func<>
Main function finish
```
