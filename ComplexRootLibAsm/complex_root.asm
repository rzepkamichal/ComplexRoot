.data	
	
	rroot dq 0h					;modulus real root to be calculated
	rrootinit dq 0h
	n dd 0h						;root
	
	mrootr dq 0h				;previous modulus real root result
	delta	dq 0h				;modulus real root result
	counter dd 0h				

	tmp dq 0h
	pi dq 0h
	k dq 0h
	kp1 dq 1h
	nd dq 0h
	arc dq 0h
	two dq 4000000000000000h
	one dq 3FF0000000000000h
	tmp_result dq 0h
	max_array_offset dq 0h
	
.code
calculateRootsAsm PROC
	
	fldpi					;load pi = 3.1415... on fpu stack
	fstp [pi]				;pop pi value to variable
	fldz					;load 0 on fpu stack
	fstp [k]				;pop 0 to variable
	fld1					;load 1.0 on fpu stack
	fstp [kp1]				;pop 1.0 to variable
	mov rax, r8				;load 3rd arg to rax
	mov [n], eax			;save 3rd arg to variable as 2B integer
	fild [n]				;convert 3rd arg to double
	fstp [nd]				;pop to variable as double
	movq rax, xmm1			;load 2nd arg
	mov [arc], rax			;save 2nd arg to variable
	movq rax, xmm0			;save 1st arg to variable
	mov [rroot], rax

	mov rbx, 0h
	mov ecx, 0h
	movlpd xmm0, [k]		
	movhpd xmm0, [kp1]

tryg_arg_loop:
		cmp ecx, [n]
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
		movlpd xmm1, [arc]
		movhpd xmm1, [arc]
		addpd xmm0, xmm1

		;div by n
		movlpd xmm1, [nd]
		movhpd xmm1, [nd]
		divpd xmm0, xmm1

		;load result to results[i], results[i+1]
		movlpd [tmp], xmm0
		mov rax, [tmp]
		mov [r9 + rbx * 8], rax
		mov [r9 + 8 + rbx * 8], rax
		
		;array guard
		mov rax, 0h
		mov eax, [n]
		add eax, [n]
		sub eax, 2h			;last address in array
		add rbx, 2h
		cmp rbx, rax
		jg end_tryg_arg_lopp
		sub rbx, 2h

		movhpd [tmp], xmm0
		mov rax, [tmp]
		mov [r9 + 16 + rbx * 8], rax
		mov [r9 + 24 + rbx * 8], rax

		;increasy k and kp1 by 2
		movlpd xmm0, [k]
		movhpd xmm0, [kp1]
		movlpd xmm1, [two]
		movhpd xmm1, [two]
		addpd xmm0, xmm1
		movlpd [k], xmm0
		movhpd [kp1], xmm0
	
		;increase array ptr by 32 (move over 4 elements)
		add rbx, 4h
		add ecx, 2h
		jmp tryg_arg_loop
	
end_tryg_arg_lopp:

		mov rax, [rroot]
		mov	[rrootinit], rax
		mov [delta], rax
		fild [n]					;fpu stack: [n]
		fld st						;fpu stack: [n], [n]
		fld1						;fpu stack: 1.0, [n], [n]
		fxch st(1)					;fpu stack: [n], 1.0, [n]
	

		nth_root_loop:
			fsub st, st(1)			;fpu stack: [n]-1, 1.0, [n]
			fld [rroot]				;fpu stack: [rroot], [n]-1, 1.0, [n]
			fxch st(1);				;fpu stack: [n]-1, [rroot], 1.0, [n]
			fld [rroot]				;fpu stack: [rroot], [n]-1, [rroot], 1.0, [n]
		
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
		

			fld [rrootinit]			;fpu stack: [rrootinit], [rroot] ^ ([n]-1), [rroot], 1.0, [n]
			fdiv st, st(1)
			fsub st, st(2)
			fdiv st, st(4)
			fxch st(2)
			fadd st, st(2)
			fxch st(2)				;fpu stack: [delta], [rroot] ^ ([n]-1), [rroot] + [delta], 1.0, [n]
		
			fabs
			fstp [delta]			;[rroot] ^ ([n]-1), [rroot] + [delta], 1.0, [n]
			fstp st
			fstp [mrootr]
			fld st(1)				;fpu stack: [n], 1.0, [n]
	
		mov rax, [delta]
		cmp rax, 0000000000000000h
		jng end_nth_root_loop
		mov rax, [mrootr]
		sub rax, [rroot]
		jz end_nth_root_loop
		mov rax, [mrootr]
		mov [rroot], rax
		jmp nth_root_loop

end_nth_root_loop:
		fstp st
		fstp st
		fstp st
		mov rax, [mrootr]
		mov [rroot], rax
		mov rbx, 0
		mov ecx, 0
		fld [rroot]
		mov edx, [n] ;guard for next loop
		add edx, [n]
		
calc_results_loop:
		cmp ecx, edx
		jge end_calc_results_loop
		mov rax, [r9 + rbx * 8]
		mov [tmp], rax
		fld [tmp]
		fcos
		fmul st, st(1)
		fstp [tmp_result]
		mov rax, [tmp_result]
		mov [r9 + rbx * 8], rax
		mov rax, [r9 + 8 + rbx * 8]
		mov [tmp], rax
		fld [tmp]
		fsin
		fmul st, st(1)
		fstp [tmp_result]
		mov rax, [tmp_result]
		mov [r9 + 8 + rbx * 8], rax

		add rbx, 2h
		inc ecx
		jmp calc_results_loop

end_calc_results_loop:

	ret
calculateRootsAsm ENDP



end