#include "frekvence.h"
#include <iostream>

void Frekvence::spocitaj(std::istream& s) {
	std::string slovo;
	s >> slovo;
	while (slovo[slovo.length()] != EOF)
	{
		auto it = m.find(slovo);
		if (it != m.end())
		{
			it->second = it->second + 1;
		}
		else
		{
			m.insert(std::pair{ slovo, 1 });
		}
		s >> slovo;
	}
}