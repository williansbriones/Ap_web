namespace api_proyecto_web.Servicios
{
    public interface IcrudCupon
    {
        //creacion cupon 
        public void Crearcupon(string Nombre, int CantidadDesuento, string Codigo, int Cantidad_limite, string FechaInicio, string FechaTermino);
        //Eliminar cupon 
        public void Eliminarcupon(int id_cupon);
        //modificar 
        public void Modificarcupon();
        //habilitar y desabiliar
        public void Des_Habilitar();


    }   
}
