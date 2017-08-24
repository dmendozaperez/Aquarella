using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  www.aquarella.com.pe.be.Financiera
{
    public class Be_Documents_trans
    {
        object varFecha;
        object varMonto;
        object varOperacion;
        object varDescripcion;
        object varBanco;

        public object getSetFecha
        {
            get
            {
                return varFecha;
            }
            set
            {
                varFecha = value;
            }
        }

        public object getSetDescripcion
        {
            get
            {
                return varDescripcion;
            }
            set
            {
                varDescripcion = value;
            }
        }

        public object getSetMonto
        {
            get
            {
                return varMonto;
            }
            set
            {
                varMonto = value;
            }
        }

        public object getSetOperacion
        {
            get
            {
                return varOperacion;
            }
            set
            {
                varOperacion = value;
            }
        }

        public object getSetBanco
        {
            get
            {
                return varBanco;
            }
            set
            {
                varBanco = value;
            }
        }

        public bool Ok { get; set; }

        public string Mensaje { get; set; }

        public object getSet_Num_Secuencia { get; set; }



    }
}