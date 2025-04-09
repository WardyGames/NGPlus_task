using UnityEngine;

namespace Wardetta.Items.Hotbars
{
    public interface IHotbarItem
    {
        string Name { get; }
        Sprite Icon { get; }
        void Use();
    }
}

