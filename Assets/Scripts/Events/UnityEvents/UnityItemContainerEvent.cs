using System;
using UnityEngine.Events;
using Wardetta.Items;

namespace Wardetta.Events.UnityEvents
{
    [Serializable] public class UnityItemContainerEvent : UnityEvent<IItemContainer> { }
}