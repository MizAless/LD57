using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts
{
    public class Restarter : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}