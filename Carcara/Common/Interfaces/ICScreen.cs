namespace Microsoft.Xna.Framework
{
    public interface ICScreen : ICUpdatable, ICDrawable
    {
        string Name { get; }    
        bool IsEnabled { get; set; }
        bool IsVisible { get; set; }

        void Load();
        void Unload();
        void Reset();
    }
}