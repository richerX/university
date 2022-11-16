#include <cstdlib>
#include <iostream>
#include <ctime>
#include <mutex>
#include <random>
#include <pthread.h>
#include <unistd.h>

const size_t artworks = 9;              // 9
const size_t visitors = 50;             // 50
const size_t maximum_viewers = 10;      // 10
const size_t sleep_seconds = 1;         // 1
const size_t probability_to_leave = 20; // 20
const size_t dont_leave_for_cycles = 2; // 2
const size_t threads = visitors;

int array[artworks][maximum_viewers];
bool visitors_left[visitors + 1];
std::mutex array_mutex;
pthread_barrier_t barrier1, barrier2;
size_t current_cycle = 0, visitors_left_count = 0;
bool stop_print = false;

std::mt19937 generator(time(nullptr));
std::uniform_int_distribution<int> distribution(0, 1'000'000);

void printGallery() {
	if (stop_print) {
		return;
	}
    current_cycle++;
    std::string string = "\n       Current cycle = " + std::to_string(current_cycle) + "\n";
    for (size_t i = 0; i < artworks; i++) {
        string += "[Artwork = " + std::to_string(i + 1) + "]";
        for (size_t j = 0; j < maximum_viewers; j++) {
            if (array[i][j] != 0) {
                string += " " + std::to_string(array[i][j]);
            }
        }
        string += "\n";
    }
    string += "\n";
    std::cout << string;
    if (visitors_left_count == visitors) {
    	stop_print = true;
    }
}

size_t getEmptyIndex(int target) {
    for (size_t j = 0; j < maximum_viewers; j++) {
        if (array[target][j] == 0) {
            return j;
        }
    }
    return -1;
}

void stepBack(int id) {
    for (size_t i = 0; i < artworks; i++) {
        for (size_t j = 0; j < maximum_viewers; j++) {
            if (array[i][j] == id) {
                array[i][j] = 0;
                break;
            }
        }
    }
}

void moveVisitor(int id, int target) {
    size_t emptyIndex = getEmptyIndex(target);
    if (emptyIndex != -1) {
        stepBack(id);
        array[target][emptyIndex] = id;
    }
}

void actionVisitor(int id) {
    if (!visitors_left[id]) {
        if (current_cycle + 1 > dont_leave_for_cycles && distribution(generator) % 100 < probability_to_leave) {
            visitors_left_count++;
            visitors_left[id] = true;
            stepBack(id);
            std::cout << "Visitor " << id << " left! (total lefts = " << visitors_left_count << ")\n";
        } else {
            moveVisitor(id, distribution(generator) % artworks);
        }
    }
}

void *visitor(void *args) {
    int id = *(int *)args;
    while (visitors_left_count < visitors) {
    	pthread_barrier_wait(&barrier1);
    	array_mutex.lock();
    	actionVisitor(id);
        array_mutex.unlock();
        pthread_barrier_wait(&barrier2);
		if (id == 1) {
		    printGallery();
		}
		sleep(sleep_seconds);
    }
    return nullptr;
}

void startThreads() {
    pthread_t thread[visitors];
    for (int i = 0; i < visitors; i++) {
        int *number = new int(i + 1);
        pthread_create(&thread[i], nullptr, visitor, (void*) number);
    }
    for (int i = 0; i < visitors; i++) {
        pthread_join(thread[i], nullptr);
    }
}

int main () {
    pthread_barrier_init(&barrier1, nullptr, threads);
    pthread_barrier_init(&barrier2, nullptr, threads);
    startThreads();
    return 0;
}
