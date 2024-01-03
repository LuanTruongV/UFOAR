using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace Project.Scripts
{
    public class UIInGame : MonoBehaviour
    {

        [SerializeField] private Button flyBtn;

        [SerializeField] private GameObject firstPanel;
        [SerializeField] private GameObject drivePanel;

        [SerializeField] private ARSession _session;

        public void InteractFlyBtn(bool active)
        {
            flyBtn.interactable = active;
        }
        public void ChangeUiToDrive()
        {
            firstPanel.SetActive(false);
            drivePanel.SetActive(true);
        }

        
    }
}