public class SignInFailException : ApplicationException
{
    public SignInFailException() : base("Usuário/e-mail ou senha incorretos. Tente novamente.")
    {

    }
}
