using UnityEngine;
using Wardetta.Events.CustomEvents;
using Wardetta.Interactables;
using Wardetta.Npcs.Occupations;

namespace Wardetta.Npcs
{
    public class Npc : MonoBehaviour, IInteractable
    {
        [SerializeField] private NpcEvent onStartInteraction = null;
        [SerializeField] private new string name = "New Npc Name";
        [SerializeField] private string greetingText = "Hello adventurer!";

        public string Name => name;
        public string GreetingText => greetingText;
        public GameObject OtherInteractor { get; private set; } = null;
        public IOccupation[] Occupations { get; private set; } = new IOccupation[0];

        private void Start() => Occupations = GetComponents<IOccupation>();

        public void Interact(GameObject other)
        {
            OtherInteractor = other;

            onStartInteraction.Raise(this);
        }
    }
}
