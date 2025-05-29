using damilah_hometask.data;
using damilah_hometask.data.impl;
using damilah_hometask.presentation;

namespace damilah_hometask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var app = new App();
            await app.Run();
        }
    }
}
