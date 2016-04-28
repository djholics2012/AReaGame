using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections;

public enum SysStates
{
    READY, PLAY, END
}
public class SysManageScript : MonoBehaviour {
    public GameObject shootUnit;
    public RawImage targetAim;
    public Text operateText;
    public Button startButton;
    public Button reset;

    private SysStates sysState;
    private SurfaceBehaviour mSmartSurface;
    private ReconstructionAbstractBehaviour mReconstructionBehaviour;
    //스마트 터레인 이벤트 핸들러
    //이미지 타겟 트렉커블 이벤트 핸들러


	// Use this for initialization
	void Start () {
        mReconstructionBehaviour = FindObjectOfType<ReconstructionAbstractBehaviour>();
        mSmartSurface = GameObject.FindObjectOfType<SurfaceBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        bool showPointFrame = false;
        bool showStartButton = false;
        bool showResetButton = false;

        switch(sysState)
        {
            //game ready state
            case SysStates.READY:
                mSmartSurface.GetComponent<Renderer>().enabled = false;
                showPointFrame = true;
                break;
            case SysStates.PLAY:

        }
	}
}
