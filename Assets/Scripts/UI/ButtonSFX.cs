using UnityEngine;
using Toon.Core;

namespace Toon.UI
{
    public class ButtonSFX : MonoBehaviour
    {
        AudioManager audioManager;

        private void Start()
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        public void PlayHoverSFX()
        {
            audioManager.PlaySound("hover");
        }

        public void PlayClickSFX()
        {
            audioManager.PlaySound("click");
        }
    }
}