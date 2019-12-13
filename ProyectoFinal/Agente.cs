/*
 * Created by SharpDevelop.
 * User: 1GX69LA_RS4
 * Date: 09/12/2019
 * Time: 10:40 a. m.
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
	/// Description of Agente.
	/// </summary>
	public class Agente
	{
		protected Vertice vActual;
		protected int velocidad;
		protected int id;
		protected Arista camino;
		protected int avanzar;
		protected int contador;
		Presa acechando;
		int distanciaAnterior;
		public Agente(Arista pa, int id)
		{
			distanciaAnterior = 201;
			acechando = null;
		//	solucion = true;
			contador = 0;
			this.id = id;
			this.velocidad = 5;
			avanzar = 5;
			camino = pa;
			vActual = pa.getOrigen();
		}
		public Agente()
		{
			
		}
		public bool moverAgente()
		{		
			velocidad += avanzar;
			 if(velocidad < camino.getListaPixeles().Count){
				return false;
			}
			velocidad = 5 + avanzar;
			vActual = camino.getDestino();
			return true;
		}
		public void tomarDecision(Point dst)
		{		
			double co = dst.Y - vActual.getCentro().Y;
			double ca = dst.X - vActual.getCentro().X;
			double tan = co/ca;
			double dg = Math.Atan2(co, ca) * (180/Math.PI);
			double menor = 360;
			double menorAux = 360;
			
			camino = null;
			Arista aux = null;
			int contador = 0;
			for(int i = 0; i<vActual.getLista().Count;i++)
			{
				double nuevo_angulo = calcularAngulo(dst, dg, i);
				if(nuevo_angulo < menor)
				{
					camino = vActual.getLista()[i];
					menor = nuevo_angulo;
				}
			}
		}
		private double calcularAngulo(Point dst, double dg, int i)
		{
			double co = vActual.getLista()[i].getDestino().getCentro().Y - vActual.getCentro().Y;
			double ca = vActual.getLista()[i].getDestino().getCentro().X - vActual.getCentro().X;
			double tan = co/ca;
			double radian = dg * (Math.PI/180);
			
			double nuevo_angulo = Math.Atan2(co, ca) * (180/Math.PI);
			double suplementario;
			
			
			if(dg < 0 )
				dg = dg + 360;
			
			if(nuevo_angulo < 0)
				nuevo_angulo = nuevo_angulo + 360;				
				
			if(nuevo_angulo>dg)
				nuevo_angulo = nuevo_angulo-dg;
			else
				nuevo_angulo = dg - nuevo_angulo;
			
			suplementario = 360 - nuevo_angulo;
			
			if(suplementario < nuevo_angulo)
				nuevo_angulo = suplementario;
			
			return nuevo_angulo;
		}
		public void dibujarFlecha(Point dst, Bitmap bm)
		{
			Graphics gr = Graphics.FromImage(bm);
		
			double co = dst.Y - camino.getListaPixeles()[velocidad].Y;
			double ca =  dst.X - camino.getListaPixeles()[velocidad].X;
			double tan = co/ca;
			double dg = Math.Atan2(co, ca) * (180/Math.PI);
			
			double hipotenusa = 45; 	
			double b = hipotenusa * Math.Sin((dg*(Math.PI/180))) + camino.getListaPixeles()[velocidad].Y;
			double a = hipotenusa * Math.Cos((dg*(Math.PI/180))) + camino.getListaPixeles()[velocidad].X;
			Pen p = new Pen(Color.Green, 5);
			Point aa = new Point(Convert.ToInt32(a),	Convert.ToInt32(b));
			Point bb = new Point(camino.getListaPixeles()[velocidad].X, camino.getListaPixeles()[velocidad].Y);
			gr.DrawLine(p, bb, aa);
			
		}
		public Presa getAcechando()
		{
			return acechando;
		}
		public void setAcechando(Presa dato)
		{
			acechando = dato;
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
			 avanzar += d;
		}
		public void setCamino(Arista p)
		{
			camino = p;
		}
		public Arista getCamino()
		{
			return camino;
		}
		public int getId()
		{
			return id;
		}
		public int getContador()
		{
			return contador;
		}
		public Point getListaCamino()
		{
			return this.camino.getListaPixeles()[velocidad];
		}
		public int getDistanciaAnterior()
		{
			return distanciaAnterior;
		}
		public void setDistanciaAnterior(int dato)
		{
			distanciaAnterior =  dato;
		}
	}
}
