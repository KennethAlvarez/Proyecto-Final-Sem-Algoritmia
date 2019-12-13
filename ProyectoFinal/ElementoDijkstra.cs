/*
 * Created by SharpDevelop.
 * User: 1GX69LA_RS4
 * Date: 09/12/2019
 * Time: 08:57 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ProyectoFinal
{
	/// <summary>
	/// Description of ElementoDijkstra.
	/// </summary>
	public class ElementoDijkstra
	{
		int peso;
		Vertice vActual;
		Vertice proveniente;
		bool definitivo; 
		public ElementoDijkstra(int p, Vertice ide)
		{
			vActual = ide;
			peso = p; 
			definitivo = false;
		}
		public ElementoDijkstra(int p, Vertice org, Vertice ide)
		{
			peso = p; 
			vActual = ide;
			definitivo = false;
			proveniente = org;
		}
		public void setDefinitivo(bool fl)
		{
			definitivo = fl;
		}
		public bool getDefinitivo()
		{
			return definitivo;
		}
		public void setPeso(int p)
		{
			peso = p;
		}
		public int getPeso()
		{
			return peso;
		}
		public void setProveniente(Vertice p)
		{
			proveniente = p;
		}
		public Vertice getProveniente()
		{
			return proveniente;
		}
		public Vertice getActual()
		{
			return vActual;
		}
		public void setActual(Vertice dato)
		{
			vActual = dato;
		}
	}
}
