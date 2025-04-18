﻿using TMPro;
using UnityEngine;

namespace Wardetta.Npcs.Occupations
{
    public class OccupationButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI occupationNameText = null;

        private IOccupation occupation = null;
        private GameObject other = null;

        public void Initialise(IOccupation occupation, GameObject other)
        {
            this.occupation = occupation;
            this.other = other;
        }

        public void TriggerOccupation() => occupation.Trigger(other);
    }
}
