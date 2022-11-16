global GetRandomName:
global GetAnimalValue:
global GetParseAnimals:
global GetRandomAnimals:
global DisplayContainer:
global binarySearch:
global binaryInsertionSort:
global errMessage:
global main:

extern atoi
extern fclose
extern fopen
extern strcmp
extern clock
extern srand
extern time
extern puts
extern __stack_chk_fail
extern fputs
extern fprintf
extern printf
extern __isoc99_fscanf
extern strlen
extern rand
extern malloc
extern _GLOBAL_OFFSET_TABLE_

SECTION .text

GetRandomName:
        push    rbp
        mov     rbp, rsp
        sub     rsp, 16
        mov     edi, 11
        call    malloc WRT ..plt
        mov     qword [rbp-8H], rax
        call    rand WRT ..plt
        movsxd  rdx, eax
        imul    rdx, rdx, 1321528399
        shr     rdx, 32
        mov     ecx, edx
        sar     ecx, 3
        cdq
        sub     ecx, edx
        mov     edx, ecx
        imul    edx, edx, 26
        sub     eax, edx
        mov     edx, eax
        mov     eax, edx
        add     eax, 65
        mov     edx, eax
        mov     rax, qword [rbp-8H]
        mov     byte [rax], dl
        mov     dword [rbp-0CH], 1
        jmp     GetRandomName_2

GetRandomName_1:
        call    rand WRT ..plt
        movsxd  rdx, eax
        imul    rdx, rdx, 1321528399
        shr     rdx, 32
        mov     ecx, edx
        sar     ecx, 3
        cdq
        sub     ecx, edx
        mov     edx, ecx
        imul    edx, edx, 26
        sub     eax, edx
        mov     edx, eax
        mov     eax, edx
        lea     ecx, [rax+61H]
        mov     eax, dword [rbp-0CH]
        movsxd  rdx, eax
        mov     rax, qword [rbp-8H]
        add     rax, rdx
        mov     edx, ecx
        mov     byte [rax], dl
        mov     eax, dword [rbp-0CH]
        cdqe
        lea     rdx, [rax+1H]
        mov     rax, qword [rbp-8H]
        add     rax, rdx
        mov     byte [rax], 0
        add     dword [rbp-0CH], 1

GetRandomName_2:
        call    rand WRT ..plt
        mov     ecx, eax
        movsxd  rax, ecx
        imul    rax, rax, 1717986919
        shr     rax, 32
        mov     edx, eax
        sar     edx, 1
        mov     eax, ecx
        sar     eax, 31
        sub     edx, eax
        mov     eax, edx
        shl     eax, 2
        add     eax, edx
        sub     ecx, eax
        mov     edx, ecx
        lea     eax, [rdx+2H]
        cmp     dword [rbp-0CH], eax
        jle     GetRandomName_1
        mov     rax, qword [rbp-8H]
        leave
        ret

GetAnimalValue:
        push    rbp
        mov     rbp, rsp
        push    rbx
        sub     rsp, 40
        mov     qword [rbp-28H], rdi
        pxor    xmm0, xmm0
        movsd   qword [rbp-18H], xmm0
        mov     dword [rbp-1CH], 0
        jmp     GetAnimalValue_2

GetAnimalValue_1:
        mov     rax, qword [rbp-28H]
        mov     rdx, qword [rax]
        mov     eax, dword [rbp-1CH]
        cdqe
        add     rax, rdx
        movzx   eax, byte [rax]
        movsx   eax, al
        cvtsi2sd xmm0, eax
        movsd   xmm1, qword [rbp-18H]
        addsd   xmm0, xmm1
        movsd   qword [rbp-18H], xmm0
        add     dword [rbp-1CH], 1

