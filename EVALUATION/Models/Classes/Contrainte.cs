using System.Globalization;
public class Contrainte{
    public static DateTime ParseDate(string? value)
{
    string[] formats = {
            "dd/MM/yyyy", "dd-MM-yyyy", "yyyy/MM/dd", "yyyy-MM-dd",
            "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "yyyy/MM/dd HH:mm", "yyyy-MM-dd HH:mm",
            "dd/MM/yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"
        };

    // Vérifier si le premier caractère est un seul chiffre
    if (value != null && value.Length >= 2 && char.IsDigit(value[0]) && value[1] == '/')
    {
        // Ajouter un zéro devant le premier jour
        value = "0" + value;
    }

    // Essayer de parser la date en utilisant plusieurs formats
    if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
    {
        return result;
    }
    else
    {
        // Si la conversion échoue, retourner la chaîne d'entrée
        // Vous pouvez choisir de traiter cette valeur plus tard dans votre logique
        throw new ArgumentException($"La valeur de la date '{value}' n'est pas dans un format de date valide.");
    }
}

    public static string ApplyConstraints(string value)
    {
        // Vérifier si la valeur est null ou vide
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"La valeur de  {value} est null ou vide.");
        }

        // Supprimer les espaces blancs inutiles
        value = value.Trim();
        value = value.Replace(",",".");

        if (value.EndsWith("%"))
        {
            value = value.TrimEnd('%');
            value = (double.Parse(value, CultureInfo.InvariantCulture)).ToString();
        }

        // Retourner la valeur après application des contraintes
        return value;
    }
    public static double ParseDistance(string? value)
{
    if (value == null)
    {
        throw new ArgumentNullException(nameof(value));
    }

    // Remplacer la virgule par un point décimal
    value = value.Replace(',', '.');

    // Rechercher la position du texte "km" dans la chaîne
    int kmIndex = value.IndexOf("km", StringComparison.OrdinalIgnoreCase);

    // Si "km" est trouvé dans la chaîne
    if (kmIndex != -1)
    {
        // Supprimer "km" de la chaîne
        value = value.Remove(kmIndex, 2).Trim();
    }

    // Convertir la chaîne en double
    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
    {
        return result;
    }
    else
    {
        throw new ArgumentException($"La valeur de la distance '{value}' n'est pas valide.");
    }
}


}