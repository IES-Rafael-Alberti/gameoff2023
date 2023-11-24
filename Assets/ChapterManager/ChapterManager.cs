using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{

    [SerializeField] List<CMChapterSO> chapters = new List<CMChapterSO>();

    public static ChapterManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        /*Método de Inicialización*/
        DontDestroyOnLoad(this);
    }

        


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
