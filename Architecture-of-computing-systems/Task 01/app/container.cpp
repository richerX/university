#include "container.h"

const int length = 30;
const int words = 10;

// Инициализация контейнера
void Init(container& storage) {
    storage.len = 0;
}

// Очистка контейнера от элементов (освобождение памяти)
void Clear(container& storage) {
    for (int i = 0; i < storage.len; i++) {
        delete storage.cont[i];
    }
    storage.len = 0;
}

char** InStringParse(const char* string) {
    char** array = new char* [words];
    for (int i = 0; i < words; i++)
        array[i] = new char[length];

	char word[length];
	for (int i = 0; i < length; i++) {
		word[i] = NULL;
		for (int j = 0; j < words; j++) {
			array[j][i] = NULL;
		}
	}

	int letters_cur = 0, words_cur = 0;
	for (int i = 0; i < strlen(string); i++)
	{
		if (string[i] != ' ')
			word[letters_cur] = string[i];
		if (string[i] == ' ' || i == strlen(string) - 1) {
			for (int k = 0; k < length; k++) {
				array[words_cur][k] = word[k];
				word[k] = NULL;
			}
			letters_cur = -1;
			words_cur++;
		}
		letters_cur++;
	}

	return array;
}

// Ввод содержимого контейнера из указанного потока
void In(container& storage, FILE* file) {
    char result_string[100];
    if (file != NULL)
    {
        while (fgets(result_string, sizeof(result_string), file))
        {
            if ((storage.cont[storage.len] = In(InStringParse(result_string))) != 0) {
                storage.len++;
            }
        }
    }
}

// Случайный ввод содержимого контейнера
void InRnd(container& storage, int size) {
    while (storage.len < size) {
        if ((storage.cont[storage.len] = InRnd()) != nullptr) {
            storage.len++;
        }
    }
}

// Вывод содержимого контейнера в указанный поток
void Out(container& storage, FILE* file) {
    fprintf(file, "Container contains %d elements.\n", storage.len);
    for (int i = 0; i < storage.len; i++) {
        fprintf(file, "%d: ", i + 1);
        Out(*(storage.cont[i]), file);
    }
}

// Вычисление суммы
double TotalValue(container& storage) {
    double sum = 0;
    for (int i = 0; i < storage.len; i++) {
        sum += Value(*(storage.cont[i]));
    }
    return sum;
}
