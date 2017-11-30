//Andre Alarcon Assignment 3
#include <iostream>
#include <iomanip>
#include "Session.h"
using namespace std;

bool conflict(Appointment *, Appointment *[], int, int&);
int select_appt(Appointment *[], int);
enum classType { appt, session };

int main() {
	Appointment *ap[100], *temp;
	Session *sp;
	classType type[100];
	int choice, selection, count = 0, index, i;
	char id[11], fname[21], lname[21];
	Date date;
	float charges = 0.0;
	bool found;

	do {
		cout << "\nAppointment Management program by Andrew Alarcon";
		cout << "\nChoose one of the following: "
			<< "\n------------------------------"
			<< "\n1. Create a new appointment"
			<< "\n2. Create a new session"
			<< "\n3. View all appointments and sessions"
			<< "\n4. Edit appointment/session"
			<< "\n5. Check a date for appointments/sessions"
			<< "\n6. View sessions for a client"
			<< "\n7. View charges for a client"
			<< "\n8. View total charges for all clients"
			<< "\n9. Delete an appointment or session"
			<< "\n10. Quit"
			<< "\n----------------------------->";
		cin >> choice;
		system("cls");
		switch (choice) {
		case 1: temp = new Appointment;
			temp->get();
			if (conflict(temp, ap, count, index)) {
				cout << "\nThis will conflict with: ";
				ap[index]->print();
				cout << "\n\nAppointment not entered.";
			}
			else {
				ap[count] = temp;
				type[count] = appt;
				count++;
			}
			break;
		case 2: temp = new Session;
			temp->get();
			if (conflict(temp, ap, count, index)) {
				cout << "\nThis will conflict with: ";
				ap[index]->print();
				cout << "\n\nSession not entered.";
			}
			else {
				ap[count] = temp;
				type[count] = session;
				count++;
			}
			break;
		case 3: for (i = 0; i < count; i++) {
			ap[i]->print();
			cout << endl;
		}
				break;
		case 4: if ((choice = select_appt(ap, count)) == -1) {
			cout << "\nThere are no appointments or sessions on this date.";
			break;
		}
				else {
					temp = ap[choice];
					if (choice < count - 1)
						for (i = choice; i < count; i++) {
							ap[i - 1] = ap[i];
							type[i - 1] = type[i];
						}
					count--;
				}

				do {
					cout << "\nEnter 1 to select an appointment and 2 to choose a session: ";
					cin >> selection;
					if (selection == 1) {
						ap[count] = new Appointment;
						type[count] = appt;
					}
					else if (selection == 2) {
						ap[count] = new Session;
						type[count] = session;
					}
				} while (selection < 1 || selection > 2);

				ap[count]->get();
				if (conflict(ap[count], ap, count, index)) {
					cout << "\nThis will conflict with:\n";
					ap[index]->print();
					cout << "\n\nAppointment or session not modified.";
					delete ap[count];
					ap[count++] = temp;
				}
				else {
					count++;
					delete temp;
				}
				break;
		case 5: cout << "\nEnter date to check: ";
			date.get();
			found = false;
			for (i = 0; i < count; i++)
				if (date == ap[i]->get_date()) {
					found = true;
					ap[i]->print();
					cout << endl;
				}
			if (!found) {
				cout << "\nYou have no appointments or sessions on ";
				date.print();
			}
			break;
		case 6: do {
			cout << "\nEnter 1 to search by I.D. or 2 to search by name: ";
			cin >> choice;
			found = false;
			if (choice == 1) {
				cout << "\nEnter client I.D.: ";
				cin >> id;
				for (i = 0; i < count; i++)
					if (type[i] == session) {
						sp = (Session*)(ap[i]);
						if (!strcmp(id, sp->get_id())) {
							sp->print();
							found = true;
						}
					}
				if (!found)
					cout << "\nClient not found. ";
			}
			else if (choice == 2) {
				cout << "\nEnter client last name: ";
				cin >> lname;
				cout << "\nEnter client first name: ";
				cin >> fname;
				for (i = 0; i < count; i++)
					if (type[i] == session) {
						sp = (Session*)(ap[i]);
						if (!strcmp(lname, sp->get_lname()) && !strcmp(fname, sp->get_fname())) {
							sp->print();
							found = true;
						}
					}
				if (!found)
					cout << "\nClient not found. ";
			}
			else
				cout << "\nPlease, enter either 1 or 2: ";
		} while (choice < 1 || choice > 2);
		break;
		case 7: charges = 0.0;
			found = false;
			do {
				cout << "\nEnter 1 to search by I.D. or 2 to search by name: ";
				cin >> choice;
				if (choice == 1) {
					cout << "\nEnter client I.D.: ";
					cin >> id;
					for (i = 0; i < count; i++)
						if (type[i] == session) {
							sp = (Session*)(ap[i]);
							if (!strcmp(id, sp->get_id())) {
								found = true;
								charges += sp->calc_charge();
								cout << "\nCharges for " << id << " is: " << charges;
							}
						}
					if (!found)
						cout << "\nClient not found. ";
				}
				else if (choice == 2) {
					cout << "\nEnter client last name: ";
					cin >> lname;
					cout << "\nEnter client first name: ";
					cin >> fname;
					for (i = 0; i < count; i++) {
						if (type[i] == session) {
							sp = (Session*)(ap[i]);
							if (!strcmp(lname, sp->get_lname()) && !strcmp(fname, sp->get_fname())) {
								found = true;
								charges += sp->calc_charge();
								cout << "\nCharges for " << fname << " " << lname << " is: " << charges;
							}
						}
					}
					if (!found)
						cout << "\nClient not found. ";
				}
				else
					cout << "\nPlease, enter either 1 or 2: ";
			} while (choice < 1 || choice > 2);
			break;
		case 8: charges = 0.0;
			for (i = 0; i < count; i++)
				if (type[i] == session) {
					sp = (Session*)(ap[i]);
					charges += sp->calc_charge();
				}
			cout << "\nTotal charges for all clients is " << setprecision(2) << fixed << charges;
			charges = 0.0;
			break;
		case 9: if ((index = select_appt(ap, count)) == -1) {
			cout << "\nThere are no appointments or sessions on this date.";
			break;
		}
				else if (index < count - 1) {
					delete ap[index];
					for (int i = index; i < count - 1; i++) {
						ap[i] = ap[i + 1];
						type[i] = type[i + 1];
					}
				}
				else
					delete ap[count - 1];
				count--;
				break;
		case 10: return 0;
		default: cout << "\nInvalid entry - please, enter a number between 1 and 10.";
		}
	} while (1);
}
bool conflict(Appointment *p, Appointment *ap[], int size, int& i) {
	Date date = p->get_date();
	Time start = p->get_start_time();
	Time end = p->get_end_time();

	for (i = 0; i < size; i++)
		if (date == ap[i]->get_date())
			if ((start == ap[i]->get_start_time()) || (start < ap[i]->get_start_time()) &&
				(end > ap[i]->get_start_time()) || (start > ap[i]->get_start_time()) &&
				(start < ap[i]->get_end_time()))
				return true;
	return false;
}
int select_appt(Appointment *ap[], int size) {
	Date date;
	int choice, int_array[10], j = 0;

	cout << "\nEnter appointment or session date: ";
	date.get();
	for (int i = 0; i < size; i++)
		if (date == ap[i]->get_date()) {
			int_array[j++] = i;
			cout << "\n----------- " << j << " ----------";
			ap[i]->print();
			cout << endl;
		}
	if (j == 0)
		return -1;
	else {
		do {
			cout << "\nSelect the appointment by entering its number: [1 - " << j << "]: ";
			cin >> choice;
		} while (choice < 1 || choice > j);
		return int_array[choice - 1];
	}
}