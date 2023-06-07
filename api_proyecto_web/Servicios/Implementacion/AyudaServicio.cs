using api_proyecto_web.Modelos;


namespace api_proyecto_web.Servicios.Implementacion
{
    public class AyudaServicio
    {
        static Ayuda Ayuda1 = new Ayuda();

        public AyudaServicio()
        {
            if (Ayuda1.email == String.Empty)
            {
                Ayuda1.email = "daniloki10@gmail.com";
                Ayuda1.descripcion = "Hola, este es un test de prueba";
            }





        }
        public void generar_Solicitud(string email, string descripcion)
        {
            Ayuda1.email = email;
            Ayuda1.descripcion = descripcion;

        }


    }
}
