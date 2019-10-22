#include "Pocitadlo.h"

void Pocitadlo::spracuj_znak(char c) {
	++pocet_znakov_;
	if (isalnum(c))
	{
		if (bol_alnum)
		{
			if (slovo != "")
			{
				slovo += c;
			}
			else if (isdigit(c))
			{
					cislo = cislo * 10;
					cislo += (int)c - (int)'0';
			}
		}
		else
		{
			if (isdigit(c))
			{
				cislo = (int)c - (int)'0';
			}
			else
			{
				slovo += c;
				slovo_vo_vete = true;
			}

			if (not slovo_alebo_cislo_na_riadku)
			{
				slovo_alebo_cislo_na_riadku = true;
				++pocet_riadkov_;
			}
		}

		bol_alnum = true;
	}
	else
	{
		if (c == '\n')
		{
			slovo_alebo_cislo_na_riadku = false;
		}
		else if (c == '.' or c == '!' or c == '?')
		{
			if (slovo_vo_vete)
			{
				++pocet_viet_;
				slovo_vo_vete = false;
			}
		}

		if (slovo != "")
		{
			++pocet_slov_;
			slovo = "";
		}
		if (cislo != -1)
		{
			++pocet_cisel_;
			sucet_cisel_ += cislo;
			cislo = -1;
		}

		bol_alnum = false;
	}
}

void Pocitadlo::spocitaj(std::istream & s){
	char c = s.get();
	while (c != EOF)
	{
		spracuj_znak(c);
		c = s.get();
	}
}