namespace Packages.EditorUtility
{
    public interface IEditorWindowTab
    {
        string Name { get; }
        int Height { get; }

        void Display();
    }
}