/*
 * Created by SharpDevelop.
 * User: 1GX69LA_RS4
 * Date: 09/12/2019
 * Time: 05:59 p. m.
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
	/// Description of Presa.
	/// </summary>
	public class Presa
	{
		Vertice vActual;
		int vidas;
		int velocidad;
		int id;
		List<Arista> camino;
		int avanzar;
		int contador;
		bool acechado;
		int intocable;
		public Presa()
		{
			
		}
		public Presa(List<Arista> pa, int id)
		{
			intocable = 0;
			vidas = 1;
			acechado = true;
			contador = 0;
			this.id = id;
			this.velocidad = 5;
			avanzar = 10;
			camino = new List<Arista>(pa);
			vActual = camino[0].getOrigen();
		}
		public bool moverAgente(List<Agente> agentes)
		{	
			if(acechado){
				velocidad += 10;
				if(velocidad >= camino[contador].getListaPixeles().Count)
				{
					contador++;
					if(contador >= camino.Count){
						contador--;
						vActual = camino[contador].getDestino();
						contador = 0;
						velocidad = 5;
						return false;
					}
					velocidad = 5;
					vActual = camino[contador].getOrigen();
				}
			}else
			{
				velocidad -= 10;
				if(velocidad < 0)
				{
					validarSeguridad(agentes);
					velocidad = 5;
				}
			}
			return true;
		}
		public Arista tomarDecision(List<Agente> agentes)
		{
			if(vActual.getLista().Count == 1){
				MessageBox.Show("Encerrado");
				//return;
			}
			int flag = 0;
			Arista aux = null;
			//for(int i = 0; i<vActual.getLista().Count;i++)
			while(flag != 5)
			{
				Random a = new Random();
				int random = a.Next(0, vActual.getLista().Count);
				for(int j = 0; j<agentes.Count;j++)
				{
					if(vActual.getLista()[random].getDestino().getID() != agentes[j].getCamino().getOrigen().getID() || vActual.getLista()[random].getOrigen().getID() != agentes[j].getCamino().getDestino().getID())
					{
						flag++;
					}
				}
				if(flag == agentes.Count)
				{
					aux = vActual.getLista()[random];
					break;
				}
				flag = 0;
			}
			camino.Clear();
			velocidad = 5;
			contador = 0;
			return  aux;
		}
		public void validarSeguridad(List<Agente> agentes)
		{
			for(int i = 0; i<agentes.Count;i++)
			{
				if(this.getAristaActual().getID() == agentes[i].getCamino().getID())
				{
					acechado = false;
					return;
				}
			}
			acechado = true;
		}
		public void setAcechado(bool l)
		{
			acechado = l;
		}
		public bool getAcechado()
		{
			return acechado;
		}
		public void setVerticeActual(Vertice a)
		{
			vActual = a;
		}
		public Vertice getVertice()
		{
			return vActual;
		}
		public int getAvanzar()
		{
			return avanzar;
		}
		public void setAvanzar(int d)
		{
			 avanzar = d;
		}
		public void reiniciarVelocidad()
		{
			velocidad = 5;
		}
		public void setCamino(List<Arista> p)
		{
			camino = p;
		}
		public List<Arista> getCamino()
		{
			return camino;
		}
		public int getId()
		{
			return id;
		}
		public Arista getAristaActual()
		{
			return camino[contador];
		}
		public int getContador()
		{
			return contador;
		}
		public int getVida()
		{
			return vidas;
		}
		public void reiniciarVida()
		{
			vidas = 0;
		}
		public void disminuirVida()
		{
			vidas--;
		}
		public void aumentarVida()
		{
			vidas++;
		}
		public Point getListaCamino()
		{
			return this.camino[contador].getListaPixeles()[velocidad];
		}
		public int getIntocable()
		{
			return intocable;
		}
		public void aumentarIntocable()
		{
			intocable++;
		}
		public void setIntocable(int dato)
		{
			intocable = dato;
		}
	}
}
