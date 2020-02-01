using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splash
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] private float _timerToStart = 1.0f;
        [SerializeField] private AlphaAnimator _alphaAnimator;
        
        private void Start()
        {
            Invoke(nameof(StartAnim), _timerToStart);
        }

        private void StartAnim()
        {
            _alphaAnimator.OnStart();
            SceneManager.LoadScene("Game",LoadSceneMode.Additive);
            
            Invoke(nameof(DeleteScene), 1);
        }

        private void DeleteScene()
        {
            SceneManager.UnloadSceneAsync("Splash");
        }
    }
}
