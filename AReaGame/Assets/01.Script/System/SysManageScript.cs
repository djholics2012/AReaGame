using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections;

public enum SysStates
{
    READY, PLAY, END, RESET, ETC
}
public class SysManageScript : MonoBehaviour {
    public FireEventScript shootUnit;
    public RawImage targetAim;
    public Text operateText;
    public Text shootText;
    public Button startButton;
    public Button resetButton;
    public Button finishButton;
    public AudioSource buttonClicked;

    private SysStates sysState;
 //   private SurfaceBehaviour mSmartSurface;
    private ReconstructionAbstractBehaviour mReconstructionBehaviour;
    //스마트 터레인 이벤트 핸들러
    //이미지 타겟 트렉커블 이벤트 핸들러


	// Use this for initialization
	void Start () {
        mReconstructionBehaviour = FindObjectOfType<ReconstructionAbstractBehaviour>();
        //     mSmartSurface = GameObject.FindObjectOfType<SurfaceBehaviour>();
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
        bool showFinishButton = false;
      //  

        switch(sysState)
        {
            
            //game ready state
            case SysStates.READY:
             //   mSmartSurface.GetComponent<Renderer>().enabled = false;
                showPointFrame = true;
                showStartButton = true;                
                break;


            case SysStates.PLAY:
                showPointFrame = false;
                showStartButton = false;
                showFinishButton = true;
                shootUnit.gameObject.SetActive(true);
            //    mReconstructionBehaviour.Reconstruction.Stop();
                break;


            case SysStates.END:
                showFinishButton = false;
                showResetButton = true;
                shootUnit.gameObject.SetActive(false);
                break;


            case SysStates.RESET:
                int loadingScene=UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex-1;
                if (loadingScene < 0)
                    loadingScene = 0;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadingScene);
                sysState = SysStates.ETC;
                break;

            case SysStates.ETC:
                break;
        }

        if(targetAim != null && showPointFrame != targetAim.enabled)
        {
            targetAim.enabled = showPointFrame;
            operateText.gameObject.SetActive(showPointFrame);
            shootText.gameObject.SetActive(!showPointFrame);
        }
        
        if(startButton != null && showStartButton != startButton.enabled)
        {
            startButton.enabled = showStartButton;
            startButton.image.enabled = showStartButton;
            startButton.gameObject.SetActive(showStartButton);
        }

        if(finishButton != null && showFinishButton != finishButton.enabled)
        {
            finishButton.enabled = showFinishButton;
            finishButton.image.enabled = showFinishButton;
            finishButton.gameObject.SetActive(showFinishButton);
        }

        if(resetButton != null && showResetButton != resetButton.enabled)
        {
            resetButton.enabled = showResetButton;
            resetButton.image.enabled = showResetButton;
            resetButton.gameObject.SetActive(showResetButton);
        }
	}

    public void Play()
    {
        
        sysState = SysStates.PLAY;
        buttonClicked.Play();
        Debug.Log("Start game");
    }

    public void End()
    {
        sysState = SysStates.END;
        buttonClicked.Play();
        Debug.Log("State : " + sysState);
    }

    public void Reset()
    {
        sysState = SysStates.RESET;
        buttonClicked.Play();
        Debug.Log("State : " + sysState);
    }
}
