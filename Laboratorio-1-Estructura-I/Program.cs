using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
					
public class Program
{
	public class InputLab
	{
		public Dictionary<string, bool>[] input1 { get; set; }
		public string[] input2 { get; set; }
	}

    //Esta clase permite eliminar elementos repetidos en un array
    public static T[] removeDuplicates<T>(T[] array)
    {
        HashSet<T> set = new HashSet<T>(array);
        T[] result = new T[set.Count];
        set.CopyTo(result);
        return result;
    }

	public static void Main()
	{
        //Variables globales
        string archivo, resultado = "", subresultado="", respuesta = "", nombre = DateTime.Now.ToString("hh-mm-ss tt"), respuestaDeCercanias = "",  respuestaRequerimientos = "", respuestaCoincidencias = "";
        var ruta = Directory.GetCurrentDirectory();
        string[] mapas, cercanias = new string[1000], requeridos = new string[10], coincidencias = new string[10];
        int mapa = 1, NotaMaxima, apartment, nota, anotados, numRequerimientos, numCoincidencias;
        int[] notas = new int[400];

        using (StreamReader r = new StreamReader("input_challenge.jsonl"))  
        {  
            archivo = r.ReadToEnd();
        } 

        mapas = archivo.Split("\n");

        foreach(string mapa_actual in mapas)
        {
            Console.WriteLine($"------------------------------");
            Console.WriteLine($"            MAPA {mapa}           ");
            Console.WriteLine($"------------------------------");
            InputLab input = JsonSerializer.Deserialize<InputLab>(mapa_actual)!;

            //Acá se reinician las variables
            apartment = 0; 
            anotados = 0;
            numRequerimientos = 0;
            numCoincidencias = 0;

            //Acá se reinician los datos de los arrays
            Array.Clear(notas);
            Array.Clear(cercanias);
            Array.Clear(requeridos);
            Array.Clear(coincidencias);

            foreach(Dictionary<string, bool> apartamento in input.input1)
            {
                nota = 0; 
                Console.WriteLine($"apartment: {apartment}");
                foreach(KeyValuePair<string, bool> entry in apartamento)
                {
                    Console.WriteLine($"key {entry.Key} - value {entry.Value}");
                    if ((entry.Key != "") && (entry.Value == true))
                    {
                        cercanias[anotados]=entry.Key;
                        anotados++;
                    }
                    foreach(string requirement in input.input2)
                    {
                        if((entry.Key == requirement) && (entry.Value == true))
                        {
                            nota++;
                        }
                    }
                }
                notas[apartment]=nota;
                apartment++;
                Console.WriteLine($"--------------");
            }

            //Acá se eliminan las cercanias duplicadas
            string[] cercanias_filtadas = removeDuplicates(cercanias);

            //Acá se presentan las cercanías en formato de texto por si se desean imprimir por pantalla
            respuestaDeCercanias = "";
            for (int i = 0; i < cercanias_filtadas.Length; i++)
            {
                if (i==0)
                {
                    respuestaDeCercanias = $"----> Cercanías de este mapa: {cercanias_filtadas[i]}";
                } else
                {
                    respuestaDeCercanias = respuestaDeCercanias + $",{cercanias_filtadas[i]}";
                }
            }
            respuestaDeCercanias = respuestaDeCercanias[..^1];

            foreach(string requirement in input.input2)
            {
                requeridos[numRequerimientos] = requirement;
                numRequerimientos++;
                Console.WriteLine($"requirement: {requirement}");
            }

            //Acá se presentan los requerimientos en formato de texto por si se desean imprimir por pantalla
            respuestaRequerimientos="";
            for (int i = 0; i < numRequerimientos; i++)
            {
                if (i==0)
                {
                    respuestaRequerimientos = $"----> Requerimientos de este mapa: {requeridos[i]}";
                } else
                {
                    respuestaRequerimientos = respuestaRequerimientos + $",{requeridos[i]}";
                }
            }

            //Acá se comparan los dos arrays
            for (int i = 0; i < cercanias_filtadas.Length-1; i++)
            {
                for (int j = 0; j < numRequerimientos; j++)
                {
                    if (cercanias_filtadas[i] == requeridos[j])
                    {
                        bool existe = false;
                        for (int m = 0; m < numCoincidencias; m++)
                        {
                            if (coincidencias[m] == requeridos[j])
                            {
                                existe = true;
                            }
                        }
                        if (!existe)
                        {
                            coincidencias[numCoincidencias++] = requeridos[j];
                        }
                    }
                }
            }

            //Acá se presentan las coincidencias en formato de texto por si se desean imprimir por pantalla
            respuestaCoincidencias = "";
            for (int i = 0; i < numCoincidencias; i++)
            {
                if (i==0)
                {
                    respuestaCoincidencias = $"----> Coincidencias de este mapa: {coincidencias[i]}";
                } else
                {
                    respuestaCoincidencias = respuestaCoincidencias + $",{coincidencias[i]}";
                }
            }

            NotaMaxima = 0;
            foreach(int notaPosible in notas)
            {
                if(NotaMaxima < notaPosible)
                {
                    NotaMaxima = notaPosible;
                }
            }

            subresultado="";
            if ((NotaMaxima!=0) && (numCoincidencias==numRequerimientos))
            {
                for (int i = 0; i < notas.Length; i++) 
                {
                    if(notas[i]==NotaMaxima)
                    {
                        if(subresultado=="")
                        {
                            subresultado = subresultado + i;
                        } else
                        {
                            subresultado = subresultado + ", " + i;
                        }
                    }
                }
            }

            //***Descomentar y comentar según corresponda. Una debe estar descomentada y las otras comentadas.
            //resultado = resultado + $"Mapa {mapa}: [{subresultado}] {respuestaDeCercanias} \n";
            //resultado = resultado + $"Mapa {mapa}: [{subresultado}] {respuestaRequerimientos} \n";
            //resultado = resultado + $"Mapa {mapa}: [{subresultado}] {respuestaCoincidencias} \n";
            resultado = resultado + $"Mapa {mapa}: [{subresultado}] \n";

            //Descomentar la siguiente línea solo si se quiere imprimir por pantalla la nota máxima obtenida
            //Console.WriteLine($"Nota máxima obtenida: {NotaMaxima}");

            if(mapa==100) //La cantidad de mapas se encontró gracias a Word
            {
                break;
            } else
            {
                mapa++;
            }
        }
        respuesta = "---------------- \n";
        respuesta = respuesta + "   RESULTADOS \n";
        respuesta = respuesta + "---------------- \n";
        respuesta = respuesta + resultado;
        Console.WriteLine(respuesta);
        //Console.WriteLine(ruta);
        System.IO.File.WriteAllText($@"{ruta}\Respuesta generada a la hora {nombre}.txt", respuesta);
        Console.ReadLine();
	}
}
