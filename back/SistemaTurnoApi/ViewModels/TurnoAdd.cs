namespace SistemaTurnoApi.ViewModels
{
    public class TurnoAdd
    {
        public string Id { get; set; }  
        public string Nombre { get; set; }  

        public Response Valid()
        {
            if (string.IsNullOrEmpty(Id))
                return new Response { Success = false, Message = "Campo Id Requerido" };
            if (string.IsNullOrEmpty(Nombre))
                return new Response { Success = false, Message = "Campo Nombre es requerido" };

            return new Response { Success = true, Message = "" };
        }
    }
}
