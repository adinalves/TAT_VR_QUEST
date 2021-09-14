using UnityEngine;
using UnityEngine.UI;

public class DaydreamController : MonoBehaviour
{

    //REFERÊNCIA PÚBLICA
    public GameObject sceneControllerReference;
    public GameObject daydreamPointer;
    public GameObject linkTestUI;

    //VARIÁVEL DE CONTROLE
    private SceneControllerInterface sceneController;

    //INICIALIZAR CONTROLADOR DA CENA
    void Start()
    {
        if (!Settings.VERSION_TYPE.Equals(Settings.VersionType.DAYDREAM))
            this.gameObject.transform.parent.gameObject.SetActive(false);
        else
        {
            sceneController = sceneControllerReference.GetComponent<SceneControllerInterface>();
            daydreamPointer.SetActive(sceneController.isDaydreamPointerEnabled());
            if (linkTestUI != null)
            {
                linkTestUI.SetActive(sceneController.isDaydreamPointerEnabled());
                GraphicRaycaster graphicRaycaster = linkTestUI.GetComponentInChildren<GraphicRaycaster>();
                if (graphicRaycaster != null)
                    graphicRaycaster.enabled = false;
                //GvrPointerGraphicRaycaster gvrPointerGraphicRaycaster = linkTestUI.GetComponentInChildren<GvrPointerGraphicRaycaster>();
                //if (gvrPointerGraphicRaycaster != null)
                //    gvrPointerGraphicRaycaster.enabled = true;
            }
        }
    }

    //GERENCIAR CLIQUES DE BOTÃO
    void Update()
    {
        //Se clicar no botão touchpad do controlador daydream
        //if (!sceneController.isDaydreamPointerEnabled() && GvrControllerInput.ClickButtonDown)
        //    sceneController.onClickDaydreamTouchpadButton();

        ////Se clicar no botão app do controlador daydream
        //if (GvrControllerInput.AppButtonDown)
        //    sceneController.onClickDaydreamAppButton();

        //Se clicar no botão voltar do Android, fechar a aplicação
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}