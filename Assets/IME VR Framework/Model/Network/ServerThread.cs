using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ServerThread {

    //ATRIBUTOS
    private Thread serverThread;
    private NetworkListenerInterface networkListener;
    private string serverIP;

    //CONSTRUTOR
    public ServerThread(NetworkListenerInterface networkListener, string serverIP)
    {
        this.networkListener = networkListener;
        this.serverIP = serverIP;
    }

    //MÉTODO DE INICIALIZAÇÃO DO SERVIDOR
    public void Start()
    {
        serverThread = new Thread(RunServerThread);
        serverThread.IsBackground = true;
        serverThread.Start();
    }

    //MÉTODO DE FINALIZAÇÃO DO SERVIDOR
    public void Stop()
    {
        if (serverThread != null)
            serverThread.Abort();
    }

    //MÉTODO DE EXECUÇÃO DA INICIALIZAÇÃO DO SERVIDOR
    private void RunServerThread()
    {
        TcpListener server = null;
        try
        {
            //Iniciar o servidor TCP
            IPAddress localIPAddress = IPAddress.Parse(serverIP);
            server = new TcpListener(localIPAddress, Settings.LOCAL_PORT);
            server.Start();
            Debug.Log("Servidor iniciado em " + serverIP + ":" + Settings.LOCAL_PORT);

            //Aguardar conexões
            while (true)
            {
                new ServerConnectionThread(server, networkListener).Start();
            }
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
        finally
        {
            //Finalizar o servidor TCP
            server.Stop();
        }
    }
}