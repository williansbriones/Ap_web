namespace api_proyecto_web.Servicios
{
    public interface IcrudCupon
    {
        //creacion cupon 
        public void Crearcupon(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino);
        //habilitar cupon 
        public void Habilitar_cupon(int id_cupon);
        //modificar 
        public void Modificarcupon(int id_cupon, string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite);
        //habilitar y desabiliar
        public void Des_Habilitar(int id_cupon);
        //eliminar
        public void Eliminar_cupon(int id);
        
       
    }   
}
