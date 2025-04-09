using UnityEngine;
using Wardetta.Items;

namespace Wardetta.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Item Container Event", menuName = "Game Events/Item Container Event")]
    public class ItemContainerEvent : BaseGameEvent<IItemContainer> { }
}
