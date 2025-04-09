using UnityEngine;

namespace Wardetta.Npcs.Occupations
{
    public interface IOccupation
    {
        string Name { get; }
        void Trigger(GameObject other);
    }
}
