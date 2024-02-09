using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutUI : MonoBehaviour
{
    private static BlackoutUI instance = null;

    [Header("Properties")]
    [Tooltip("# of seconds for screen to fade to black.")]
    [SerializeField] private float fadeInTime = 3.0f;
    [Tooltip("# of seconds for black to fade out.")]
    [SerializeField] private float fadeOutTime = 0.5f;

    //Whether fade coroutine is running or not
    private bool isRunning = false;
    //The rate alpha will fade in/out per second;
    private float fadeInSpeed;
    private float fadeOutSpeed;

    [Header("Object References")]
    [SerializeField] private Image blackoutImage = null;
    private RectTransform blackoutRect;
    private RectTransform canvasRect;

    #region Properties for External Scripts (Getter/Setters)
    //Used in GProgManager
    public static bool IsRunning
    {
        get
        {
            if (instance == null)
                return false;
            return instance.isRunning;
        }
    }
    #endregion

    private void Awake()
    {
        if(instance == null) 
            instance = this;

        if(blackoutImage == null)
            blackoutImage = GetComponent<Image>();

        //Get rectTransform references
        blackoutRect = blackoutImage.rectTransform;
        canvasRect = blackoutImage.canvas.GetComponent<RectTransform>();

        //Setup Image
        blackoutImage.gameObject.SetActive(false);

        StopAllCoroutines();
        isRunning = false;

        fadeInSpeed = (1f / fadeInTime);
        fadeOutSpeed = (-1f / fadeOutTime);
    }

    //Fade screen to black
    public static void FadeToBlack()
    {
        if (instance == null)
            return;

        instance._FadeToBlack();
    }
    private void _FadeToBlack()
    {
        StopAllCoroutines();
        blackoutImage.gameObject.SetActive(false);

        Color c = blackoutImage.color;
        c.a = 0f;
        blackoutImage.color = c;

        UpdateImageSize();
        blackoutImage.gameObject.SetActive(true);
        StartCoroutine(FadeImage(fadeInSpeed));
    }



    //Fade out of black screen
    public static void FadeOutBlack()
    {
        if (instance == null)
            return;

        instance._FadeOutBlack();
    }
    private void _FadeOutBlack()
    {
        StopAllCoroutines();
        
        Color c = blackoutImage.color;
        c.a = 1f;
        blackoutImage.color = c;

        UpdateImageSize();
        blackoutImage.gameObject.SetActive(true);
        StartCoroutine(FadeImage(fadeOutSpeed));
    }

    private IEnumerator FadeImage(float speed)
    {
        isRunning = true;
        Color c = blackoutImage.color;

        bool fadeIn = speed > 0;

        while((fadeIn) ? c.a < 1 : c.a > 0)
        {
            //Stop Fade animation if game is paused
            if(PauseMenuManager.I != null)
            {
                if (PauseMenuManager.I.GamePaused)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }
                    
                
            }

            //Slightly change alpha of image
            c.a = Mathf.Clamp(c.a + (speed * Time.deltaTime), 0f, 1f);
            blackoutImage.color = c;

            yield return new WaitForEndOfFrame();
        }

        UpdateImageSize();
        blackoutImage.gameObject.SetActive(fadeIn);
        isRunning = false;
    }

    private void UpdateImageSize()
    {
        try
        {
            blackoutRect.sizeDelta = new Vector2(canvasRect.sizeDelta.x + 5f, canvasRect.sizeDelta.y + 5f);
        }
        catch { }
    }
}