GetAnimalValue_2:
        mov     eax, dword [rbp-1CH]
        movsxd  rbx, eax
        mov     rax, qword [rbp-28H]
        mov     rax, qword [rax]
        mov     rdi, rax
        call    strlen WRT ..plt
        cmp     rbx, rax
        jc      GetAnimalValue_1
        mov     rax, qword [rbp-28H]
        mov     eax, dword [rax+10H]
        test    eax, eax
        jz      GetAnimalValue_3
        mov     rax, qword [rbp-28H]
        mov     eax, dword [rax+10H]
        cvtsi2sd xmm1, eax
        movsd   xmm0, qword [rbp-18H]
        divsd   xmm0, xmm1
        jmp     GetAnimalValue_4

GetAnimalValue_3:
        movsd   xmm0, qword [rel Data_23]

GetAnimalValue_4:
        add     rsp, 40
        pop     rbx
        pop     rbp
        ret

GetParseAnimals:
        push    rbp
        mov     rbp, rsp
        sub     rsp, 48
        mov     qword [rbp-28H], rdi
        mov     qword [rbp-30H], rsi
        mov     edi, 32
        call    malloc WRT ..plt
        mov     qword [rbp-10H], rax
        mov     edi, 100
        call    malloc WRT ..plt
        mov     qword [rbp-8H], rax
        jmp     GetParseAnimals_2

GetParseAnimals_1:
        mov     rax, qword [rbp-10H]
        lea     rdi, [rax+0CH]
        mov     rax, qword [rbp-10H]
        lea     rcx, [rax+10H]
        mov     rax, qword [rbp-10H]
        lea     rsi, [rax+8H]
        mov     rdx, qword [rbp-8H]
        mov     rax, qword [rbp-30H]
        mov     r9, rdi
        mov     r8, rcx
        mov     rcx, rdx
        mov     rdx, rsi
        lea     rsi, [rel Data_1]
        mov     rdi, rax
        mov     eax, 0
        call    __isoc99_fscanf WRT ..plt
        mov     dword [rbp-14H], eax
        cmp     dword [rbp-14H], 4
        jnz     GetParseAnimals_3
        mov     rax, qword [rbp-10H]
        mov     rdx, qword [rbp-8H]
        mov     qword [rax], rdx
        mov     rax, qword [rbp-10H]
        mov     rdi, rax
        call    GetAnimalValue
        movq    rax, xmm0
        mov     rdx, qword [rbp-10H]
        mov     qword [rdx+18H], rax
        mov     rax, qword [rbp-28H]
        mov     edx, dword [rax]
        mov     rax, qword [rbp-28H]
        movsxd  rdx, edx
        mov     rcx, qword [rbp-10H]
        mov     qword [rax+rdx*8+8H], rcx
        mov     rax, qword [rbp-28H]
        mov     eax, dword [rax]
        lea     edx, [rax+1H]
        mov     rax, qword [rbp-28H]
        mov     dword [rax], edx
        mov     edi, 32
        call    malloc WRT ..plt
        mov     qword [rbp-10H], rax
        mov     edi, 100
        call    malloc WRT ..plt
        mov     qword [rbp-8H], rax

GetParseAnimals_2:
        mov     rax, qword [rbp-28H]
        mov     eax, dword [rax]
        cmp     eax, 9999
        jle     GetParseAnimals_1
        jmp     GetParseAnimals_4

GetParseAnimals_3:
        nop

GetParseAnimals_4:
        nop
        leave
        ret

GetRandomAnimals:
        push    rbp
        mov     rbp, rsp
        sub     rsp, 32
        mov     qword [rbp-18H], rdi
        mov     dword [rbp-1CH], esi
        jmp     GetRandomAnimals_2

