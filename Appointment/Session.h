//Andrew Alarcon Assignment 3
#pragma once
class Date {
private:
	int day, month, year;
public:
	Date(int = 1, int = 1, int = 1900);
	void set(int = 1, int = 1, int = 1900);
	void get();
	void print() const;
	bool operator==(const Date &) const;
};

class Time {
private:
	int hour, minute;
public:
	Time(int = 0, int = 0);
	void set(int = 0, int = 0);
	void get();
	void print() const;
	int get_hour() const;
	int get_minute() const;
	Time operator-(const Time &) const;
	bool operator==(const Time &) const;
	bool operator<(const Time &) const;
	bool operator>(const Time &) const;
};

class Appointment {
protected:
	Date date;
	Time start_time, end_time;
	char description[40], location[40];
public:
	Appointment();
	virtual void get();
	virtual void print() const;
	Date get_date() const;
	Time get_start_time() const;
	Time get_end_time() const;
};

class Session : public Appointment {
private:
	char client_id[11], lname[21], fname[21];
	float hourly_charge;
public:
	Session();
	void get();
	void print() const;
	char *get_id() const;
	char *get_lname() const;
	char *get_fname() const;
	float calc_charge() const;
};

class CourtSession : public Appointment {
private:
	char courtName[21], location[21];
};