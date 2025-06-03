using Container.DTOs;

namespace Templates;

public interface IView
{
    public T GetValidInput<T>(string prompt = "", string? menu = null, bool clear = false, T? _default = default);
    public int GetChoice(string prompt = "",string? question = null, int minimum = 0, int maximum = 99, bool clear = false, string _default = "0");
    

}