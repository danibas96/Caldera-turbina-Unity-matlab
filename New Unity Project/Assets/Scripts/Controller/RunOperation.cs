using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using System;
using ChartAndGraph;

public class RunOperation : MonoBehaviour
{
    /* //Sliders de agua vapor y fuego
      [SerializeField]
      private Slider sp_va;// valor del setpoint que ser tomado del slider VA.
      [SerializeField]
      private Slider sp_vf;// valor del setpoint que ser tomado del slider VF.
      [SerializeField]
      private Slider sp_vv;// valor del setpoint que ser tomado del slider VV.
 */
    public static RunOperation runOperation;

[SerializeField]
    private Slider sp_H;// valor del setpoint que ser tomado del slider VA.
    [SerializeField]
    private Slider sp_Pr;// valor del setpoint que ser tomado del slider VF.
    [SerializeField]
    private Slider sp_Po;// valor del setpoint que ser tomado del slider VV.
    //
    [SerializeField]
    private Slider pr_lectura;
    //
    [SerializeField]
    private float altura; //valor del volumen que se obtendra de los calculos realizado.
    [SerializeField]
    private float presion; //valor del error que se obtendra de los calculos realizados
    [SerializeField]
    private float potencia;
    [SerializeField]
    private float altura_error; //valor del volumen que se obtendra de los calculos realizado.
    [SerializeField]
    private float presion_error; //valor del error que se obtendra de los calculos realizados
    [SerializeField]
    private float potencia_error;

    [SerializeField]
    private GameObject water; // declaracion del objeto;
    private Vector3 positionwater;// Vector para tomar la posicion del objeto water
    private double heigth; // valor del volumen que va a ser calculado

    [SerializeField]
    private GameObject fuel; // declaracion del objeto;
    private Vector3 positionfuel;// Vector para tomar la posicion del objeto water
    private double heigth1; // valor del volumen que va a ser calculado

    [SerializeField]
    private double velocity; // valor con que velocidad se va a relizar la animacion del llenado del recipiente
    private double volumen;

    private int tf; //numero de intreacciones que suceden en el tiempo
    private int t; //numero de intreacciones que suceden en el tiempo
    private float volume; //declaro el valor del volumen a calacular
    [SerializeField]
    private TMP_Text[] label; //declaro los labels que tendran los valores de la variables anterior mente declaradas
    // Start is called before the first frame update

    //MEMORIA COMPARTIDA
    const string dllPath = "smClient64.dll";//importar la dll
    const string Sistema = "Sistema";
    [DllImport(dllPath)] // For 64 Bits System
    static extern int openMemory(String name, int type);  //abrir memoria


    [DllImport(dllPath)]
    static extern void setFloat(String memName, int position, float value);// leer

    [DllImport(dllPath)]
    static extern float getFloat(String memName, int position);//escribir
    // private Memoryshare memoryshare; //Intanciando la clase para la memoria compartida y poder hacer uso de ello
    private int position; //posicion de la memoria compartida
    private float[] Sppoint, Outpoint, errpoint; //valores de los arrreglos de las entradas, saldas, errores votados al realizar

    public GameObject fuego;
    Vector3 posF;
    public GameObject vapor;
    Vector3 posV;

    public GameObject turbina;
    Vector3 rotTurbina;

    public GameObject Cubo;
    public GameObject Cubo2;
    public GameObject Cubo3;
    public GameObject Cubocasa;
    Vector3 poscub;
    [SerializeField]
    private GraphChart[] graph;
    private float Timer = 1f;
    private float X= 4f;

