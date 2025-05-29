namespace damilah_hometask.data.services;

public interface IConsoleService
{
   string? ReadLine();
   void WriteLine(string s);
   void Write(string s);
   void Clear();
}