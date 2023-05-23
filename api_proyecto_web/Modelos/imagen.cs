namespace api_proyecto_web.Modelos
{
    public class imagen
    {

        public int id { get; set; }
        public int id_usuario { get; set; }

        public string nombre { get; set; }
        public string URL { get; set; }

        public imagen()
        {
            id = new int();
            id_usuario = new int();
            nombre = string.Empty;
            URL = string.Empty;
        }
    }
}
