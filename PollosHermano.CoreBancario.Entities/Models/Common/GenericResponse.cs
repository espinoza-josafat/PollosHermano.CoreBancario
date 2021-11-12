namespace PollosHermano.CoreBancario.Entities.Models.Common
{
    public class GenericResponse
    {
        public GenericResponse()
        {
            Status = 1;
            Message = "OK";
            Description = "OK";
            Data = null;
        }

        public int Status { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public object Data { get; set; }
    }

    public class GenericResponse<T>
    {
        public GenericResponse()
        {
            Status = 1;
            Message = "OK";
            Description = "OK";
            Data = default;
        }

        public int Status { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public T Data { get; set; }
    }
}
