using Container.DTOs;

namespace Templates;

public interface IView
{
    public T GetValidInput<T>(string prompt = "");
    public int GetChoice(string prompt = "");
}