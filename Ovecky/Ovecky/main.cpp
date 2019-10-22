#include <iostream>
#include "Pocitadlo.h"

int main()
{
	Pocitadlo ovecky;
	ovecky.spocitaj(std::cin);
	std::cout << "znaku: ";
	std::cout << ovecky.pocet_znakov();
	std::cout << "\nslov: ";
	std::cout << ovecky.pocet_slov();
	std::cout << "\nvet: ";
	std::cout << ovecky.pocet_viet();
	std::cout << "\nradku: ";
	std::cout << ovecky.pocet_riadkov();
	std::cout << "\ncisel: ";
	std::cout << ovecky.pocet_cisel();
	std::cout << "\nsoucet: ";
	std::cout << ovecky.sucet_cisel();


}
