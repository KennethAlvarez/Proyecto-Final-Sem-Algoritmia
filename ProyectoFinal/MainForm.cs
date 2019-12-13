/*
 * Created by SharpDevelop.
 * User: 1GX69LA_RS4
 * Date: 09/12/2019
 * Time: 09:57 a. m.
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
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Grafo grafo = new Grafo();
		Bitmap bm;
		Bitmap back;
		List<Presa> lPresas = new List<Presa>();
		List<Agente> aList = new List<Agente>();
		int porActualizar = -1;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	
		void Button1Click(object sender, EventArgs e)
		{
			aList.Clear();
			grafo.getVertices().Clear();
			this.comboBox1.Items.Clear();
			this.comboBox2.Items.Clear();
			this.comboBox3.Items.Clear();
			this.openFileDialog1.ShowDialog();
			back = new Bitmap(this.openFileDialog1.FileName);
			this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
			this.pictureBox1.BackgroundImage = back;
			bm = new Bitmap(back.Width, back.Height);
			this.pictureBox1.Image = bm;
			this.button2.Enabled = true;			
		}
		
		void Button2Click(object sender, EventArgs e)
		{	
			Point centro;
			int radio = 0;
			int contador = 1;
			for(int i = 0; i<back.Width;i++)
			{
				for(int j = 0; j<back.Height;j++)
				{
					if(back.GetPixel(i, j) == Color.FromArgb(0, 0, 0))
					{
						centro = obtenerCentro(i, j);
						radio = obtenerRadio(centro);
						dibujarCirculo(i, j, radio);
						if(radio != -1)
						{
							Vertice verticeAux = new Vertice(contador, centro, radio);
							grafo.agregarVertice(verticeAux);
							contador++;
						}
					}
				}
			}
			generarAristas();
			dibujarEtiqueta();
			//mostrarInformacion();
		//	generarTreeView();
			generarComboBox();
			pictureBox1.Refresh();			
		}
		void generarComboBox()
		{
			for(int i = 0; i<grafo.getVertices().Count;i++)
			{
				this.comboBox1.Items.Add(i+1);
				this.comboBox2.Items.Add(i+1);
				this.comboBox3.Items.Add(i+1);
			}
		}
		void dibujarEtiqueta()
		{
			Graphics grap = Graphics.FromImage(back);

			for(int i = 0; i<grafo.getVertices().Count;i++)
			{	
				Font a = new Font("Arial", grafo.getVertices()[i].getRadio());
				SolidBrush brocha = new SolidBrush(Color.Black);
				StringFormat cad = new StringFormat();
				cad.FormatFlags = StringFormatFlags.DirectionVertical;
				string cadena = ""+(i+1);
				grap.DrawString(cadena, a, brocha, grafo.getVertices()[i].getCentro().X, grafo.getVertices()[i].getCentro().Y);
			}
			pictureBox1.Refresh();
		}
		Point obtenerCentro(int x, int y)
		{
			int aux=0;
			int auxy=0;
			aux = x;
			auxy = y;
			//MessageBox.Show("antes x: "+aux+" y: "+auxy);
			while(back.GetPixel(aux, auxy) == Color.FromArgb(0, 0, 0))
			{
				aux++;
			}
			int xTotal = (aux-x)/2;
			aux = x;
			while(back.GetPixel(aux, auxy) == Color.FromArgb(0, 0, 0))
			{
				auxy++;
			}
			int yTotal = (auxy-y)/2;
			Point centro = new Point((xTotal+x), (yTotal+y));
			//MessageBox.Show("Total: "+(xTotal+x)+" "+(yTotal+y));
			return centro;
			
		}
		int obtenerRadio(Point p)
		{	
			int _x = p.X;
			int _y = p.Y;
			int contx = 0;
			int conty = 0;
			
			while(back.GetPixel(_x, p.Y) != Color.FromArgb(255,255,255))
			{
				_x++;
				contx++;	
			}
			while(back.GetPixel(p.X, _y) != Color.FromArgb(255,255,255))
			{
				_y++;
				conty++;
			}
			int radio = contx - conty;
			if(radio < -10 || radio > 10)
			{
				return -1;
			}
			return contx;
		}
		void dibujarCirculo(int x, int y, int radio)
		{
			int _x = x;
			int _y = y;
			Color col;
			if(radio == -1)
			{			
				//MessageBox.Show("x: "+x+" Y: "+y);
				col = Color.FromArgb(255,255,255);
			}else
			{
				col = Color.Blue;
			}
			while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
			{
				while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
				{
					back.SetPixel(_x, _y, col);
					_y--;
				}
				//pictureBox1.Refresh();
				_x++;
				_y = y;
			}
			_x = x-1;
			_y = y;
			while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
			{
				while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
				{
					back.SetPixel(_x, _y, col);
					_y--;
				}
				//pictureBox1.Refresh();
				_x--;
				_y = y;
			}
			_x = x;
			_y = y+1;
			while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
			{
				while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
				{
					back.SetPixel(_x, _y, col);
					_y++;
				}
				//pictureBox1.Refresh();
				_x++;
				_y = y+1;
			}
			_x = x-1;
			_y = y+1;
			while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
			{
				while(back.GetPixel(_x, _y) != Color.FromArgb(255, 255, 255))
				{
					back.SetPixel(_x, _y, col);
					_y++;
				}
				//pictureBox1.Refresh();
				_x--;
				_y = y;
			}
			pictureBox1.Refresh();

		}
		int obtenerDistancia(Point origenCentro, Point destinoCentro)
		{
			int distancia = (int)Math.Round(Math.Sqrt(Math.Pow((destinoCentro.X - origenCentro.X), 2) + Math.Pow((destinoCentro.Y - origenCentro.Y), 2)));
			return distancia;		
		}
		void generarAristas()
		{
			int ponderacion = 0;
			int j = 0;
			int generarID = 0;
			//Arista aux = new Arista(-1, null, null, 5000);
			List<Point> pList;
			for(int i = 0; i<grafo.getVertices().Count;i++)
			{
				for(j = i+1; j<grafo.getVertices().Count;j++)
				{
					ponderacion = obtenerDistancia(grafo.getVertices()[i].getCentro(), grafo.getVertices()[j].getCentro());
					pList = new List<Point>(detectarObstaculos(grafo.getVertices()[i].getCentro(), grafo.getVertices()[j].getCentro()));
					if(pList.Count != 0)
					{
						Arista arista = new Arista(generarID, grafo.getVertices()[i], grafo.getVertices()[j], ponderacion, pList);
						grafo.getVertices()[i].agregarArista(arista);
						generarID++;
						pList.Reverse();
						Arista _arista = new Arista(generarID, grafo.getVertices()[j], grafo.getVertices()[i], ponderacion, pList);
						grafo.getVertices()[j].agregarArista(_arista);
						generarID++;
					//	MessageBox.Show("Primero elemento X: "+arista.getListaPixeles()[0].X+" Y:"+arista.getListaPixeles()[0].Y);
						
						//MessageBox.Show("Primero elemento X: "+_arista.getListaPixeles()[0].X+" Y:"+_arista.getListaPixeles()[0].Y);
						pictureBox1.Refresh();
					}

				}
			}
			pictureBox1.Refresh();
			pintarAristas();
		}
		void pintarAristas(List<Arista> lAristas)
		{
			Pen lapiz = new Pen(Color.Green, 10);
			Graphics gr = Graphics.FromImage(bm);
			for(int i = 0; i<lAristas.Count;i++)
			{
				gr.DrawLine(lapiz, lAristas[i].getOrigen().getCentro(), lAristas[i].getDestino().getCentro());
				pictureBox1.Refresh();
			}
			
		}
		void pintarAristas()
		{
			Pen lapiz = new Pen(Color.Red, 3);
			Graphics gr = Graphics.FromImage(back);
			for(int i = 0; i<grafo.getVertices().Count;i++)
			{
				for(int j = 0; j<grafo.getVertices()[i].getLista().Count;j++)
				{
					if(grafo.getVertices()[i].getID() < grafo.getVertices()[i].getLista()[j].getDestino().getID());
					gr.DrawLine(lapiz, grafo.getVertices()[i].getCentro(),grafo.getVertices()[i].getLista()[j].getDestino().getCentro());
					
				}
				pictureBox1.Refresh();
			}
			
		}
		List<Point> detectarObstaculos(Point origenCentro, Point DestinoCentro)
		{
			int x = origenCentro.X;
			int y = origenCentro.Y;
			int x1 = DestinoCentro.X;
			int y1 = DestinoCentro.Y;
			List<Point> camino = new List<Point>();
			float Dx = x1 -x;
			float Dy = y1-y;
			float m, b;
			int band = 0;
			if(Math.Abs(Dx) < Math.Abs(Dy))
			{
				if(Dy != 0)
				{
					m = Dx / Dy;
					b = x - m*y;
					if(Dy < 0)
					{
						Dy = -1;
					}
					else
					{
						Dy = 1;
					}
				while(y != y1)
				{					
					y += (int)Dy;
					x = (int)Math.Round(m*y + b);
					switch(band)
					{
						case 0:
							if(Color.Equals(Color.FromArgb(255,255,255), back.GetPixel(x, y)))
								band = 1;
							break;
						case 1:
							if(!Color.Equals(Color.FromArgb(255,255,255), back.GetPixel(x, y)))
							{
								band = 2;
							}
							break;
						case 2:
							if(Color.Equals(Color.FromArgb(255,255,255), back.GetPixel(x, y)))
							{
								camino.Clear();
								//MessageBox.Show("Origen X:"+origenCentro.X+" Y: "+origenCentro.Y+" Destino X: "+DestinoCentro.X+" Y: "+DestinoCentro.Y);
								return camino;
							}
							break;
						}
					Point p = new Point(x, y);
					camino.Add(p);
						
					}
				}
			}else
			{
				m = Dy / Dx;
				b = y - m*x;
				if(Dx < 0)
				{
					MessageBox.Show("aagria");
					                
					Dx = -1;
				}
				else
				{
					Dx = 1;
				}
				while(x != x1)
				{
					x += (int)Dx;
					y = (int)Math.Round(m*x + b);
					switch(band)
					{
						case 0:
							if(Color.Equals(Color.FromArgb(255,255,255), back.GetPixel(x, y)))
								band = 1;
							break;
						case 1:
							if(!Color.Equals(Color.FromArgb(255,255,255), back.GetPixel(x, y)))
							{
								band = 2;
							}
							break;
						case 2:
							if(Color.Equals(Color.FromArgb(255,255,255), back.GetPixel(x, y)))
							{
								camino.Clear();
								return camino;
							} break;
					}
					
					Point p = new Point(x, y);
					camino.Add(p);
				}
			}
			return camino;
		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			this.comboBox1.Enabled = false;
			this.comboBox2.Enabled = true;			
		}
		void ComboBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			this.comboBox1.Enabled = true;
			this.button3.Enabled = true;
		}
		void ComboBox3SelectedIndexChanged(object sender, EventArgs e)
		{
			this.button4.Enabled = true;			
		}
		void Button3Click(object sender, EventArgs e)
		{
			Dijkstra a = new Dijkstra(grafo, grafo.getVertices()[this.comboBox1.SelectedIndex], grafo.getVertices()[this.comboBox2.SelectedIndex]);
			Presa presa = new Presa(a.ejecutarDijkstra(), 1);
			dibujarEspectro(presa.getListaCamino(), false);
			lPresas.Add(presa);
			pictureBox1.Refresh();
		}
		void Button4Click(object sender, EventArgs e)
		{
			Random a = new Random();
			Agente agente = new Agente(grafo.getVertices()[this.comboBox3.SelectedIndex].getLista()[a.Next(0, grafo.getVertices()[this.comboBox3.SelectedIndex].getLista().Count)], 1);
			aList.Add(agente);
			dibujarEspectro(agente.getVertice().getCentro(), true);
			pictureBox1.Refresh();
		}
		void dibujarEspectro(Point centro, bool band)
		{
			Graphics gr = Graphics.FromImage(bm);
			if(!band)
			gr.FillEllipse(Brushes.Purple, centro.X-12, centro.Y-12, 25, 25);
			else
			gr.FillEllipse(Brushes.Black, centro.X-12, centro.Y-12, 25, 25);
			//pictureBox1.Refresh();
			
		}
		Presa presaMasCercana(int i)
		{
			Presa presa = null;
			int max = 1000;
			int dist = 0;
			for(int j = 0; j<lPresas.Count;j++)
			{
				dist = obtenerDistancia(aList[i].getListaCamino(), lPresas[j].getListaCamino());
				if(dist < 200)
				{
					if(dist < max)
					{
						presa = lPresas[j];
						max = dist;
					}
				}
			}
			return presa;
		}
		bool validarToque(Agente agente, Presa presa)
		{
			int radioD = agente.getAcechando().getAristaActual().getDestino().getRadio();
			int radioO =  agente.getAcechando().getAristaActual().getOrigen().getRadio();
			bool flag = false;
			
			if(agente.getCamino().getID() == agente.getAcechando().getAristaActual().getID())
				flag = true;
			
			if(agente.getCamino().getOrigen().getID() == agente.getAcechando().getAristaActual().getDestino().getID()){
				if(agente.getCamino().getDestino().getID() == agente.getAcechando().getAristaActual().getOrigen().getID())
					flag = true;
			}
					
			if(obtenerDistancia(agente.getAcechando().getListaCamino(), agente.getAcechando().getAristaActual().getDestino().getCentro()) < (radioD+12))
				flag = false;
			
			if(obtenerDistancia(agente.getAcechando().getListaCamino(), agente.getAcechando().getAristaActual().getOrigen().getCentro()) < (radioO+12))
				flag = false;
				
			if(flag) 
			{
				if(presa.getVida() > 0){
					presa.reiniciarVelocidad();
					flag = false;
					presa.disminuirVida();
				}
			}
			return flag;
		}
		bool actualizarAgentes()
		{
			Random random = new Random();
			for(int i = 0; i<aList.Count;i++)
			{	
				if(aList[i].getAcechando() == null)
				{
					if(aList[i].moverAgente()){
						int max = random.Next(0, aList[i].getVertice().getLista().Count);
						aList[i].setCamino(aList[i].getVertice().getLista()[max]);
						//aList[i].setSolucion(false);
					}
					dibujarEspectro(aList[i].getListaCamino(), true);
					aList[i].setAcechando(presaMasCercana(i));
				}else
				{
					if(aList[i].moverAgente())
						aList[i].tomarDecision(aList[i].getAcechando().getListaCamino());
					dibujarEspectro(aList[i].getListaCamino(), true);
					int distancia = obtenerDistancia(aList[i].getListaCamino(), aList[i].getAcechando().getListaCamino());
					
					aList[i].dibujarFlecha(aList[i].getAcechando().getListaCamino(), bm);
					
					if(distancia < 20){
						if(validarToque(aList[i], aList[i].getAcechando()))
							return false;
					}
					
					if(distancia < aList[i].getDistanciaAnterior())
					{
						if(aList[i].getAvanzar() < 15)
						aList[i].setAvanzar(1);
					}
							
					if(distancia > 200){
						aList[i].setAcechando(null);
						aList[i].setAvanzar(5 - aList[i].getAvanzar());
					}
				}
				//dibujarEspectro(aList[i].getListaCamino(), true);		
			}
			return true;
		}
		bool actualizarPresas()
		{
			for(int i = 0; i<lPresas.Count;i++)
			{					
				if(!lPresas[i].moverAgente(aList)){
					lPresas[i].aumentarVida();
					porActualizar = i;
					return false;
				}
				int radioD = lPresas[i].getAristaActual().getDestino().getRadio();
				int radioO = lPresas[i].getAristaActual().getOrigen().getRadio();
				
				if(obtenerDistancia(lPresas[i].getListaCamino(), lPresas[i].getAristaActual().getOrigen().getCentro()) < (radioO+12))
				{
					lPresas[i].aumentarIntocable();
					if(lPresas[i].getIntocable() == 50)
					{
						lPresas[i].setIntocable(0);
						Arista aux1 = lPresas[i].tomarDecision(aList);
						if(aux1 != null)
						{
							Dijkstra aux = new Dijkstra(grafo, aux1.getDestino(), grafo.getVertices()[this.comboBox2.SelectedIndex]);
							lPresas[i].setCamino(aux.ejecutarDijkstra());
							lPresas[i].getCamino().Insert(0, aux1);
						}
						MessageBox.Show("Expulsado");
					}
				}else
				{
					lPresas[i].setIntocable(0);
				}
				 
				if(lPresas[i].getAcechado())
				{
					for(int j = 0; j<aList.Count;j++)
					{
						if(lPresas[i].getAristaActual().getDestino().getID() == aList[j].getCamino().getOrigen().getID() && lPresas[i].getAristaActual().getOrigen().getID() == aList[j].getCamino().getDestino().getID())
						{
							bool flag = false;
							//int radioD = lPresas[i].getAristaActual().getDestino().getRadio();
							
							if(aList[j].getAcechando() != null)
							if(obtenerDistancia(lPresas[i].getListaCamino(), aList[j].getAcechando().getAristaActual().getDestino().getCentro()) < (radioD+12))
							{
								flag = true;
							}
							
							if(lPresas[i].getAristaActual().getID() == aList[j].getCamino().getID());
								flag = false;
								
							lPresas[i].setAcechado(flag);
							if(flag)
								aList[j].setAcechando(null);
						}
					}
				}
				dibujarEspectro(lPresas[i].getListaCamino(), false);

			}
			return true;
		}
		void animar()
		{
			Graphics gr = Graphics.FromImage(bm);
			
			while(actualizarAgentes())
			{
				if(!actualizarPresas())
					return;
				pictureBox1.Refresh();
				gr.Clear(Color.Transparent);
			}
			for(int i = 0; i<aList.Count;i++)
			{
				dibujarEspectro(aList[i].getListaCamino(), true);
			}
			for(int i = 0; i<lPresas.Count;i++)
			{
				dibujarEspectro(lPresas[i].getListaCamino(), false);
			}
			pictureBox1.Refresh();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			this.comboBox1.Enabled = false;
			animar();
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			this.comboBox1.Enabled = true;
			Graphics gr = Graphics.FromImage(bm);
			gr.Clear(Color.Transparent);
			pictureBox1.Refresh();
			aList.Clear();
			lPresas.Clear();
		}
		
		void Button7Click(object sender, EventArgs e)
		{
			Dijkstra a = new Dijkstra(grafo, lPresas[porActualizar].getVertice(), grafo.getVertices()[this.comboBox2.SelectedIndex]);
			lPresas[porActualizar].getCamino().Clear();
			lPresas[porActualizar].setCamino(a.ejecutarDijkstra());
			animar();
		}
	}
}
