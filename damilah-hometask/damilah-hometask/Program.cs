using damilah_hometask.data.services;
using damilah_hometask.presentation;

namespace damilah_hometask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConsoleService console = new RealConsoleService();

            var app = new App(console);

            await app.Run();
        }
    }
}

