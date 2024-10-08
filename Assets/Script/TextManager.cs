using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
public class TextManager : MonoBehaviourPunCallbacks,IOnEventCallback
{
    // テキストエリアの設定
    public TMP_InputField inputField;
    public TextMeshProUGUI text;
    public TextMeshProUGUI anstext;
    public TextMeshProUGUI writenumtext;
    public GameObject button;
    public static List<string> textarr=new List<string>();
    private const byte TextWriteEventCode=1;
    public static int writenum=0;
    public static bool writeflag=false;
    // Start is called before the first frame update
    void Start()
    {
        // イベントコード登録
        PhotonNetwork.AddCallbackTarget(this);
        // オブジェクトを入手
        inputField=inputField.GetComponent<TMP_InputField>();
        text=text.GetComponent<TextMeshProUGUI>();
        anstext=anstext.GetComponent<TextMeshProUGUI>();
        writenumtext=writenumtext.GetComponent<TextMeshProUGUI>();
        
    }

    // オブジェクトがなくなった時に呼ばれないようにするためオブジェクトがシーン内になければコールバック解除
    void OnDestroy(){
        // イベントコード解除
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(writenum==4){
                SceneManager.LoadScene("ShowScene");
        }
    }

    // テキスト表示用
    public void InputText(){
        text.text=inputField.text;
    }

    // テストでテキストのみ送信
    public void OnClick(){
        object[] content=new object[]{text.text};

        RaiseEventOptions raiseEventOptions=new RaiseEventOptions{
            Receivers=ReceiverGroup.All
        };

        PhotonNetwork.RaiseEvent(TextWriteEventCode,content,raiseEventOptions,SendOptions.SendReliable);
        button.SetActive(false);

    }

    public void OnEvent(EventData photonEvent){
        if(photonEvent.Code==TextWriteEventCode){
            object[] data=(object[])photonEvent.CustomData;
            string writetext=(string)data[0];
            textarr.Add(writetext);
            writenum+=1;
            writenumtext.text=writenum.ToString("F1");
            // if(writenum==4){
            //     writeflag=true;
            // }
            
        }
    }



}