GetRandomAnimals_1:
        mov     edi, 32
        call    malloc WRT ..plt
        mov     qword [rbp-8H], rax
        mov     eax, 0
        call    GetRandomName
        mov     rdx, qword [rbp-8H]
        mov     qword [rdx], rax
        call    rand WRT ..plt
        mov     ecx, eax
        movsxd  rax, ecx
        imul    rax, rax, 1431655766
        shr     rax, 32
        mov     rdx, rax
        mov     eax, ecx
        sar     eax, 31
        mov     esi, edx
        sub     esi, eax
        mov     eax, esi
        mov     edx, eax
        add     edx, edx
        add     edx, eax
        mov     eax, ecx
        sub     eax, edx
        mov     rdx, qword [rbp-8H]
        mov     dword [rdx+8H], eax
        call    rand WRT ..plt
        cdq
        shr     edx, 31
        add     eax, edx
        and     eax, 01H
        sub     eax, edx
        mov     edx, eax
        mov     rax, qword [rbp-8H]
        mov     dword [rax+0CH], edx
        call    rand WRT ..plt
        movsxd  rdx, eax
        imul    rdx, rdx, 1759218605
        shr     rdx, 32
        mov     ecx, edx
        sar     ecx, 12
        cdq
        sub     ecx, edx
        mov     edx, ecx
        imul    edx, edx, 10000
        sub     eax, edx
        mov     edx, eax
        add     edx, 1000
        mov     rax, qword [rbp-8H]
        mov     dword [rax+10H], edx
        mov     rax, qword [rbp-8H]
        mov     rdi, rax
        call    GetAnimalValue
        movq    rax, xmm0
        mov     rdx, qword [rbp-8H]
        mov     qword [rdx+18H], rax
        mov     rax, qword [rbp-18H]
        mov     edx, dword [rax]
        mov     rax, qword [rbp-18H]
        movsxd  rdx, edx
        mov     rcx, qword [rbp-8H]
        mov     qword [rax+rdx*8+8H], rcx
        mov     rax, qword [rbp-18H]
        mov     eax, dword [rax]
        lea     edx, [rax+1H]
        mov     rax, qword [rbp-18H]
        mov     dword [rax], edx

GetRandomAnimals_2:
        mov     rax, qword [rbp-18H]
        mov     eax, dword [rax]
        cmp     dword [rbp-1CH], eax
        jg      GetRandomAnimals_1
        nop
        nop
        leave
        ret

DisplayContainer:
        push    rbp
        mov     rbp, rsp
        sub     rsp, 96
        mov     qword [rbp-58H], rdi
        mov     qword [rbp-60H], rsi
        mov     rax, qword [fs:abs 28H]
        mov     qword [rbp-8H], rax
        xor     eax, eax
        lea     rax, [rel Data_2]
        mov     qword [rbp-40H], rax
        lea     rax, [rel Data_3]
        mov     qword [rbp-38H], rax
        lea     rax, [rel Data_4]
        mov     qword [rbp-30H], rax
        lea     rax, [rel Data_5]
        mov     qword [rbp-28H], rax
        lea     rax, [rel Data_6]
        mov     qword [rbp-20H], rax
        lea     rax, [rel Data_7]
        mov     qword [rbp-18H], rax
        mov     rax, qword [rbp-58H]
        mov     eax, dword [rax]
        mov     esi, eax
        lea     rdi, [rel Data_8]
        mov     eax, 0
        call    printf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rax]
        mov     rax, qword [rbp-60H]
        lea     rsi, [rel Data_8]
        mov     rdi, rax
        mov     eax, 0
        call    fprintf WRT ..plt
        mov     dword [rbp-44H], 0
        jmp     DisplayContainer_2

