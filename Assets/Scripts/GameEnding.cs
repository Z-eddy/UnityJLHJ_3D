using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [Tooltip("玩家角色")] public GameObject player;
    [Tooltip("更改透明度的时间")] public float fadeDuration = 1f;
    [Tooltip("显示结束UI的时间")] public float displayImageDuration = 1f;
    [Tooltip("结束UI")] public CanvasGroup exitBackgroundImageCanvasGroup;
    [Tooltip("重新开始UI")] public CanvasGroup restartBackgroundImageCanvasGroup;

    // 是否被抓住
    private bool _IsCaught;

    // 是否结束游戏
    private bool _IsEnding;

    // 计时器
    private float _timer;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (_IsEnding) EndLevel(exitBackgroundImageCanvasGroup, true);
        else if (_IsCaught) EndLevel(restartBackgroundImageCanvasGroup, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 判断是否是玩家进入
        if (other.gameObject == player)
            // 设置游戏结束
            _IsEnding = true;
    }

    private void EndLevel(CanvasGroup exitImg, bool exitApp)
    {
        // 更新计时器
        _timer += Time.deltaTime;
        // 更新透明度
        exitImg.alpha = _timer / fadeDuration;
        // 判断是否超过显示时间
        if (_timer > fadeDuration + displayImageDuration)
            if (exitApp)
                // 结束游戏
                EditorApplication.isPlaying = false;
            else
            {
                // 重新开始游戏
                SceneManager.LoadScene(0);
                _IsCaught = false;
            }
        // Application.Quit();
    }

    public void Caught()
    {
        _IsCaught = true;
    }
}