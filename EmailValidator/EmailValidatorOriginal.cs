namespace EmailValidator;

public static class EmailValidatorOriginal
{
    /// <summary>
    /// Retorna True caso o email informado tenha pelo menos 5 caracteres, e uma '@' com um '.' depois. Não valida
    /// quando localPart contém valores entre aspas, que é permitido pela RFC mas é raríssimo existir.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsWellFormed(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Length < 5)
            return false;

        // precisa ter um @
        var separatorIndex = email.IndexOf('@');
        if (separatorIndex == -1)
            return false;

        var localPart = email[..separatorIndex]; // antes do arroba
        if (!IsValidLocalPart(localPart))
            return false; 
        
        var domain = email[(separatorIndex + 1)..]; // depois do arroba
        if (!IsValidDomain(domain))
            return false; 
        
        if (domain.IndexOf('.') == -1)
            return false; // precisa ter um ponto em algum lugar depois do @
        
        return true; // PARECE ser um email ok
    }

    private static bool IsValidLocalPart(string localPart)
    {
        // O formato para endereço de email é local-part@domain, onde o local-Part pode ter até 64 caracteres. A
        // definição formal está na RFC 5322, na RFC 5321 e RFC 3696.
        if (string.IsNullOrWhiteSpace(localPart) || localPart.Length > 64)
            return false;
        
        return ValidName(localPart, true);
    }

    private static bool IsValidDomain(string domain)
    {
        // Comprimento máximo: O comprimento máximo permitido para um nome de domínio completo é de 253 caracteres,
        // contando todos os subdomínios e o domínio de nível superior (.com, .org, .net, etc.). No entanto, o
        // comprimento recomendado é de até 63 caracteres para garantir a máxima compatibilidade entre os sistemas.
        if (string.IsNullOrWhiteSpace(domain) || (domain.Length < 3 || domain.Length > 63))
            return false;

        return ValidName(domain);
    }

    private static bool IsValidLocalPartChar(int value)
    {
        switch (value)
        {
            case 33: // !
            case 35: // #
            case 36: // $
            case 37: // %
            case 38: // &
            case 39: // '
            case 42: // *
            case 43: // +
            case 47: // /
            case 61: // =
            case 63: // ?
            case 94: // ^
            case 95: // _
            case 96: // `
            case 123: // {
            case 124: // |
            case 125: // }
                return true;
        }

        return false;
    }

    private static bool ValidName(string value, bool localPart = false)
    {
        // Caracteres permitidos: letras (de "a" a "z"), números (de "0" a "9"), ponto (".") e o hífen ("-").
        var containLetters = false;

        for (var i = 0; i < value.Length; i++)
        {
            var current = (int)value[i];
            switch (current)
            {
                case >= 65 and <= 90: // A-Z
                case >= 97 and <= 122: // a-z
                    containLetters = true;
                    continue;
                case >= 48 and <= 57: // 0-9
                case 45: // -
                case 46: // .
                    continue;
            }

            // - outros caracteres permitidos no caso de ser localPart
            if (localPart && IsValidLocalPartChar(current))
                continue;

            return false;
        }

        // o hífen ou o ponto não pode ser o primeiro ou o último caractere do nome de domínio.
        var primeiro = (int)value[0];
        var ultimo = (int)value[^1];
        if (primeiro == 45 || primeiro == 46 || ultimo == 45 || ultimo == 46)
            return false;

        // nem dois pontos ou dois hífens juntos
        if (value.Contains("..") || value.Contains("--"))
            return false;

        // nem endereço IP, ou somente números
        if (!containLetters)
            return false;

        return true;
    }
}