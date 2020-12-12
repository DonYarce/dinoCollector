using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{
    public GameObject[] cards;
    public Text texto;
    public static CardsController instance = null;
    private float firstAPX;
    private float firstAPY;
    public bool isSelected = false;
    public int cardsNumber=-1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        texto.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator selectCard(GameObject card)
    {
        texto.text = card.GetComponent<CardsAnimation>().txt;
        float transitionTime = 0.5f;
        for (int i = 0; i < cardsNumber; i++) {
            if (cards[i] != card) {
                LeanTween.scale(cards[i].GetComponent<RectTransform>(), Vector3.zero, transitionTime);
            }
        }
        RectTransform rt = card.GetComponent<RectTransform>();
        firstAPY = rt.anchoredPosition.y;
        firstAPX = rt.anchoredPosition.x;
        LeanTween.moveX(rt, 0f, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(rt, 0f, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(rt, new Vector3(4f,4f,4f), transitionTime);
        yield return ChangeColor(transitionTime, card.GetComponent<Image>(), new Color(0.5f, 0.5f, 0.5f));
    }

    public IEnumerator ChangeColor(float duration,Image img,Color desiredColor)
    {
        Color actualColor;
        actualColor = img.color;
        float t = 0f;
        while (t < duration)
        {
            img.color = Color.Lerp(actualColor, desiredColor, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        if (isSelected == true)
        {
            yield return Typewriter();
        }
    }

    public IEnumerator Typewriter()
    {
        texto.gameObject.SetActive(true);
        string actualText = texto.text;
        texto.text = "";
        for (int i = 0; i < actualText.Length; i++)
        {
            texto.text += actualText[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator unselectCard(GameObject card) {

        texto.gameObject.SetActive(false);
        float transitionTime = 0.5f;
        for (int i = 0; i < cardsNumber; i++)
        {
            if (cards[i] != card)
            {
                LeanTween.scale(cards[i].GetComponent<RectTransform>(), Vector3.one, transitionTime);
            }
        }
        RectTransform rt = card.GetComponent<RectTransform>();
        LeanTween.moveX(rt, firstAPX, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(rt, firstAPY, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(rt, new Vector3(1f, 1f, 1f), transitionTime);
        yield return ChangeColor(transitionTime, card.GetComponent<Image>(), new Color(1f, 1f, 1f));
    }
}
