using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Departamentos { get; set; }

        public static Dictionary<string, object> GetAll()
        {
            Departamento departamento = new Departamento();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { {"Departamento", departamento}, {"Exepcion", excepcion}, {"Resultado", false} };
            try
            {
                using (AccesoDatos.DButronEstructuraEntities context = new AccesoDatos.DButronEstructuraEntities())
                {
                    var listaDepartamentos = context.DepartamentoGetAll().ToList();
                    if (listaDepartamentos != null)
                    {
                        departamento.Departamentos = new List<object>();
                        foreach (var registro in listaDepartamentos)
                        {
                            Departamento department = new Departamento();
                            department.DepartamentoID = registro.DepartamentoID;
                            department.Descripcion = registro.Descripcion;
                            departamento.Departamentos.Add(department);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Departamento"] = departamento;
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
