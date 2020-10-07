using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public Text successCountText;
    public Text failureCountText;
    public int successCount;
    public int failureCount;

    void Awake()
    {
        //シングルトンかつ、シーン遷移しても破棄されないようにする
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 成功回数表示
    /// </summary>
    public void SuccessCount()
    {
        successCount++;
        successCountText.text = "成功回数 ： " + successCount;
    }

    /// <summary>
    /// 失敗回数表示
    /// </summary>
    public void FailureCount()
    {
        failureCount++;
        failureCountText.text = "失敗回数 ： " + failureCount;
    }


}
