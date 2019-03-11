#include <iostream>
#include <thread>
#include <mutex>

using namespace std;

mutex mtx;

void threadFunction() {
	mtx.lock();
	for (int i = 0; i < 10; i++) {
		printf("Broj je %d\n", i);
	}
	mtx.unlock();
}

int main() {

	thread t(threadFunction);


	for (int i = 0; i < 10; i++) {
		printf("gitproba\n");
	}

	t.join();

	return 0;
}