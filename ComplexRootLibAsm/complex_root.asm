
.data
	pi dq 400921FB54442D18h		;most accurate PI value in double precision
	one dq 3FF0000000000000h	;double representation of 1.0
	two dq 4000000000000000h	;double representation of 2.0

;=======================================================================
;registers are used as follows

;r8 - input root value (n)
;r9 - input array ptr
;rbx - array offset
;rcx - loop counter
;xmm0 - input moudlus value (further used for calculations)
;xmm2 - modulus value store
;xmm1 - input arc value (further used for calculations)
;xmm3 - arc value store

;calculating trygonometric arguments for n-th complex root calculation
;r10 - k (see: n-th complex root formula)
;r11 - k + 1 (see: n-th complex root formula)

;calculating n-th real root (see: n-th root algorithm)
;r12 - initial root value 
;r13 - previous root value 
;r14 - current root value
;r15 - current delta value 
;=======================================================================

.code
calculateRootsAsm PROC

	;preserve nonvolatile register values
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

	finit
	movq xmm2, xmm0			;modulus
	movq xmm3, xmm1			;arc

	mov r10, 0				;k
	mov r11, [one]			;k+1
	mov rcx, 0				;reset loop counter
	mov rbx, 0				;reset array ptr

	
	mov rax, 0h				;reset xmm0 and xmm1 (use them fo calculation)
	movq xmm0, rax
	movq xmm1, rax

	sub rsp, 8
	mov qword ptr [rsp], r10
	movlpd xmm0, qword ptr [rsp]
	sub rsp, 8
	mov qword ptr [rsp], r11
	movhpd xmm0, qword ptr [rsp]
	add rsp, 8
	add rsp, 8

	tryg_arg_loop:
		cmp rcx, r8
		jge end_tryg_arg_lopp

		;mul k, k+1 by 2
		movlpd xmm1, [two]
		movhpd xmm1, [two]
		mulpd xmm0, xmm1

		;mul by pi
		movlpd xmm1, [pi]
		movhpd xmm1, [pi]
		mulpd xmm0, xmm1

		;add arc
		sub rsp, 8
		movq qword ptr [rsp], xmm3
		movlpd xmm1, qword ptr [rsp]
		movhpd xmm1, qword ptr [rsp]
		addpd xmm0, xmm1
		add rsp, 8

		;div by n
		sub rsp, 8
		mov qword ptr [rsp], r8
		cvtsi2sd xmm1, qword ptr [rsp]
		movq qword ptr [rsp], xmm1
		movlpd xmm1, qword ptr [rsp]
		movhpd xmm1, qword ptr [rsp]
		add rsp, 8
		divpd xmm0, xmm1

		;load result to results[i], results[i+1]
		sub rsp, 8
		movlpd qword ptr [rsp], xmm0
		mov rax, qword ptr [rsp]
		mov [r9 + rbx * 8], rax
		mov [r9 + 8 + rbx * 8], rax
		add rsp, 8

		;array guard
		mov rax, 0h
		mov rax, r8
		add rax, r8
		sub rax, 2h			;last address in array
		add rbx, 2h
		cmp rbx, rax
		jg end_tryg_arg_lopp
		sub rbx, 2h

		sub rsp, 8
		movhpd qword ptr [rsp], xmm0
		mov rax, qword ptr [rsp]
		mov [r9 + 16 + rbx * 8], rax
		mov [r9 + 24 + rbx * 8], rax
		add rsp, 8

		;increase k and kp1 by 2
		sub rsp, 8
		mov qword ptr [rsp], r10
		movlpd xmm0, qword ptr [rsp]
		sub rsp, 8
		mov qword ptr [rsp], r11
		movhpd xmm0, qword ptr [rsp]
		movlpd xmm1, [two]
		movhpd xmm1, [two]
		addpd xmm0, xmm1
		movhpd qword ptr [rsp], xmm0
		mov r11, qword ptr [rsp]
		add rsp, 8
		movlpd qword ptr [rsp], xmm0
		mov r10, qword ptr [rsp]
		add rsp, 8
	
		;increase array ptr by 32 (move over 4 elements)
		add rbx, 4h
		add rcx, 2h
		jmp tryg_arg_loop
	
