//Andrew Alarcon Assignment 3
#include <iostream>
#include <iomanip>
#include "Session.h"
using namespace std;

Date::Date(int d, int m, int y) {
	set(d, m, y);
}
void Date::set(int d, int m, int y) {
	if (d >= 1 && d <= 31)
		day = d;
	else {
		cout << "\ninvalid day: " << d;
		cout << "\nProgram ending. . .";
		system("pause");
		exit(1);
	}
	if (m >= 1 && m <= 12)
		month = m;
	else {
		cout << "\ninvalid month: " << m;
		cout << "\nProgram ending. . .";
		system("pause");
		exit(1);
	}
	if (y >= 1900 && y <= 3000)
		year = y;
	else {
		cout << "\ninvalid year: " << y;
		cout << "\nProgram ending. . .";
		system("pause");
		exit(1);
	}
}
void  Date::get() {
	char ch;
	cout << "Enter date in mm/dd/yyyy format: ";
	cin >> month >> ch >> day >> ch >> year;
	while (month < 1 || month > 12 || day < 1 || day > 31 || year < 1900 || year > 3000) {
		cout << "\nInvalid date entered";
		cout << "\nEnter date in mm/dd/yyyy format: ";
		cin >> month >> ch >> day >> ch >> year;
	}
}
void Date::print() const {
	cout << month << "/" << day << "/" << year;
}
bool Date::operator==(const Date &d) const {
	return day == d.day && month == d.month && year == d.year;
}
Time::Time(int h, int m) {
	set(h, m);
}
void Time::set(int h, int m) {
	if (h >= 0 && h <= 24)
		hour = h;
	else {
		cout << "\nInvalid hour: " << h;
		cout << "\nProgram ending. . .";
		system("pause");
		exit(1);
	}
	if (m >= 0 && m <= 60)
		minute = m;
	else {
		cout << "\nInvalid minute: " << m;
		cout << "\nProgram ending. . .";
		system("pause");
		exit(1);
	}
}
void Time::get() {
	char ch;

	cout << "Enter time in military [hour:minute] format: ";
	cin >> hour >> ch >> minute;
	while (hour < 0 || hour > 24 || minute < 0 || minute > 60) {
		cout << "\nInvalid time entered";
		cout << "\nEnter time in military [hour:minute] format: ";
		cin >> hour >> ch >> minute;
	}
}
void Time::print() const {
	cout << hour << ":" << minute;
}
int Time::get_hour() const {
	return hour;
}
int Time::get_minute() const {
	return minute;
}
Time Time::operator-(const Time &t) const {
	if (minute >= t.minute)
		return Time(hour - t.hour, minute - t.minute);
	else
		return Time(hour - t.hour - 1, minute + 60 - t.minute);
}
bool Time::operator==(const Time &t) const {
	return hour == t.hour && minute == t.minute;
}
bool Time::operator<(const Time &t) const {
	if (hour < t.hour)
		return true;
	else if ((hour == t.hour) && (minute < t.minute))
		return true;
	else
		return false;
}
bool Time::operator>(const Time &t) const {
	return !(*this == t) && !(*this < t);
}
Appointment::Appointment() : date(), start_time(), end_time() {
	strcpy_s(description, "");
	strcpy_s(location, "");
}
void Appointment::get() {
	char c;

	date.get();
	do {
		cout << "Start time - ";
		start_time.get();
		cin.ignore(20, '\n');
		cout << "End time - ";
		end_time.get();
		cin.ignore(20, '\n');
		if (start_time.get_hour() > end_time.get_hour())
			cout << "\nEnd time cannot be earlier than start time" << endl;
	} while (start_time.get_hour() > end_time.get_hour());
	cout << "Enter description: ";
	int i = 0;
	cin.get(c);
	while (c != '\n') {
		description[i++] = c;
		cin.get(c);
	}
	description[i] = '\0';
	cout << "Enter location: ";
	i = 0;
	cin.get(c);
	while (c != '\n') {
		location[i++] = c;
		cin.get(c);
	}
	location[i] = '\0';
}
void Appointment::print() const {
	cout << "\nAppointment Date: ";
	date.print();
	cout << "\nStart time: ";
	start_time.print();
	cout << "\nEnd time: ";
	end_time.print();
	cout << "\nDescription: " << description;
	cout << "\nLocation: " << location;
}
Date Appointment::get_date() const {
	return date;
}
Time Appointment::get_start_time() const {
	return start_time;
}
Time Appointment::get_end_time() const {
	return end_time;
}
Session::Session() : Appointment() {
	strcpy_s(client_id, "");
	strcpy_s(lname, "");
	strcpy_s(fname, "");
	hourly_charge = 100.00;
}
void Session::get() {
	Appointment::get();
	cout << "Enter client I.D.: ";
	cin >> client_id;
	cout << "Enter client first name: ";
	cin >> fname;
	cout << "Enter client last name: ";
	cin >> lname;
	cout << "Enter hourly charge: ";
	cin >> hourly_charge;
}
void Session::print() const {
	Appointment::print();
	cout << "\nClient I.D.: " << client_id;
	cout << "\nClient name: " << fname << " " << lname;
	cout << "\nCharges: " << setprecision(2) << fixed << calc_charge();
}
char *Session::get_id() const {
	return (char*)client_id;
}
char *Session::get_lname() const {
	return (char*)lname;
}
char *Session::get_fname() const {
	return (char*)fname;
}
float Session::calc_charge() const {
	Time period = end_time - start_time;
	float hours = period.get_hour() + period.get_minute() / 60.0;
	
	return hours * hourly_charge;
}