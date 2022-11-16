#!/bin/bash
nasm -f elf64 task.asm
gcc task.o -o task
