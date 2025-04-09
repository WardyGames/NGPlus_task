using UnityEngine;
using Wardetta.Npcs.Occupations.Vendors;

namespace Wardetta.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Vendor Data Event", menuName = "Game Events/Vendor Data Event")]
    public class VendorDataEvent : BaseGameEvent<VendorData> { }
}
