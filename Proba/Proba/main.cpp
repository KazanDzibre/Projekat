#include <iostream>
#include <thread>
#include <mutex>
#include <conio.h>
#include <stdlib.h>     //for using the function sleep
#include <time.h>
#include "windows.h" 



using namespace std;

mutex mtx;
bool permission = false;
bool permissionForMain = false;

void interuptHandler() {
	printf("javljanje iz callback-a");
}

void threadFunction() {
	while (true)
	{
		if (permission) {
			interuptHandler();
			permission = false;
		}
	}
}

int main() {

	thread t(threadFunction);

	getch();
	permission = true;


	getch();
	permission = true;


	while (1) {
		Sleep(5000);
		printf("main");
	}
	t.join();

	printf("kraj");

	return 0;
}