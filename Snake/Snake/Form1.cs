using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        Cola cabeza;
        Graphics g;
        int xdir = 0, ydir = 0;
        int cuadro = 10;
        int puntaje = 0;
        Boolean ejex = true, ejey = true;
        Comida comida;
        public Form1()
        {
            InitializeComponent();
            cabeza = new Cola(10, 10);
            comida = new Comida();
            g = canvas.CreateGraphics();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            cabeza.dibujar(g);
            comida.dibujar(g);
            movimiento();
            if(cabeza.interseccion(comida))
            {
                comida.colocar();
                cabeza.meter();
                puntaje++;
                puntos.Text = puntaje.ToString();
            }
            choquedeljuego();
            choquePared();
        }
        public void choquePared()
        {
            if(cabeza.verX()<0 || cabeza.verX() > ClientRectangle.Width || cabeza.verY()<0 || cabeza.verY()> canvas.ClientRectangle.Height)
            {
                findeljuego();
            }
        }
        public void findeljuego()
        {
            puntaje = 0;
            puntos.Text = "0";
            ejex = true;
            ejey = true;
            xdir = 0;
            ydir = 0;
            cabeza = new Cola(10, 10);
            comida = new Comida();
            MessageBox.Show("GAME OVER");
        }
        public void choquedeljuego()
        {
            Cola temp;
            try
            {
                temp = cabeza.versiguiente().versiguiente();
            }
            catch (Exception ex)
            {
                temp = null;
            }
            while(temp!= null)
            {
                if(cabeza.interseccion(temp))
                {
                    findeljuego();
                }
                else
                {
                    temp = temp.versiguiente();
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(ejex)
            {
                if (e.KeyCode == Keys.Up)
                {
                    ydir = -cuadro;
                    xdir = 0;
                    ejex = false;
                    ejey = true;
                }
                if (e.KeyCode == Keys.Down)
                {
                    ydir = cuadro;
                    xdir = 0;
                    ejex = false;
                    ejey = true;
                }
            }
            if(ejey)
            {
                if(e.KeyCode == Keys.Right)
                {
                    ydir = 0;
                    xdir = cuadro;
                    ejex = true;
                    ejey = false;
                }
                if (e.KeyCode == Keys.Left)
                {
                    ydir = 0;
                    xdir = -cuadro;
                    ejex = true;
                    ejey = false;
                }
            }
        }
        public void movimiento()
        {
            cabeza.setxy(cabeza.verX() + xdir, cabeza.verY() + ydir);
        }
        
    }
}
