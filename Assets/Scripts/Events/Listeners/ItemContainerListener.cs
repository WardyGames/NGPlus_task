using Wardetta.Events.CustomEvents;
using Wardetta.Events.UnityEvents;
using Wardetta.Items;

namespace Wardetta.Events.Listeners
{
    public class ItemContainerListener : BaseGameEventListener<IItemContainer, ItemContainerEvent, UnityItemContainerEvent> { }
}
