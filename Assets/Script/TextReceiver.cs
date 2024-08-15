using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
public class TextReceiver : MonoBehaviourPunCallbacks,IOnEventCallback
{

    // public static int writenum=0;
    Dictionary<int,string> textarr=new Dictionary<int,string>();
    // public TextMeshProUGUI ans1;
    // public TextMeshProUGUI ans2;
    // public TextMeshProUGUI ans3;
    // public TextMeshProUGUI ans4;
    public Button[] buttons;
    public TextMeshProUGUI[] ans;
    public TextMeshProUGUI writenum;
    public TextMeshProUGUI[] votetext;
    List<int> numbers=new List<int>();
    int[] ransu=new int[4];
    int[] votes=new int[5];

    private const byte RansuEventCode=1;
    private const byte VoteEventCode=2;
    // Start is called before the first frame update
    void Start()
    {
        // イベントコード登録
        PhotonNetwork.AddCallbackTarget(this);
        // writenum=writenum.GetComponent<TextMeshProUGUI>();
        if (PhotonNetwork.IsMasterClient)
        {
            CreateRandomNum();
        }
        
    }
    // オブジェクトがなくなった時に呼ばれないようにするためオブジェクトがシーン内になければコールバック解除
    void OnDestroy(){
        // イベントコード解除
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void CreateRandomNum(){
    for(int i=1;i<=4;i++){
        numbers.Add(i);
    }
    int num = 0;
    while(numbers.Count > 0){
        int index = Random.Range(0, numbers.Count);
        ransu[num] = numbers[index];
        numbers.RemoveAt(index);
        num++;
    }
 
    // 配列を送信する
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
    {
        Receivers = ReceiverGroup.All
    };
 
    PhotonNetwork.RaiseEvent(RansuEventCode, ransu, raiseEventOptions, SendOptions.SendReliable);
}
    
    public void OnButtonClick(int index)
    {
        Debug.Log(ransu[0]+","+ransu[1]+","+ransu[2]+","+ransu[3]+",");
        // votes[ransu[index]]++;
        int voteindex=index;
        // 以下イベント送信用
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All
        };

        PhotonNetwork.RaiseEvent(VoteEventCode, voteindex, raiseEventOptions, SendOptions.SendReliable);
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(false);
            }
        }
       Debug.Log("click:"+index);
    }
    

    public void OnEvent(EventData photonEvent){
        if (photonEvent.Code == RansuEventCode)
        {
            int[] receivedRansu = (int[])photonEvent.CustomData;
            for (int i = 0; i < 4; i++)
            {
                ransu[i] = receivedRansu[i]; // 受信した配列の値で更新
                ans[i].text = TextManager.textarr[ransu[i]];
            }
        }
        else if (photonEvent.Code == VoteEventCode)
        {
            int index = (int)photonEvent.CustomData;
            votes[ransu[index]]++;
            for (int i = 0; i < 4; i++)
            {
                votetext[i].text = votes[i + 1].ToString();
            }
            votetext[4].text = votes[0].ToString();
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
