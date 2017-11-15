using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epson_QR
{
    public class Ticket
    {
        #region<REGION DE IMPRESION TICKETS>
        StringBuilder linea = new StringBuilder();
        //Creamos una variable para almacenar el numero maximo de caracteres que permitiremos en el ticket.
        int maxCar = 40, cortar;//Para una impresora ticketera que imprime a 40 columnas. La variable cortar cortara el texto cuando rebase el limte.
        #endregion

        private ArrayList headerLines = new ArrayList();
        private ArrayList subHeaderLines = new ArrayList();
        private ArrayList items = new ArrayList();
        private ArrayList totales = new ArrayList();
        private ArrayList footerLines = new ArrayList();
        private int maxChar = 40;
        private int maxCharDescription = 20;
        private float topMargin = 3f;
        private string fontName = "Tahoma";
        private int fontSize = 9;
        private SolidBrush myBrush = new SolidBrush(Color.Black);
        private Image headerImage;
        private int count;
        private int imageHeight;
        private float leftMargin;
        private Font printFont;
        private Graphics gfx;
        private string line;

        public Image HeaderImage
        {
            get
            {
                return this.headerImage;
            }
            set
            {
                if (this.headerImage == value)
                    return;
                this.headerImage = value;
            }
        }
        public int MaxChar
        {
            get
            {
                return this.maxChar;
            }
            set
            {
                if (value == this.maxChar)
                    return;
                this.maxChar = value;
            }
        }

        public int MaxCharDescription
        {
            get
            {
                return this.maxCharDescription;
            }
            set
            {
                if (value == this.maxCharDescription)
                    return;
                this.maxCharDescription = value;
            }
        }
        public int FontSize
        {
            get
            {
                return this.fontSize;
            }
            set
            {
                if (value == this.fontSize)
                    return;
                this.fontSize = value;
            }
        }

        public string FontName
        {
            get
            {
                return this.fontName;
            }
            set
            {
                if (!(value != this.fontName))
                    return;
                this.fontName = value;
            }
        }
        private float YPosition()
        {
            return this.topMargin + ((float)this.count * this.printFont.GetHeight(this.gfx) + (float)this.imageHeight);
        }
        public void TextoCentro(string texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                   // this.gfx.DrawString(line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    caracterActual += maxCar;
                }
                //Variable para poner espacios restntes
                string espacios = "";
                //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                int centrar = (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length) / 2;
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";//Agrega espacios para centrar
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
                //this.gfx.DrawString(line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            }
            else
            {
                string espacios = "";
                //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                int centrar = (maxCar - texto.Length) / 2;
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";//Agrega espacios para centrar
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto);
                //this.gfx.DrawString(line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            }
        }

        public void PrintTicket(string impresora)
        {
            this.printFont = new Font(this.fontName, (float)this.FontSize, FontStyle.Regular);
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = impresora;

            printDocument.PrintPage += new PrintPageEventHandler(this.pr_PrintPage);
            printDocument.Print();
        }
        private void pr_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            this.gfx = e.Graphics;
            this.DrawHeader();
        }
        private void DrawHeader()
        {
            string str = "Use TextFormatFlags and Rectangle objects to"
 + " center text in a rectangle.";
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Rectangle rect1 = new Rectangle(10, 10, 60, 10);
            this.gfx.DrawString(str, this.printFont, (Brush)this.myBrush, rect1, stringFormat);
        }
    }
}
