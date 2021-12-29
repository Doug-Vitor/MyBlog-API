public class NotFoundException : ApplicationException
{
    public NotFoundException() : base("Não foi possível encontrar um registro correspondente ao ID fornecido.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}