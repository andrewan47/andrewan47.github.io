GraphType::GraphType(int s) : size(s) {
	if (size > 0) {
		list = new GraphType[size];
		marks = new bool[size];
		path = new bool[size];
		printPath = new string[size];
		milesPath = new int[size];
	}
	else {
		list = NULL;
		marks = NULL;
		path = NULL;
		printPath = NULL;
		milesPath = NULL;
	}
}

GraphType::~GraphType() {
	delete[] list;
	delete[] marks;
}

GraphType::GraphType(string c, int w, GraphType* nxt) {
	city = c;
	weight = w;
	next = nxt;
}

void GraphType::Reset() {
	for (int x = 0; x < size; x++) {
		marks[x] = false;
		path[x] = false;
	}
}

void GraphType::ResetMarks() {
	for (int x = 0; x < size; x++)
		marks[x] = false;
}

void GraphType::AddVertex(string c, int i) {
	list[i].city = c;
}

void GraphType::AddEdge(int nv, int w, string c) {
	GraphType* nodePtr = &list[nv];

	while (nodePtr->next != NULL)
		nodePtr = nodePtr->next;

	nodePtr->next = new GraphType(c, w);
}

void GraphType::PrintGraph(int depart, int dest, int temp) {
	bool check = false;
	GraphType* nodePtr = &list[depart];
	nodePtr = nodePtr->next;

	while (nodePtr) {
		if (nodePtr->city == list[dest].city) {
			check = true;
			totalMiles += nodePtr->weight;
		}
		nodePtr = nodePtr->next;
	}

	if (check == false) {
		nodePtr = &list[depart];
		nodePtr = nodePtr->next;

		while (nodePtr) {
			for (int x = 0; x < size; x++) {
				if (x == temp || x == dest) {}
				else if (marks[x] == true && path[x] == true && list[x].city == nodePtr->city) {
					marks[x] = false;
					totalMiles += nodePtr->weight;
					cout << list[x].city << " ";
					PrintGraph(x, dest, depart);
				}
			}
			nodePtr = nodePtr->next;
		}
	}
	for (int x = 0; x < size; x++) {
		cout << "\nEnd Marks: " << marks[x] << " End paths: " << path[x] << endl;
	}
	system("pause");
}

int GraphType::GetSize() {
	return size;
}

void GraphType::SetTotalMiles() {
	totalMiles = 0;
}

int GraphType::GetTotalMiles() {
	return totalMiles;
}

bool GraphType::IsConnected(int depart, int dest, int temp) {
	GraphType* nodePtr = &list[depart];
	nodePtr = nodePtr->next;
	bool connect = false;

	marks[depart] = true;

		while (nodePtr && connect == false) {
			if (nodePtr->city == list[dest].city) {
				connect = true;
				path[depart] = true;
			}
			else
				nodePtr = nodePtr->next;
		}

	nodePtr = &list[depart];
	nodePtr = nodePtr->next;

	while (nodePtr && connect == false) {
		for (int x = 0; x < size; x++) {
			if (nodePtr->city == list[x].city && marks[x] == false) {
				connect = IsConnected(x, dest, depart);
				if (connect == true)
					path[x] = true;
			}
		}
		nodePtr = nodePtr->next;
	}

	return connect;
}

bool GraphType::IsThrough(int depart, int dest, int temp) {
	GraphType* nodePtr = &list[depart];
	nodePtr = nodePtr->next;
	bool connect = false;

	marks[depart] = true;

	if (temp != -1) {
		while (nodePtr && connect == false) {
			if (nodePtr->city == list[dest].city) {
				connect = true;
				path[depart] = true;
			}
			else
				nodePtr = nodePtr->next;
		}
		nodePtr = &list[depart];
		nodePtr = nodePtr->next;
	}

	while (nodePtr && connect == false) {
		for (int x = 0; x < size; x++) {
			if (nodePtr->city == list[x].city && marks[x] == false && nodePtr->city != list[dest].city) {
				connect = IsConnected(x, dest, depart);
				if (connect == true)
					path[x] = true;
			}
		}
		nodePtr = nodePtr->next;
	}

	return connect;
}

bool GraphType::IsDirect(int depart, int dest) {
	bool direct = false;
	GraphType* nodePtr = &list[depart];
	nodePtr = nodePtr->next;

	while (nodePtr && direct == false) {
		if (nodePtr->city == list[dest].city)
			direct = true;
		else
			nodePtr = nodePtr->next;
	}

	return direct;
}

void GraphType::AllConnections(int depart, int dest, int start, int miles,int last, int index) {
	GraphType* nodePtr = &list[depart];
	nodePtr = nodePtr->next;
	bool connect = false;

	marks[depart] = true;

	if (index >= 0) {
		printPath[index] = list[depart].city;
		milesPath[index] = miles;

		while (nodePtr && connect == false) {
			if (nodePtr->city == list[dest].city) {
				connect = true;
				path[depart] = true;
				cout << "Through connection between " << list[start].city << " and " << list[dest].city << " via ";
				for (int x = 0; x <= index; x++)
					if (printPath[x] != list[dest].city) {
						cout << printPath[x] << " ";
						totalMiles += milesPath[x];
					}
					else
					{
					}
				totalMiles += nodePtr->weight;
				cout << "- " << totalMiles << " miles" << endl;
				totalMiles = 0;
			}
			else
				nodePtr = nodePtr->next;
		}
	}

	nodePtr = &list[depart];
	nodePtr = nodePtr->next;

	while (nodePtr) {
		for (int x = 0; x < size; x++) {
			if (nodePtr->city == list[x].city && marks[x] == false && nodePtr->city != list[dest].city) {
				index++;
				AllConnections(x, dest, start, nodePtr->weight, depart, index);
				if (nodePtr->city == list[x].city && marks[x] == true) {
					index--;
					path[x] = false;
					marks[x] = false;
				}
				if (index < 0)
					index = -1;
			}
		}
		nodePtr = nodePtr->next;
	}
	index--;
}

string& GraphType::operator[](int index) {
	return list[index].city;
}

string GraphType::operator[] (int index) const {
	return list[index].city;
}