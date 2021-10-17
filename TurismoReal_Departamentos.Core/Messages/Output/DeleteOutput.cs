namespace TurismoReal_Departamentos.Core.Messages.Output
{
    public class DeleteOutput
    {
        public DeleteOutput() { }

        public DeleteOutput(string message, bool removed)
        {
            this.message = message;
            this.removed = removed;
        }

        public string message { get; set; }
        public bool removed { get; set; }
    }
}
