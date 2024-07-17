using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
public class TextReceiver : MonoBehaviour
{

    // public static int writenum=0;
    List<string> textarr=new List<string>();
    public TextMeshProUGUI ans1;
    public TextMeshProUGUI ans2;
    public TextMeshProUGUI ans3;
    public TextMeshProUGUI ans4;
    public TextMeshProUGUI writenum;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        ans1=ans1.GetComponent<TextMeshProUGUI>();
        ans2=ans2.GetComponent<TextMeshProUGUI>();
        ans3=ans3.GetComponent<TextMeshProUGUI>();
        ans4=ans4.GetComponent<TextMeshProUGUI>();
        writenum=writenum.GetComponent<TextMeshProUGUI>();
        // num=TextManager.getWritenum();
        //if(num==4){
            // textarr=TextManager.textarr;
            // ans1.text=textarr[0];
            // ans2.text=textarr[1];
            // ans3.text=textarr[2];
            // ans4.text=textarr[3];

        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        //bool flag=TextManager.writeflag;
        // num=TextManager.getWritenum();
        //writenum.text=flag.ToString();
        textarr=TextManager.textarr;
        //if(flag){
            ans1.text=textarr[0];
            ans2.text=textarr[1];
            ans3.text=textarr[2];
            ans4.text=textarr[3];
        //}
    }
}
