using System;
using System.Threading.Tasks;

namespace DotCache.Server;

internal class Program
{
    static async Task Main(string[] args)
    {
        var app = new ServerApp();
        app.Initiallize();

        // TODO : 서비스로 우아하게 처리하자
        Console.ReadLine();

        app.Close();
    }
}
    