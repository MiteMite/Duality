using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawner : MonoBehaviour, IPhaseListener, IDeathListener
{
    public GameObject currencyPreFab;
    private Transform m_Transform;
    private GameObject m_CurrentCurrency;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterPhaseListener(this);
        EventManager.Instance.RegisterDeathListener(this);

        m_Transform = GetComponent<Transform>();
    }

    void OnDestroy()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterPhaseListener(this);
            EventManager.Instance.UnregisterDeathListener(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhaseChangeEvent(BaseLevelStat levelStat)
    {
        if (levelStat == LevelStateManager.Instance.playingState)
        {
            m_CurrentCurrency = Instantiate(currencyPreFab, m_Transform.position, Quaternion.identity);
        }
    }

    public void OnDeathEvent()
    {
        m_CurrentCurrency = Instantiate(currencyPreFab, m_Transform.position, Quaternion.identity);
    }
}
