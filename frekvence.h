#include <iostream>
#include <map>

#ifndef frekvence__h_
#define frekvence__h_

class Frekvence {
public:
	void spocitaj(std::istream & s);
	std::map<std::string, int> histogram() { return m; };
private:
	std::map<std::string, int> m;
	std::string slovo;
};

#endif // !frekvence__h_

