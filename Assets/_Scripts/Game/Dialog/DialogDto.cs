namespace Game.Dialog
{
    public struct DialogDto
    {
        public int DialogId;
        public int Index;

        public DialogDto(int dialogId, int index)
        {
            DialogId = dialogId;
            Index = index;
        }
    }
}