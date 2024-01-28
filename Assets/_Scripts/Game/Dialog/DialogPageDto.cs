namespace Game.Dialog
{
    public struct DialogPageDto
    {
        public DialogId Id;
        public int Index;

        public DialogPageDto(DialogId id, int index)
        {
            Id = id;
            Index = index;
        }
    }
}