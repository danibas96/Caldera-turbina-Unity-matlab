using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_BT : MonoBehaviour
{
    public static Setting_BT setting;
   
    [SerializeField]
    private float sp_va; // valor del setpoint que ser tomado del slider VA.
    [SerializeField]
    private float sp_vf; // valor del setpoint que ser tomado del slider VF.
    [SerializeField]
    private float sp_vv; // valor del setpoint que ser tomado del slider VV.
 
    [SerializeField]
    private float temperatura; //valor del volumen que se obtendra de los calculos realizado.
    [SerializeField]
    private float presion; //valor del error que se obtendra de los calculos realizados
    [SerializeField]
    private float potencia; //valor del error que se obtendra de los calculos realizados
    [SerializeField]
    private float error; //valor del error que se obtendra de los calculos realizados
    
    
    public float _sp_va { set { this.sp_va = value; } get { return sp_va; } } //asignacion de valor privado a una variable publica para la obtencion de los datos utilizando la sintaxis del lenguaje.
    public float _sp_vf { set { this.sp_vf = value; } get { return sp_vf; } }
    public float _sp_vv { set { this.sp_vv = value; } get { return sp_vv; } }
 
    public float _presion { set { this.presion = value; } get { return presion; } } //asignacion de valor privado a una variable publica para la obtencion de los datos utilizando la sintaxis del lenguaje.
    public float _temperatura { set { this.temperatura = value; } get { return temperatura; } } //asignacion de valor privado a una variable publica para la obtencion de los datos utilizando la sintaxis del lenguaje.
    public float _potencia { set { this.potencia = value; } get { return potencia; } } //asignacion de valor privado a una variable publica para la obtencion de los datos utilizando la sintaxis del lenguaje.
    public float _error { set { this.error = value; } get { return error; } }

    [SerializeField]
    private double containerVolume;
    public double _containerVolume { set { this.containerVolume = value; } get { return containerVolume; } }
    [SerializeField]
    private double volumen;
    public double _volumen { set { this.volumen = value; } get { return volumen; } }


    private void Awake() //Esta se ejecuta para inicializar el objeto
    {
        if (Setting_BT.setting == null) //verificar que el objeto se encuentre en nulo
        {
            Setting_BT.setting = this; //se asiga el objeto setting  y this a punta si mismo.
        }
        else
        {
            if (Setting_BT.setting != this) //si setting es diferente de si mismo procede a destruir.
            {
                Destroy(this.gameObject); //Desgtruye el objeto.
            }
        }
        DontDestroyOnLoad(this.gameObject); //crea el objeto pero que no sea destruido al momento de realizar un cambio de escena.
        //Debug.Log(sp_va);
    }
}
