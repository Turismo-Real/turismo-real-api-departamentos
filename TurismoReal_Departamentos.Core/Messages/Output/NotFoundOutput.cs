namespace TurismoReal_Departamentos.Core.Messages.Output
{
    public class NotFoundOutput
    {
        public NotFoundOutput() { }

        public NotFoundOutput(string message)
        {
            this.message = message;
        }

        public string message { get; set; }
    }
}
