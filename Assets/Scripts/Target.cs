using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour, IShotHit {

    Animator animator;
    public int acertos = 0;
    public List<Vector3> vetores = new List<Vector3>();
    public float mediaX = 0;
    public float mediaY = 0;
    
    

    

    void Start() {
        
          
        animator = transform.parent.GetComponent<Animator>();
    }

    public void Hit(Vector3 direction) {
        
        animator.Play("TargetShot", -1, 0);
        
        acertos ++;
        Debug.Log(acertos);
        
    }

    public void Posicao(Vector3 coordenada){
            Debug.Log(coordenada);
            vetores.Add(coordenada);
            mediaX += coordenada[0];
            mediaY += coordenada[1];
           
    }

    public double Dispersao()
    {
        double sigmaX = 0;
        double sigmaY = 0;
        double sigmaResultante;
        mediaX = mediaX/acertos;
        mediaY = mediaY/acertos;

        if(acertos != 0) {
        for(int i=0;i<acertos;i++)
        {
            sigmaX += (vetores[i][0] - mediaX)*(vetores[i][0] - mediaX);
            
            sigmaY += (vetores[i][1] - mediaY)*(vetores[i][1] - mediaY);
            
        }
        
        sigmaResultante = Math.Sqrt(sigmaX/acertos + sigmaY/acertos);

        acertos = 0;
        mediaY = 0;
        mediaX = 0;
        vetores.Clear();

        return sigmaResultante;
        }
        acertos = 0;
        mediaY = 0;
        mediaX = 0;
        vetores.Clear();
        return 0;
    }

    public int getHit(){
        return acertos;
    }

    


}

