using VS;

namespace Templates
{
    public interface IHomeView
    {
        int MainMenu();
        bool Bye();
        void InvalidChoice<T>(T error);
        VibeShell GetVibe();
    }
}