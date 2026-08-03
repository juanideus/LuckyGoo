

public class GeneratePassword
{
    public static string GenerateRandomPassword(int length = 12)
    {
        //la contraseña no puede empezar con 0
        const string validChars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%^&*()_+-=[]{}|;:,.";
        var random = new Random();
        var passwordChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            passwordChars[i] = validChars[random.Next(validChars.Length)];
        }
        //verificamos que la contraseña no empiece con 0
        if (passwordChars[0] == '0')
        {
            passwordChars[0] = validChars[random.Next(1, validChars.Length)];
        }
        return new string(passwordChars);
    }
}