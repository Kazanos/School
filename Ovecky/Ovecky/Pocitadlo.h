#include <iostream>

#ifndef Pocitadlo__h_
#define Pocitadlo__h_

class Pocitadlo {
public:

	void spracuj_znak(char c);
	void spocitaj(std::istream& s);

	int pocet_znakov() { return pocet_znakov_; };
	int pocet_riadkov() { return pocet_riadkov_; };
	int pocet_slov() { return pocet_slov_; };
	int pocet_viet() { return pocet_viet_; };
	int pocet_cisel() { return pocet_cisel_; };
	int sucet_cisel() { return sucet_cisel_; };
private:
	int pocet_znakov_ = 0;
	int pocet_riadkov_ = 0;
	int pocet_slov_ = 0;
	int pocet_viet_ = 0;
	int pocet_cisel_ = 0;
	int sucet_cisel_ = 0;
	std::string slovo = "";
	int cislo = -1;
	bool bol_alnum = false;
	bool slovo_vo_vete = false;
	bool slovo_alebo_cislo_na_riadku = false;
};

#endif // !Pocitadlo__h_
