using UnityEngine;
using GameAnalyticsSdk.Api;

public class AndroidLifeCycleCallBack : MonoBehaviour
{
        void Awake() {
            DontDestroyOnLoad(transform.gameObject);
        }

        void OnApplicationPause(bool isPause) {
            if (isPause) {
                GASdk.OnPause();
                Debug.Log("gasdk: OnPause");
            } else {
                GASdk.OnResume();
            Debug.Log("gasdk: OnResume");
            }
        }

        void OnApplicationQuit() {
            GASdk.OnKillProcess();
        }
}