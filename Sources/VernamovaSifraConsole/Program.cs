using System;
using System.Collections.Generic;

namespace VernamovaSifraConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			vytvorAbecedy();

			Console.WriteLine("Zmacknete \'s\' pro sifrovani a \'d\' pro desifrovani.");
			char zmacknuto = Console.ReadKey().KeyChar;
			Console.Clear();

			Console.Write("Zadejte klic: ");
			string klic = Console.ReadLine();
			Console.Write("Zadejte text: ");
			string text = Console.ReadLine();

			switch (zmacknuto)
			{
				case 's':
					Console.WriteLine("\n{0}", Sifruj(klic, text));
					break;

				case 'd':
					Console.WriteLine("\n{0}", Desifruj(klic, text));
					break;

				default:
					throw new Exception("neznama klavesa");
			}

			Console.ReadKey();
		}

		static Dictionary<int, char> abecedaPismena = new Dictionary<int, char>();
		static Dictionary<char, int> abecedaCisla = new Dictionary<char, int>();

		static void vytvorAbecedy()
		{
			const string pismena = "abcdefghijklmnopqrstuvwxyz";
			int[] cisla = new int[26];
			for (int i = 0; i < cisla.Length; i++)
			{
				cisla[i] = i;
			}

			foreach (int i in cisla)
			{
				abecedaPismena.Add(i, pismena[i]);
				abecedaCisla.Add(pismena[i], i);
			}
		}

		static string Sifruj(string klic, string text)
		{
			int delka = klic.Length;

			int[] klicCisla = new int[delka];
			int[] textCisla = new int[delka];
			int[] souctyCisla = new int[delka];

			for (int i = 0; i < delka; i++)
			{
				klicCisla[i] = abecedaCisla[klic[i]];
				textCisla[i] = abecedaCisla[text[i]];
			}

			for (int i = 0; i < delka; i++)
			{
				souctyCisla[i] = klicCisla[i] + textCisla[i];
			}

			char[] vysledek = new char[delka];

			for (int i = 0; i < delka; i++)
			{
				vysledek[i] = abecedaPismena[souctyCisla[i] % 25];
			}

			return new string(vysledek);
		}

		static string Desifruj(string klic, string text)
		{
			int delka = klic.Length;

			int[] klicCisla = new int[delka];
			int[] textCisla = new int[delka];
			int[] rozdilyCisla = new int[delka];

			for (int i = 0; i < delka; i++)
			{
				klicCisla[i] = abecedaCisla[klic[i]];
				textCisla[i] = abecedaCisla[text[i]];
			}

			for (int i = 0; i < delka; i++)
			{
				rozdilyCisla[i] = textCisla[i] - klicCisla[i];
			}

			char[] vysledek = new char[delka];

			for (int i = 0; i < delka; i++)
			{
				vysledek[i] = rozdilyCisla[i] < 0 ? abecedaPismena[25 + rozdilyCisla[i]] : abecedaPismena[rozdilyCisla[i]];
			}

			return new string(vysledek);
		}
	}
}
