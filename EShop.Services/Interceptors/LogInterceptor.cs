using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShop.Services.Interceptors
{
    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            StringBuilder premsg = new StringBuilder();

            premsg.Append("Metoda: ").AppendLine(invocation.Method.Name);
            premsg.AppendLine("Parametry: ");
            Stopwatch stopwatch = new Stopwatch();

            foreach (var arg in invocation.Arguments)
            {
               premsg.Append("[").Append(JsonSerializer.Serialize(arg)).AppendLine("]");
            }

            stopwatch.Start();
            invocation.Proceed();
            stopwatch.Stop();

            StringBuilder postmsg = new StringBuilder();
            postmsg.Append("Metoda: ").AppendLine(invocation.Method.Name);
            postmsg.Append("Czas wykonania: ").AppendLine(stopwatch.Elapsed.ToString());
            postmsg.Append("Wynik: ");

            if (invocation.ReturnValue != null)
                postmsg.AppendLine(JsonSerializer.Serialize(invocation.ReturnValue));
            else
                postmsg.AppendLine("void");

            Console.WriteLine(premsg.ToString());
            Console.WriteLine(postmsg.ToString());
        }
    }
}
