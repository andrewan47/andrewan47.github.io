#pragma once
class GraphType {
private:
	int size;
	int weight;
	int totalMiles;
	string city;
	bool* marks;
	bool* path;
	string* printPath;
	int* milesPath;
	GraphType* next = NULL;
	GraphType* list;
public:
	GraphType(int = 0);
	~GraphType();
	GraphType(string, int, GraphType* = NULL);
	void Reset();
	void ResetMarks();
	void AddVertex(string, int);
	void AddEdge(int, int, string);
	void PrintGraph(int, int, int = -1);
	int GetSize();
	int GetTotalMiles();
	void SetTotalMiles();
	bool IsConnected(int, int, int = -1);
	bool IsThrough(int, int, int = -1);
	bool IsDirect(int, int);
	void AllConnections(int, int, int, int = 0, int = 0,int = -1);
	string& operator[](int);
	string operator[](int) const;
};

#include "graphtype.cpp"