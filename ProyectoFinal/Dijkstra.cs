/*
 * Created by SharpDevelop.
 * User: 1GX69LA_RS4
 * Date: 09/12/2019
 * Time: 08:24 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFinal
{
	/// <summary>
	/// Description of Dijkstra.
	/// </summary>
	public class Dijkstra
	{
		int infinito;
		List<ElementoDijkstra> candidatos;
		Grafo grafo;
		Vertice origen, objetivo;
		List<Arista> camino;
		String caminoString;
		public Dijkstra(Grafo grafo, Vertice origen, Vertice objetivo)
		{
			this.objetivo = objetivo;
			camino = new List<Arista>();
			this.grafo = grafo;	
			this.origen = origen;
			candidatos = new List<ElementoDijkstra>();
			infinito = sumarValores();
			caminoString = "";
			for(int i = 0; i<grafo.getVertices().Count;i++)
			{
				ElementoDijkstra dks;
				if(i != origen.getID()-1)
					dks = new ElementoDijkstra(infinito, grafo.getVertices()[i]);
				else
					dks = new ElementoDijkstra(0, origen, grafo.getVertices()[i]);
				
				candidatos.Add(dks);
			}
		}
		public String getCaminoString()
		{
			return caminoString;
		}
		private int sumarValores()
		{
			for(int i = 0; i<grafo.getVertices().Count;i++){
				for(int j = 0; j<grafo.getVertices()[i].getLista().Count;j++){
					infinito += grafo.getVertices()[i].getLista()[j].getPonderacion();
				}
			}
			return infinito;
		}
		public List<Arista> ejecutarDijkstra()
		{
			ElementoDijkstra aux;
			while(!solucion())
			{
				aux = seleccion();
				if(aux.getPeso() == infinito){
					break;
				}
					factible(aux);
			}
			generarCamino();
			return camino;
		}
		private bool solucion()
		{
			for(int i = 0; i<candidatos.Count;i++)
			{
				if(!candidatos[i].getDefinitivo())
					return false;
			}
			return true;			
		}
		public ElementoDijkstra seleccion()
		{
			ElementoDijkstra auxiliar = new ElementoDijkstra(infinito+1, null);
			int j = -1;
			for(int i = 0; i<candidatos.Count;i++)
			{
				if(!candidatos[i].getDefinitivo())
				{	
					if(candidatos[i].getPeso() < auxiliar.getPeso()){
						auxiliar = candidatos[i];
						j = i;
					}
				}
			}
			candidatos[j].setDefinitivo(true);
			return auxiliar;
		}
		private void factible(ElementoDijkstra aux)
		{
			int total = 0;
			for(int i = 0; i<aux.getActual().getLista().Count;i++)
			{
				total += aux.getPeso() + aux.getActual().getLista()[i].getPonderacion();
				//aux.getActual().getLista()[i]
				if(total < candidatos[aux.getActual().getLista()[i].getDestino().getID()-1].getPeso())
				{
					candidatos[aux.getActual().getLista()[i].getDestino().getID()-1].setPeso(total);
					candidatos[aux.getActual().getLista()[i].getDestino().getID()-1].setProveniente(aux.getActual());
				}
				total = 0;
			}
		}
		public int getMaximo()
		{
			return infinito;
		}
		public void generarCamino()
		{
			caminoString = "";
			camino.Clear();
			ElementoDijkstra aux = candidatos[objetivo.getID()-1];
			while(aux.getActual().getID() != origen.getID())
			{
				if(aux.getActual() == null || aux.getProveniente() == null)
				{
					camino.Clear();
					return;
				}
				camino.Add(encontrarVertice(aux.getActual(), aux.getProveniente()));
				aux = candidatos[aux.getProveniente().getID()-1];
			}
			camino.Reverse();
			for(int i = 0; i<camino.Count;i++)
				caminoString+=camino[i].getOrigen().getID()+" ->";
			
			caminoString+=" "+objetivo.getID();
			MessageBox.Show(caminoString);
		}
		private Arista encontrarVertice(Vertice v_1, Vertice v_2)
		{
			for(int i = 0; i < v_2.getLista().Count; i++)
			{
				if(v_1.getID() == v_2.getLista()[i].getDestino().getID())
				{
					return v_2.getLista()[i];
				}
			}
			MessageBox.Show("Regresando null");
			return null;
		}
	}
}
