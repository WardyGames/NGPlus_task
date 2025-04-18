﻿using TMPro;
using UnityEngine;
using Wardetta.Npcs.Occupations;

namespace Wardetta.Npcs
{
    public class NpcUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI npcNameText = null;
        [SerializeField] private TextMeshProUGUI npcGreetingText = null;
        [SerializeField] private Transform occupationButtonHolder = null;
        [SerializeField] private GameObject occupationButtonPrefab = null;

        public void SetNpc(Npc npc)
        {
            npcNameText.text = npc.Name;
            npcGreetingText.text = npc.GreetingText;

            foreach (Transform child in occupationButtonHolder)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < npc.Occupations.Length; i++)
            {
                GameObject buttonInstance = Instantiate(occupationButtonPrefab, occupationButtonHolder);
                buttonInstance.GetComponent<OccupationButton>().Initialise(npc.Occupations[i], npc.OtherInteractor);
            }
        }
    }
}
