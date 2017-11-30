//Andrew Alarcon CSIT 832 Assignment 2
#include <iostream>
#include <fstream>
#include <string>
#include <stdio.h>
#include <stdlib.h>
using namespace std;
#include "graphtype.h"

int main() {
	ifstream in("load.txt");
	int menu, depart, dest, number, totalMiles;
	int index = 0, miles = 0;
	string city, city2;
	GraphType cities = GraphType(9);

	while (!in.eof()) {
		getline(in, city, '\n');
		cities.AddVertex(city, index);
		getline(in, city, '\n');
		number = std::stoi(city);
		for (int x = 0; x < number; x++) {
			getline(in, city, '\n');
			getline(in, city2, '\n');
			miles = std::stoi(city2);
			cities.AddEdge(index, miles, city);
		}
		index++;
	}

	do {
		cout << "1. Choose departure city\n";
		cout << "2. Exit\n";
		cout << "Enter input: ";
		cin >> menu;

		switch (menu)
		{
		case 1: cout << endl;
			cities.Reset();
			cities.SetTotalMiles();
				for (int x = 0; x < cities.GetSize(); x++) {
					cout << x + 1 << ". " << cities[x] << endl;
				}
				cout << "Choose City: ";
				cin >> depart;
				depart -= 1;
				cout << endl;
				for (int x = 0; x < cities.GetSize(); x++) {
					if (depart > x)
						cout << x + 1 << ". " << cities[x] << endl;
					else if (depart == x) {}
					else
						cout << x << ". " << cities[x] << endl;
				}
					cout << "Choose City: ";
					cin >> dest;
					cout << endl;
					if (depart >= dest)
						dest -= 1;
					if (cities.IsConnected(depart, dest)) {
						cities.Reset();
						if (cities.IsDirect(depart, dest))
							cout << "Direct connection between " << cities[depart] << " and " << cities[dest] << endl;
						else {
							cout << "No direct connection between " << cities[depart] << " and " << cities[dest] << endl;
							cout << "Only through flights available." << endl;
						}
						cities.Reset();
						if (!cities.IsThrough(depart, dest))
							cout << "No through connection between " << cities[depart] << " and " << cities[dest] << endl;
						cities.Reset();
						cities.AllConnections(depart, dest, depart);
					}
					else 
						cout << "No connection between " << cities[depart] << " and " << cities[dest] << endl;
					cout << "Press any key to return to menu." << endl << endl;
					system("pause>nul");
		case 2: {}
			}
	} while (menu != 2);

	return 0;
}