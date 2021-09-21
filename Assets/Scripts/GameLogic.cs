using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class GameLogic : MonoBehaviour
{
    
    public InputField nameInputField;
    public GameObject timerCanvas;
    public GameObject initialCanvas;
    public GameObject CanvasRanking;
    public GameObject keyboardCanvas;
    public GameObject CanvasResultado;
    public GameObject XR;
    public GameObject XR_RIG;
    public GameObject OVR;
    public GameObject UIHelper;
    public Text logText;
    private bool isRightTriggerReleased = true;


    public Text timerText;
    public Text TextTiros;
    public Text TextAcertos;
    public float TimerStart;
    


    public GameObject NovaAvaliacao;


     private TouchScreenKeyboard overlayKeyboard;
     public static string inputText = "";

    public GameObject alvo;
    public GameObject arma;

    public Text NameResultado;
    public Text TimeResultado;
    public Text AcertosResultado;
    public Text MencaoResultado;
    public Text DispersaoResultado;


public TextAsset textJSON;

    public Text rankingNome;
    public Text rankingImpactos;
    public Text rankingTempo;
    public Text rankingDispersao;

    
   
   

    [System.Serializable]
    public class Player 
    {
        public string nome;
        public int acertos;
        public string tempo;
        public double dispersao;
    }
    
    [System.Serializable]
    public class PlayerList
    {
       


        public List<Player> player = new List<Player>();
        

    }


   

    public PlayerList myPlayerList = new PlayerList();
    public Player jogador = new Player();
    public string auxNome;
    public string auxImpactos;
    public string auxTempo;
    public string auxRankingDispersao;

    private StreamWriter escritor;
    [SerializeField] private string json;


    public void SerializarJSON()
    {
        
        
        json = JsonUtility.ToJson(myPlayerList);
        escritor = new StreamWriter(Application.dataPath + "/Json/Jogadores.json", false);        
        escritor.Write(json);
        escritor.Close();
    }
    void Start(){

       // myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);
        //logText.text = "AAAAAAAAAA";
        myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);
    }

   

    private void Update()
    {

          if(timerCanvas.activeSelf) {

            
        int minutes = (int) (Time.time - TimerStart)/ 60;
        int seconds = (int) (Time.time - TimerStart) % 60;
        timerText.text = 
            ((minutes < 10) ? "0" + minutes : "" + minutes) + ":" +
            ((seconds < 10) ? "0" + seconds : "" + seconds); 
        }
         
        //Se clicar com o gatilho direito, mostrar/esconder binóculo
       //OVRInput.Update();

        //logText.text = (alvo.GetComponent<Target>().getHit()).ToString();
       //importante logText.text = (alvo.GetComponent<Target>().getNum()).ToString();
       //logText.text = (alvo.GetComponent<Target>().getHit()).ToString();
       //logText.text = (arma.GetComponent<SimpleShoot>().jogador()).ToString();
       //logText.text= OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger).ToString();
        // if (isRightTriggerReleased && (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) >= 0.5f))
        // {
        //     isRightTriggerReleased = false;
        //     logText.text = "APERTOU O GATILHO - Posição " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        // }
        // else
        // {
        //     isRightTriggerReleased = true;
        //     logText.text = "GATILHO LIVRE - Posição " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        // }

        if(arma.GetComponent<SimpleShoot>().jogador() == true)
        {
           
            Invoke("CallFinalAvaliacao", 5f);
             timerCanvas.SetActive(false);
             TimeResultado.text = timerText.text;
             OVR.SetActive(true);
            UIHelper.SetActive(true);
            XR_RIG.SetActive(false);
            CanvasResultado.SetActive(true);
        }

        logText.text = (alvo.GetComponent<Target>().getHit()).ToString();
        TextAcertos.text = (alvo.GetComponent<Target>().getHit()).ToString();
        TextTiros.text = (arma.GetComponent<SimpleShoot>().tiros()).ToString();

        
    }

     public void CallFinalAvaliacao(){
    
            
            //myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);
            AcertosResultado.text = (alvo.GetComponent<Target>().getHit()).ToString();
           

            int hit = alvo.GetComponent<Target>().getHit();
            MencaoResultado.text = mencao(hit);

            double disp = alvo.GetComponent<Target>().Dispersao();
            DispersaoResultado.text = disp.ToString("F");
            myPlayerList.player.Add(new Player{nome=jogador.nome, acertos=hit, tempo = timerText.text, dispersao = disp});
           // SerializarJSON();
            SerializarJSON();
          
            
    }

    public void onClickInitialCanvasButton(Button button)
    {
        initialCanvas.SetActive(false);
    }

    public void onClickButtonNovaAvaliacao()
    {
        // overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        // Debug.Log("TESTE ");
         //overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        //keyboardCanvas.SetActive(true);
       
       initialCanvas.SetActive(false);
       NovaAvaliacao.SetActive(true);
    
        // initialCanvas.SetActive(false);
        // OVR.SetActive(false);
        // UIHelper.SetActive(false);
        // XR_RIG.SetActive(true);
        
       
       
    }

    public void onClickNameInputField(){

        
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void onClickKeyboardCanvasButton(Text text)
    {
        if(text.text.Equals("Concluído"))
            keyboardCanvas.SetActive(false);
        else
            logText.text += text.text;
    }


    public void onClickVoltar() {
        
        initialCanvas.SetActive(true);
        NovaAvaliacao.SetActive(false);

    }

    public void Iniciar() {

         NameResultado.text = nameInputField.text;
         jogador.nome = nameInputField.text;
         AcertosResultado.text = "-";
        MencaoResultado.text = "-";
        DispersaoResultado.text = "-";
        TimerStart = Time.time;
        timerCanvas.SetActive(true);
        NovaAvaliacao.SetActive(false);
        OVR.SetActive(false);
        UIHelper.SetActive(false);
        XR_RIG.SetActive(true);
    }

    public void VoltarTelaInicial()
    {
        initialCanvas.SetActive(true);
        CanvasRanking.SetActive(false);
    }

    public void Ranking(){



       // myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);
         auxNome = "";
         auxImpactos = "";
         auxTempo = "";
         auxRankingDispersao = "";

   

        List<Player> aux = new List<Player>();
        aux = myPlayerList.player;

        aux.Sort((x, y) => {
                        int ret = y.acertos.CompareTo(x.acertos);
                        return ret != 0 ? ret : x.dispersao.CompareTo(y.dispersao);
                    });



       for(int i = 0; i < aux.Count; i++)
        {
            auxNome += aux[i].nome + "\n"; 
            auxImpactos += aux[i].acertos +  " (" + mencao(aux[i].acertos) + ")" + "\n"; 
            auxTempo += aux[i].tempo + "\n"; 
            auxRankingDispersao += aux[i].dispersao.ToString("F") + "\n";
        }

        rankingNome.text = auxNome;
        rankingImpactos.text = auxImpactos;
        rankingTempo.text = auxTempo;
        rankingDispersao.text = auxRankingDispersao;


        

        

        CanvasResultado.SetActive(false);
		initialCanvas.SetActive(false);
        CanvasRanking.SetActive(true);

	}

        public string mencao(int hits){

        if(hits == 15)
        {
            return "E";
        }
        else if(hits==14 || hits==13){
            return "MB";
        }
        else if(hits<=12 && hits>=9) {
            return "B";
        }
        else if(hits<=8 && hits>=6){
            return "R";
        }
        else
        {
            return "I";
        }

    }

    // public void NovaAvaliacaoResultado()
    // {
    //     nameInputField.text = "";
    //     CanvasResultado.SetActive(false);
    //     NovaAvaliacao.SetActive(true);
        
    // }
     
}