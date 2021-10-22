using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Threading;
using System.Threading;





namespace Modifica_archivo_Gcode
{

    public partial class Form1 : Form
    {
        //string Copia = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\copia.tap";
    // string Espejo = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\Espejo.tap";
    //string Codigogenerado = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\Codigo2.tap";
  //string Configuracion = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\Configuraciones.txt";
        string Configuracion = @"C:\Mach3\Codigos\Configuraciones.txt";
        //string codigopuntos = @"C:\Mach3\GCode\PUNTOS.txt";
        string Archivo;

        string Ruta;
        string Espejo;
        string Espejo2;
        string Codigogenerado;
        // string Configuracion;
        string Copia;
        string[] campos3 = new string[50];
        string[] campos4 = new string[10];
        string w, h, k, l, m;

        bool o;

        //string n = campos3[3];

        string r, s;
        string ultimopunto;
        string[] p = new string[10];
        string[] q = new string[10];
        decimal L, LL;
        int a;
        double ZZZ;
        string yy;
        double  Q;
        double ZZ;
        string RD2, RD1, RI1, RI2, u, v, uu, vv, uuu, vvv, uuuu, vvvv;
        string[] vectorespejo = new string[50000];
        string[] vectorespejo2 = new string[50000];
        int CantCuchillas;
        int Pasadasporcuchillas;
        int VueltasdeRolo;
        string[] campos = new string[2];
        int lineas = 0;
        // string[] Guardaconf = new string[];

        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("Derecha a Izquierda");
            comboBox1.Items.Add("Izquierda a Derecha");
            comboBox1.Items.Add("Ambos Sentidos");
            
            comboBox1.SelectedIndex = 0;
            string Sentido = comboBox1.Text;

            label16.Text = null;
            label17.Text = null;
        //   r = textBox8.Text;

            //using (StreamReader leer10 = new StreamReader(codigopuntos))
            //{

            //    string t;
            //    string[] vectorpuntos = new string[50];
            //    string[] campos10 = new string[6];
            //    string[] campos11 = new string[3];
            //    string[] camposespejo = new string[5];
                                    
            //}
            

           

