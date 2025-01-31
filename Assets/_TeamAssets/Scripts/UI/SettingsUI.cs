using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        exitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        AudioManager.Instance.PlayMusicOnce("select");
        StartCoroutine(QuitGameWithDelay());
    }

    private IEnumerator QuitGameWithDelay()
    {
        yield return new WaitForSeconds(2f);

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); // Para fechar o jogo na build final
        #endif
    }
}
