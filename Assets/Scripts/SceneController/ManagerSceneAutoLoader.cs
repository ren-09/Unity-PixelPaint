using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneAutoLoader : MonoBehaviour {
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadManagerScene(){
        string managerSceneName = "ManagerScene";

        //ManagerSceneが有効でないときに追加ロード
        if(!SceneManager.GetSceneByName(managerSceneName).IsValid()){

           SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);

        }
    }
}