end_tryg_arg_lopp:

	finit
	movq r12, xmm2			;set modulus as initial root value
	mov r13, r12			;set modulus as previous root value
	mov r14, r12			;set modulus as current root value
	mov r15, r12			;set modulus as current delta value

		sub rsp, 8
		mov qword ptr [rsp], r8
		fild qword ptr[rsp]			;fpu stack: [n]
		add rsp, 8
		fld st						;fpu stack: [n], [n]
		fld1						;fpu stack: 1.0, [n], [n]
		fxch st(1)					;fpu stack: [n], 1.0, [n]

		nth_root_loop:
			fsub st, st(1)			;fpu stack: [n]-1, 1.0, [n]
			sub rsp, 8
			mov qword ptr [rsp], r14
			fld qword ptr [rsp]				;fpu stack: [rroot], [n]-1, 1.0, [n]
			fxch st(1);				;fpu stack: [n]-1, [rroot], 1.0, [n]
			fld qword ptr [rsp]    ;fpu stack: [rroot], [n]-1, [rroot], 1.0, [n]
			add rsp, 8
		
			;calculate the (n-1)th power of [rroot] according to: [rroot] ^ ([n]-1) = 2 ^ (([n]-1) * log_2([rroot]))
			fyl2x					;st(0) = st(1) * log_2(st(0))					
			fld1					;load 1 to top
			fld st(1)				;copy logarithm result to top
			fprem					;st(0) = st(0) - (st(0) div st(1)) * st(1)
			f2xm1					;st(0) = 2 ^ (st(0)) - 1
			fadd					;st(0) += 1
			fscale					;scale st(0) by st(1)
			fxch st(1)				;move temp result top
			fstp st					;pop temp result, fpu stack: [rroot] ^ ([n]-1), [rroot], 1.0, [n]
		
			sub rsp, 8
			mov qword ptr [rsp], r12
			fld qword ptr [rsp]			;fpu stack: [rrootinit], [rroot] ^ ([n]-1), [rroot], 1.0, [n]
			add rsp, 8
			fdiv st, st(1)
			fsub st, st(2)
			fdiv st, st(4)
			fxch st(2)
			fadd st, st(2)
			fxch st(2)				;fpu stack: [delta], [rroot] ^ ([n]-1), [rroot] + [delta], 1.0, [n]
		
			fabs
			sub rsp, 8
			fstp qword ptr [rsp]			;[rroot] ^ ([n]-1), [rroot] + [delta], 1.0, [n]
			mov r15, qword ptr [rsp]
			add rsp, 8
			fstp st
			sub rsp, 8
			fstp qword ptr [rsp]
			mov r13, qword ptr [rsp]
			add rsp, 8
			fld st(1)				;fpu stack: [n], 1.0, [n]
	
		mov rax, r15
		cmp rax, 0000000000000000h
		jng end_nth_root_loop
		mov rax, r13
		sub rax, r14
		jz end_nth_root_loop
		mov rax, r13
		mov r14, rax
		jmp nth_root_loop

end_nth_root_loop:

		fstp st
		fstp st
		fstp st
		mov r14, r13
		mov rbx, 0
		mov rcx, 0
		sub rsp, 8
		mov qword ptr [rsp], r14
		fld qword ptr [rsp]
		add rsp, 8

calc_results_loop:
		cmp rcx, r8
		jge end_calc_results_loop
		fld qword ptr [r9 + rbx * 8]
		fcos
		fmul st, st(1)
		fstp qword ptr [r9 + rbx * 8]
		inc rbx
		mov rax, [r9 + rbx * 8]
		fld qword ptr [r9 + rbx * 8]
		fsin
		fmul st, st(1)
		fstp qword ptr [r9 + rbx * 8]

		inc rbx
		inc rcx
		jmp calc_results_loop
end_calc_results_loop:

	;restire nonvolatile register values
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