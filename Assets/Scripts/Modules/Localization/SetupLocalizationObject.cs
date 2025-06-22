using UnityEngine;

namespace Assets.SeaTodo.Scripts.Modules.Localization
{
    public class SetupLocalizationObject : MonoBehaviour
    {
        public LocalizationObject localizationObject;
    
        private void Awake()
        {
            LocalizationObject.SetupInstanceCustom(localizationObject);
        }
    }
}
