namespace EmailValidator;

public static class EmailValidatorFast
{
    private static readonly HashSet<int> ValidLocalPartChars = new()
    {
        33 /* ! */, 35 /* # */, 36 /* $ */, 37 /* % */, 38 /* & */, 39 /* " */, 42 /* * */, 43 /* + */, 47 /* / */,
        61 /* = */, 63 /* ? */, 94 /* ^ */, 95 /* _ */, 96 /* ' */, 123 /* { */, 124 /* | */, 125 /* } */
    };

    /// <summary>
    /// Verifica se o valor representa um email bem formado, de acordo com a RFC. Precisa ter no mínimo 5 e no máximo
    /// 128 caracteres. Ter um sinal de arroba "@", que separa o "localPart" do "domain". LocalPart pode conter
    /// caracteres ASCII de A-Z, a-z, 0-9 e os sinais -.!#$%&"*+=?^_'{/} e ter no máximo 64 caracteres. Domain pode
    /// conter caracteres ASCII A-Z, a-z, 0-9, hífen ("-"), ponto ("."), ter no mínimo 3 e no máximo 63 caracteres.
    /// Domain precisa conter ao menos um ponto (".com", ".net.br", etc). LocalPart e domain não podem começar nem
    /// terminar com ponto ou hífen, também não podem conter dois pontos ou dois hífens juntos. Apesar da RFC dizer que
    /// é possível ter várias outras variações, desde que estejam na localPart e entre aspas, tais variações não são
    /// consideradas, por serem raríssimas. Adicionalmente, somente números não é considerado válido.  
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsWellFormed(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Length < 5 || email.Length > 128)
            return false; // tamanho inválido

        var emailSpan = email.AsSpan();
        var atSignIndex = emailSpan.IndexOf('@');
        if (atSignIndex == -1)
            return false; // não tem arroba

        var localPart = emailSpan[..atSignIndex];
        if (!IsValidLocalPart(localPart))
            return false;

        var domain = emailSpan[(atSignIndex + 1)..];
        if (!IsValidDomain(domain))
            return false;

        if (domain.IndexOf('.') == -1)
            return false; // tem que ter pelo menos um ponto depois da @

        if (emailSpan.IndexOf("..") > -1 || emailSpan.IndexOf("--") > -1)
            return false; // não pode ter dois pontos nem dois hífens        

        return true;
    }
    
    private static bool IsValidLocalPart(ReadOnlySpan<char> localPart)
    {
        if (localPart.IsEmpty || localPart.Length > 64)
            return false;

        return ValidName(localPart, true);
    }

    private static bool IsValidDomain(ReadOnlySpan<char> domain)
    {
        if (domain.IsEmpty || domain.Length < 3 || domain.Length > 63)
            return false;

        return ValidName(domain);
    }

    private static bool ValidName(ReadOnlySpan<char> value, bool localPart = false)
    {
        var containLetters = false;
        for (var i = 0; i < value.Length; i++)
        {
            var current = (int)value[i];

            switch (current)
            {
                case >= 65 /* A */ and <= 90 /* Z */ or >= 97 /* a */ and <= 122 /* z */:
                    containLetters = true;
                    continue;
                case >= 48 /* 0 */ and <= 57 /* 9 */ or 45 /* - */ or 46 /* . */:
                    continue;
            }

            // se for localPart, precisa testar pelos caracteres a mais que são permitidos
            if (localPart && ValidLocalPartChars.Contains(current))
                continue;

            return false;
        }

        // não pode começar nem terminar com "-" ou "."
        var primeiro = (int)value[0];
        var ultimo = (int)value[^1];
        if (primeiro == 45 /* - */ || primeiro == 46 /* . */ || ultimo == 45 /* - */ || ultimo == 46 /* . */)
            return false;

        if (!containLetters)
            return false;

        return true;
    }
}