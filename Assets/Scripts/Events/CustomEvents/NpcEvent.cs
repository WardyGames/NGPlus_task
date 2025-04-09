using UnityEngine;
using Wardetta.Npcs;

namespace Wardetta.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Npc Event", menuName = "Game Events/Npc Event")]
    public class NpcEvent : BaseGameEvent<Npc> { }
}
