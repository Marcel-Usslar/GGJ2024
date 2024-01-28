using System.Collections.Generic;

namespace Game.Dialog
{
    public struct DialogChoiceDto
    {
        public DialogPageDto Page;
        public List<DialogActionDto> ActionDtos;

        public DialogChoiceDto(DialogPageDto page, List<DialogActionDto> actionDtos)
        {
            Page = page;
            ActionDtos = actionDtos;
        }
    }
}