DisplayContainer_1:
        mov     eax, dword [rbp-44H]
        add     eax, 1
        mov     esi, eax
        lea     rdi, [rel Data_9]
        mov     eax, 0
        call    printf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     eax, dword [rax+8H]
        lea     ecx, [rax+rax]
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     eax, dword [rax+0CH]
        add     eax, ecx
        cdqe
        mov     rax, qword [rbp+rax*8-40H]
        mov     rsi, rax
        lea     rdi, [rel Data_10]
        mov     eax, 0
        call    printf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     rax, qword [rax]
        mov     rsi, rax
        lea     rdi, [rel Data_11]
        mov     eax, 0
        call    printf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     eax, dword [rax+10H]
        mov     esi, eax
        lea     rdi, [rel Data_12]
        mov     eax, 0
        call    printf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     rax, qword [rax+18H]
        movq    xmm0, rax
        lea     rdi, [rel Data_13]
        mov     eax, 1
        call    printf WRT ..plt
        mov     eax, dword [rbp-44H]
        lea     edx, [rax+1H]
        mov     rax, qword [rbp-60H]
        lea     rsi, [rel Data_9]
        mov     rdi, rax
        mov     eax, 0
        call    fprintf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     eax, dword [rax+8H]
        lea     ecx, [rax+rax]
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     eax, dword [rax+0CH]
        add     eax, ecx
        cdqe
        mov     rax, qword [rbp+rax*8-40H]
        mov     rdx, qword [rbp-60H]
        mov     rsi, rdx
        mov     rdi, rax
        call    fputs WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     rdx, qword [rax]
        mov     rax, qword [rbp-60H]
        lea     rsi, [rel Data_11]
        mov     rdi, rax
        mov     eax, 0
        call    fprintf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     edx, dword [rax+10H]
        mov     rax, qword [rbp-60H]
        lea     rsi, [rel Data_12]
        mov     rdi, rax
        mov     eax, 0
        call    fprintf WRT ..plt
        mov     rax, qword [rbp-58H]
        mov     edx, dword [rbp-44H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     rdx, qword [rax+18H]
        mov     rax, qword [rbp-60H]
        movq    xmm0, rdx
        lea     rsi, [rel Data_13]
        mov     rdi, rax
        mov     eax, 1
        call    fprintf WRT ..plt
        add     dword [rbp-44H], 1

DisplayContainer_2:
        mov     rax, qword [rbp-58H]
        mov     eax, dword [rax]
        cmp     dword [rbp-44H], eax
        jl      DisplayContainer_1
        nop
        mov     rax, qword [rbp-8H]
        xor     rax, qword [fs:abs 28H]
        jz      DisplayContainer_3
        call    __stack_chk_fail WRT ..plt

DisplayContainer_3:
        leave
        ret

binarySearch:
        push    rbp
        mov     rbp, rsp
        sub     rsp, 48
        mov     qword [rbp-18H], rdi
        movsd   qword [rbp-20H], xmm0
        mov     dword [rbp-24H], esi
        mov     dword [rbp-28H], edx
        mov     eax, dword [rbp-28H]
        cmp     eax, dword [rbp-24H]
        jg      binarySearch_2
        mov     rax, qword [rbp-18H]
        mov     edx, dword [rbp-24H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        movsd   xmm1, qword [rax+18H]
        movsd   xmm0, qword [rbp-20H]
        comisd  xmm0, xmm1
        jbe     binarySearch_1
        mov     eax, dword [rbp-24H]
        add     eax, 1
        jmp     binarySearch_5

binarySearch_1:
        mov     eax, dword [rbp-24H]
        jmp     binarySearch_5

binarySearch_2:
        mov     edx, dword [rbp-24H]
        mov     eax, dword [rbp-28H]
        add     eax, edx
        mov     edx, eax
        shr     edx, 31
        add     eax, edx
        sar     eax, 1
        mov     dword [rbp-4H], eax
        mov     rax, qword [rbp-18H]
        mov     edx, dword [rbp-4H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        movsd   xmm0, qword [rax+18H]
        ucomisd xmm0, [rbp-20H]
        jpe     binarySearch_3
        ucomisd xmm0, [rbp-20H]
        jnz     binarySearch_3
        mov     eax, dword [rbp-4H]
        add     eax, 1
        jmp     binarySearch_5

binarySearch_3:
        mov     rax, qword [rbp-18H]
        mov     edx, dword [rbp-4H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        movsd   xmm1, qword [rax+18H]
        movsd   xmm0, qword [rbp-20H]
        comisd  xmm0, xmm1
        jbe     binarySearch_4
        mov     eax, dword [rbp-4H]
        lea     esi, [rax+1H]
        mov     edx, dword [rbp-28H]
        mov     rcx, qword [rbp-20H]
        mov     rax, qword [rbp-18H]
        movq    xmm0, rcx
        mov     rdi, rax
        call    binarySearch
        jmp     binarySearch_5

binarySearch_4:
        mov     eax, dword [rbp-4H]
        lea     edx, [rax-1H]
        mov     esi, dword [rbp-24H]
        mov     rcx, qword [rbp-20H]
        mov     rax, qword [rbp-18H]
        movq    xmm0, rcx
        mov     rdi, rax
        call    binarySearch

binarySearch_5:
        leave
        ret

binaryInsertionSort:
        push    rbp
        mov     rbp, rsp
        sub     rsp, 48
        mov     qword [rbp-28H], rdi
        mov     rax, qword [rbp-28H]
        mov     eax, dword [rax]
        mov     dword [rbp-10H], eax
        mov     dword [rbp-18H], 1
        jmp     binaryInsertionSort_4

binaryInsertionSort_1:
        mov     eax, dword [rbp-18H]
        sub     eax, 1
        mov     dword [rbp-14H], eax
        mov     rax, qword [rbp-28H]
        mov     edx, dword [rbp-18H]
        movsxd  rdx, edx
        mov     rax, qword [rax+rdx*8+8H]
        mov     qword [rbp-8H], rax
        mov     rax, qword [rbp-8H]
        mov     rcx, qword [rax+18H]
        mov     edx, dword [rbp-14H]
        mov     rax, qword [rbp-28H]
        mov     esi, 0
        movq    xmm0, rcx
        mov     rdi, rax
        call    binarySearch
        mov     dword [rbp-0CH], eax
        jmp     binaryInsertionSort_3

binaryInsertionSort_2:
        mov     eax, dword [rbp-14H]
        lea     esi, [rax+1H]
        mov     rax, qword [rbp-28H]
        mov     edx, dword [rbp-14H]
        movsxd  rdx, edx
        mov     rcx, qword [rax+rdx*8+8H]
        mov     rax, qword [rbp-28H]
        movsxd  rdx, esi
        mov     qword [rax+rdx*8+8H], rcx
        sub     dword [rbp-14H], 1

binaryInsertionSort_3:
        mov     eax, dword [rbp-14H]
        cmp     eax, dword [rbp-0CH]
        jge     binaryInsertionSort_2
        mov     eax, dword [rbp-14H]
        lea     edx, [rax+1H]
        mov     rax, qword [rbp-28H]
        movsxd  rdx, edx
        mov     rcx, qword [rbp-8H]
        mov     qword [rax+rdx*8+8H], rcx
        add     dword [rbp-18H], 1

binaryInsertionSort_4:
        mov     eax, dword [rbp-18H]
        cmp     eax, dword [rbp-10H]
        jl      binaryInsertionSort_1
        nop
        nop
        leave
        ret

errMessage:
        push    rbp
        mov     rbp, rsp
        lea     rdi, [rel Data_14]
        call    puts WRT ..plt
        lea     rdi, [rel Data_15]
        call    puts WRT ..plt
        lea     rdi, [rel Data_16]
        call    puts WRT ..plt
        nop
        pop     rbp
        ret

main:
        push    rbp
        mov     rbp, rsp
        sub     rsp, 64
        mov     dword [rbp-34H], edi
        mov     qword [rbp-40H], rsi
        mov     edi, 0
        call    time WRT ..plt
        mov     edi, eax
        call    srand WRT ..plt
        call    clock WRT ..plt
        cvtsi2sd xmm0, rax
        movsd   qword [rbp-20H], xmm0
        cmp     dword [rbp-34H], 4
        jz      main_1
        mov     eax, 0
        call    errMessage
        mov     eax, 1
        jmp     main_7

main_1:
        mov     edi, 80008
        call    malloc WRT ..plt
        mov     qword [rbp-18H], rax
        mov     rax, qword [rbp-18H]
        mov     dword [rax], 0
        mov     rax, qword [rbp-40H]
        add     rax, 8
        mov     rax, qword [rax]
        lea     rsi, [rel Data_17]
        mov     rdi, rax
        call    strcmp WRT ..plt
        test    eax, eax
        jnz     main_2
        mov     rax, qword [rbp-40H]
        add     rax, 16
        mov     rax, qword [rax]
        lea     rsi, [rel Data_18]
        mov     rdi, rax
        call    fopen WRT ..plt
        mov     qword [rbp-10H], rax
        mov     rdx, qword [rbp-10H]
        mov     rax, qword [rbp-18H]
        mov     rsi, rdx
        mov     rdi, rax
        call    GetParseAnimals
        mov     rax, qword [rbp-10H]
        mov     rdi, rax
        call    fclose WRT ..plt
        jmp     main_6

main_2:
        mov     rax, qword [rbp-40H]
        add     rax, 8
        mov     rax, qword [rax]
        lea     rsi, [rel Data_19]
        mov     rdi, rax
        call    strcmp WRT ..plt
        test    eax, eax
        jnz     main_5
        mov     rax, qword [rbp-40H]
        add     rax, 16
        mov     rax, qword [rax]
        mov     rdi, rax
        call    atoi WRT ..plt
        mov     dword [rbp-24H], eax
        cmp     dword [rbp-24H], 0
        jle     main_3
        cmp     dword [rbp-24H], 10000
        jle     main_4

main_3:
        mov     eax, dword [rbp-24H]
        mov     esi, eax
        lea     rdi, [rel Data_20]
        mov     eax, 0
        call    printf WRT ..plt
        mov     eax, 2
        jmp     main_7

main_4:
        mov     edx, dword [rbp-24H]
        mov     rax, qword [rbp-18H]
        mov     esi, edx
        mov     rdi, rax
        call    GetRandomAnimals
        jmp     main_6

main_5:
        mov     eax, 0
        call    errMessage
        mov     eax, 3
        jmp     main_7

main_6:
        mov     rax, qword [rbp-18H]
        mov     rdi, rax
        call    binaryInsertionSort
        mov     rax, qword [rbp-40H]
        add     rax, 24
        mov     rax, qword [rax]
        lea     rsi, [rel Data_21]
        mov     rdi, rax
        call    fopen WRT ..plt
        mov     qword [rbp-8H], rax
        mov     rdx, qword [rbp-8H]
        mov     rax, qword [rbp-18H]
        mov     rsi, rdx
        mov     rdi, rax
        call    DisplayContainer
        call    clock WRT ..plt
        cvtsi2sd xmm0, rax
        subsd   xmm0, [rbp-20H]
        movsd   xmm1, qword [rel Data_24]
        divsd   xmm0, xmm1
        lea     rdi, [rel Data_22]
        mov     eax, 1
        call    printf WRT ..plt
        call    clock WRT ..plt
        cvtsi2sd xmm0, rax
        subsd   xmm0, [rbp-20H]
        movsd   xmm1, qword [rel Data_24]
        divsd   xmm0, xmm1
        mov     rax, qword [rbp-8H]
        lea     rsi, [rel Data_22]
        mov     rdi, rax
        mov     eax, 1
        call    fprintf WRT ..plt
        mov     eax, 0

main_7:
        leave
        ret

SECTION .data

SECTION .bss

SECTION .rodata

Data_1:
        db "%d %s %d %d", 0

Data_2:
        db "Lake fish", 0

Data_3:
        db "Sea Fish", 0

Data_4:
        db "Predator beast", 0

Data_5:
        db "Herbivores beast", 0

Data_6:
        db "Fly away bird", 0

Data_7:
        db "Stay home bird", 0

Data_8:
        db "Container contains %d elements.", 10, 0

Data_9:
        db "%d) ", 0

Data_10:
        db "%s", 0

Data_11:
        db ": name = %s", 0

Data_12:
        db ", weight = %d", 0

Data_13:
        db ", value = %f", 10, 0

Data_14:
        db "Incorrect command line! Waited:", 0

Data_15:
        db "	command -f infile outfile", 0

Data_16:
        db "	command -n number outfile", 0

Data_17:
        db "-f", 0

Data_18:
        db "rt", 0

Data_19:
        db "-n", 0

Data_20:
        db "Incorrect number of figures = %d. Set 1 <= number <= 10000", 0

Data_21:
        db "w", 0

Data_22:
        db "Total time = %fsec.", 10, 0

Data_23:
        dq 0BFF0000000000000H

Data_24:
        dq 412E848000000000H

SECTION .note.gnu.property
