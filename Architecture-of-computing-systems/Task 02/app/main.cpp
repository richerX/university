#include "container.h"


void errMessage() {
    cout << "Incorrect command line! Waited:\n";
    cout << "\tcommand -f infile outfile01 outfile02\n";
    cout << "\tcommand -n number outfile01 outfile02\n";
}

int binarySearch(Container& storage, double item, int left, int right) {
    if (right <= left)
        return (item > (*storage.storage[left]).Value() ? (left + 1) : left);

    int mid = (left + right) / 2;
    if (item == (*storage.storage[mid]).Value())
        return mid + 1;
    if (item > (*storage.storage[mid]).Value())
        return binarySearch(storage, item, mid + 1, right);
    return binarySearch(storage, item, left, mid - 1);
}

void binaryInsertionSort(Container& storage)
{
    int n = storage.length;
    int i, newLocation, j, k;
    Animal* selected;

    for (i = 1; i < n; ++i)
    {
        j = i - 1;
        selected = storage.storage[i];
        newLocation = binarySearch(storage, selected->Value(), 0, j);
        while (j >= newLocation)
        {
            storage.storage[j + 1] = storage.storage[j];
            j--;
        }
        storage.storage[j + 1] = selected;
    }
}

// Основная функция программы
int main(int argc, char* argv[]) {
    auto start = clock();
    if (argc != 5) {
        errMessage();
        return 1;
    }

    cout << "Start" << endl;
    Container* storage = new Container();

    if (!strcmp(argv[1], "-f")) {
        ifstream ifst(argv[2]);
        storage->In(ifst);
    } else if (!strcmp(argv[1], "-n")) {
        int size = atoi(argv[2]);
        if ((size < 1) || (size > 10000)) {
            cout << "incorrect numer of figures = " << size << ". Set 0 < number <= 10000\n";
            return 3;
        }
        storage->InRnd(size);
    } else {
        errMessage();
        return 2;
    }

    // Сортировка
    binaryInsertionSort(*storage);

    // Вывод содержимого контейнера в файл
    ofstream ofst1(argv[3]);
    ofst1 << "Filled container:\n";
    storage->Out(ofst1);

    // The 2nd part of task
    ofstream ofst2(argv[4]);
    cout << "Value sum = " << storage->Value() << "\n";
    ofst2 << "Value sum = " << storage->Value() << "\n";

    delete storage;
    cout << "Stop" << endl;
    auto stop = clock();
    cout << "Total time = " << double(stop - start) / CLOCKS_PER_SEC << "sec.\n";
    return 0;
}
