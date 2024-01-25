namespace CustomButton
{
    public interface IButton : IReadOnlyButton
    {
        bool Interactable { get; set; }
    }
}