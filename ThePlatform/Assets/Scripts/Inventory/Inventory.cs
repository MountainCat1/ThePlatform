using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        public EnumDictionary<ResourceType, int> Resources { get; set; } = new();
    }
}