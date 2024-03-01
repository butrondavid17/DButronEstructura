using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Puesto
    {
        public int PuestoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Puestos { get; set; }

        public static Dictionary<string, object> GetAll()
        {
            Puesto puesto = new Puesto();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Puesto", puesto }, { "Exepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (AccesoDatos.DButronEstructuraEntities context = new AccesoDatos.DButronEstructuraEntities())
                {
                    var listaPuestos = context.PuestoGetAll().ToList();
                    if (listaPuestos != null)
                    {
                        puesto.Puestos = new List<object>();
                        foreach (var registro in listaPuestos)
                        {
                            Puesto position = new Puesto();
                            position.PuestoID = registro.PuestoID;
                            position.Descripcion = registro.Descripcion;
                            puesto.Puestos.Add(position);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Puesto"] = puesto;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}
