using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // Required for scene loading
using System.Collections; // Required for IEnumerator

public class AmuletInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject storyText;
    public Animator amuletAnimator;
    public Animator transitionAnimator; // Animator for the fade-out effect
    public float transitionTime = 1f; // Duration of the fade-out effect
    private bool isAmuletClicked = false;

    void Start()
    {
        amuletAnimator.SetBool("IsGlowing", false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isAmuletClicked)
        {
            amuletAnimator.SetBool("IsGlowing", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isAmuletClicked)
        {
            amuletAnimator.SetBool("IsGlowing", false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isAmuletClicked)
        {
            isAmuletClicked = true;
            amuletAnimator.SetBool("IsGlowing", false);
            if (storyText != null)
            {
                //storyText.GetComponent<Text>().color = new Color(1, 1, 1, 1); // Replace with your desired color
            }
            else
            {
                Debug.LogError("Story text is not assigned in the Inspector");
            }
            StartCoroutine(TransitionToNextScene());
        }
    }

    IEnumerator TransitionToNextScene()
    {
        transitionAnimator.SetTrigger("StartFadeOut");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("New_Overworld"); // Change to your scene name
    }
}
