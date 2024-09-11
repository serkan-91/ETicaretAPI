﻿namespace EticaretAPI.Infrastructure.Operations;

public static class NameOperation
{
	public static string CharacterRegularity(string name) =>
		name.Replace("\"", "")
			.Replace("'", "")
			.Replace(" ", "-")
			.Replace("ç", "c")
			.Replace("Ç", "C")
			.Replace("ğ", "g")
			.Replace("Ğ", "G")
			.Replace("ı", "i")
			.Replace("İ", "I")
			.Replace("ö", "o")
			.Replace("Ö", "O")
			.Replace("ş", "s")
			.Replace("Ş", "S")
			.Replace("ü", "u")
			.Replace("Ü", "U")
			.Replace("?", "")
			.Replace("%", "")
			.Replace("!", "")
			.Replace("^", "")
			.Replace(":", "")
			.Replace("~", "")
			.Replace("<", "")
			.Replace(">", "")
			.Replace(";", "")
			.Replace("+", "")
			.Replace("*", "")
			.Replace("@", "")
			.Replace("&", "")
			.Replace("=", "")
			.Replace(")", "")
			.Replace("(", "")
			.Replace("]", "")
			.Replace("[", "")
			.Replace("}", "")
			.Replace("{", "")
			.Replace("|", "")
			.Replace("\\", "")
			.Replace("=", "")
			.Replace("/", "")
			.Replace("~", "")
			.Replace("_", "")
			.Replace("_", "")
			.Replace("£", "")
			.Replace("$", "")
			.Replace("€", "")
			.Replace("ä", "a")
			.Replace("Ä", "A")
			.Replace("ß", "")
			.Replace("ñ", "n")
			.Replace("Ñ", "N")
			.Replace("é", "e")
			.Replace("É", "E")
			.Replace("à", "a")
			.Replace("À", "A")
			.Replace("è", "e")
			.Replace("È", "E")
			.Replace("ù", "u")
			.Replace("Ù", "U")
			.Replace("â", "a")
			.Replace("Â", "A")
			.Replace("ê", "e")
			.Replace("Ê", "E")
			.Replace("î", "i")
			.Replace("Î", "I")
			.Replace("ô", "o")
			.Replace("Ô", "O")
			.Replace("û", "u")
			.Replace("Û", "U")
			.Replace("#", "")
			.Replace("`", "");
}
