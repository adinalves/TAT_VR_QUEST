using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings {

    //NOME DA APLICAÇÃO
    public static readonly string APP_NAME = "IME VR";
    
    //CONFIGURAÇÃO DO TIPO DE VERSÃO
    public enum VersionType
    {
        DESKTOP, CARDBOARD, DAYDREAM, QUEST,
    }
    public static readonly VersionType VERSION_TYPE = VersionType.DESKTOP;

    //CONFIGURAÇÃO DE REDE
    public static readonly bool NETWORK_ENABLED = false;
    public static readonly int LOCAL_PORT = 4445;
    public static readonly int REMOTE_PORT = 4444;
    public static readonly bool INSTRUCTOR_VERSION = true;
}