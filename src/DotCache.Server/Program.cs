using System;
using System.Threading.Tasks;

namespace DotCache.Server;

internal class Program
{
    static async Task Main(string[] args)
    {
        var app = new ServerApp();
        app.Initiallize();

        // TODO : ���񽺷� ����ϰ� ó������
        Console.ReadLine();

        app.Close();
    }
}
    