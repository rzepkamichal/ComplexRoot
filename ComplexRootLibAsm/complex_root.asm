
.data
	pi dq 400921FB54442D18h		;most accurate PI approximation in double precision
	one dq 3FF0000000000000h	;double representation of 1.0
	two dq 4000000000000000h	;double representation of 2.0

;========================================================================================================
;registers are used as follows

;r8 - input root value (n) (passed as third arument - int)
;r9 - input array ptr (passed as fourth argument - double[] ptr)
;rbx - array offset
;rcx - loop counter
;xmm0 - input moudlus value (passed as first argument - double, further used for calculations)
;xmm2 - modulus value store
;xmm1 - input arc value (passed as second argument - double, further used for calculations)
;xmm3 - arc value store

;calculating trygonometric arguments for n-th complex root calculation
;r10 - k (see: n-th complex root formula in documentation)
;r11 - k + 1 (see: n-th complex root formula in documentation)

;calculating n-th real root (see: n-th root algorithm https://en.wikipedia.org/wiki/Nth_root_algorithm)
;r12 - initial root value (the value, for which the root is calculated is chosen as initial root value)
;r13 - previous root value (in previous iteration)
;r14 - current root value (in current iteration)
;r15 - current delta value (difference between root value in previous and current iteration)
;========================================================================================================

.code
calculateRootsAsm PROC

	;preserve nonvolatile register values on stack
	;according to guidelines: https://docs.microsoft.com/en-us/cpp/build/x64-software-conventions?view=vs-2019
	push rsp
	push rbp
	push rbx
	push rsi
	push rdi
	push r12
	push r13
	push r14
	push r15

	finit					;initialize fpu
	movq xmm2, xmm0			;safe modulus input argument to xmm2
	movq xmm3, xmm1			;safe arc input argument to xmm3

	mov r10, 0				;initialize k=0
	mov r11, [one]			;initialize k+1 = 1
	mov rcx, 0				;reset loop counter
	mov rbx, 0				;reset array ptr
	mov rax, 0h				;reset rax
	movq xmm0, rax			;reset xmm0 (use for calculation)
	movq xmm1, rax			;reset xmm1 (use for calculation)

	sub rsp, 8							;alloc double-size on stack
	mov qword ptr [rsp], r10			;push k value on stack
	movlpd xmm0, qword ptr [rsp]		;move k value from stack to lower part of xmm0
	sub rsp, 8							;allock double-size on stack
	mov qword ptr [rsp], r11			;push k+1 value on stack
	movhpd xmm0, qword ptr [rsp]		;move k+1 value from stack to higher part of xmm0
	add rsp, 8							;free double-size from stack
	add rsp, 8							;free double-size from stack

	;prepare the trygonometric argument for each result calculation (see documentation)
	;using simd instructions, in each loop iteration two tryg arguments are calculated and saved to 4 next input array elements
	;input array size - 2n, loop repetitions - 2n / 4 = n/2
	tryg_arg_loop:							
		cmp rcx, r8							;repeat for each of n/2 times
		jge end_tryg_arg_lopp				;end loop if repeated n/2 times

		movlpd xmm1, [two]					;load 2.0 to lower part of xmm1
		movhpd xmm1, [two]					;load 2.0 to higher part of xmm1
		mulpd xmm0, xmm1					;multiply vector [k, k+1] by [2, 2]
			
		movlpd xmm1, [pi]					;load pi to lower part of xmm1
		movhpd xmm1, [pi]					;load pi to higher part of xmm1
		mulpd xmm0, xmm1					;multiply vector [2k, 2(k+1)] by [pi, pi]

		sub rsp, 8							;alloc double-size on stack
		movq qword ptr [rsp], xmm3			;push input arc value on stack
		movlpd xmm1, qword ptr [rsp]		;load arc value to lower part of xmm1
		movhpd xmm1, qword ptr [rsp]		;load arc value to higher part of xmm1
		addpd xmm0, xmm1					;add vectors [2k*pi, 2(k+1)*pi] and [arc, arc]
		add rsp, 8							;free double-size from stack

		sub rsp, 8							;alloc double-size on stack
		mov qword ptr [rsp], r8				;push input n value to stack
		cvtsi2sd xmm1, qword ptr [rsp]		;convert input n value to double and load to xmm1
		movq qword ptr [rsp], xmm1			;put double n value on stack
		movlpd xmm1, qword ptr [rsp]		;load double n value to lower part of xmm1
		movhpd xmm1, qword ptr [rsp]		;load double n value to higher part of xmm1
		divpd xmm0, xmm1					;divide vector [2k*pi + arc, 2(k+1)*pi + arc] by [n,n]
		add rsp, 8							;free double-size from stack

		sub rsp, 8							;alloc double-size on stack
		movlpd qword ptr [rsp], xmm0		;push calculated tryg arg for first number on stack
		mov rax, qword ptr [rsp]			;move value from stack to rax
		mov [r9 + rbx * 8], rax				;save value in array
		mov [r9 + 8 + rbx * 8], rax			;save value in next array element
		add rsp, 8							;free double-size from stack

		;check, whether all results have been calculated
		;could happen, if (2 * n) % 4 != 0
		;if so, break loop
		mov rax, 0h				;reset rax
		mov rax, r8				;load n to rax
		add rax, r8				;add n to rax
		sub rax, 2h				;rax = last address in array
		add rbx, 2h				;increse array ptr by 2
		cmp rbx, rax			;check if array offset is out of array range
		jg end_tryg_arg_lopp	;break loop if array offset is out of range
		sub rbx, 2h				;restore array offset

		sub rsp, 8							;alloc double-size on stack
		movhpd qword ptr [rsp], xmm0		;push calculated tryg arg for second number on stack	
		mov rax, qword ptr [rsp]			;move value from stack to rax
		mov [r9 + 16 + rbx * 8], rax		;save value in next array element
		mov [r9 + 24 + rbx * 8], rax		;save value in next array element 
		add rsp, 8							;free double-size from stack

		sub rsp, 8							;alloc double-size on stack
		mov qword ptr [rsp], r10			;load k value on stack
		movlpd xmm0, qword ptr [rsp]		;load k value to lower part of xmm0
		sub rsp, 8							;alloc double-size on stack
		mov qword ptr [rsp], r11			;load k+1 value on stack
		movhpd xmm0, qword ptr [rsp]		;load k+1 value to higher part of xmm0
		movlpd xmm1, [two]					;load 2.0 to lower part of xmm1
		movhpd xmm1, [two]					;load 2.0 to higher part of xmm1
		addpd xmm0, xmm1					;add vectors [k, k+1] and [2,2]
		movhpd qword ptr [rsp], xmm0		;move result from higher part of xmm0 to stack
		mov r11, qword ptr [rsp]			;move result from stack to r11 (stores k+1 value)
		add rsp, 8							;free double-size from stack
		movlpd qword ptr [rsp], xmm0		;move result from lower part of xmm0 to stack
		mov r10, qword ptr [rsp]			;move result from stack to r10 (stores k value)
		add rsp, 8							;free double-size from stack
	
		add rbx, 4h							;move array offset by 4 next elements
		add rcx, 2h							;increase loop counter by 2
		jmp tryg_arg_loop					;move to beginning of loop
	
end_tryg_arg_lopp:

	finit					;reset fpu stack
	movq r12, xmm2			;set modulus as initial root value
	mov r13, r12			;set modulus as previous root value
	mov r14, r12			;set modulus as current root value
	mov r15, r12			;set modulus as current delta value

	sub rsp, 8					;alloc double-size on stack
	mov qword ptr [rsp], r8		;move n value to stack
	fild qword ptr[rsp]			;fpu stack: n
	add rsp, 8					;free double-size from stack
	fld st						;fpu stack: n, n
	fld1						;fpu stack: 1.0, n, n
	fxch st(1)					;fpu stack: n, 1.0, n

	;calculate the n-th root of modulus according to Newton-Raphson algorithm
	nth_root_loop:
		fsub st, st(1)					;fpu stack: n-1, 1.0, n
		sub rsp, 8						;alloc double-size on stack
		mov qword ptr [rsp], r14		;load current root value on stack
		fld qword ptr [rsp]				;fpu stack: current_root, n-1, 1.0, n
		fxch st(1)						;fpu stack: n-1, current_root, 1.0, n
		fld qword ptr [rsp]				;fpu stack: current_root, n-1, current_root, 1.0, n
		add rsp, 8						;free double-size from stack
		
		;calculate the (n-1)th power of current_root
		;use formula: current_root ^ ((n)-1) = 2 ^ ((n-1) * log_2(current_root))
		fyl2x						;st(0) = st(1) * log_2(st(0))					
		fld1						;load 1 to top
		fld st(1)					;copy logarithm result to top
		fprem						;st(0) = st(0) - (st(0) div st(1)) * st(1)
		f2xm1						;st(0) = 2 ^ (st(0)) - 1
		fadd						;st(0) += 1
		fscale						;scale st(0) by st(1)
		fxch st(1)					;move temp result top
		fstp st						;pop temp result, fpu stack: current_root ^ (n-1), current_root, 1.0, n
		
		sub rsp, 8					;allock double-size on stack
		mov qword ptr [rsp], r12	;move initial_root to stack
		fld qword ptr [rsp]			;fpu stack: initial_root, current_root ^ (n-1), current_root, 1.0, n
		add rsp, 8					;free double-size from stack
		fdiv st, st(1)				;fpu stack: initial_root / (current_root ^ (n-1)), current_root ^ (n-1), current_root, 1.0, n
		fsub st, st(2)				;fpu stack: initial_root / (current_root ^ (n-1)) - current_root , current_root ^ (n-1), current_root, 1.0, n
		fdiv st, st(4)				;fpu stack: (initial_root / (current_root ^ (n-1)) - current_root) / n = delta, current_root ^ (n-1), current_root, 1.0, n
		fxch st(2)					;fpu stack: current_root, current_root ^ (n-1), delta, 1.0, n
		fadd st, st(2)				;fpu stack: current_root + delta, current_root ^ (n-1), delta, 1.0, n
		fxch st(2)					;fpu stack: delta, current_root ^ (n-1), current_root + delta, 1.0, n
		
		fabs						;fpu stack: |delta|, current_root ^ (n-1), current_root + delta, 1.0, n
		sub rsp, 8					;allock double-size on stack
		fstp qword ptr [rsp]		;save delta on stack, fpu stack: current_root ^ (n-1), current_root + delta, 1.0, n
		mov r15, qword ptr [rsp]	;store delta in r15
		add rsp, 8					;free double-size on stack
		fstp st						;fpu stack: current_root + delta, 1.0, n
		sub rsp, 8					;alloc double-size on stack
		fstp qword ptr [rsp]		;save new root on stack, fpu stack: 1.0, n
		mov r13, qword ptr [rsp]	;store new root to r13
		add rsp, 8					;free double-size from stack
		fld st(1)					;fpu stack: n, 1.0, n
	
	mov rax, r15					;move delta value to rax
	cmp rax, 0000000000000000h		;check, if delta > 0 (max precision is reached)
	jng end_nth_root_loop			;jump out of loop, when delta <= 0
	mov rax, r13					;move new root value to rax
	sub rax, r14					;compare new and previous root value
	jz end_nth_root_loop			;end loop if old and new root value are the same
	mov rax, r13					;save new root value in rax
	mov r14, rax					;store new root value as previous value in r14
	jmp nth_root_loop				;jump to next iteration

end_nth_root_loop:

		;reset fpu stack
		fstp st								;fpu stack: 1.0, n
		fstp st								;fpu stack: n
		fstp st								;fpu stack: -
		mov rbx, 0							;reset array offset
		mov rcx, 0							;reset loop counter
		sub rsp, 8							;alloc double-size on stack
		mov qword ptr [rsp], r13			;move modulus root value to stack
		fld qword ptr [rsp]					;fpu stack: modulus^(1/n)
		add rsp, 8							;free double-size from stack

;calculate each of n results
;for each result the real and imaginary part is calculated, which are placed adjacent in results array
calc_results_loop:
		cmp rcx, r8							;compare with n (repeat n times)
		jge end_calc_results_loop			;end loop if repeated n times
		fld qword ptr [r9 + rbx * 8]		;fpu stack: results[rbx], modulus^(1/n)
		fcos								;fpu stack: cos(results[rbx]), modulus^(1/n)
		fmul st, st(1)						;fpu stack: cos(results[rbx]) * modulus^(1/n), modulus^(1/n)
		fstp qword ptr [r9 + rbx * 8]		;save real result in array, fpu stack: modulus^(1/n)
		inc rbx								;move array offset to immaginary part
		fld qword ptr [r9 + rbx * 8]		;fpu stack: results[rbx], modulus^(1/n)
		fsin								;fpu stack: sin(results[rbx]) , modulus^(1/n)
		fmul st, st(1)						;fpu stack: sin(results[rbx]) * modulus^(1/n) , modulus^(1/n)
		fstp qword ptr [r9 + rbx * 8]		;save imaginary result in array

		inc rbx								;move array offset to next number
		inc rcx								;increment loop counter
		jmp calc_results_loop				;jump to next iteration
end_calc_results_loop:

	;restore nonvolatile register values from stack
	;according to guidelines: https://docs.microsoft.com/en-us/cpp/build/x64-software-conventions?view=vs-2019
	pop r12
	pop r13
	pop r14
	pop r15
	pop rdi
	pop rsi
	pop rbx
	pop rbp
	pop rsp

	ret

calculateRootsAsm ENDP

end