    void Start()
    {

        openMemory(Sistema, 2);
        for (int i = 0; i < label.Length; i++)
        {
            label[i].text = System.Convert.ToString(0); //asignado el valor de cero a los label para la cual utilizamos la transformacion de propo lenguaje.
        }
        /*
        setFloat(Sistema, 9, sp_va.value);
        setFloat(Sistema, 10, sp_vf.value);
        setFloat(Sistema, 11, sp_vv.value);
        */
        setFloat(Sistema, 3, sp_H.value);
        setFloat(Sistema, 4, sp_Pr.value);
        setFloat(Sistema, 5, sp_Po.value);

        posF = fuego.transform.localPosition;
        posV = vapor.transform.localPosition;
        positionwater = water.transform.localPosition;
        positionfuel = fuel.transform.localPosition;
       // poscub = Cubocasa.transform.localPosition;

        //if (graph == null) // the ChartGraph info is obtained via the inspector
        //  return;
        graph[0].DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        graph[0].DataSource.ClearCategory("SpNivel"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
        graph[0].DataSource.ClearCategory("Altugrafica"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
        graph[0].DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        graph[1].DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        graph[1].DataSource.ClearCategory("SpPresion");
        graph[1].DataSource.ClearCategory("Pregrafica"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
        graph[1].DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        graph[2].DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        graph[2].DataSource.ClearCategory("SpPotencia");
        graph[2].DataSource.ClearCategory("Potegrafica"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
        graph[2].DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        graph[3].DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        graph[3].DataSource.ClearCategory("AltuError");
        graph[3].DataSource.EndBatch();
        graph[4].DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        graph[4].DataSource.ClearCategory("PreError");
        graph[4].DataSource.EndBatch();
        graph[5].DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
        graph[5].DataSource.ClearCategory("PoteError");
        graph[5].DataSource.EndBatch();
    }

    // Update is called once per frame
    void Update()
    {//TOTTAL EN LA MEMORIA 9 VALORES=3SET Y 6GET
        altura = getFloat(Sistema, 0);
        presion = getFloat(Sistema, 1);
        potencia = getFloat(Sistema, 2);
        //Debug.Log(temperatura);
        setFloat(Sistema, 3, sp_H.value);
        setFloat(Sistema, 4, sp_Pr.value);
        setFloat(Sistema, 5, sp_Po.value);
        //ERRORES
        altura_error = getFloat(Sistema, 6);
        presion_error = getFloat(Sistema, 7);
        potencia_error = getFloat(Sistema, 8);

        /*
        setFloat(Sistema, 3, sp_va.value);
        setFloat(Sistema, 4, sp_vf.value);
        setFloat(Sistema, 5, sp_vv.value);
        label[0].text = System.Convert.ToString(System.Math.Round(sp_va.value, 2)); //asigno el valor a label haciendo que solo apareca dos decimales
        label[1].text = System.Convert.ToString(System.Math.Round(sp_vf.value, 2));//asigno el valor a label haciendo que solo apareca dos decimales
        label[2].text = System.Convert.ToString(System.Math.Round(sp_vv.value, 2));//asigno el valor a label haciendo que solo apareca dos decimales 
        */
        label[0].text = System.Convert.ToString(System.Math.Round(sp_H.value, 2)); //asigno el valor a label haciendo que solo apareca dos decimales
        label[1].text = System.Convert.ToString(System.Math.Round(sp_Pr.value, 2));//asigno el valor a label haciendo que solo apareca dos decimales
        label[2].text = System.Convert.ToString(System.Math.Round(sp_Po.value, 2));//asigno el valor a label haciendo que solo apareca dos decimales 
        label[3].text = System.Convert.ToString(System.Math.Round(altura, 2));//asigno el valor a label haciendo que solo apareca dos decimales
        label[4].text = System.Convert.ToString(System.Math.Round(presion, 2));//asigno el valor a label haciendo que solo apareca dos decimales
        label[5].text = System.Convert.ToString(System.Math.Round(potencia, 2));//asigno el valor a label haciendo que solo apareca dos decimales
        label[6].text = System.Convert.ToString(System.Math.Round(presion, 2));//es el label de panel plomo lectura de presion el mismo que el panel azul
        /* ANIMACION AGUA,PARTICULAS,VAPOR
         //Slider Fuego
         posF.x = -sp_vf.value * 3.3f+1.9f;
         fuego.transform.position = new Vector3(posF.x, posF.y, posF.z);

         //Slider Vapor
         posV.z = sp_vv.value * 3.7f -0.84f;
         posV.y = 4f;
         posV.x = 0f;
         vapor.transform.position = (posV.x, posV.y, posV.z);


         //Vaceado del agua
         heigth = (sp_va.value * 0.68f) ; // calculo de la altura del objeto
         float scaley = System.Convert.ToSingle(positionwater.y - heigth * velocity); // conversion del valor velocidad valor 1
         water.transform.localScale = new Vector3(water.transform.localScale.x, scaley, water.transform.localScale.z); // afectando la escala del objeto 
         float transformy = System.Convert.ToSingle(1.42f-(positionwater.y + heigth * velocity));
         water.transform.localPosition = new Vector3(positionwater.x, transformy, positionwater.z);

         //Vaceado del gasolina

         heigth1 = (sp_vf.value * 0.68f); // calculo de la altura del objeto
         float scalefy = System.Convert.ToSingle(positionfuel.y - heigth1 * velocity); // conversion del valor
         fuel.transform.localScale = new Vector3(fuel.transform.localScale.x, scalefy, fuel.transform.localScale.z); // afectando la escala del objeto 
         float transformfy = System.Convert.ToSingle(1.42f - (positionfuel.y + heigth1 * velocity));
         fuel.transform.localPosition = new Vector3(positionfuel.x, transformfy, positionfuel.z);
         */

        turbina.transform.Rotate( new Vector3(0, 0, sp_Po.value) * Time.deltaTime*2f);//reemplazar sp_Po.value por potencia****

        if (sp_Po.value <= 120f && sp_Po.value > 61f)//reemplazar sp_Po.value por potencia
        {

            Instantiate(Cubo2, new Vector3(-3.574f, 1.477f, 3.284f), Quaternion.identity);


        }

        if (sp_Po.value <= 60f && sp_Po.value > 0.1f)//reemplazar sp_Po.value por potencia
        {
            Instantiate(Cubo, new Vector3(-4.5998f, 0.6404f, 4.768f), Quaternion.identity);



        }
        if (sp_Po.value <= 180f && sp_Po.value > 121f)//reemplazar sp_Po.value por potencia
        {

            Instantiate(Cubo3, new Vector3(-3.574f, 2.711f, 3.26f), Quaternion.identity);


        }

        pr_lectura.value = sp_Pr.value;//remplazar por presion **recordar q el slider esta con el min0 y max 170

        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            Timer= 1f;
            graph[0].DataSource.AddPointToCategoryRealtime("Altugrafica", X, sp_Pr.value,1f); // reemplazar sp_Pr.value por  altura**** DE matlab
            graph[0].DataSource.AddPointToCategoryRealtime("SpNivel", X, sp_H.value, 1f);
            graph[1].DataSource.AddPointToCategoryRealtime("Pregrafica", X, sp_H.value,1f); // reemplazar sp_H.value por presion**** DE matlab
            graph[1].DataSource.AddPointToCategoryRealtime("SpPresion", X, sp_Pr.value, 1f);
            graph[2].DataSource.AddPointToCategoryRealtime("Potegrafica", X,sp_H.value,1f); // reemplazar sp_H.value por potencia**** DE matlab
            graph[2].DataSource.AddPointToCategoryRealtime("SpPotencia", X, sp_Po.value, 1f);
            graph[3].DataSource.AddPointToCategoryRealtime("AltuError", X, sp_H.value, 1f);// reemplazar sp_H.value por altura_error**** DE matlab
            graph[4].DataSource.AddPointToCategoryRealtime("PreError", X, sp_Pr.value, 1f);// reemplazar sp_Pr.value por presion_error**** DE matlab
            graph[5].DataSource.AddPointToCategoryRealtime("PoteError", X, sp_Po.value, 1f);// reemplazar sp_Po.value por potencia_error**** DE matlab
            X++;

            
        }
    }




}
