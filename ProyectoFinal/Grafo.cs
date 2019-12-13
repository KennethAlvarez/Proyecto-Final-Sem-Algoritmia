/*
 * Created by SharpDevelop.
 * User: 1GX69LA_RS4
 * Date: 09/12/2019
 * Time: 10:12 a. m.
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
	/// Description of Grafo.
	/// </summary>
	public class Grafo
	{
		List<Vertice> vL;
		bool senueloExists;
		public Grafo()
		{
			senueloExists = false;
			vL = new List<Vertice>();
		}
		public void agregarVertice(Vertice v)
		{
			this.vL.Add(v);
		}
		public List<Vertice> getVertices()
		{
			return this.vL;
		}
		public bool getFlag()
		{
			return senueloExists;
		}
		public void setFlag(bool f)
		{
			this.senueloExists = f;
		}
	}
	public class Vertice
	{
		int id;
		List<Arista> aL;
		Point centro;
		int radio;
		List<int> rastros;
		public Vertice(int ide, Point cntr, int r)
		{
			this.id = ide;
			this.centro = new Point(cntr.X, cntr.Y);
			this.radio = r;
			this.rastros = new List<int>();
			aL = new List<Arista>();
			
		}
		public int getRadio()
		{
			return radio;
		}
		public void agregarArista(Arista l)
		{
			this.aL.Add(l);
		}
		public int getID()
		{
			return this.id;
		}
		public void setID(int a)
		{
			id = a;
		}
		public List<Arista> getLista()
		{
			return this.aL;
		}
		public Point getCentro()
		{
			return centro;
		}
		public List<int> getRastros()
		{
			return rastros;
		}
		public void setRastros(int tamano)
		{
			rastros = new List<int>(tamano);
		}
	}
	public class Arista
	{
		int id;
		Vertice origen;
		Vertice destino;
		int ponderacion;
		List<Point> camino_pixeles;
		public Arista(int ide, Vertice v_1, Vertice v_2, int p, List<Point> cam)
		{
			this.id = ide;
			origen = v_1;
			destino = v_2;
			ponderacion = p;
			camino_pixeles = new List<Point>(cam);
		}
		public Vertice getOrigen()
		{
			return origen;
		}
		public Vertice getDestino()
		{
			return destino;
		}
		public int getPonderacion()
		{
			return ponderacion;
		}
		public int getID()
		{
			return id;
		}
		public void setPonderacion(int dato)
		{
			ponderacion = dato;
		}
		public List<Point> getListaPixeles()
		{
			return camino_pixeles;
		}
	}
}