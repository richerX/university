#include "container.h"

void errMessage() {
    printf("Incorrect command line! Waited:\n");
    printf("\tcommand -f infile outfile01 outfile02\n");
    printf("\tcommand -n number outfile01 outfile02\n");
}

int binarySearch(container& storage, double item, int left, int right) {
    if (right <= left)
        return (item > Value(*storage.cont[left])) ? (left + 1) : left;

    int mid = (left + right) / 2;
    if (item == Value(*storage.cont[mid]))
        return mid + 1;
    if (item > Value(*storage.cont[mid]))
        return binarySearch(storage, item, mid + 1, right);
    return binarySearch(storage, item, left, mid - 1);
}

void binaryInsertionSort(container& storage)
{
    int n = storage.len;
    int i, newLocation, j, k;
    animal* selected;

    for (i = 1; i < n; ++i)
    {
        j = i - 1;
        selected = storage.cont[i];
        newLocation = binarySearch(storage, Value(*selected), 0, j);
        while (j >= newLocation)
        {
            storage.cont[j + 1] = storage.cont[j];
            j--;
        }
        storage.cont[j + 1] = selected;
    }
}

// Основная функция программы
int main(int argc, char* argv[]) {
    auto start = clock();
    if(argc != 5) {
        errMessage();
        return 1;
    }

    printf("Start\n");
    container storage;
    Init(storage);

    if (!strcmp(argv[1], "-f")) {
        auto file = fopen(argv[2], "r");
        In(storage, file);
        fclose(file);
    } else if(!strcmp(argv[1], "-n")) {
        auto size = atoi(argv[2]);
        if((size < 1) || (size > 10000)) {
            printf("Incorrect number of figures = %d. Set 1 <= number <= 10000", size);
            return 2;
        }
        srand(static_cast<unsigned int>(time(0)));
        InRnd(storage, size);
    } else {
        errMessage();
        return 3;
    }
    
    // Сортировка
    binaryInsertionSort(storage);

    // Вывод содержимого контейнера в файл
    auto file1 = fopen(argv[3], "w");
    fprintf(file1, "Filled container:\n");
    Out(storage, file1);
    fclose(file1);

    // Вторая часть задания
    auto file2 = fopen(argv[4], "w");
    fprintf(file2, "Total value = %f\n", TotalValue(storage));
    fclose(file2);

    // Завершение
    Clear(storage);
    printf("Stop\n");
    
    auto stop = clock();
    printf("Total time = %fsec.\n", double(stop - start)/CLOCKS_PER_SEC);
    //printf("%d\n", start);
    //printf("%d\n", stop);
    return 0;
}
