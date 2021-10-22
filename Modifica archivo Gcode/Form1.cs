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


namespace Modifica_archivo_Gcode
{

    public partial class Form1 : Form
    {
        //string Copia = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\copia.tap";
        // string Espejo = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\Espejo.tap";
        //string Codigogenerado = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\Codigo2.tap";
       string Configuracion = @"C:\Users\sebas\Desktop\Mach3 Afiladora\Codigos\Configuraciones.txt";
        string Archivo;
        string Ruta;
        string Espejo;
        string Codigogenerado;
       // string Configuracion;
        string Copia;



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
            comboBox1.Items.Add("Ambos sentidos");
            comboBox1.SelectedIndex = 0;
           string Sentido = comboBox1.Text;
            label16.Text = null;
            label17.Text = null;


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
                        Ruta= j;
                        Copia = Ruta + "\\copia.tap";
                        Espejo =  Ruta + "\\Espejo.tap";
                        Codigogenerado = Ruta + "\\Codigogenerado.tap";
                      //  string Configuracion = Ruta + "\\Configuraciones.txt";

                    }
                    if (i == 2)
                    {//RIY
                        string j = campos[1];
                        textBox3.Text = j;
                    }
                    if (i == 3)
                    {//RIX
                        string j = campos[1];
                        textBox4.Text = j;
                    }
                    if (i == 4)
                    {//RDY
                        string j = campos[1];
                        textBox5.Text = j;
                    }
                    if (i == 5)
                    {//RDX
                        string j = campos[1];
                        textBox6.Text = j;
                    }
                    if (i == 6)
                    {//Rzz
                        string j = campos[1];
                        textBox1.Text = j;
                    }
                    if (i == 7)
                    {//Velocidad
                        string j = campos[1];
                       textBox7.Text = j;
                    }
                    if (i == 8)
                    {//
                        string j = campos[1];
                    numericUpDown3.Text    = j;
                    }
                    if (i == 9)
                    {
                        string j = campos[1];
                      numericUpDown2.Text = j;
                    }
                    if (i == 10)
                    {
                        string j = campos[1];
                       numericUpDown1.Text  = j;                       
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
       private void button1_Click(object sender, EventArgs e)
        {

            //File.Delete(Espejo);
            //File.Create(Espejo);
            //File.Delete(Copia);
            //File.Create(Copia);
            //File.Delete(Codigogenerado);
            //File.Create(Codigogenerado);

            if (!File.Exists(Archivo))

                     {
                MessageBox.Show("No selecciono un Archivo", "Mesage de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            else { 
                string RIx = "X" + textBox4.Text;
            string RIy = "Y" + textBox3.Text;
            string RDx = "X" + textBox6.Text;
            string RDy = "Y" + textBox5.Text;
            string Rderecha = "G0 "+ RDx + " "+ RDy + " F" + textBox7.Text;
            string Rizquierda ="G0 " + RIx +" "+ RIy + " F" + textBox7.Text;

            double Z;
            double ZZ;
            string RZ;


            comboBox1.Items.Add("Derecha a Izquierda");
            comboBox1.Items.Add("Izquierda a derecha");
            comboBox1.Items.Add("Ambos sentidos");
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
            System.IO.FileInfo fi3 = new System.IO.FileInfo(Codigogenerado);
            fi3.Delete();

            //Funcion1();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //using (StreamReader leer = new StreamReader(Archivo))
            //{
            //    while (!leer.EndOfStream)
            //    {
            //        using (StreamWriter Escribe = new StreamWriter(Copia, true))
            //        {
            //            string y = " ";
            //            Escribe.WriteLine(y);
            //            Escribe.Close();
            //        }
            //    }
            //}


            //Tomar la variable Archivo y abrir otro archivo "copia" y escribir lo mismo una vez 
            //Toma el tamño de las lineas del archivo
            using (StreamReader leer = new StreamReader(Archivo))
                {
                    while (!leer.EndOfStream)
                    {
                        lineas = lineas + 1;
                        string y = leer.ReadLine();
                    using (StreamWriter Escribe8 = new StreamWriter(Copia,true))

                    {
                        Escribe8.WriteLine(y);
                        Escribe8.Close();
                    }
                    }
                }
            
////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Defino un vector del tamano del archivo
            string[] vectorespejo = new string[lineas];
          //defiino una variable para el vector
            int lineas2 = 0;
            //leo el archivo
            using (StreamReader leer2 = new StreamReader(Archivo))
            { 
                while (!leer2.EndOfStream)
                {
                    string y = leer2.ReadLine();
                    //cargo la lectura del archivo en una posicion del vector
                    vectorespejo[lineas2] = y;
                    //incremento el indice
                    lineas2 = lineas2 + 1;
                }
            }
  
            using (StreamWriter Escribe2 = new StreamWriter(Espejo, true))
            {               
                for (int i = lineas - 1; i >= 0; i--)
                {
                    string j = vectorespejo[i];
                    Escribe2.WriteLine(j);               
                }
                //Escribe.Close();
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
            }
///////////////////////////////////////////////////////////////////////////////////////////////////////////
            void FuncionEspejo()
           {
                using (StreamReader leer4 = new StreamReader(Espejo))
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
                            
                                           
                                            using (StreamWriter Escribe5 = new StreamWriter(Codigogenerado, true))
                                                        {

                                                            Escribe5.WriteLine(Rderecha);
                                                            Escribe5.Close();
                                                        }
                                            using (StreamWriter Escribe7 = new StreamWriter(Codigogenerado, true))
                                            {

                                                Escribe7.WriteLine(Rizquierda);
                                                Escribe7.Close();
                                            }
                                        }
                                        break;
                                    }
                                case "Izquierda a Derecha":
                                    {
                                        for (int i = 0; i < Pasadasporcuchillas; i++)
                                        {
                                            FuncionEspejo();
                                            using (StreamWriter Escribe7 = new StreamWriter(Codigogenerado, true))
                                            {

                                                Escribe7.WriteLine(Rizquierda);
                                                Escribe7.Close();
                                            }
                                             using (StreamWriter Escribe8 = new StreamWriter(Codigogenerado, true))
                                            {

                                                Escribe8.WriteLine(Rderecha);
                                                Escribe8.Close();
                                            }
                                        }
                                        break;
                                    }
                                case "Ambos Sentidos":
                                    {
                                        for (int i = 0; i < Pasadasporcuchillas; i++)
                                        {
                                            FuncionCopia();
                                            FuncionEspejo();
                                        }
                                        break;
                                    }
                            }
                        }

///////////////////////////////////////////////////////////////////////////////////////////////////////////
            void Funcioncantidaddecuchillas()
            {
                 Z = Convert.ToInt32(textBox1.Text);
                 ZZ = Z;
                for (int i = 0; i < CantCuchillas; i++)
                            {
                                FuncionPasadasporcuchillas();
                                using (StreamWriter Escribe9 = new StreamWriter(Codigogenerado, true))
                                {
                        if (i == 0)
                        {
                            ZZ = Z;
                        }
                        else
                        {
                            ZZ = Z + ZZ;
                        }
                              RZ = "G0 Z" + Convert.ToString(ZZ) + " " + " F" + textBox7.Text;

                                    Escribe9.WriteLine(RZ);
                                    Escribe9.Close();
                                }


                            }
                        }

///////////////////////////////////////////////////////////////////////////////////////////////////////////


      void FuncioncantidadeVueltasdeRolo()
                {
                    for (int i = 0; i < VueltasdeRolo; i++)
                    {
                        Funcioncantidaddecuchillas();
                        //using (StreamWriter Escribe = new StreamWriter(Codigogenerado, true))
                        //{

                        //    Escribe.WriteLine(RZ);
                        //    Escribe.Close();
                        //}


                    }
                    using (StreamWriter Escribe11 = new StreamWriter(Codigogenerado, true))
                    {
                        RZ = "M05 (Spindle off)\r\n" +
                            "M30(Program end)\r\n"+
                            "M99\r\n";

                        Escribe11.WriteLine(RZ);
                        Escribe11.Close();
                    }
                }

            FuncioncantidadeVueltasdeRolo();
            label16.Text = "Se ha generado un nuevo Codigo";
        }
            }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            // Se crea  y se instancia la clase de ficheros.
            OpenFileDialog open = new OpenFileDialog();
            //le agregamos un filtro para los tipos de archivos a leer en este caso XML.
            open.Filter = "txt files (*.tap)|*.tap";
            //cuando presionamos sobre el botón validamos que el resultado esperado sea la selección de un archivo.
            if (open.ShowDialog() == DialogResult.OK && open.ToString() != " ")
            {
                //Movemos la ruta del archivo a nuestro Textbox creado para su posterior uso.
                textBox2.Text = open.FileName;
                Archivo = textBox2.Text;
           
            }
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo fi2 = new System.IO.FileInfo(Configuracion);
            fi2.Delete();
            using (StreamWriter Escribe10 = new StreamWriter(Configuracion, true))
            {
              
                string Guardadatos= "Ruta=C:\\Users\\sebas\\Desktop\\Mach3 Afiladora\\Codigos\\ " + "\r\n"+
                    "RIY = " + textBox3.Text + "\r\n"+
                    "RIX =" +textBox4.Text + "\r\n"+
                    "RDY =" +textBox5.Text+ "\r\n"+
                    "RDX =" +textBox6.Text+ "\r\n"+
                    "RZZ =" +textBox1.Text+ "\r\n"+
                    "Velocidad =" +textBox7.Text+ "\r\n"+
                    "Pasadas por Cuchilla =" + numericUpDown3.Text+ "\r\n"+
                    "Cantidad de Cuchillas =" + numericUpDown2.Text + "\r\n"+
                    "Vueltas de Rolo =" + numericUpDown1.Text; 


                Escribe10.WriteLine(Guardadatos);
                Escribe10.Close();
                label17.Text = "Se han guardado los cambios";
                        


            }


        }
    }
}
