namespace Packages.EditorUtility
{
    public interface IEditorWindowProgressTab : IEditorWindowTab
    {
        void TrackProgress(ProgressTracker tracker);
    }
}