            using (StreamReader leer5 = new StreamReader(Configuracion))
            {
                int i = 0;
                while (!leer5.EndOfStream)
                {
                    string y = leer5.ReadLine();
                    campos = y.Split('=');

                    i = i + 1;
                    if (i == 1)
                    {
                        string j = campos[1];
                        Ruta = j;
                        Copia = Ruta + "\\copia.tap";
                        Espejo = Ruta + "\\Espejo.tap";
                        Espejo2=Ruta+"\\Espejo2.tap";
                        Codigogenerado = Ruta + "\\Codigogenerado.tap";
                     //  string Configuracion = Ruta + "\\Configuraciones.txt";
                    }
                    
                    //if (i == 10)
                    //{//Rzz
                    //    string j = campos[1];
                    //    textBox1.Text = j;
                    //}
                    //if (i == 11)
                    //{//VR
                    //    string j = campos[1];
                    //    textBox8.Text = j;
                    //}
                    if (i == 2)
                    {//VELOCIDAD
                        string j = campos[1];
                        textBox7.Text = j;
                    }
                    if (i == 3)
                    {//hUSILLO
                        string j = campos[1];
                        textBox1.Text = j;
                    }
                    if (i == 4)
                    {//
                        string j = campos[1];
                        numericUpDown2.Text = j;
                    }
                    if (i == 5)
                    {
                        string j = campos[1];
                        numericUpDown1.Text = j;
                    }
                    if (i == 6)
                    {
                        string j = campos[1];
                        numericUpDown3.Text = j;
                    }
                    //if (i == 11)
                    //{
                    //    string j = campos[1];
                    //   comboBox1.Text = j;
                    //}
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
/////////////////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {

            label16.Text = null;
                
            if (comboBox1.SelectedIndex == 0)
            {
                a = 0;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                a = 1;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                a = 2;
            }

            if (!File.Exists(Archivo))

            {
                MessageBox.Show("No selecciono un Archivo", "Mesage de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                button1.Text = "ESPERE";
                
                button1.Enabled = false;

                string RZ;
                string Sentido = comboBox1.Text;

                //Distancia entre cuchillas
                //Dis = Convert.ToInt32(textBox1.Text);
                CantCuchillas = Convert.ToInt32(numericUpDown1.Text);
                Pasadasporcuchillas = Convert.ToInt32(numericUpDown2.Text);
                VueltasdeRolo = Convert.ToInt32(numericUpDown3.Text);
                lineas = 0;


                //void Funcion1()
                //{
                System.IO.FileInfo fi = new System.IO.FileInfo(Copia);
                fi.Delete();
                //}
                System.IO.FileInfo fi2 = new System.IO.FileInfo(Espejo);
                fi2.Delete();
                System.IO.FileInfo fi4 = new System.IO.FileInfo(Espejo2);
                fi4.Delete();
              //  System.IO.FileInfo fi3 = new System.IO.FileInfo(Codigogenerado);
              //  fi3.Delete();

                //private bool IsFileOpen(string filePath)
                //{
                //bool result = false;
                try
                    {
                        System.IO.FileStream fs = System.IO.File.OpenWrite(Codigogenerado);
                        fs.Close();
                    System.IO.FileInfo fi3 = new System.IO.FileInfo(Codigogenerado);
                    fi3.Delete();// File/Stream manipulating code here 
                      }
                    catch (IOException ex)
                    {
                    MessageBox.Show("Archivo en uso. Cierre archivo en DEFYMACH", "Mesage de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto salidaerror;
                                    
                    }
                            
                        
                 

                   
               
                //System.IO.FileInfo fi3 = new System.IO.FileInfo(Codigogenerado);
                //fi3.Delete();


                //Tomar la variable Archivo y abrir otro archivo "copia" y escribir lo mismo una vez 
                //Toma el tamño de las lineas del archivo
                using (StreamReader leer = new StreamReader(Archivo))
                {
                    
                    //bool b=false;
                    while (!leer.EndOfStream)
                    {
                       
                        lineas = lineas + 1;
                        string y = leer.ReadLine();
                       
                        //if (b == true)
                        //{
                        //    yy = y + " F" + textBox7.Text;
                        //}
                        campos4 = y.Split(' ');
                        if (campos4[3] == "F50")
                        {
                            yy = y;
                        }
                        else {
                            yy = y + " F" + textBox7.Text;
                            }
                            using (StreamWriter Escribe8 = new StreamWriter(Copia, true))

                        {
                            Escribe8.WriteLine(yy);
                            Escribe8.Close();
                        }
                    }
                }

 ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Defino un vector del tamano del archivo

                int lineas2 = 0;
                //leo el archivo
                using (StreamReader leer2 = new StreamReader(Archivo))
                {
                    while (!leer2.EndOfStream)
                    {
                        string y = leer2.ReadLine();
                        campos4 = y.Split(' ');
                        if (campos4[3] == "F50")
                        {
                            yy = y;
                        }
                        else
                        {
                            yy = y + " F" + textBox7.Text;
                        }
                        //cargo la lectura del archivo en una posicion del vector
                        vectorespejo[lineas2] = yy;
                        //incremento el indice
                        lineas2 = lineas2 + 1;
                    }
                    ultimopunto = vectorespejo[lineas2-1];

                }

                using (StreamWriter Escribe2 = new StreamWriter(Espejo, true))
                {
                    for (int i = lineas - 1; i >= 0; i--)
                    {
                        string j = vectorespejo[i];

                        Escribe2.WriteLine(j);
                    }
                }
////////////////////////////////////////////////////////////////////////////////////////////////
                using (StreamReader leer13 = new StreamReader(Espejo))
                {
                    int lineas3 = 0;
                    while (!leer13.EndOfStream)
                    {
                        string y = leer13.ReadLine();
                        //cargo la lectura del archivo en una posicion del vector
                        vectorespejo2[lineas3] = y;
                        //incremento el indice
                        lineas3 = lineas3 + 1;
                    }
                }
                using (StreamWriter Escribe15 = new StreamWriter(Espejo2, true))
                {
#pragma warning disable CS0219 // La variable 'll' está asignada pero su valor nunca se usa
                    string lll, ll, ww, hh, www, hhh;
#pragma warning restore CS0219 // La variable 'll' está asignada pero su valor nunca se usa
                    l = "G1";
                    ll = "G1";
                    ww = "";
                    hh = "";
                    m = "";
                    w = "";
                    h = "";


                    for (int i = 0; i < lineas; i++)
                    {
                        //ll = l;
                        //ww = w;
                        //hh = h;


                        string j = vectorespejo2[i];
                        campos3 = j.Split(' ');
                        if (campos3[0] == "M1021" || campos3[0] == "M1020" || campos3[0] == "M1022")
                        {
                            k = campos3[0];
                            Escribe15.WriteLine(k);
                            goto salida;
                        }
                       if (i <= 3)
                        {
                            k = campos3[0] + " " + campos3[1] + " " + campos3[2];
                            Escribe15.WriteLine(k);
                            goto salida;
                        }
                        if (i > 3)
                        {
                            k = l + " " + campos3[1] + " " + campos3[2] + " " + w + " " + h + "F"+textBox7.Text;
                            Escribe15.WriteLine(k);
                        }

                        if (campos3[0] == "G2" || campos3[0] == "G3")
                        {

                            // l = campos3[0];
                            lll = campos3[4].Replace(".", ",");
                            L = Convert.ToDecimal(lll);
                            LL = L * (1);
                            ww = Convert.ToString(LL);
                            www = ww.Replace(",", ".");
                            w = campos3[3] + " " + www;
                            //  w = campos3[3] + " " + Convert.ToString(LL);
                            //  textBox14.Text = w;

                            lll = campos3[6].Replace(".", ",");

                            L = Convert.ToDecimal(lll);
                            LL = L * (1);
                            hh = Convert.ToString(LL);
                            hhh = hh.Replace(",", ".");
                            h = campos3[5] + " " + hhh;
                            // h = campos3[5] + " " + Convert.ToString(LL);
                            if (campos3[0] == "G2")
                            {
                                l = "G3";
                            }
                            if (campos3[0] == "G3")
                            {
                                l = "G2";
                            }
                            //if (campos3[0] == "G0")
                            //{
                            //    l = "G0";
                            //    goto salida;
                          //  }

                            //else { lll = "G1"; }
                        }
                        else
                        {
                            if (campos3[0] == "G0")
                            {
                                l = "G0";
                            }
                            else { 
                            l = "G1";
                              }
                        w = "";
                            h = "";
                        }
                        //k = ll + " " + campos3[1] + " " + campos3[2] + " " + ww + hh;
                        //    Escribe15.WriteLine(k);
                        salida:;

                    }

                }

///////////////////////////////////////////////////////////////////////////////////////////////////////////
                void FuncionCopia()
                {
                    using (StreamReader leer3 = new StreamReader(Copia))
                    {
                        while (!leer3.EndOfStream)
                        {
                            string y = leer3.ReadLine();
                            using (StreamWriter Escribe3 = new StreamWriter(Codigogenerado, true))
                            {
                                Escribe3.WriteLine(y);
                                Escribe3.Close();
                            }
                           
                        }

                    }
                    //using (StreamWriter Escribe20 = new StreamWriter(Codigogenerado, true))
                    //{
                    //    RZ = "M1030 \r\n";
                    //    Escribe20.WriteLine(RZ);
                    //    Escribe20.Close();
                    //}
                }
///////////////////////////////////////////////////////////////////////////////////////////////////////////
                void FuncionCopiasinretorno()
                {
                    using (StreamReader leer30 = new StreamReader(Copia))
                    {
                        while (!leer30.EndOfStream)
                        {                            
                            string y = leer30.ReadLine();
                            campos4 = y.Split(' ');
                            if (campos4[0] != "G0")
                            {

                                using (StreamWriter Escribe30 = new StreamWriter(Codigogenerado, true))
                                {
                                    Escribe30.WriteLine(y);
                                    Escribe30.Close();
                                }
                            }
                        }
                    }
                }
///////////////////////////////////////////////////////////////////////////////////////////////////////////
                void FuncionEspejo()
                {
                    using (StreamReader leer4 = new StreamReader(Espejo2))
                    {
                        while (!leer4.EndOfStream)
                        {
                            string y = leer4.ReadLine();
                            using (StreamWriter Escribe4 = new StreamWriter(Codigogenerado, true))
                            {
                                Escribe4.WriteLine(y);
                                Escribe4.Close();
                            }
                        }
                    }
                }

///////////////////////////////////////////////////////////////////////////////////////////////////////////
                void FuncionEspejosinretorno()
                {
                    using (StreamReader leer4 = new StreamReader(Espejo2))
                    {
                        while (!leer4.EndOfStream)
                        {
                            string y = leer4.ReadLine();
                            campos4 = y.Split(' ');
                            if (campos4[0] != "G0")
                            {
                                using (StreamWriter Escribe4 = new StreamWriter(Codigogenerado, true))
                                {
                                    Escribe4.WriteLine(y);
                                    Escribe4.Close();
                                }
                            }
                        }
                    }
                }


///////////////////////////////////////////////////////////////////////////////////////////////////
                void FuncionPasadasporcuchillas()
                {
                   

                    switch (Sentido)
                    {

                        case "Derecha a Izquierda":
                            {
                                for (int i = 0; i < Pasadasporcuchillas; i++)
                                {
                                    FuncionCopia();
                                   
                                        //using (StreamWriter Escribe20 = new StreamWriter(Codigogenerado, true))
                                        //{
                                        //  String M = "M1030 \r\n";
                                        //    Escribe20.WriteLine(M);
                                        //   Escribe20.Close();
                                        //}
                                   
                                }
                                break;
                            }
                        case "Izquierda a Derecha":
                            {
                                for (int i = 0; i < Pasadasporcuchillas; i++)
                                {
                                    FuncionEspejo();
                                    //using (StreamWriter Escribe20 = new StreamWriter(Codigogenerado, true))
                                    //{
                                    //    String M = "M1030 \r\n";
                                    //    Escribe20.WriteLine(M);
                                    //    Escribe20.Close();
                                    //}
                                    using (StreamWriter Escribe40 = new StreamWriter(Codigogenerado, true))
                                    {
                                        Escribe40.WriteLine(ultimopunto);
                                        Escribe40.Close();
                                    }
                                }
                                break;
                            }
                        case "Ambos Sentidos":
                            {
                                Pasadasporcuchillas = Convert.ToInt32(numericUpDown2.Text);
                                for (int i = 0; i < Pasadasporcuchillas; i++)
                                {
                                    FuncionCopiasinretorno();
                                    //using (StreamWriter Escribe20 = new StreamWriter(Codigogenerado, true))
                                    //{
                                    //    String M = "M1030 \r\n";
                                    //    Escribe20.WriteLine(M);
                                    //    Escribe20.Close();
                                    //}
                                    FuncionEspejosinretorno();
                                    using (StreamWriter Escribe40 = new StreamWriter(Codigogenerado, true))
                                    {
                                        Escribe40.WriteLine(ultimopunto);
                                        Escribe40.Close();
                                    }
                                    //using (StreamWriter Escribe20 = new StreamWriter(Codigogenerado, true))
                                    //{
                                    //    String M = "M1030 \r\n";
                                    //    Escribe20.WriteLine(M);
                                    //    Escribe20.Close();
                                    //}

                                    // ultimopunto = vectorespejo[lineas2];

                                }
                                break;
                            }
                    }
                }


///////////////////////////////////////////////////////////////////////////////////////////////////////////
                void Funcioncantidaddecuchillas()
                {
                    
                    ZZZ = Convert.ToInt32(numericUpDown1.Text);
                 
                    switch (ZZZ)
                    {
                        case 8:
                            { Q = 1; }
                            break;
                        case 4:
                            { Q = 2; }
                            break;
                        case 6:
                            { Q =1.33; }//es 1.33                            
                            break;
                        case 2:
                            { Q = 4; }
                            break;
                        case 10:
                            { Q = 0.8; }//0.8
                            break;
                        case 12:
                            { Q = 0.665; }//0.66
                            break;
                    }

                    // Z = Convert.ToInt32(numericUpDown3.Text);
                    ZZ = Q;
                    for (int i = 0; i < CantCuchillas; i++)
                    {

                        FuncionPasadasporcuchillas();
                        using (StreamWriter Escribe9 = new StreamWriter(Codigogenerado, true))
                        {
                            if (i == 0)
                            {
                                ZZ = Q;
                            }
                            else
                            {
                                ZZ = Q + ZZ;
                            }
                            if (i < CantCuchillas - 1)
                            {
                                RZ = "G1 Z" + Convert.ToString(ZZ) + " " + " F" + textBox7.Text+ "\r\n";
                            }
                            Escribe9.WriteLine(RZ);
                            Escribe9.Close();
                        }


                    }
                }

///////////////////////////////////////////////////////////////////////////////////////////////////////////
                void FuncioncantidadeVueltasdeRolo()
                {
                    
                    using (StreamWriter Escribe12 = new StreamWriter(Codigogenerado, true))
                    {
                        RZ = "M90 \r\n" +
                            "M3\r\n" +
                            "F" + textBox7.Text + "\r\n"+
                            "S" + textBox1.Text + "\r\n" +
                            "G1 Z0.00\r\n";

                         Escribe12.WriteLine(RZ);
                        Escribe12.Close();
                    }
                    for (int i = 0; i < VueltasdeRolo; i++)
                    {
                        Funcioncantidaddecuchillas();
                        using (StreamWriter Escribe20 = new StreamWriter(Codigogenerado, true)) //compensacion de desgaste x cada vuelta
                        {
                            String M = "Z0.0\r\n" + 
                                "M1030 \r\n" +
                                "F" + textBox7.Text + "\r\n" +
                                "S" + textBox1.Text + "\r\n" ;
                           Escribe20.WriteLine(M);
                           Escribe20.Close();
                        }
                    }
                    // label16.Text = "Se ha generado un nuevo Codigo";
                    using (StreamWriter Escribe11 = new StreamWriter(Codigogenerado, true))
                    {
                        RZ = "M05 (Spindle off)\r\n" +
                            "M30(Program end)\r\n" +
                            "M99\r\n";

                        Escribe11.WriteLine(RZ);
                        Escribe11.Close();
                    }
                }

                
                FuncioncantidadeVueltasdeRolo();
                comboBox1.SelectedIndex = a;
                //label2.Text = ultimopunto;
                Thread.Sleep(500);
                label16.Text = "Se ha generado un nuevo Código";
                salidaerror:;
                button1.Text = "Generar Código";
                button1.Enabled = true;
                //Thread.Sleep(500);
                // label16.Text = "ESPERE";
                

            }
        
    }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            // Se crea  y se instancia la clase de ficheros.
            OpenFileDialog open = new OpenFileDialog();
            //le agregamos un filtro para los tipos de archivos a leer en este caso XML.
            open.Filter = "txt files (*.cnc)|*.cnc";
            //cuando presionamos sobre el botón validamos que el resultado esperado sea la selección de un archivo.
            if (open.ShowDialog() == DialogResult.OK && open.ToString() != " ")
            {
                //Movemos la ruta del archivo a nuestro Textbox creado para su posterior uso.
                textBox2.Text = open.FileName;
                Archivo = textBox2.Text;
           
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = "ESPERE";
            button4.Enabled = false;
            System.IO.FileInfo fi2 = new System.IO.FileInfo(Configuracion);
            fi2.Delete();
            using (StreamWriter Escribe10 = new StreamWriter(Configuracion, true))
            {


                label17.Text = "";
                //  string Guardadatos = "Ruta=C:\\Users\\sebas\\Desktop\\Mach3 Afiladora\\Codigos\\ " + "\r\n" +
                string Guardadatos = "Ruta=C:\\Mach3\\Codigos\\ " + "\r\n" +
                   "Velocidad =" + textBox7.Text + "\r\n" +
                   "Husillo RPM =" + textBox1.Text + "\r\n" +
                   "Pasadas por Cuchilla =" + numericUpDown2.Text + "\r\n" +
                   "Cantidad de Cuchillas =" + numericUpDown1.Text + "\r\n" +
                   "Vueltas de Cabezal =" + numericUpDown3.Text;

                Escribe10.WriteLine(Guardadatos);
                Escribe10.Close();

            }
            Thread.Sleep(1000);
            label17.Text = "Se han guardado los cambios";
            button4.Text = "Guardar Cambios";
            button4.Enabled = true;
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_hov;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_def;
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_hov;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_def;
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_hov;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_def;
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_hov;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackgroundImage = Modifica_archivo_Gcode.Properties.Resources.btn_def;
        }

        private void Gcode_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
      
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
       
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        
        
        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
